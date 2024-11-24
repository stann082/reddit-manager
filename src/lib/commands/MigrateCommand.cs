using MongoDB.Driver;
using Serilog;

namespace lib.commands;

public class MigrateCommand(ISavedService service, IMongoDatabase database)
{
    #region Public Methods

    public async Task<int> Execute()
    {
        Log.Information("Migrating comments to a new db....");
        var comments = await service.GetAllItemsAsync();
        var collection = database.GetCollection<CommentModel>("comments2");
        await collection.InsertManyAsync(comments.Select(c => new CommentModel(c)));
        Console.WriteLine($"Migrated {comments.Length} comments to comments2");
        return await Task.FromResult(0);
    }

    #endregion
}