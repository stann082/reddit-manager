using Newtonsoft.Json;
using System.Linq;

namespace Domain
{
    public class SubmissionData : IRedditData
    {

        #region Constructors

        public SubmissionData()
        {
            Submissions = new Submission[0];
        }

        #endregion

        #region IRedditData Members

        public IContent[] Contents => Submissions.Cast<IContent>().ToArray();

        #endregion

        #region Properties

        [JsonProperty("data")]
        private Submission[] Submissions { get; set; }

        #endregion

    }
}
