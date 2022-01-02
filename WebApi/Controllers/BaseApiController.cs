using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    /// <summary>
    /// BaseApiController
    /// </summary>
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        /// <summary>
        /// map Meditor
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}