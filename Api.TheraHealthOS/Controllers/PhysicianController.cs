using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library.TheraHealth.Models;
using Api.TheraHealthOS.Enterprise;
using Api.TheraHealth.Database;
using Library.TheraHealth.Data;
using Library.TheraHealth.DTO;

namespace Api.TheraHealthOS.Controllers;

[ApiController]
[Route("[controller]")]
public class PhysicianController : ControllerBase
{
    private readonly ILogger<PhysicianController> _logger;

    public PhysicianController(ILogger<PhysicianController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<PhysicianDTO> Get()
    {
        return new PhysicianEC().GetPhysicians();
    }

    [HttpGet("{id}")]
    public PhysicianDTO? GetById(int id)
    {
        return new PhysicianEC().GetById(id);
    }

    [HttpDelete("{id}")]
    public PhysicianDTO? Delete(int id)
    {
        return new PhysicianEC().Delete(id);
    }

    [HttpPost]
    public PhysicianDTO? AddOrUpdate([FromBody] PhysicianDTO physician)
    {
        return new PhysicianEC().AddOrUpdate(physician);
    }

    [HttpPost("Search")]
    public IEnumerable<PhysicianDTO> Search([FromBody] QueryRequest query)
    {
        return new PhysicianEC().Search(query.Content);
    }
}
