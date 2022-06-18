using Domain;
using Presentation;
using Service;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PushshiftAPI
{
    public partial class MainForm : Form, ISearchOptions
    {

        #region Constants

        private static readonly DateTime START_DATE = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();
            ServiceFactoryProxy.Singleton = new ServiceFactory();
            Environment.Initialize();

            DefaultSelectionBackColor = rtbResponse.SelectionBackColor;
            DefaultSelectionColor = rtbResponse.SelectionColor;

            Presenter = new SearchPresenter();

            chkPeriodFilter_CheckedChanged(null, EventArgs.Empty);
            rdoDescending.Checked = true;
            ddlQueryType.DataSource = new[] { "comment", "submission" };
            ddlSortBy.DataSource = new[] { "created_utc", "score" };
            txtTotalResults.Text = "100";

            PrePopulateFields();
        }

        #endregion

        #region Properties

        public bool IsPeriodSearchEnabled => chkPeriodFilter.Checked;

        public string Query => txtQuery.Text;
        public QueryType QueryType => GetQueryType();

        public string ScoreGreaterThan => txtGreaterThan.Text;
        public string ScoreLessThan => txtLessThan.Text;
        public bool ShowExactMatches => chkShowExactMatches.Checked;
        public string SortDirection => GetSortDirection();
        public string SortType => ddlSortBy.SelectedItem.ToString();
        public long StartDateUnixTimeStamp => ToUnixTime(dpStartDate.Value);
        public long StopDateUnixTimeStamp => ToUnixTime(dpStopDate.Value);
        public string Subreddit => txtSubreddit.Text;

        public string TotalResults => txtTotalResults.Text;

        public string UserName => txtUserName.Text;

        private Color DefaultSelectionBackColor { get; set; }
        private Color DefaultSelectionColor { get; set; }

        private ApplicationEnvironment Environment => ApplicationEnvironment.Singleton;

        private SearchPresenter Presenter { get; set; }

        #endregion

        #region Event Handlers

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            ToggleControls(false);

            try
            {
                await Presenter.BuildResponseContent(this);
                lblCounter.Text = Presenter.Counter;
                rtbResponse.Text = Presenter.Response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
                Environment.LogError(ex);
                ToggleControls(true);
                return;
            }

            HighlightQuery();

            ToggleControls(true);
            lblCounter.Focus();

            Environment.SaveAutoCompletes(this);
            Environment.InitializeAutoCompleteSaves();
            PrePopulateFields();
        }

        private void chkHighlightQuery_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHighlightQuery.Checked)
            {
                return;
            }

            rtbResponse.SelectionStart = 0;
            rtbResponse.SelectionLength = rtbResponse.Text.Length;
            rtbResponse.SelectionBackColor = DefaultSelectionBackColor;
            rtbResponse.SelectionColor = DefaultSelectionColor;
        }

        private void chkPeriodFilter_CheckedChanged(object sender, EventArgs e)
        {
            tblFilterByPeriod.Enabled = chkPeriodFilter.Checked;
        }

        private void rdoFilterByScoreSortOrder_CheckedChanged(object sender, EventArgs e)
        {
            rdoAscending.Checked = !rdoDescending.Checked;
        }

        private void rtbResponse_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            string browserName = Environment.WebBrowserFilePath;
            if (string.IsNullOrEmpty(browserName))
            {
                MessageBox.Show("Could not determine the default browser", "ERROR");
                return;
            }

            try
            {
                Process.Start(browserName, e.LinkText);
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        #endregion

        #region Helper Methods

        private QueryType GetQueryType()
        {
            if (ddlQueryType.SelectedItem.ToString() == "comment")
            {
                return QueryType.Comment;
            }

            if (ddlQueryType.SelectedItem.ToString() == "submission")
            {
                return QueryType.Submission;
            }

            return QueryType.Comment;
        }

        private string GetSortDirection()
        {
            if (rdoAscending.Checked)
            {
                return "asc";
            }
            else if (rdoDescending.Checked)
            {
                return "desc";
            }
            else
            {
                return "asc";
            }
        }

        private void HighlightQuery()
        {
            if (!chkHighlightQuery.Checked)
            {
                return;
            }

            foreach (Match match in Regex.Matches(rtbResponse.Text, $"\\b{Query}\\b", RegexOptions.IgnoreCase))
            {
                rtbResponse.Select(match.Index, Query.Length);
                rtbResponse.SelectionColor = Color.White;
                rtbResponse.SelectionBackColor = Color.Blue;
            }
        }

        private void PrePopulateFields()
        {
            SetAutoCompleteField(txtQuery, Environment.SavedQueries);
            SetAutoCompleteField(txtUserName, Environment.SavedUserNames);
            SetAutoCompleteField(txtSubreddit, Environment.SavedSubreddits);
        }

        private void SetAutoCompleteField(TextBox textBox, string[] allowedTypes)
        {
            textBox.Text = allowedTypes.LastOrDefault();

            AutoCompleteStringCollection autoCompleteList = new AutoCompleteStringCollection();
            autoCompleteList.AddRange(allowedTypes);

            textBox.AutoCompleteCustomSource = autoCompleteList;
            textBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void ToggleControls(bool enable)
        {
            btnSearch.Enabled = enable;
            tblFilterByPeriod.Enabled = enable;
            tblFilterByPeriodCheckBox.Enabled = enable;
            tblFilterByScore.Enabled = enable;
            tblFilterByScoreLabel.Enabled = enable;
            tblGeneral.Enabled = enable;
            tblSortByDirection.Enabled = enable;
            tblSortByType.Enabled = enable;
            tlpTextOptions.Enabled = enable;
            chkPeriodFilter_CheckedChanged(null, EventArgs.Empty);
        }

        private long ToUnixTime(DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - START_DATE).TotalSeconds;
        }

        #endregion

    }
}
