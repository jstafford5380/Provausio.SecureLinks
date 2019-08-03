using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Provausio.SecureLink.Application.Services;

namespace Provausio.SecureLink.Application.Commands
{
    public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand, SecuredLink>
    {
        private readonly ISecuredLinkService _linkService;

        public CreateLinkCommandHandler(ISecuredLinkService linkService)
        {
            _linkService = linkService;
        }

        public Task<SecuredLink> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
        {
            var result = _linkService.CreateLinkAsync(
                request.Data,
                request.IsEncrypted,
                request.Secrets.ToArray(),
                DateTimeOffset.Now.AddSeconds(request.TtlSeconds), 
                cancellationToken);

            return result;
        }
    }
}
