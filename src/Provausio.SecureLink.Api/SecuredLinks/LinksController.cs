using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Provausio.SecureLink.Api.SecuredLinks.Models;
using Provausio.SecureLink.Application.Commands;
using Provausio.SecureLink.Application.Queries;

namespace Provausio.SecureLink.Api.SecuredLinks
{
    public class LinksController : Controller
    {
        private readonly IMediator _mediator;

        public LinksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Route("links", Name = "create_link")]
        public async Task<IActionResult> CreateLink(
            [FromBody] CreateLinkRequest request,
            CancellationToken cancellationToken)
        {
            var createLink = new CreateLinkCommand(
                request.Data,
                request.IsEncrypted,
                request.Secrets,
                request.TtlSeconds);

            var link = await _mediator.Send(createLink, cancellationToken);

            var response = new CreateLinkResponse
            {
                ExpiresAt = link.ExpiresAt,
                LinkId = link.LinkId
            };

            return Created("links", response);
        }

        [HttpGet, Route("links/{hash}", Name = "decode_link")]
        public async Task<IActionResult> DecodeLink(string hash)
        {
            var getData = new DecodeLinkQuery(hash.ToLower());
            var result = await _mediator.Send(getData);

            if (result == null)
                return NotFound();

            var response = new SecuredDataResponse
            {
                Value = result
            };

            return Ok(response);
        }
    }
}
