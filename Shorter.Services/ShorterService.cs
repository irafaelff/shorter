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

        public string Generate(string url)
        {
            var newAlias = aliasGenerator.GetRandomAlias();

            var newShorterUrl = new ShorterUrl
            {
                Url = url,
                Alias = newAlias
            };
                                    
            urlRepository.Add(newShorterUrl);

            var urlGenerated = shorterOptions.BaseUrl + newAlias;
            return urlGenerated;
        }
    }
}