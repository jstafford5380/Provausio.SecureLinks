using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Provausio.SecureLink.Api.SecuredLinks.Models;
using Provausio.SecureLink.Application.Commands;
using Provausio.SecureLink.Application.Queries;

namespace Provausio.SecureLink.Api.SecuredLinks
{
    public class LinksController : ApiController
    {
        /// <summary>
        /// Creates the link.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpPost("links", Name = "create_link")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateLinkResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
        public async Task<IActionResult> CreateLink(
            [FromBody] CreateLinkRequest request,
            CancellationToken cancellationToken)
        {
            var createLink = new CreateLinkCommand(
                request.Data,
                request.IsEncrypted,
                request.Secrets,
                request.TtlSeconds);

            var link = await Mediator.Send(createLink, cancellationToken);

            var response = new CreateLinkResponse
            {
                ExpiresAt = link.ExpiresAt,
                LinkId = link.LinkId
            };

            await LinksService.AddLinksAsync(response);

            return Created("links/{{hash}}", response);
        }

        /// <summary>
        /// Decodes the link.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        [HttpGet("links/{hash}", Name = "get_link_data")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SecuredDataResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DecodeLink(string hash)
        {
            var getData = new DecodeLinkQuery(hash.ToLower());
            var result = await Mediator.Send(getData);

            if (result == null)
                return NotFound();

            var response = new SecuredDataResponse
            {
                Value = result.Value,
                IsEncrypted = result.IsEncrypted,
                ExpiresAt = result.ExpiresAt
            };

            await LinksService.AddLinksAsync(response);

            return Ok(response);
        }
    }
}
