using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using lib.options;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace lib;

public class SavedService(IMongoDatabase database) : AbstractService, ISavedService
{

    #region Public Methods

    public async Task<CommentModel[]> GetAllItemsAsync()
    {
        var comments = database.GetCollection<CommentModel>("comments").AsQueryable().ToArray();
        return await Task.FromResult(comments.ToArray());
    }
    
    public async Task<CommentModel[]> GetCommentsFromPushshiftArchive(CacheOptions options)
    {
        List<string> files = [];

        var basePath = Environment.GetEnvironmentVariable("PUSHSHIFT_PATH") ?? @"E:\PushshiftDumps\user_comments\author";
        var dirs = Directory.GetDirectories(basePath, "*", SearchOption.AllDirectories);
        foreach (string dir in dirs)
        {
            string[] allFiles = Directory.GetFiles(dir, "*.json", SearchOption.AllDirectories);
            if (allFiles.Length == 0) continue;
            files.AddRange(allFiles);
        }

        List<CommentModel> comments = [];
        comments.AddRange(from file in files
            from comment in StreamCommentsFromFile(file)
            select new CommentModel(comment));
        return await Task.FromResult(comments.ToArray());
    }

    public async Task<(CommentPreview[], int)> GetFilteredItemsAsync(IOptions options)
    {
        CommentPreview[] commentPreviews = (await GetAllItemsAsync()).Select(c => new CommentPreview(c)).ToArray();
        CommentPreview[] allComments = commentPreviews.OrderByDescending(c => c.Date).ToArray();
        CommentPreview[] filteredComments = FilterComments(allComments, options).ToArray();
        CommentPreview[] pagedComments = filteredComments.Take(options.Limit).ToArray();
        return (pagedComments, filteredComments.Length);
    }

    #endregion

    #region Helper Methods

    private static IEnumerable<PushshiftModel> StreamCommentsFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        while (reader.ReadLine() is { } line)
        {
            yield return JsonConvert.DeserializeObject<PushshiftModel>(line);
        }
    }

    #endregion
    
}
