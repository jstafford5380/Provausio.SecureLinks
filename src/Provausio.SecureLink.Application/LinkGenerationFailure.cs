using System;

namespace Provausio.SecureLink.Application
{
    public class LinkGenerationFailure : Exception
    {
        public LinkGenerationFailure(int attempts)
            : base($"Couldn't generate a link without collisions after {attempts} tries.")
        {
        }
    }
}