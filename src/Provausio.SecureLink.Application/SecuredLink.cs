using System;
using System.Xml.Schema;

namespace Provausio.SecureLink.Application
{
    public class SecuredLink
    {
        /// <summary>
        /// The Link ID.
        /// </summary>
        /// <value>
        /// The link identifier.
        /// </value>
        public string LinkId { get; set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> on which the link will expire.
        /// </summary>
        /// <value>
        /// The expires at.
        /// </value>
        public DateTimeOffset ExpiresAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecuredLink"/> class.
        /// </summary>
        /// <param name="linkId">The link identifier.</param>
        /// <param name="expiresAt">The expires at.</param>
        public SecuredLink(string linkId, DateTimeOffset expiresAt)
        {
            LinkId = linkId;
            ExpiresAt = expiresAt;
        }
    }

    public class SecuredValue : IEquatable<SecuredValue>
    {
        public static SecuredValue Empty = new SecuredValue(null, false, DateTimeOffset.MinValue);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is encrypted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is encrypted; otherwise, <c>false</c>.
        /// </value>
        public bool IsEncrypted { get; }

        /// <summary>
        /// Gets the expires at.
        /// </summary>
        /// <value>
        /// The expires at.
        /// </value>
        public DateTimeOffset ExpiresAt { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecuredValue"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="isEncrypted">if set to <c>true</c> [is encrypted].</param>
        /// <param name="expiresAt">The expires at.</param>
        public SecuredValue(string value, bool isEncrypted, DateTimeOffset expiresAt)
        {
            Value = value;
            IsEncrypted = isEncrypted;
            ExpiresAt = expiresAt;
        }


        public bool Equals(SecuredValue other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Value, other.Value) && IsEncrypted == other.IsEncrypted && ExpiresAt.Equals(other.ExpiresAt);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SecuredValue) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsEncrypted.GetHashCode();
                hashCode = (hashCode * 397) ^ ExpiresAt.GetHashCode();
                return hashCode;
            }
        }
    }
}