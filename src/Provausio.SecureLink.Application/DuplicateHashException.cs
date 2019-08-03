using System;

namespace Provausio.SecureLink.Application
{
    public class DuplicateHashException : Exception
    {
        public DuplicateHashException()
            : base("Duplicate hash encountered. This is an unlikely case where there is a collision of LinkIDs.")
        {}
    }
}