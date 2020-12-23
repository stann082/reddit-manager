using Newtonsoft.Json;
using System.Linq;

namespace Domain
{
    public class CommentData : IRedditData
    {

        #region Constructors

        public CommentData()
        {
            Comments = new Comment[0];
        }

        #endregion

        #region IRedditData Members

        public IContent[] Contents => Comments.Cast<IContent>().ToArray();

        #endregion

        #region Properties

        [JsonProperty("data")]
        private Comment[] Comments { get; set; }

        #endregion

    }
}
