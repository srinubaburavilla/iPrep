using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using iprep_api.Models.Data;
using iprep_api.Models.Response;

namespace iprep_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IPrepContext _dbContext;
    private readonly IMapper _mapper;

    public ReportsController(IPrepContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet("allsubjectswithquestionsandanswers")]
    public IActionResult AllSubjectcsWithQuestionsAndAnswers()
    {
        var result = from mapper in _dbContext.IprepMappers
                     join subject in _dbContext.SubjectMasters on mapper.SubjectId equals subject.Id
                     join question in _dbContext.QuestionMasters on mapper.QuestionId equals question.Id
                     join answer in _dbContext.AnswerMasters on mapper.AnswerId equals answer.Id
                     select new
                     {
                         Subject = subject.Subject,
                         Question = question.Question,
                         Answer = answer.Answer,
                     };

        return Ok(result.ToList());
    }
}
