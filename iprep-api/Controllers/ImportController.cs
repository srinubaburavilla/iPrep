using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using iprep_api.Models.Data;
using iprep_api.Models.Response;
using iprep_api.Models.Request;
using Newtonsoft.Json;

namespace iprep_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ImportController : ControllerBase
{
    private readonly IPrepContext _dbContext;
    private readonly IMapper _mapper;

    public ImportController(IPrepContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpPost("uploadquestionsfile")]
    public IActionResult UploadQuestionsFile([FromForm] UploadedFileDetails fileDetails)
    {
        using (var reader = new StreamReader(fileDetails.File.OpenReadStream()))
        {
            string fileContent = reader.ReadToEnd();
            var importedQuestions = JsonConvert.DeserializeObject<List<UploadedQuestion>>(fileContent);
            System.Console.WriteLine($"Processing {importedQuestions.Count} questions");
            ProcessQuestionInsertion(importedQuestions);
        }
        return Ok();
    }

    private void ProcessQuestionInsertion(List<UploadedQuestion> questions)
    {
        var uploadedSubjects = questions.Select(x => x.Subject).ToHashSet<string>();
        var subjectsInDb = _dbContext.SubjectMasters.Select(x=> x.Subject).ToList();
        var technologiesWhichDoesNotExists = uploadedSubjects.Where(x=> !subjectsInDb.Contains(x.Trim())); 

        if (technologiesWhichDoesNotExists.Any())
        {
            throw new Exception($"Uploaded technologies {string.Join(",", technologiesWhichDoesNotExists)} not found. Please check and re-upload the file");
        }

        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                foreach (var question in questions)
                {
                    var technology = _dbContext.SubjectMasters.FirstOrDefault(x => x.Subject.Equals(question.Subject.Trim()));
                    if (technology == null)
                    {
                        throw new Exception($"Technology {question.Subject} not found.");
                    }

                    System.Console.WriteLine($"Inserting question {question.Question}");
                    var questionToAdd = new iprep_api.Models.Data.QuestionMaster() { Question = question.Question, Created = DateTime.Now, LastModified = DateTime.Now };
                    _dbContext.QuestionMasters.Add(questionToAdd);

                    System.Console.WriteLine($"Inserting answer {question.Answer}");
                    var answerToAdd = new iprep_api.Models.Data.AnswerMaster() { Answer = question.Answer, Created = DateTime.Now, LastModified = DateTime.Now };
                    _dbContext.AnswerMasters.Add(answerToAdd);

                    _dbContext.SaveChanges();

                    var mapper = new iprep_api.Models.Data.IprepMapper()
                    {
                        SubjectId = technology.Id,
                        QuestionId = questionToAdd.Id,
                        AnswerId = answerToAdd.Id,
                        Created = DateTime.Now,
                        LastModified = DateTime.Now,
                    };
                    _dbContext.IprepMappers.Add(mapper);
                }
                _dbContext.SaveChanges();
                transaction.Commit();
                System.Console.WriteLine($"All questions processed successfully");
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                System.Console.WriteLine($"Exception occurred {ex.Message}");
                throw;
            }
        }
    }
}
