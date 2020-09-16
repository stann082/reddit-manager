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
            this.tblGeneral.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblFilterByPeriod.SuspendLayout();
            this.tblFilterByPeriodCheckBox.SuspendLayout();
            this.tblFilterByScore.SuspendLayout();
            this.tblFilterByScoreLabel.SuspendLayout();
            this.tblSortByDirection.SuspendLayout();
            this.tblSortByType.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(12, 6);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(58, 13);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "Username:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(76, 3);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(153, 20);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // txtSubreddit
            // 
            this.txtSubreddit.Location = new System.Drawing.Point(76, 29);
            this.txtSubreddit.Name = "txtSubreddit";
            this.txtSubreddit.Size = new System.Drawing.Size(153, 20);
            this.txtSubreddit.TabIndex = 3;
            this.txtSubreddit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // lblSubreddit
            // 
            this.lblSubreddit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSubreddit.AutoSize = true;
            this.lblSubreddit.Location = new System.Drawing.Point(15, 32);
            this.lblSubreddit.Name = "lblSubreddit";
            this.lblSubreddit.Size = new System.Drawing.Size(55, 13);
            this.lblSubreddit.TabIndex = 2;
            this.lblSubreddit.Text = "Subreddit:";
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(76, 55);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(153, 20);
            this.txtQuery.TabIndex = 5;
            this.txtQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // lblQuery
            // 
            this.lblQuery.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblQuery.AutoSize = true;
            this.lblQuery.Location = new System.Drawing.Point(32, 58);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(38, 13);
            this.lblQuery.TabIndex = 4;
            this.lblQuery.Text = "Query:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(90, 445);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(146, 29);
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
            this.rtbResponse.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbResponse.Location = new System.Drawing.Point(242, 10);
            this.rtbResponse.Name = "rtbResponse";
            this.rtbResponse.ReadOnly = true;
            this.rtbResponse.Size = new System.Drawing.Size(1106, 772);
            this.rtbResponse.TabIndex = 8;
            this.rtbResponse.Text = "";
            this.rtbResponse.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbResponse_LinkClicked);
            // 
            // tblGeneral
            // 
            this.tblGeneral.ColumnCount = 2;
            this.tblGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.5F));
            this.tblGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.5F));
            this.tblGeneral.Controls.Add(this.lblTotal, 0, 3);
            this.tblGeneral.Controls.Add(this.lblUserName, 0, 0);
            this.tblGeneral.Controls.Add(this.txtUserName, 1, 0);
            this.tblGeneral.Controls.Add(this.lblSubreddit, 0, 1);
            this.tblGeneral.Controls.Add(this.txtQuery, 1, 2);
            this.tblGeneral.Controls.Add(this.lblQuery, 0, 2);
            this.tblGeneral.Controls.Add(this.txtSubreddit, 1, 1);
            this.tblGeneral.Controls.Add(this.txtTotalResults, 1, 3);
            this.tblGeneral.Location = new System.Drawing.Point(4, 43);
            this.tblGeneral.Name = "tblGeneral";
            this.tblGeneral.RowCount = 4;
            this.tblGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblGeneral.Size = new System.Drawing.Size(232, 104);
            this.tblGeneral.TabIndex = 9;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(3, 84);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(67, 13);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Total results:";
            // 
            // txtTotalResults
            // 
            this.txtTotalResults.Location = new System.Drawing.Point(76, 81);
            this.txtTotalResults.Name = "txtTotalResults";
            this.txtTotalResults.Size = new System.Drawing.Size(153, 20);
            this.txtTotalResults.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lblCounter, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 10);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(232, 27);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // lblCounter
            // 
            this.lblCounter.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCounter.AutoSize = true;
            this.lblCounter.Location = new System.Drawing.Point(229, 7);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(0, 13);
            this.lblCounter.TabIndex = 0;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(17, 6);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(58, 13);
            this.lblStartDate.TabIndex = 8;
            this.lblStartDate.Text = "Start Date:";
            // 
            // dpStartDate
            // 
            this.dpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            this.dpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpStartDate.Location = new System.Drawing.Point(81, 3);
            this.dpStartDate.Name = "dpStartDate";
            this.dpStartDate.Size = new System.Drawing.Size(148, 20);
            this.dpStartDate.TabIndex = 10;
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
            this.tblFilterByPeriod.Location = new System.Drawing.Point(4, 192);
            this.tblFilterByPeriod.Name = "tblFilterByPeriod";
            this.tblFilterByPeriod.RowCount = 2;
            this.tblFilterByPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblFilterByPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblFilterByPeriod.Size = new System.Drawing.Size(232, 52);
            this.tblFilterByPeriod.TabIndex = 11;
            // 
            // dpStopDate
            // 
            this.dpStopDate.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            this.dpStopDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpStopDate.Location = new System.Drawing.Point(81, 29);
            this.dpStopDate.Name = "dpStopDate";
            this.dpStopDate.Size = new System.Drawing.Size(148, 20);
            this.dpStopDate.TabIndex = 11;
            // 
            // lblStopDate
            // 
            this.lblStopDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblStopDate.AutoSize = true;
            this.lblStopDate.Location = new System.Drawing.Point(17, 32);
            this.lblStopDate.Name = "lblStopDate";
            this.lblStopDate.Size = new System.Drawing.Size(58, 13);
            this.lblStopDate.TabIndex = 9;
            this.lblStopDate.Text = "Stop Date:";
            // 
            // tblFilterByPeriodCheckBox
            // 
            this.tblFilterByPeriodCheckBox.ColumnCount = 1;
            this.tblFilterByPeriodCheckBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilterByPeriodCheckBox.Controls.Add(this.chkPeriodFilter, 0, 0);
            this.tblFilterByPeriodCheckBox.Location = new System.Drawing.Point(4, 163);
            this.tblFilterByPeriodCheckBox.Name = "tblFilterByPeriodCheckBox";
            this.tblFilterByPeriodCheckBox.RowCount = 1;
            this.tblFilterByPeriodCheckBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilterByPeriodCheckBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblFilterByPeriodCheckBox.Size = new System.Drawing.Size(232, 26);
            this.tblFilterByPeriodCheckBox.TabIndex = 12;
            // 
            // chkPeriodFilter
            // 
            this.chkPeriodFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkPeriodFilter.AutoSize = true;
            this.chkPeriodFilter.Location = new System.Drawing.Point(3, 4);
            this.chkPeriodFilter.Name = "chkPeriodFilter";
            this.chkPeriodFilter.Size = new System.Drawing.Size(94, 17);
            this.chkPeriodFilter.TabIndex = 0;
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
            this.tblFilterByScore.Location = new System.Drawing.Point(4, 287);
            this.tblFilterByScore.Name = "tblFilterByScore";
            this.tblFilterByScore.RowCount = 2;
            this.tblFilterByScore.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblFilterByScore.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblFilterByScore.Size = new System.Drawing.Size(232, 53);
            this.tblFilterByScore.TabIndex = 14;
            // 
            // txtLessThan
            // 
            this.txtLessThan.Location = new System.Drawing.Point(80, 29);
            this.txtLessThan.Name = "txtLessThan";
            this.txtLessThan.Size = new System.Drawing.Size(149, 20);
            this.txtLessThan.TabIndex = 15;
            // 
            // txtGreaterThan
            // 
            this.txtGreaterThan.Location = new System.Drawing.Point(80, 3);
            this.txtGreaterThan.Name = "txtGreaterThan";
            this.txtGreaterThan.Size = new System.Drawing.Size(149, 20);
            this.txtGreaterThan.TabIndex = 15;
            // 
            // lblLessThan
            // 
            this.lblLessThan.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLessThan.AutoSize = true;
            this.lblLessThan.Location = new System.Drawing.Point(18, 33);
            this.lblLessThan.Name = "lblLessThan";
            this.lblLessThan.Size = new System.Drawing.Size(56, 13);
            this.lblLessThan.TabIndex = 15;
            this.lblLessThan.Text = "Less than:";
            // 
            // lblGreaterThan
            // 
            this.lblGreaterThan.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblGreaterThan.AutoSize = true;
            this.lblGreaterThan.Location = new System.Drawing.Point(5, 6);
            this.lblGreaterThan.Name = "lblGreaterThan";
            this.lblGreaterThan.Size = new System.Drawing.Size(69, 13);
            this.lblGreaterThan.TabIndex = 15;
            this.lblGreaterThan.Text = "Greater than:";
            // 
            // tblFilterByScoreLabel
            // 
            this.tblFilterByScoreLabel.ColumnCount = 1;
            this.tblFilterByScoreLabel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilterByScoreLabel.Controls.Add(this.lblFilterByScore, 0, 0);
            this.tblFilterByScoreLabel.Location = new System.Drawing.Point(4, 259);
            this.tblFilterByScoreLabel.Name = "tblFilterByScoreLabel";
            this.tblFilterByScoreLabel.RowCount = 1;
            this.tblFilterByScoreLabel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilterByScoreLabel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblFilterByScoreLabel.Size = new System.Drawing.Size(232, 26);
            this.tblFilterByScoreLabel.TabIndex = 13;
            // 
            // lblFilterByScore
            // 
            this.lblFilterByScore.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFilterByScore.AutoSize = true;
            this.lblFilterByScore.Location = new System.Drawing.Point(3, 6);
            this.lblFilterByScore.Name = "lblFilterByScore";
            this.lblFilterByScore.Size = new System.Drawing.Size(72, 13);
            this.lblFilterByScore.TabIndex = 0;
            this.lblFilterByScore.Text = "Filter by score";
            // 
            // tblSortByDirection
            // 
            this.tblSortByDirection.ColumnCount = 1;
            this.tblSortByDirection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSortByDirection.Controls.Add(this.rdoAscending, 0, 0);
            this.tblSortByDirection.Controls.Add(this.rdoDescending, 0, 1);
            this.tblSortByDirection.Location = new System.Drawing.Point(4, 389);
            this.tblSortByDirection.Name = "tblSortByDirection";
            this.tblSortByDirection.RowCount = 2;
            this.tblSortByDirection.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblSortByDirection.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblSortByDirection.Size = new System.Drawing.Size(232, 50);
            this.tblSortByDirection.TabIndex = 15;
            // 
            // rdoAscending
            // 
            this.rdoAscending.AutoSize = true;
            this.rdoAscending.Location = new System.Drawing.Point(3, 3);
            this.rdoAscending.Name = "rdoAscending";
            this.rdoAscending.Size = new System.Drawing.Size(75, 17);
            this.rdoAscending.TabIndex = 0;
            this.rdoAscending.TabStop = true;
            this.rdoAscending.Text = "Ascending";
            this.rdoAscending.UseVisualStyleBackColor = true;
            this.rdoAscending.CheckedChanged += new System.EventHandler(this.rdoFilterByScoreSortOrder_CheckedChanged);
            // 
            // rdoDescending
            // 
            this.rdoDescending.AutoSize = true;
            this.rdoDescending.Location = new System.Drawing.Point(3, 26);
            this.rdoDescending.Name = "rdoDescending";
            this.rdoDescending.Size = new System.Drawing.Size(82, 17);
            this.rdoDescending.TabIndex = 1;
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
            this.tblSortByType.Location = new System.Drawing.Point(4, 356);
            this.tblSortByType.Name = "tblSortByType";
            this.tblSortByType.RowCount = 1;
            this.tblSortByType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblSortByType.Size = new System.Drawing.Size(232, 27);
            this.tblSortByType.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Sort by";
            // 
            // ddlSortBy
            // 
            this.ddlSortBy.FormattingEnabled = true;
            this.ddlSortBy.Location = new System.Drawing.Point(56, 3);
            this.ddlSortBy.Name = "ddlSortBy";
            this.ddlSortBy.Size = new System.Drawing.Size(173, 21);
            this.ddlSortBy.TabIndex = 19;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 794);
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
    }
}

