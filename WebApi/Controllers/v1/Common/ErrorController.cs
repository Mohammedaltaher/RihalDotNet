using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>

    [ApiVersion("1.0")]
    public class ErrorController : BaseApiController
    {
        private readonly ILogger _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Development error
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("/development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            ObjectResult problem = Problem( detail: context.Error.StackTrace, title: context.Error.Message);
#pragma warning disable CA2254 // Template should be a static expression
            _logger.LogError( problem.ToString());
#pragma warning restore CA2254 // Template should be a static expression
            return problem;
        }
        /// <summary>
        /// Live Error
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("/live")]
        public IActionResult Error()
        {
            ObjectResult problem = Problem();
#pragma warning disable CA2254 // Template should be a static expression
            _logger.LogError(problem.ToString());
#pragma warning restore CA2254 // Template should be a static expression
            return problem;
        }
    }
}