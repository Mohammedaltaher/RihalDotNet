using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.AdditionalFeatureFeatures.Commands;
using Application.Features.AdditionalFeatureFeatures.Queries;
using Application.Model.settings;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers.v1;
/// <summary>
/// 
/// </summary>

[ApiVersion("1.0")]
public class AdditionalFeatureController : BaseApiController
{
    /// <summary>
    /// Creates a New AdditionalFeature.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentCommand command)
    {
        var obj = await Mediator.Send(command);
        return StatusCode(obj.StatusCode,(AdditionalFeatureDto)obj.Data);
    }
    /// <summary>
    /// Gets all AdditionalFeatures.
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        var obj = await Mediator.Send(new GetAllStudentQuery());
        return StatusCode(obj.StatusCode, (List<AdditionalFeatureDto>)obj.Data);
    }
    /// <summary>
    /// Gets AdditionalFeature Entity by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var obj = await Mediator.Send(new GetStudentByIdQuery { Id = id });
        return StatusCode(obj.StatusCode,  (AdditionalFeatureDto)obj.Data);

    }
    /// <summary>
    /// Deletes AdditionalFeature Entity based on Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var obj = await Mediator.Send(new DeleteStudentByIdCommand { Id = id });
        return StatusCode(obj.StatusCode,  (AdditionalFeatureDto)obj.Data);
    }
    /// <summary>
    /// Updates the AdditionalFeature Entity based on Id.   
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("[action]")]
    public async Task<IActionResult> Update(int id, UpdateStudentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var obj = await Mediator.Send(command);
        return StatusCode(obj.StatusCode,(AdditionalFeatureDto)obj.Data );
    }
}
