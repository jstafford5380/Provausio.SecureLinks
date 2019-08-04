using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Provausio.SecureLink.Application.Services;

namespace Provausio.SecureLink.Application.Queries
{
    public class DecodeLinkQueryHandler : IRequestHandler<DecodeLinkQuery, SecuredValue>
    {
        private readonly ISecuredLinkStore _store;

        public DecodeLinkQueryHandler(ISecuredLinkStore store)
        {
            _store = store;
        }

        public async Task<SecuredValue> Handle(DecodeLinkQuery request, CancellationToken cancellationToken)
        {
            var result = await _store.GetDataAsync(request.Hash, cancellationToken);
            return result.Equals(SecuredValue.Empty) ? null : result;
        }
    }
}