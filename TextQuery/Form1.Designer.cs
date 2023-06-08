namespace TextQuery
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            regexTextBox = new TextBox();
            label2 = new Label();
            splitContainer1 = new SplitContainer();
            loadButton = new Button();
            saveButton = new Button();
            linkLabel1 = new LinkLabel();
            processorTabControl = new TabControl();
            regexTab = new TabPage();
            regexGlobalCheckBox = new CheckBox();
            escapeButton = new Button();
            wholeMatchCheckBox = new CheckBox();
            csvTsvTab = new TabPage();
            editHeadingsButton = new Button();
            csvTsvHeadingsCheckBox = new CheckBox();
            label1 = new Label();
            csvTsvHeadingsTextBox = new TextBox();
            csvTsvComboBox = new ComboBox();
            dataSourceTabs = new TabControl();
            textTab = new TabPage();
            textTextBox = new TextBox();
            singleFileTab = new TabPage();
            openSingleFileButton = new Button();
            fileNameTextBox = new TextBox();
            label3 = new Label();
            multipleFileTab = new TabPage();
            multipleFileRecursiveCheckBox = new CheckBox();
            label5 = new Label();
            multipleFileNamePatternTextBox = new TextBox();
            multipleFileBrowseButton = new Button();
            label4 = new Label();
            multipleFilePathTextBox = new TextBox();
            queryTextBox = new ScintillaNET.Scintilla();
            queryButton = new Button();
            dataGridView1 = new DataGridView();
            statusStrip1 = new StatusStrip();
            toolStripProgressBar1 = new ToolStripProgressBar();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            openSingleFileDialog = new OpenFileDialog();
            saveProfileFileDialog = new SaveFileDialog();
            openProfileFileDialog = new OpenFileDialog();
            multipleFileFolderBrowserDialog = new FolderBrowserDialog();
            queryBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            processorTabControl.SuspendLayout();
            regexTab.SuspendLayout();
            csvTsvTab.SuspendLayout();
            dataSourceTabs.SuspendLayout();
            textTab.SuspendLayout();
            singleFileTab.SuspendLayout();
            multipleFileTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // regexTextBox
            // 
            regexTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            regexTextBox.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
            regexTextBox.Location = new Point(7, 11);
            regexTextBox.Name = "regexTextBox";
            regexTextBox.Size = new Size(715, 28);
            regexTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 266);
            label2.Name = "label2";
            label2.Size = new Size(60, 25);
            label2.TabIndex = 3;
            label2.Text = "Query";
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AutoScroll = true;
            splitContainer1.Panel1.Controls.Add(loadButton);
            splitContainer1.Panel1.Controls.Add(saveButton);
            splitContainer1.Panel1.Controls.Add(linkLabel1);
            splitContainer1.Panel1.Controls.Add(processorTabControl);
            splitContainer1.Panel1.Controls.Add(dataSourceTabs);
            splitContainer1.Panel1.Controls.Add(queryTextBox);
            splitContainer1.Panel1.Controls.Add(queryButton);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1MinSize = 340;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView1);
            splitContainer1.Panel2.RightToLeft = RightToLeft.No;
            splitContainer1.Size = new Size(1323, 917);
            splitContainer1.SplitterDistance = 523;
            splitContainer1.TabIndex = 4;
            // 
            // loadButton
            // 
            loadButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            loadButton.Location = new Point(187, 482);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(161, 34);
            loadButton.TabIndex = 13;
            loadButton.Text = "Load Profile...";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += loadButton_Click;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            saveButton.Location = new Point(23, 483);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(158, 34);
            saveButton.TabIndex = 12;
            saveButton.Text = "Save Profile...";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(23, 291);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(54, 25);
            linkLabel1.TabIndex = 11;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "(KQL)";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // processorTabControl
            // 
            processorTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            processorTabControl.Controls.Add(regexTab);
            processorTabControl.Controls.Add(csvTsvTab);
            processorTabControl.Location = new Point(12, 177);
            processorTabControl.Name = "processorTabControl";
            processorTabControl.SelectedIndex = 0;
            processorTabControl.Size = new Size(1299, 86);
            processorTabControl.TabIndex = 10;
            // 
            // regexTab
            // 
            regexTab.Controls.Add(regexGlobalCheckBox);
            regexTab.Controls.Add(escapeButton);
            regexTab.Controls.Add(regexTextBox);
            regexTab.Controls.Add(wholeMatchCheckBox);
            regexTab.Location = new Point(4, 34);
            regexTab.Name = "regexTab";
            regexTab.Padding = new Padding(3);
            regexTab.Size = new Size(1291, 48);
            regexTab.TabIndex = 0;
            regexTab.Text = "Regex";
            regexTab.UseVisualStyleBackColor = true;
            // 
            // regexGlobalCheckBox
            // 
            regexGlobalCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            regexGlobalCheckBox.AutoSize = true;
            regexGlobalCheckBox.Location = new Point(737, 11);
            regexGlobalCheckBox.Name = "regexGlobalCheckBox";
            regexGlobalCheckBox.Size = new Size(89, 29);
            regexGlobalCheckBox.TabIndex = 9;
            regexGlobalCheckBox.Text = "Global";
            regexGlobalCheckBox.UseVisualStyleBackColor = true;
            // 
            // escapeButton
            // 
            escapeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            escapeButton.Location = new Point(1103, 6);
            escapeButton.Name = "escapeButton";
            escapeButton.Size = new Size(182, 36);
            escapeButton.TabIndex = 8;
            escapeButton.Text = "Escape Selection";
            escapeButton.UseVisualStyleBackColor = true;
            escapeButton.Click += escapeButton_Click;
            // 
            // wholeMatchCheckBox
            // 
            wholeMatchCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            wholeMatchCheckBox.AutoSize = true;
            wholeMatchCheckBox.Location = new Point(832, 11);
            wholeMatchCheckBox.Name = "wholeMatchCheckBox";
            wholeMatchCheckBox.Size = new Size(266, 29);
            wholeMatchCheckBox.TabIndex = 7;
            wholeMatchCheckBox.Text = "Include Whole Match (as \"0\")";
            wholeMatchCheckBox.UseVisualStyleBackColor = true;
            // 
            // csvTsvTab
            // 
            csvTsvTab.Controls.Add(editHeadingsButton);
            csvTsvTab.Controls.Add(csvTsvHeadingsCheckBox);
            csvTsvTab.Controls.Add(label1);
            csvTsvTab.Controls.Add(csvTsvHeadingsTextBox);
            csvTsvTab.Controls.Add(csvTsvComboBox);
            csvTsvTab.Location = new Point(4, 34);
            csvTsvTab.Name = "csvTsvTab";
            csvTsvTab.Padding = new Padding(3);
            csvTsvTab.Size = new Size(1291, 48);
            csvTsvTab.TabIndex = 1;
            csvTsvTab.Text = "CSV/TSV";
            csvTsvTab.UseVisualStyleBackColor = true;
            // 
            // editHeadingsButton
            // 
            editHeadingsButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            editHeadingsButton.Location = new Point(1173, 5);
            editHeadingsButton.Name = "editHeadingsButton";
            editHeadingsButton.Size = new Size(112, 34);
            editHeadingsButton.TabIndex = 4;
            editHeadingsButton.Text = "Edit";
            editHeadingsButton.UseVisualStyleBackColor = true;
            // 
            // csvTsvHeadingsCheckBox
            // 
            csvTsvHeadingsCheckBox.AutoSize = true;
            csvTsvHeadingsCheckBox.Location = new Point(212, 8);
            csvTsvHeadingsCheckBox.Name = "csvTsvHeadingsCheckBox";
            csvTsvHeadingsCheckBox.Size = new Size(190, 29);
            csvTsvHeadingsCheckBox.TabIndex = 3;
            csvTsvHeadingsCheckBox.Text = "First Row Headings";
            csvTsvHeadingsCheckBox.UseVisualStyleBackColor = true;
            csvTsvHeadingsCheckBox.CheckedChanged += csvTsvHeadingsCheckBox_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(433, 9);
            label1.Name = "label1";
            label1.Size = new Size(91, 25);
            label1.TabIndex = 2;
            label1.Text = "Headings:";
            // 
            // csvTsvHeadingsTextBox
            // 
            csvTsvHeadingsTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            csvTsvHeadingsTextBox.Location = new Point(530, 6);
            csvTsvHeadingsTextBox.Name = "csvTsvHeadingsTextBox";
            csvTsvHeadingsTextBox.Size = new Size(630, 31);
            csvTsvHeadingsTextBox.TabIndex = 1;
            // 
            // csvTsvComboBox
            // 
            csvTsvComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            csvTsvComboBox.FormattingEnabled = true;
            csvTsvComboBox.Items.AddRange(new object[] { "CSV", "TSV" });
            csvTsvComboBox.Location = new Point(7, 6);
            csvTsvComboBox.Name = "csvTsvComboBox";
            csvTsvComboBox.Size = new Size(182, 33);
            csvTsvComboBox.TabIndex = 0;
            // 
            // dataSourceTabs
            // 
            dataSourceTabs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataSourceTabs.Controls.Add(textTab);
            dataSourceTabs.Controls.Add(singleFileTab);
            dataSourceTabs.Controls.Add(multipleFileTab);
            dataSourceTabs.Location = new Point(12, 12);
            dataSourceTabs.Name = "dataSourceTabs";
            dataSourceTabs.SelectedIndex = 0;
            dataSourceTabs.Size = new Size(1299, 163);
            dataSourceTabs.TabIndex = 9;
            // 
            // textTab
            // 
            textTab.Controls.Add(textTextBox);
            textTab.Location = new Point(4, 34);
            textTab.Name = "textTab";
            textTab.Padding = new Padding(3);
            textTab.Size = new Size(1291, 125);
            textTab.TabIndex = 1;
            textTab.Text = "Text";
            textTab.UseVisualStyleBackColor = true;
            // 
            // textTextBox
            // 
            textTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textTextBox.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
            textTextBox.Location = new Point(7, 6);
            textTextBox.Multiline = true;
            textTextBox.Name = "textTextBox";
            textTextBox.Size = new Size(1278, 113);
            textTextBox.TabIndex = 0;
            // 
            // singleFileTab
            // 
            singleFileTab.Controls.Add(openSingleFileButton);
            singleFileTab.Controls.Add(fileNameTextBox);
            singleFileTab.Controls.Add(label3);
            singleFileTab.Location = new Point(4, 34);
            singleFileTab.Name = "singleFileTab";
            singleFileTab.Padding = new Padding(3);
            singleFileTab.Size = new Size(1291, 125);
            singleFileTab.TabIndex = 0;
            singleFileTab.Text = "Single File";
            singleFileTab.UseVisualStyleBackColor = true;
            // 
            // openSingleFileButton
            // 
            openSingleFileButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            openSingleFileButton.Location = new Point(1131, 73);
            openSingleFileButton.Name = "openSingleFileButton";
            openSingleFileButton.Size = new Size(143, 34);
            openSingleFileButton.TabIndex = 6;
            openSingleFileButton.Text = "Choose File...";
            openSingleFileButton.UseVisualStyleBackColor = true;
            openSingleFileButton.Click += openSingleFileButton_Click;
            // 
            // fileNameTextBox
            // 
            fileNameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            fileNameTextBox.Location = new Point(51, 16);
            fileNameTextBox.Name = "fileNameTextBox";
            fileNameTextBox.Size = new Size(1234, 31);
            fileNameTextBox.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 19);
            label3.Name = "label3";
            label3.Size = new Size(38, 25);
            label3.TabIndex = 5;
            label3.Text = "File";
            // 
            // multipleFileTab
            // 
            multipleFileTab.Controls.Add(multipleFileRecursiveCheckBox);
            multipleFileTab.Controls.Add(label5);
            multipleFileTab.Controls.Add(multipleFileNamePatternTextBox);
            multipleFileTab.Controls.Add(multipleFileBrowseButton);
            multipleFileTab.Controls.Add(label4);
            multipleFileTab.Controls.Add(multipleFilePathTextBox);
            multipleFileTab.Location = new Point(4, 34);
            multipleFileTab.Name = "multipleFileTab";
            multipleFileTab.Size = new Size(1291, 125);
            multipleFileTab.TabIndex = 2;
            multipleFileTab.Text = "File Search";
            multipleFileTab.UseVisualStyleBackColor = true;
            // 
            // multipleFileRecursiveCheckBox
            // 
            multipleFileRecursiveCheckBox.AutoSize = true;
            multipleFileRecursiveCheckBox.Location = new Point(389, 71);
            multipleFileRecursiveCheckBox.Name = "multipleFileRecursiveCheckBox";
            multipleFileRecursiveCheckBox.Size = new Size(111, 29);
            multipleFileRecursiveCheckBox.TabIndex = 5;
            multipleFileRecursiveCheckBox.Text = "Recursive";
            multipleFileRecursiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 72);
            label5.Name = "label5";
            label5.Size = new Size(150, 25);
            label5.TabIndex = 4;
            label5.Text = "File Name Pattern";
            // 
            // multipleFileNamePatternTextBox
            // 
            multipleFileNamePatternTextBox.Location = new Point(159, 69);
            multipleFileNamePatternTextBox.Name = "multipleFileNamePatternTextBox";
            multipleFileNamePatternTextBox.Size = new Size(212, 31);
            multipleFileNamePatternTextBox.TabIndex = 3;
            // 
            // multipleFileBrowseButton
            // 
            multipleFileBrowseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            multipleFileBrowseButton.Location = new Point(1161, 15);
            multipleFileBrowseButton.Name = "multipleFileBrowseButton";
            multipleFileBrowseButton.Size = new Size(112, 34);
            multipleFileBrowseButton.TabIndex = 2;
            multipleFileBrowseButton.Text = "Browse...";
            multipleFileBrowseButton.UseVisualStyleBackColor = true;
            multipleFileBrowseButton.Click += multipleFileBrowseButton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(67, 20);
            label4.Name = "label4";
            label4.Size = new Size(84, 25);
            label4.TabIndex = 1;
            label4.Text = "Directory";
            // 
            // multipleFilePathTextBox
            // 
            multipleFilePathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            multipleFilePathTextBox.Location = new Point(159, 17);
            multipleFilePathTextBox.Name = "multipleFilePathTextBox";
            multipleFilePathTextBox.Size = new Size(996, 31);
            multipleFilePathTextBox.TabIndex = 0;
            // 
            // queryTextBox
            // 
            queryTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            queryTextBox.AutoCMaxHeight = 9;
            queryTextBox.BiDirectionality = ScintillaNET.BiDirectionalDisplayType.Disabled;
            queryTextBox.CaretLineBackColor = Color.LightYellow;
            queryTextBox.CaretLineVisible = true;
            queryTextBox.EolMode = ScintillaNET.Eol.Cr;
            queryTextBox.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point);
            queryTextBox.LexerName = "";
            queryTextBox.Location = new Point(83, 265);
            queryTextBox.Name = "queryTextBox";
            queryTextBox.ScrollWidth = 74;
            queryTextBox.Size = new Size(1228, 211);
            queryTextBox.TabIndents = true;
            queryTextBox.TabIndex = 2;
            queryTextBox.Text = "result";
            queryTextBox.UseRightToLeftReadingLayout = false;
            queryTextBox.WrapMode = ScintillaNET.WrapMode.Whitespace;
            // 
            // queryButton
            // 
            queryButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            queryButton.Location = new Point(1199, 482);
            queryButton.Name = "queryButton";
            queryButton.Size = new Size(112, 34);
            queryButton.TabIndex = 6;
            queryButton.Text = "Query";
            queryButton.UseVisualStyleBackColor = true;
            queryButton.Click += queryButton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 33;
            dataGridView1.Size = new Size(1317, 384);
            dataGridView1.TabIndex = 100;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 920);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1324, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(100, 20);
            toolStripProgressBar1.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 15);
            // 
            // saveProfileFileDialog
            // 
            saveProfileFileDialog.Filter = "Text Query profile|*.tq.json";
            saveProfileFileDialog.SupportMultiDottedExtensions = true;
            // 
            // openProfileFileDialog
            // 
            openProfileFileDialog.Filter = "Text Query profile|*.tq.json";
            openProfileFileDialog.SupportMultiDottedExtensions = true;
            // 
            // queryBackgroundWorker
            // 
            queryBackgroundWorker.WorkerReportsProgress = true;
            queryBackgroundWorker.WorkerSupportsCancellation = true;
            queryBackgroundWorker.DoWork += queryBackgroundWorker_DoWork;
            queryBackgroundWorker.ProgressChanged += queryBackgroundWorker_ProgressChanged;
            queryBackgroundWorker.RunWorkerCompleted += queryBackgroundWorker_RunWorkerCompleted;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1324, 942);
            Controls.Add(statusStrip1);
            Controls.Add(splitContainer1);
            MinimumSize = new Size(0, 500);
            Name = "Form1";
            Text = "Text Query";
            FormClosing += Form1_FormClosing;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            processorTabControl.ResumeLayout(false);
            regexTab.ResumeLayout(false);
            regexTab.PerformLayout();
            csvTsvTab.ResumeLayout(false);
            csvTsvTab.PerformLayout();
            dataSourceTabs.ResumeLayout(false);
            textTab.ResumeLayout(false);
            textTab.PerformLayout();
            singleFileTab.ResumeLayout(false);
            singleFileTab.PerformLayout();
            multipleFileTab.ResumeLayout(false);
            multipleFileTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox regexTextBox;
        private Label label2;
        private SplitContainer splitContainer1;
        private Label label3;
        private TextBox fileNameTextBox;
        private Button queryButton;
        private DataGridView dataGridView1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private CheckBox wholeMatchCheckBox;
        private Button escapeButton;
        private ScintillaNET.Scintilla queryTextBox;
        private TabControl dataSourceTabs;
        private TabPage singleFileTab;
        private TabPage textTab;
        private TextBox textTextBox;
        private TabControl processorTabControl;
        private TabPage regexTab;
        private TabPage csvTsvTab;
        private ComboBox csvTsvComboBox;
        private Button editHeadingsButton;
        private CheckBox csvTsvHeadingsCheckBox;
        private Label label1;
        private TextBox csvTsvHeadingsTextBox;
        private Button openSingleFileButton;
        private OpenFileDialog openSingleFileDialog;
        private LinkLabel linkLabel1;
        private TabPage multipleFileTab;
        private Label label5;
        private TextBox multipleFileNamePatternTextBox;
        private Button multipleFileBrowseButton;
        private Label label4;
        private TextBox multipleFilePathTextBox;
        private Button loadButton;
        private Button saveButton;
        private SaveFileDialog saveProfileFileDialog;
        private OpenFileDialog openProfileFileDialog;
        private FolderBrowserDialog multipleFileFolderBrowserDialog;
        private CheckBox multipleFileRecursiveCheckBox;
        private System.ComponentModel.BackgroundWorker queryBackgroundWorker;
        private ToolStripProgressBar toolStripProgressBar1;
        private CheckBox regexGlobalCheckBox;
    }
}