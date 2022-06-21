
namespace Shorter.UnitTest
{
    public class ShorterServiceTest
    {

        ShorterService shorterService;
        IAliasGenerator aliasGenerator;
        IUrlRepository urlRepository;

        public ShorterServiceTest()
        {
            aliasGenerator = new Mock.Of<IAliasGenerator>();
            urlRepository = new Mock.Of<IUrlRepository>();
            shorterService = new ShorterService(aliasGenerator, urlRepository);
        }

        [Fact]
        public void Generate_SemUrl_DeveGerarUrlAleatoria()
        {
            aliasGenerator.Setup(p => p.GetRandomAlias()).Returns("AbCdEfGhIjK");
            var url = shorterService.Generate();

            url.Should().Be("AbCdEfGhIjK");
        }
    }
}