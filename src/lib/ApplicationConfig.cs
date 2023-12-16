using Newtonsoft.Json;

namespace lib;

public class ApplicationConfig
{

    #region Constants

    public const string AppName = "redditapi";

    #endregion

    #region Utility

    public static string AppConfigRootPath = Path.Combine(
        Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData,
            Environment.SpecialFolderOption.Create
        ),
        AppName
    );

    public static string AppConfigFilePath = Path.Combine(
        AppConfigRootPath,
        "config.json"
    );

    #endregion

    #region Properties

    [JsonProperty("app_id")]
    public string AppId { get; set; } = string.Empty;

    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; } = string.Empty;

    #endregion

    #region Public Methods

    public static ApplicationConfig Load()
    {
        Directory.CreateDirectory(AppConfigRootPath);
        if (!File.Exists(AppConfigFilePath))
        {
            return new ApplicationConfig();
        }

        var configContent = File.ReadAllText(AppConfigFilePath);
        return JsonConvert.DeserializeObject<ApplicationConfig>(configContent);
    }

    #endregion

}
