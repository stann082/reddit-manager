
namespace PushshiftAPI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtSubreddit = new System.Windows.Forms.TextBox();
            this.lblSubreddit = new System.Windows.Forms.Label();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.lblQuery = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.rtbResponse = new System.Windows.Forms.RichTextBox();
            this.tblGeneral = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotalResults = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCounter = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dpStartDate = new System.Windows.Forms.DateTimePicker();
            this.tblFilterByPeriod = new System.Windows.Forms.TableLayoutPanel();
            this.dpStopDate = new System.Windows.Forms.DateTimePicker();
            this.lblStopDate = new System.Windows.Forms.Label();
            this.tblFilterByPeriodCheckBox = new System.Windows.Forms.TableLayoutPanel();
            this.chkPeriodFilter = new System.Windows.Forms.CheckBox();
            this.tblFilterByScore = new System.Windows.Forms.TableLayoutPanel();
            this.txtLessThan = new System.Windows.Forms.TextBox();
            this.txtGreaterThan = new System.Windows.Forms.TextBox();
            this.lblLessThan = new System.Windows.Forms.Label();
            this.lblGreaterThan = new System.Windows.Forms.Label();
            this.tblFilterByScoreLabel = new System.Windows.Forms.TableLayoutPanel();
            this.lblFilterByScore = new System.Windows.Forms.Label();
            this.tblSortByDirection = new System.Windows.Forms.TableLayoutPanel();
            this.rdoAscending = new System.Windows.Forms.RadioButton();
            this.rdoDescending = new System.Windows.Forms.RadioButton();
            this.tblSortByType = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlSortBy = new System.Windows.Forms.ComboBox();
            this.tlpTextOptions = new System.Windows.Forms.TableLayoutPanel();
            this.chkShowExactMatches = new System.Windows.Forms.CheckBox();
            this.chkHighlightQuery = new System.Windows.Forms.CheckBox();
            this.lblQueryType = new System.Windows.Forms.Label();
            this.ddlQueryType = new System.Windows.Forms.ComboBox();
            this.tblGeneral.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblFilterByPeriod.SuspendLayout();
            this.tblFilterByPeriodCheckBox.SuspendLayout();
            this.tblFilterByScore.SuspendLayout();
            this.tblFilterByScoreLabel.SuspendLayout();
            this.tblSortByDirection.SuspendLayout();
            this.tblSortByType.SuspendLayout();
            this.tlpTextOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(18, 7);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(63, 15);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "Username:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(89, 3);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(178, 23);
            this.txtUserName.TabIndex = 2;
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // txtSubreddit
            // 
            this.txtSubreddit.Location = new System.Drawing.Point(89, 32);
            this.txtSubreddit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSubreddit.Name = "txtSubreddit";
            this.txtSubreddit.Size = new System.Drawing.Size(178, 23);
            this.txtSubreddit.TabIndex = 3;
            this.txtSubreddit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // lblSubreddit
            // 
            this.lblSubreddit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSubreddit.AutoSize = true;
            this.lblSubreddit.Location = new System.Drawing.Point(20, 36);
            this.lblSubreddit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubreddit.Name = "lblSubreddit";
            this.lblSubreddit.Size = new System.Drawing.Size(61, 15);
            this.lblSubreddit.TabIndex = 2;
            this.lblSubreddit.Text = "Subreddit:";
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(89, 61);
            this.txtQuery.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(178, 23);
            this.txtQuery.TabIndex = 4;
            this.txtQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // lblQuery
            // 
            this.lblQuery.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblQuery.AutoSize = true;
            this.lblQuery.Location = new System.Drawing.Point(39, 65);
            this.lblQuery.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(42, 15);
            this.lblQuery.TabIndex = 4;
            this.lblQuery.Text = "Query:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(105, 540);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(170, 33);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // rtbResponse
            // 
            this.rtbResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbResponse.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbResponse.Location = new System.Drawing.Point(282, 50);
            this.rtbResponse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rtbResponse.Name = "rtbResponse";
            this.rtbResponse.ReadOnly = true;
            this.rtbResponse.Size = new System.Drawing.Size(1290, 852);
            this.rtbResponse.TabIndex = 14;
            this.rtbResponse.Text = "";
            this.rtbResponse.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbResponse_LinkClicked);
            // 
            // tblGeneral
            // 
            this.tblGeneral.ColumnCount = 2;
            this.tblGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.5F));
            this.tblGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.5F));
            this.tblGeneral.Controls.Add(this.lblQueryType, 0, 4);
            this.tblGeneral.Controls.Add(this.lblTotal, 0, 3);
            this.tblGeneral.Controls.Add(this.lblUserName, 0, 0);
            this.tblGeneral.Controls.Add(this.txtUserName, 1, 0);
            this.tblGeneral.Controls.Add(this.lblSubreddit, 0, 1);
            this.tblGeneral.Controls.Add(this.txtQuery, 1, 2);
            this.tblGeneral.Controls.Add(this.lblQuery, 0, 2);
            this.tblGeneral.Controls.Add(this.txtSubreddit, 1, 1);
            this.tblGeneral.Controls.Add(this.txtTotalResults, 1, 3);
            this.tblGeneral.Controls.Add(this.ddlQueryType, 1, 4);
            this.tblGeneral.Location = new System.Drawing.Point(5, 50);
            this.tblGeneral.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tblGeneral.Name = "tblGeneral";
            this.tblGeneral.RowCount = 5;
            this.tblGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblGeneral.Size = new System.Drawing.Size(271, 148);
            this.tblGeneral.TabIndex = 9;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(9, 94);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(72, 15);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Total results:";
            // 
            // txtTotalResults
            // 
            this.txtTotalResults.Location = new System.Drawing.Point(89, 90);
            this.txtTotalResults.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTotalResults.Name = "txtTotalResults";
            this.txtTotalResults.Size = new System.Drawing.Size(178, 23);
            this.txtTotalResults.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lblCounter, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 12);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(271, 31);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // lblCounter
            // 
            this.lblCounter.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCounter.AutoSize = true;
            this.lblCounter.Location = new System.Drawing.Point(267, 8);
            this.lblCounter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(0, 15);
            this.lblCounter.TabIndex = 0;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(27, 7);
            this.lblStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(61, 15);
            this.lblStartDate.TabIndex = 8;
            this.lblStartDate.Text = "Start Date:";
            // 
            // dpStartDate
            // 
            this.dpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            this.dpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpStartDate.Location = new System.Drawing.Point(96, 3);
            this.dpStartDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dpStartDate.Name = "dpStartDate";
            this.dpStartDate.Size = new System.Drawing.Size(171, 23);
            this.dpStartDate.TabIndex = 7;
            // 
            // tblFilterByPeriod
            // 
            this.tblFilterByPeriod.ColumnCount = 2;
            this.tblFilterByPeriod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.05172F));
            this.tblFilterByPeriod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.94828F));
            this.tblFilterByPeriod.Controls.Add(this.lblStartDate, 0, 0);
            this.tblFilterByPeriod.Controls.Add(this.dpStartDate, 1, 0);
            this.tblFilterByPeriod.Controls.Add(this.dpStopDate, 1, 1);
            this.tblFilterByPeriod.Controls.Add(this.lblStopDate, 0, 1);
            this.tblFilterByPeriod.Location = new System.Drawing.Point(5, 249);
            this.tblFilterByPeriod.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tblFilterByPeriod.Name = "tblFilterByPeriod";
            this.tblFilterByPeriod.RowCount = 2;
            this.tblFilterByPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblFilterByPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblFilterByPeriod.Size = new System.Drawing.Size(271, 60);
            this.tblFilterByPeriod.TabIndex = 11;
            // 
            // dpStopDate
            // 
            this.dpStopDate.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            this.dpStopDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpStopDate.Location = new System.Drawing.Point(96, 32);
            this.dpStopDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dpStopDate.Name = "dpStopDate";
            this.dpStopDate.Size = new System.Drawing.Size(171, 23);
            this.dpStopDate.TabIndex = 8;
            // 
            // lblStopDate
            // 
            this.lblStopDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblStopDate.AutoSize = true;
            this.lblStopDate.Location = new System.Drawing.Point(27, 37);
            this.lblStopDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStopDate.Name = "lblStopDate";
            this.lblStopDate.Size = new System.Drawing.Size(61, 15);
            this.lblStopDate.TabIndex = 9;
            this.lblStopDate.Text = "Stop Date:";
            // 
            // tblFilterByPeriodCheckBox
            // 
            this.tblFilterByPeriodCheckBox.ColumnCount = 1;
            this.tblFilterByPeriodCheckBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilterByPeriodCheckBox.Controls.Add(this.chkPeriodFilter, 0, 0);
            this.tblFilterByPeriodCheckBox.Location = new System.Drawing.Point(5, 215);
            this.tblFilterByPeriodCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tblFilterByPeriodCheckBox.Name = "tblFilterByPeriodCheckBox";
            this.tblFilterByPeriodCheckBox.RowCount = 1;
            this.tblFilterByPeriodCheckBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilterByPeriodCheckBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblFilterByPeriodCheckBox.Size = new System.Drawing.Size(271, 30);
            this.tblFilterByPeriodCheckBox.TabIndex = 12;
            // 
            // chkPeriodFilter
            // 
            this.chkPeriodFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkPeriodFilter.AutoSize = true;
            this.chkPeriodFilter.Location = new System.Drawing.Point(4, 5);
            this.chkPeriodFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkPeriodFilter.Name = "chkPeriodFilter";
            this.chkPeriodFilter.Size = new System.Drawing.Size(105, 19);
            this.chkPeriodFilter.TabIndex = 6;
            this.chkPeriodFilter.Text = "Filter by period";
            this.chkPeriodFilter.UseVisualStyleBackColor = true;
            this.chkPeriodFilter.CheckedChanged += new System.EventHandler(this.chkPeriodFilter_CheckedChanged);
            // 
            // tblFilterByScore
            // 
            this.tblFilterByScore.ColumnCount = 2;
            this.tblFilterByScore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.62069F));
            this.tblFilterByScore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.37931F));
            this.tblFilterByScore.Controls.Add(this.txtLessThan, 1, 1);
            this.tblFilterByScore.Controls.Add(this.txtGreaterThan, 1, 0);
            this.tblFilterByScore.Controls.Add(this.lblLessThan, 0, 1);
            this.tblFilterByScore.Controls.Add(this.lblGreaterThan, 0, 0);
            this.tblFilterByScore.Location = new System.Drawing.Point(5, 358);
            this.tblFilterByScore.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tblFilterByScore.Name = "tblFilterByScore";
            this.tblFilterByScore.RowCount = 2;
            this.tblFilterByScore.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblFilterByScore.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblFilterByScore.Size = new System.Drawing.Size(271, 61);
            this.tblFilterByScore.TabIndex = 14;
            // 
            // txtLessThan
            // 
            this.txtLessThan.Location = new System.Drawing.Point(95, 32);
            this.txtLessThan.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtLessThan.Name = "txtLessThan";
            this.txtLessThan.Size = new System.Drawing.Size(172, 23);
            this.txtLessThan.TabIndex = 10;
            // 
            // txtGreaterThan
            // 
            this.txtGreaterThan.Location = new System.Drawing.Point(95, 3);
            this.txtGreaterThan.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtGreaterThan.Name = "txtGreaterThan";
            this.txtGreaterThan.Size = new System.Drawing.Size(172, 23);
            this.txtGreaterThan.TabIndex = 9;
            // 
            // lblLessThan
            // 
            this.lblLessThan.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLessThan.AutoSize = true;
            this.lblLessThan.Location = new System.Drawing.Point(28, 37);
            this.lblLessThan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLessThan.Name = "lblLessThan";
            this.lblLessThan.Size = new System.Drawing.Size(59, 15);
            this.lblLessThan.TabIndex = 15;
            this.lblLessThan.Text = "Less than:";
            // 
            // lblGreaterThan
            // 
            this.lblGreaterThan.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblGreaterThan.AutoSize = true;
            this.lblGreaterThan.Location = new System.Drawing.Point(12, 7);
            this.lblGreaterThan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGreaterThan.Name = "lblGreaterThan";
            this.lblGreaterThan.Size = new System.Drawing.Size(75, 15);
            this.lblGreaterThan.TabIndex = 15;
            this.lblGreaterThan.Text = "Greater than:";
            // 
            // tblFilterByScoreLabel
            // 
            this.tblFilterByScoreLabel.ColumnCount = 1;
            this.tblFilterByScoreLabel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilterByScoreLabel.Controls.Add(this.lblFilterByScore, 0, 0);
            this.tblFilterByScoreLabel.Location = new System.Drawing.Point(5, 326);
            this.tblFilterByScoreLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tblFilterByScoreLabel.Name = "tblFilterByScoreLabel";
            this.tblFilterByScoreLabel.RowCount = 1;
            this.tblFilterByScoreLabel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilterByScoreLabel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblFilterByScoreLabel.Size = new System.Drawing.Size(271, 30);
            this.tblFilterByScoreLabel.TabIndex = 13;
            // 
            // lblFilterByScore
            // 
            this.lblFilterByScore.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFilterByScore.AutoSize = true;
            this.lblFilterByScore.Location = new System.Drawing.Point(4, 7);
            this.lblFilterByScore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilterByScore.Name = "lblFilterByScore";
            this.lblFilterByScore.Size = new System.Drawing.Size(80, 15);
            this.lblFilterByScore.TabIndex = 0;
            this.lblFilterByScore.Text = "Filter by score";
            // 
            // tblSortByDirection
            // 
            this.tblSortByDirection.ColumnCount = 1;
            this.tblSortByDirection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSortByDirection.Controls.Add(this.rdoAscending, 0, 0);
            this.tblSortByDirection.Controls.Add(this.rdoDescending, 0, 1);
            this.tblSortByDirection.Location = new System.Drawing.Point(5, 476);
            this.tblSortByDirection.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tblSortByDirection.Name = "tblSortByDirection";
            this.tblSortByDirection.RowCount = 2;
            this.tblSortByDirection.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblSortByDirection.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblSortByDirection.Size = new System.Drawing.Size(271, 58);
            this.tblSortByDirection.TabIndex = 15;
            // 
            // rdoAscending
            // 
            this.rdoAscending.AutoSize = true;
            this.rdoAscending.Location = new System.Drawing.Point(4, 3);
            this.rdoAscending.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdoAscending.Name = "rdoAscending";
            this.rdoAscending.Size = new System.Drawing.Size(81, 19);
            this.rdoAscending.TabIndex = 12;
            this.rdoAscending.TabStop = true;
            this.rdoAscending.Text = "Ascending";
            this.rdoAscending.UseVisualStyleBackColor = true;
            this.rdoAscending.CheckedChanged += new System.EventHandler(this.rdoFilterByScoreSortOrder_CheckedChanged);
            // 
            // rdoDescending
            // 
            this.rdoDescending.AutoSize = true;
            this.rdoDescending.Location = new System.Drawing.Point(4, 28);
            this.rdoDescending.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdoDescending.Name = "rdoDescending";
            this.rdoDescending.Size = new System.Drawing.Size(87, 19);
            this.rdoDescending.TabIndex = 13;
            this.rdoDescending.TabStop = true;
            this.rdoDescending.Text = "Descending";
            this.rdoDescending.UseVisualStyleBackColor = true;
            this.rdoDescending.CheckedChanged += new System.EventHandler(this.rdoFilterByScoreSortOrder_CheckedChanged);
            // 
            // tblSortByType
            // 
            this.tblSortByType.ColumnCount = 2;
            this.tblSortByType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.84483F));
            this.tblSortByType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.15517F));
            this.tblSortByType.Controls.Add(this.label1, 0, 0);
            this.tblSortByType.Controls.Add(this.ddlSortBy, 1, 0);
            this.tblSortByType.Location = new System.Drawing.Point(5, 438);
            this.tblSortByType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tblSortByType.Name = "tblSortByType";
            this.tblSortByType.RowCount = 1;
            this.tblSortByType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblSortByType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblSortByType.Size = new System.Drawing.Size(271, 31);
            this.tblSortByType.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Sort by";
            // 
            // ddlSortBy
            // 
            this.ddlSortBy.FormattingEnabled = true;
            this.ddlSortBy.Location = new System.Drawing.Point(65, 3);
            this.ddlSortBy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddlSortBy.Name = "ddlSortBy";
            this.ddlSortBy.Size = new System.Drawing.Size(201, 23);
            this.ddlSortBy.TabIndex = 11;
            // 
            // tlpTextOptions
            // 
            this.tlpTextOptions.ColumnCount = 2;
            this.tlpTextOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.02715F));
            this.tlpTextOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.97285F));
            this.tlpTextOptions.Controls.Add(this.chkShowExactMatches, 1, 0);
            this.tlpTextOptions.Controls.Add(this.chkHighlightQuery, 0, 0);
            this.tlpTextOptions.Location = new System.Drawing.Point(284, 12);
            this.tlpTextOptions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tlpTextOptions.Name = "tlpTextOptions";
            this.tlpTextOptions.RowCount = 1;
            this.tlpTextOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTextOptions.Size = new System.Drawing.Size(1289, 31);
            this.tlpTextOptions.TabIndex = 18;
            // 
            // chkShowExactMatches
            // 
            this.chkShowExactMatches.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkShowExactMatches.AutoSize = true;
            this.chkShowExactMatches.Location = new System.Drawing.Point(184, 6);
            this.chkShowExactMatches.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkShowExactMatches.Name = "chkShowExactMatches";
            this.chkShowExactMatches.Size = new System.Drawing.Size(134, 19);
            this.chkShowExactMatches.TabIndex = 1;
            this.chkShowExactMatches.Text = "Show exact matches";
            this.chkShowExactMatches.UseVisualStyleBackColor = true;
            // 
            // chkHighlightQuery
            // 
            this.chkHighlightQuery.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkHighlightQuery.AutoSize = true;
            this.chkHighlightQuery.Location = new System.Drawing.Point(4, 6);
            this.chkHighlightQuery.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkHighlightQuery.Name = "chkHighlightQuery";
            this.chkHighlightQuery.Size = new System.Drawing.Size(145, 19);
            this.chkHighlightQuery.TabIndex = 0;
            this.chkHighlightQuery.Text = "Highlight query in text";
            this.chkHighlightQuery.UseVisualStyleBackColor = true;
            this.chkHighlightQuery.CheckedChanged += new System.EventHandler(this.chkHighlightQuery_CheckedChanged);
            // 
            // lblQueryType
            // 
            this.lblQueryType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblQueryType.AutoSize = true;
            this.lblQueryType.Location = new System.Drawing.Point(47, 124);
            this.lblQueryType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQueryType.Name = "lblQueryType";
            this.lblQueryType.Size = new System.Drawing.Size(34, 15);
            this.lblQueryType.TabIndex = 8;
            this.lblQueryType.Text = "Type:";
            // 
            // ddlQueryType
            // 
            this.ddlQueryType.FormattingEnabled = true;
            this.ddlQueryType.Location = new System.Drawing.Point(88, 119);
            this.ddlQueryType.Name = "ddlQueryType";
            this.ddlQueryType.Size = new System.Drawing.Size(178, 23);
            this.ddlQueryType.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1587, 916);
            this.Controls.Add(this.tlpTextOptions);
            this.Controls.Add(this.tblSortByType);
            this.Controls.Add(this.tblSortByDirection);
            this.Controls.Add(this.tblFilterByScore);
            this.Controls.Add(this.tblFilterByScoreLabel);
            this.Controls.Add(this.tblFilterByPeriodCheckBox);
            this.Controls.Add(this.tblFilterByPeriod);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tblGeneral);
            this.Controls.Add(this.rtbResponse);
            this.Controls.Add(this.btnSearch);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.tblGeneral.ResumeLayout(false);
            this.tblGeneral.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tblFilterByPeriod.ResumeLayout(false);
            this.tblFilterByPeriod.PerformLayout();
            this.tblFilterByPeriodCheckBox.ResumeLayout(false);
            this.tblFilterByPeriodCheckBox.PerformLayout();
            this.tblFilterByScore.ResumeLayout(false);
            this.tblFilterByScore.PerformLayout();
            this.tblFilterByScoreLabel.ResumeLayout(false);
            this.tblFilterByScoreLabel.PerformLayout();
            this.tblSortByDirection.ResumeLayout(false);
            this.tblSortByDirection.PerformLayout();
            this.tblSortByType.ResumeLayout(false);
            this.tblSortByType.PerformLayout();
            this.tlpTextOptions.ResumeLayout(false);
            this.tlpTextOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtSubreddit;
        private System.Windows.Forms.Label lblSubreddit;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Label lblQuery;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.RichTextBox rtbResponse;
        private System.Windows.Forms.TableLayoutPanel tblGeneral;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotalResults;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dpStartDate;
        private System.Windows.Forms.TableLayoutPanel tblFilterByPeriod;
        private System.Windows.Forms.TableLayoutPanel tblFilterByPeriodCheckBox;
        private System.Windows.Forms.CheckBox chkPeriodFilter;
        private System.Windows.Forms.DateTimePicker dpStopDate;
        private System.Windows.Forms.Label lblStopDate;
        private System.Windows.Forms.TableLayoutPanel tblFilterByScore;
        private System.Windows.Forms.Label lblLessThan;
        private System.Windows.Forms.Label lblGreaterThan;
        private System.Windows.Forms.TextBox txtLessThan;
        private System.Windows.Forms.TextBox txtGreaterThan;
        private System.Windows.Forms.TableLayoutPanel tblFilterByScoreLabel;
        private System.Windows.Forms.Label lblFilterByScore;
        private System.Windows.Forms.TableLayoutPanel tblSortByDirection;
        private System.Windows.Forms.RadioButton rdoAscending;
        private System.Windows.Forms.RadioButton rdoDescending;
        private System.Windows.Forms.TableLayoutPanel tblSortByType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlSortBy;
        private System.Windows.Forms.TableLayoutPanel tlpTextOptions;
        private System.Windows.Forms.CheckBox chkHighlightQuery;
        private System.Windows.Forms.CheckBox chkShowExactMatches;
        private System.Windows.Forms.Label lblQueryType;
        private System.Windows.Forms.ComboBox ddlQueryType;
    }
}

