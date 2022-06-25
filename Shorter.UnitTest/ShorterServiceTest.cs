using FluentAssertions;
using FluentAssertions.ArgumentMatchers.Moq;
using Moq;
using Shorter.Entities;
using Shorter.Repository.Abstractions;
using Shorter.Services;
using Shorter.Services.Abstractions;

namespace Shorter.UnitTest.Services
{
    public class ShorterServiceTest
    {
        readonly ShorterService shorterService;
        readonly Mock<IAliasGenerator> aliasGeneratorMock;
        readonly Mock<IUrlRepository> urlRepository;
        readonly Mock<IShorterOptions> shorterOptions;

        public ShorterServiceTest()
        {
            aliasGeneratorMock = new Mock<IAliasGenerator>();
            urlRepository = new Mock<IUrlRepository>();
            shorterOptions = new Mock<IShorterOptions>();
            shorterService = new ShorterService(aliasGeneratorMock.Object, urlRepository.Object, shorterOptions.Object);
        }

        [Fact]
        public async Task GenerateAsync_DeveGerarUrlAleatoria()
        {
            shorterOptions.SetupGet(p => p.BaseUrl).Returns("http://shtr.url/");
            aliasGeneratorMock.Setup(p => p.GetRandomAlias()).Returns("AbCdEfGhIjK");

            var url = await shorterService.GenerateAsync("http://www.google.com");

            url.Should().Be("http://shtr.url/AbCdEfGhIjK");
        }

        [Fact]
        public async Task GenerateAsync_DeveSalvarNovaUrlNoBanco()
        {
            shorterOptions.SetupGet(p => p.BaseUrl).Returns("http://shorter.url/");
            aliasGeneratorMock.Setup(p => p.GetRandomAlias()).Returns("AbCdEfGhIjK");

            await shorterService.GenerateAsync("http://www.google.com");

            var shorterUrl = new ShorterUrl 
            { 
                Alias = "AbCdEfGhIjK",
                Url = "http://www.google.com"
            };

            urlRepository.Verify(p => p.Add(Its.EquivalentTo(shorterUrl)), Times.Once);           
        }

        [Theory]
        [InlineData("/www.google.com")]
        [InlineData("//www.google.com")]
        [InlineData("h//www.google.com")]
        [InlineData("ht//www.google.com")]
        [InlineData("htt//www.google.com")]
        [InlineData("www.google.com")]
        [InlineData("abcdefg")]
        public async Task GenerateAsync_ComUrlInvalida_DeveLancarExcecao(string url)
        {           
            var action = () => shorterService.GenerateAsync(url);

            await action.Should().ThrowAsync<ArgumentException>();
        }
    }
}