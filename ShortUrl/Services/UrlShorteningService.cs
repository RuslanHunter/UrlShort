using System;
using Microsoft.EntityFrameworkCore;

namespace ShortUrl.Services
{
	public class UrlShorteningService
	{
		public const int NumberOfCharsInShortLink = 7;

		private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

		private readonly Random _random = new();

		private readonly ApplicationDbContext _dbcontext;

        public UrlShorteningService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<string> GenerationUniqueCode()
		{
			var codeChars = new char[NumberOfCharsInShortLink];

			while (true)
			{
                for (int i = 0; i < NumberOfCharsInShortLink; i++)
                {
                    int randomIndex = _random.Next(Alphabet.Length - 1);

                    codeChars[i] = Alphabet[randomIndex];
                }

                var code = new string(codeChars);

                if (!await _dbcontext.ShortenedUrls.AnyAsync(s => s.Code == code))
                {
                    return code;
                }
            }
		}
    }
}

