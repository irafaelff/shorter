using Shorter.Entities;
using Shorter.Repository.Abstractions;
using Shorter.Services.Abstractions;

namespace Shorter.Services
{
    public class ShorterService
    {
        private readonly IAliasGenerator aliasGenerator;
        private readonly IUrlRepository urlRepository;
        private readonly IShorterOptions shorterOptions;

        public ShorterService(IAliasGenerator aliasGenerator, IUrlRepository urlRepository, IShorterOptions shorterOptions)
        {
            this.aliasGenerator = aliasGenerator;
            this.urlRepository = urlRepository;
            this.shorterOptions = shorterOptions;
        }

        public Task<string> GenerateAsync(string url)
        {
            if (!IsValidUrl(url))
                throw new ArgumentException("Url inválida");

            var newAlias = aliasGenerator.GetRandomAlias();

            var newShorterUrl = new ShorterUrl
            {
                Url = url,
                Alias = newAlias
            };
                                    
            urlRepository.AddAsync(newShorterUrl);

            var urlGenerated = shorterOptions.BaseUrl + newAlias;
            return Task.FromResult(urlGenerated);
        }

        private static bool IsValidUrl(string source)
        {
           return Uri.TryCreate(
               source,
               UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeHttp);
        }
    }
}