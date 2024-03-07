using System.Net;
using Moq;
using Newtonsoft.Json;
using Reddit.Things;
using StackExchange.Redis;

namespace lib.test.csproj;

public class SavedServiceTest
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TestBlueSky_GetFilteredItemsAsync()
    {
        // Arrange
        var mockMultiplexer = new Mock<IConnectionMultiplexer>();
        var mockDatabase = new Mock<IDatabase>();
        var mockServer = new Mock<IServer>();
        var endPoint = new DnsEndPoint("localhost", 6379);
        var keys = new RedisKey[] { "key1", "key2" };
        var comments = new List<Comment>
        {
            new Comment { /* Initialize properties */ },
            new Comment { /* Initialize properties */ }
        };
        
        mockMultiplexer.Setup(m => m.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(mockDatabase.Object);
        mockMultiplexer.Setup(m => m.GetEndPoints(It.IsAny<bool>())).Returns(new EndPoint[] { endPoint });
        mockMultiplexer.Setup(m => m.GetServer(It.IsAny<EndPoint>(), It.IsAny<object>())).Returns(mockServer.Object);

        mockServer.Setup(s => s.Keys(It.IsAny<int>(), It.IsAny<RedisValue>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<CommandFlags>())).Returns(keys);
        foreach (var key in keys)
        {
            mockDatabase.Setup(d => d.StringGetAsync(key, It.IsAny<CommandFlags>()))
                .ReturnsAsync(JsonConvert.SerializeObject(comments.First()));
        }

        var savedService = new SavedService(mockMultiplexer.Object);
        var savedOptions = new Mock<IOptions>();

        // Act
        var result = await savedService.GetFilteredItemsAsync(savedOptions.Object);

        // Assert
        Assert.That(result, Is.Not.Null);
    }
}