using System.Net;
using MongoDB.Driver;
using Moq;
using Newtonsoft.Json;
using Reddit.Things;

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
        // // Arrange
        // var mockMongoDatabase = new Mock<IMongoDatabase>();
        // var comments = new List<Comment>
        // {
        //     new Comment { /* Initialize properties */ },
        //     new Comment { /* Initialize properties */ }
        // };
        //
        // mockMongoDatabase.Setup(m => m.GetCollection<>(It.IsAny<int>(), It.IsAny<object>())).Returns(mockDatabase.Object);
        //
        // mockServer.Setup(s => s.Keys(It.IsAny<int>(), It.IsAny<RedisValue>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<CommandFlags>())).Returns(keys);
        // foreach (var key in keys)
        // {
        //     mockDatabase.Setup(d => d.StringGetAsync(key, It.IsAny<CommandFlags>()))
        //         .ReturnsAsync(JsonConvert.SerializeObject(comments.First()));
        // }
        //
        // var savedService = new SavedService(mockMongoDatabase.Object);
        // var savedOptions = new Mock<IOptions>();
        //
        // // Act
        // var result = await savedService.GetFilteredItemsAsync(savedOptions.Object);
        //
        // // Assert
        // Assert.That(result, Is.Not.Null);
    }
}