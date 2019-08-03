using System;
using System.Text;

namespace Provausio.SecureLink.Application.Services
{
    public static class LinkIdGenerator
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const int DefaultLength = 15;

        private static readonly StringBuilder Builder = new StringBuilder();
        private static readonly Random Rand = new Random();

        public static string Next(int length = DefaultLength)
        {
            for (var i = 0; i < length; i++)
                Builder.Append(Alphabet[Rand.Next(0, Alphabet.Length - 1)]);

            var id = Builder.ToString();
            Builder.Clear();
            return id;
        }
    }
}
