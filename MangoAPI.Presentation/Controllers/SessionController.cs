﻿using System.Threading;
using System.Threading.Tasks;
using MangoAPI.BusinessLogic.ApiCommands.Auth;
using MangoAPI.BusinessLogic.Responses;
using MangoAPI.BusinessLogic.Responses.Auth;
using MangoAPI.Presentation.Extensions;
using MangoAPI.Presentation.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MangoAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/session")]
    public class SessionController : ApiControllerBase, ISessionController
    {
        public SessionController(IMediator mediator) : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost]
        [SwaggerOperation(Summary = "Performs login to the messenger. Returns: Access token, Session Id.")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            return await RequestAsync(request.ToCommand(), cancellationToken);
        }

        [AllowAnonymous]
        [HttpPost("{id}")]
        [SwaggerOperation(Summary = "Refreshes current user's session. Returns: Access token, Session Id.")]
        [ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshSession([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new RefreshTokenCommand {RefreshTokenId = id};
            return await RequestAsync(command, cancellationToken);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes session of current user's device.")]
        [ProducesResponseType(typeof(LogoutResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LogoutAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new LogoutCommand {RefreshTokenId = id};
            return await RequestAsync(command, cancellationToken);
        }

        [Authorize]
        [HttpDelete]
        [SwaggerOperation(Summary = "Deletes all sessions of current user.")]
        [ProducesResponseType(typeof(LogoutResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LogoutAllAsync([FromBody] LogoutAllRequest request,
            CancellationToken cancellationToken)
        {
            var userId = HttpContext.User.GetUserId();
            var command = request.ToCommand(userId);
            return await RequestAsync(command, cancellationToken);
        }
    }
}