using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using iprep_api.Models.Data;
namespace iprep_api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubjectsController : ControllerBase
{
    private readonly IPrepContext _dbContext;
    private readonly IMapper _mapper;

    public SubjectsController(IPrepContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public IActionResult All()
    {
        var questions = _dbContext.SubjectMasters.Where(x => !x.IsDeleted).Select(x=> _mapper.Map<iprep_api.Models.Response.SubjectMaster>(x)).ToList();
        return Ok(questions);
    }

    [HttpGet("getbyid/{id}")]
    public IActionResult GetById(int id)
    {
        if (id <= 0)
            return BadRequest($"Not a valid subject id {id}");

        var question = _dbContext.SubjectMasters.FirstOrDefault(x => x.Id == id);

        if (question == null)
        {
            return NotFound($"Subject does not exists with the id {id}");
        }

        return Ok(_mapper.Map<iprep_api.Models.Data.SubjectMaster,iprep_api.Models.Response.SubjectMaster>(question));
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        if (id <= 0)
            return BadRequest($"Not a valid subject id {id}");

        var question = _dbContext.SubjectMasters.FirstOrDefault(x => x.Id == id);
        if (question != null)
        {
            question.IsDeleted = true;
            _dbContext.SaveChanges();
        }

        return Ok();
    }


    [HttpPut("Update")]
    public IActionResult Put(iprep_api.Models.Request.SubjectMaster model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model received");
        }

        var question = _dbContext.SubjectMasters.FirstOrDefault(x => x.Id == model.Id);

        if (question != null)
        {
            question.Subject = model.Subject;
            question.LastModified = DateTime.Now;
            _dbContext.SaveChanges();
        }

        return Ok();
    }

    [HttpPost("add")]
    public IActionResult Add(iprep_api.Models.Request.SubjectMaster model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model received");
        }

        var mappedModel = _mapper.Map<iprep_api.Models.Data.SubjectMaster>(model);
        _dbContext.SubjectMasters.Add(mappedModel);
        _dbContext.SaveChanges();

        return Ok();
    }

}
