using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using iprep_api.Models.Data;
using iprep_api.Models.Response;

namespace iprep_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ManageQuestionsController : ControllerBase
{
    private readonly IPrepContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public ManageQuestionsController(IPrepContext dbContext, IMapper mapper, ILogger<ManageQuestionsController> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = from mapper in _dbContext.IprepMappers
                     join subject in _dbContext.SubjectMasters on mapper.SubjectId equals subject.Id
                     join question in _dbContext.QuestionMasters on mapper.QuestionId equals question.Id
                     join answer in _dbContext.AnswerMasters on mapper.AnswerId equals answer.Id
                     select new SearchResponse
                     {
                         MapperId = mapper.Id,
                         SubjectId = subject.Id,
                         Subject = subject.Subject,
                         Question = question.Question,
                         Answer = answer.Answer,
                     };

        return Ok(result.ToList());
    }

    [HttpGet("getbyid/{mapperId:int}")]
    public IActionResult GetById(int mapperId)
    {
        if (mapperId <= 0)
        {
            return BadRequest("Invalid mapperId received");
        }

        // Get mapper Id's
        var iPrepMapper = _dbContext.IprepMappers.FirstOrDefault(x => x.Id == mapperId);
        if (iPrepMapper == null)
        {
            return NotFound("The requested mapper details was not found.");
        }

        var result = from mapper in _dbContext.IprepMappers
                     join subject in _dbContext.SubjectMasters on mapper.SubjectId equals subject.Id
                     join question in _dbContext.QuestionMasters on mapper.QuestionId equals question.Id
                     join answer in _dbContext.AnswerMasters on mapper.AnswerId equals answer.Id
                     where mapper.Id == mapperId
                     select new SearchResponse
                     {
                         MapperId = mapper.Id,
                         SubjectId = subject.Id,
                         Subject = subject.Subject,
                         Question = question.Question,
                         Answer = answer.Answer,
                     };

        return Ok(result.ToList());
    }

    [HttpPost("addnew")]
    public IActionResult AddNew(iprep_api.Models.Request.SQAModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model received");
        }

        var subject = _dbContext.SubjectMasters.FirstOrDefault(x => x.Id == model.SubjectId);

        if (subject == null)
        {
            return NotFound($"Subject {model.SubjectId} not found");
        }
        var question = new iprep_api.Models.Data.QuestionMaster() { Question = model.Question, Created = DateTime.Now, LastModified = DateTime.Now };
        _dbContext.QuestionMasters.Add(question);

        var answer = new iprep_api.Models.Data.AnswerMaster() { Answer = model.Answer, Created = DateTime.Now, LastModified = DateTime.Now };
        _dbContext.AnswerMasters.Add(answer);

        _dbContext.SaveChanges();

        var mapper = new iprep_api.Models.Data.IprepMapper()
        {
            SubjectId = subject.Id,
            QuestionId = question.Id,
            AnswerId = answer.Id,
            Created = DateTime.Now,
            LastModified = DateTime.Now,
        };

        _dbContext.IprepMappers.Add(mapper);
        _dbContext.SaveChanges();

        return Ok();
    }

    [HttpDelete("delete/{mapperId}")]
    public IActionResult Delete(int mapperId)
    {
        if (mapperId <= 0)
        {
            return BadRequest("Invalid mapperId received");
        }

        // Get mapper Id's
        var iPrepMapper = _dbContext.IprepMappers.FirstOrDefault(x => x.Id == mapperId);
        if (iPrepMapper == null)
        {
            _logger.LogInformation($"The requested mapperid {mapperId} details was not found.");
            return NotFound("The requested mapper details was not found.");
        }
        
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                _logger.LogInformation($"Removing mappings {iPrepMapper.Id}");
                // Delete Mappings
                _dbContext.IprepMappers.Remove(iPrepMapper);
                _dbContext.SaveChanges();

                // Delete Answer
                var answer = _dbContext./* A table that stores the answers to the questions. */
                /* A table that stores the answers to the questions. */
                AnswerMasters.FirstOrDefault(x => x.Id == iPrepMapper.AnswerId);
                if (answer == null)
                {
                    return NotFound($"Answer does not exists to delete");
                }
                else
                {
                    _logger.LogInformation($"Removing answer {answer.Id}");
                    _dbContext.AnswerMasters.Remove(answer);
                    _dbContext.SaveChanges();
                }

                // Delete Question
                var question = _dbContext.QuestionMasters.FirstOrDefault(x => x.Id == iPrepMapper.QuestionId);
                if (question == null)
                {
                    return NotFound($"Question does not exists to delete");
                }
                else
                {
                    _logger.LogInformation($"Removing question {question.Id}");
                    _dbContext.QuestionMasters.Remove(question);
                    _dbContext.SaveChanges();
                }

                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error ocurred ", ex.Message, ex.StackTrace);
                transaction.Rollback();
                throw;
            }
        }

        return Ok();
    }
}
