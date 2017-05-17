namespace DB_Lab6
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.SplitContainer splitContainer1;
            System.Windows.Forms.GroupBox groupBox1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.guiMathGroupBox = new System.Windows.Forms.GroupBox();
            this.guiMathAlgo = new System.Windows.Forms.ComboBox();
            this.guiMathColumn = new System.Windows.Forms.ComboBox();
            this.guiMath = new System.Windows.Forms.Button();
            this.guiFilterGroupBox = new System.Windows.Forms.GroupBox();
            this.guiFilterDirection = new System.Windows.Forms.ComboBox();
            this.guiFilterColumn = new System.Windows.Forms.ComboBox();
            this.guiFilter = new System.Windows.Forms.Button();
            this.guiSearchGroupBox = new System.Windows.Forms.GroupBox();
            this.guiSearchColumn = new System.Windows.Forms.ComboBox();
            this.guiSearhPattern = new System.Windows.Forms.TextBox();
            this.guiSearch = new System.Windows.Forms.Button();
            this.guiTables = new System.Windows.Forms.ComboBox();
            this.guiUpdateTable = new System.Windows.Forms.Button();
            this.guiQueryTable = new System.Windows.Forms.Button();
            this.guiDGV = new System.Windows.Forms.DataGridView();
            label1 = new System.Windows.Forms.Label();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            this.guiMathGroupBox.SuspendLayout();
            this.guiFilterGroupBox.SuspendLayout();
            this.guiSearchGroupBox.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guiDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 30);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(34, 13);
            label1.TabIndex = 1;
            label1.Text = "Table";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(this.guiMathGroupBox);
            splitContainer1.Panel1.Controls.Add(this.guiFilterGroupBox);
            splitContainer1.Panel1.Controls.Add(this.guiSearchGroupBox);
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(this.guiDGV);
            splitContainer1.Size = new System.Drawing.Size(783, 406);
            splitContainer1.SplitterDistance = 180;
            splitContainer1.SplitterWidth = 3;
            splitContainer1.TabIndex = 100;
            // 
            // guiMathGroupBox
            // 
            this.guiMathGroupBox.Controls.Add(this.guiMathAlgo);
            this.guiMathGroupBox.Controls.Add(this.guiMathColumn);
            this.guiMathGroupBox.Controls.Add(this.guiMath);
            this.guiMathGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.guiMathGroupBox.Enabled = false;
            this.guiMathGroupBox.Location = new System.Drawing.Point(0, 303);
            this.guiMathGroupBox.Name = "guiMathGroupBox";
            this.guiMathGroupBox.Size = new System.Drawing.Size(180, 103);
            this.guiMathGroupBox.TabIndex = 7;
            this.guiMathGroupBox.TabStop = false;
            this.guiMathGroupBox.Text = "Math";
            // 
            // guiMathAlgo
            // 
            this.guiMathAlgo.FormattingEnabled = true;
            this.guiMathAlgo.Items.AddRange(new object[] {
            "min",
            "max",
            "average",
            "sum"});
            this.guiMathAlgo.Location = new System.Drawing.Point(6, 45);
            this.guiMathAlgo.Name = "guiMathAlgo";
            this.guiMathAlgo.Size = new System.Drawing.Size(166, 21);
            this.guiMathAlgo.TabIndex = 5;
            // 
            // guiMathColumn
            // 
            this.guiMathColumn.FormattingEnabled = true;
            this.guiMathColumn.Location = new System.Drawing.Point(6, 19);
            this.guiMathColumn.Name = "guiMathColumn";
            this.guiMathColumn.Size = new System.Drawing.Size(166, 21);
            this.guiMathColumn.TabIndex = 0;
            // 
            // guiMath
            // 
            this.guiMath.Location = new System.Drawing.Point(6, 72);
            this.guiMath.Name = "guiMath";
            this.guiMath.Size = new System.Drawing.Size(166, 23);
            this.guiMath.TabIndex = 4;
            this.guiMath.Text = "Evaluate";
            this.guiMath.UseVisualStyleBackColor = true;
            this.guiMath.Click += new System.EventHandler(this.guiMath_Click);
            // 
            // guiFilterGroupBox
            // 
            this.guiFilterGroupBox.Controls.Add(this.guiFilterDirection);
            this.guiFilterGroupBox.Controls.Add(this.guiFilterColumn);
            this.guiFilterGroupBox.Controls.Add(this.guiFilter);
            this.guiFilterGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.guiFilterGroupBox.Enabled = false;
            this.guiFilterGroupBox.Location = new System.Drawing.Point(0, 193);
            this.guiFilterGroupBox.Name = "guiFilterGroupBox";
            this.guiFilterGroupBox.Size = new System.Drawing.Size(180, 110);
            this.guiFilterGroupBox.TabIndex = 6;
            this.guiFilterGroupBox.TabStop = false;
            this.guiFilterGroupBox.Text = "Filter";
            // 
            // guiFilterDirection
            // 
            this.guiFilterDirection.FormattingEnabled = true;
            this.guiFilterDirection.Items.AddRange(new object[] {
            "ascending",
            "descending"});
            this.guiFilterDirection.Location = new System.Drawing.Point(6, 45);
            this.guiFilterDirection.Name = "guiFilterDirection";
            this.guiFilterDirection.Size = new System.Drawing.Size(166, 21);
            this.guiFilterDirection.TabIndex = 5;
            // 
            // guiFilterColumn
            // 
            this.guiFilterColumn.FormattingEnabled = true;
            this.guiFilterColumn.Location = new System.Drawing.Point(6, 19);
            this.guiFilterColumn.Name = "guiFilterColumn";
            this.guiFilterColumn.Size = new System.Drawing.Size(166, 21);
            this.guiFilterColumn.TabIndex = 0;
            // 
            // guiFilter
            // 
            this.guiFilter.Location = new System.Drawing.Point(6, 72);
            this.guiFilter.Name = "guiFilter";
            this.guiFilter.Size = new System.Drawing.Size(166, 23);
            this.guiFilter.TabIndex = 4;
            this.guiFilter.Text = "Filter";
            this.guiFilter.UseVisualStyleBackColor = true;
            this.guiFilter.Click += new System.EventHandler(this.guiFilter_Click);
            // 
            // guiSearchGroupBox
            // 
            this.guiSearchGroupBox.Controls.Add(this.guiSearchColumn);
            this.guiSearchGroupBox.Controls.Add(this.guiSearhPattern);
            this.guiSearchGroupBox.Controls.Add(this.guiSearch);
            this.guiSearchGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.guiSearchGroupBox.Enabled = false;
            this.guiSearchGroupBox.Location = new System.Drawing.Point(0, 83);
            this.guiSearchGroupBox.Name = "guiSearchGroupBox";
            this.guiSearchGroupBox.Size = new System.Drawing.Size(180, 110);
            this.guiSearchGroupBox.TabIndex = 5;
            this.guiSearchGroupBox.TabStop = false;
            this.guiSearchGroupBox.Text = "Search";
            // 
            // guiSearchColumn
            // 
            this.guiSearchColumn.FormattingEnabled = true;
            this.guiSearchColumn.Location = new System.Drawing.Point(6, 19);
            this.guiSearchColumn.Name = "guiSearchColumn";
            this.guiSearchColumn.Size = new System.Drawing.Size(166, 21);
            this.guiSearchColumn.TabIndex = 0;
            // 
            // guiSearhPattern
            // 
            this.guiSearhPattern.Location = new System.Drawing.Point(6, 46);
            this.guiSearhPattern.Name = "guiSearhPattern";
            this.guiSearhPattern.Size = new System.Drawing.Size(166, 20);
            this.guiSearhPattern.TabIndex = 3;
            // 
            // guiSearch
            // 
            this.guiSearch.Location = new System.Drawing.Point(6, 72);
            this.guiSearch.Name = "guiSearch";
            this.guiSearch.Size = new System.Drawing.Size(166, 23);
            this.guiSearch.TabIndex = 4;
            this.guiSearch.Text = "Search";
            this.guiSearch.UseVisualStyleBackColor = true;
            this.guiSearch.Click += new System.EventHandler(this.guiSearch_Click);
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(this.guiTables);
            groupBox1.Controls.Add(this.guiUpdateTable);
            groupBox1.Controls.Add(this.guiQueryTable);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(180, 83);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Table";
            // 
            // guiTables
            // 
            this.guiTables.FormattingEnabled = true;
            this.guiTables.Location = new System.Drawing.Point(51, 27);
            this.guiTables.Name = "guiTables";
            this.guiTables.Size = new System.Drawing.Size(121, 21);
            this.guiTables.TabIndex = 0;
            // 
            // guiUpdateTable
            // 
            this.guiUpdateTable.Enabled = false;
            this.guiUpdateTable.Location = new System.Drawing.Point(90, 54);
            this.guiUpdateTable.Name = "guiUpdateTable";
            this.guiUpdateTable.Size = new System.Drawing.Size(82, 23);
            this.guiUpdateTable.TabIndex = 4;
            this.guiUpdateTable.Text = "Update table";
            this.guiUpdateTable.UseVisualStyleBackColor = true;
            this.guiUpdateTable.Click += new System.EventHandler(this.guiUpdateTable_Click);
            // 
            // guiQueryTable
            // 
            this.guiQueryTable.Location = new System.Drawing.Point(9, 54);
            this.guiQueryTable.Name = "guiQueryTable";
            this.guiQueryTable.Size = new System.Drawing.Size(75, 23);
            this.guiQueryTable.TabIndex = 4;
            this.guiQueryTable.Text = "Query table";
            this.guiQueryTable.UseVisualStyleBackColor = true;
            this.guiQueryTable.Click += new System.EventHandler(this.guiQueryTable_Click);
            // 
            // guiDGV
            // 
            this.guiDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.guiDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.guiDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guiDGV.Location = new System.Drawing.Point(0, 0);
            this.guiDGV.MultiSelect = false;
            this.guiDGV.Name = "guiDGV";
            this.guiDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.guiDGV.Size = new System.Drawing.Size(600, 406);
            this.guiDGV.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 406);
            this.Controls.Add(splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Hospital DB - Lab7";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            this.guiMathGroupBox.ResumeLayout(false);
            this.guiFilterGroupBox.ResumeLayout(false);
            this.guiSearchGroupBox.ResumeLayout(false);
            this.guiSearchGroupBox.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guiDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox guiTables;
        private System.Windows.Forms.DataGridView guiDGV;
        private System.Windows.Forms.TextBox guiSearhPattern;
        private System.Windows.Forms.Button guiSearch;
        private System.Windows.Forms.Button guiQueryTable;
        private System.Windows.Forms.ComboBox guiSearchColumn;
        private System.Windows.Forms.Button guiUpdateTable;
        private System.Windows.Forms.GroupBox guiSearchGroupBox;
        private System.Windows.Forms.GroupBox guiFilterGroupBox;
        private System.Windows.Forms.ComboBox guiFilterColumn;
        private System.Windows.Forms.Button guiFilter;
        private System.Windows.Forms.ComboBox guiFilterDirection;
        private System.Windows.Forms.GroupBox guiMathGroupBox;
        private System.Windows.Forms.ComboBox guiMathAlgo;
        private System.Windows.Forms.ComboBox guiMathColumn;
        private System.Windows.Forms.Button guiMath;
    }
}

