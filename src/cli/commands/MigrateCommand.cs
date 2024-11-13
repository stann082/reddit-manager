﻿using lib;
using MongoDB.Driver;

namespace cli.commands;

public class MigrateCommand(ISavedService service, IMongoDatabase database)
{

    #region Public Methods

    public async Task<int> Execute()
    {
        CommentModel[] comments = await service.GetAllItemsAsync();
        IMongoCollection<CommentModel> collection = database.GetCollection<CommentModel>("comments2");
        await collection.InsertManyAsync(comments.Select(c => new CommentModel(c)));
        Console.WriteLine($"Migrated {comments.Length} comments to comments2");
        return await Task.FromResult(0);
    }

    #endregion
    
}