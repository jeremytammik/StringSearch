namespace ADNPlugin.Revit.StringSearch
{
  partial class SearchForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.label1 = new System.Windows.Forms.Label();
      this.cmbSearchString = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmbCategory = new System.Windows.Forms.ComboBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.chkBuiltInParams = new System.Windows.Forms.CheckBox();
      this.chkElementType = new System.Windows.Forms.CheckBox();
      this.chkRegex = new System.Windows.Forms.CheckBox();
      this.chkWholeWord = new System.Windows.Forms.CheckBox();
      this.chkMatchCase = new System.Windows.Forms.CheckBox();
      this.radioButtonProject = new System.Windows.Forms.RadioButton();
      this.radioButtonView = new System.Windows.Forms.RadioButton();
      this.radioButtonSelection = new System.Windows.Forms.RadioButton();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.cmbParameter = new System.Windows.Forms.ComboBox();
      this.lblParameter = new System.Windows.Forms.Label();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip( this.components );
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.displayLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point( 7, 7 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 56, 13 );
      this.label1.TabIndex = 0;
      this.label1.Text = "Find what:";
      // 
      // cmbSearchString
      // 
      this.cmbSearchString.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cmbSearchString.FormattingEnabled = true;
      this.cmbSearchString.Location = new System.Drawing.Point( 10, 24 );
      this.cmbSearchString.Name = "cmbSearchString";
      this.cmbSearchString.Size = new System.Drawing.Size( 228, 21 );
      this.cmbSearchString.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point( 7, 49 );
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size( 52, 13 );
      this.label2.TabIndex = 2;
      this.label2.Text = "Category:";
      // 
      // cmbCategory
      // 
      this.cmbCategory.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbCategory.FormattingEnabled = true;
      this.cmbCategory.Location = new System.Drawing.Point( 10, 66 );
      this.cmbCategory.Name = "cmbCategory";
      this.cmbCategory.Size = new System.Drawing.Size( 228, 21 );
      this.cmbCategory.TabIndex = 3;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add( this.chkBuiltInParams );
      this.groupBox1.Controls.Add( this.chkElementType );
      this.groupBox1.Controls.Add( this.chkRegex );
      this.groupBox1.Controls.Add( this.chkWholeWord );
      this.groupBox1.Controls.Add( this.chkMatchCase );
      this.groupBox1.Location = new System.Drawing.Point( 10, 142 );
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size( 166, 123 );
      this.groupBox1.TabIndex = 6;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Find options";
      // 
      // chkBuiltInParams
      // 
      this.chkBuiltInParams.AutoSize = true;
      this.chkBuiltInParams.Location = new System.Drawing.Point( 4, 79 );
      this.chkBuiltInParams.Name = "chkBuiltInParams";
      this.chkBuiltInParams.Size = new System.Drawing.Size( 148, 17 );
      this.chkBuiltInParams.TabIndex = 3;
      this.chkBuiltInParams.Text = "Search built-in parameters";
      this.chkBuiltInParams.UseVisualStyleBackColor = true;
      this.chkBuiltInParams.CheckedChanged += new System.EventHandler( this.chkBuiltInParams_CheckedChanged );
      // 
      // chkElementType
      // 
      this.chkElementType.AutoSize = true;
      this.chkElementType.Location = new System.Drawing.Point( 4, 100 );
      this.chkElementType.Name = "chkElementType";
      this.chkElementType.Size = new System.Drawing.Size( 130, 17 );
      this.chkElementType.TabIndex = 4;
      this.chkElementType.Text = "Search ElementTypes";
      this.chkElementType.UseVisualStyleBackColor = true;
      this.chkElementType.CheckedChanged += new System.EventHandler( this.chkNonElementType_CheckedChanged );
      // 
      // chkRegex
      // 
      this.chkRegex.AutoSize = true;
      this.chkRegex.Location = new System.Drawing.Point( 4, 58 );
      this.chkRegex.Name = "chkRegex";
      this.chkRegex.Size = new System.Drawing.Size( 138, 17 );
      this.chkRegex.TabIndex = 2;
      this.chkRegex.Text = "Use regular expressions";
      this.chkRegex.UseVisualStyleBackColor = true;
      // 
      // chkWholeWord
      // 
      this.chkWholeWord.AutoSize = true;
      this.chkWholeWord.Location = new System.Drawing.Point( 4, 37 );
      this.chkWholeWord.Name = "chkWholeWord";
      this.chkWholeWord.Size = new System.Drawing.Size( 113, 17 );
      this.chkWholeWord.TabIndex = 1;
      this.chkWholeWord.Text = "Match whole word";
      this.chkWholeWord.UseVisualStyleBackColor = true;
      // 
      // chkMatchCase
      // 
      this.chkMatchCase.AutoSize = true;
      this.chkMatchCase.Location = new System.Drawing.Point( 4, 16 );
      this.chkMatchCase.Name = "chkMatchCase";
      this.chkMatchCase.Size = new System.Drawing.Size( 82, 17 );
      this.chkMatchCase.TabIndex = 0;
      this.chkMatchCase.Text = "Match case";
      this.chkMatchCase.UseVisualStyleBackColor = true;
      // 
      // radioButtonProject
      // 
      this.radioButtonProject.AutoSize = true;
      this.radioButtonProject.Location = new System.Drawing.Point( 10, 55 );
      this.radioButtonProject.Name = "radioButtonProject";
      this.radioButtonProject.Size = new System.Drawing.Size( 87, 17 );
      this.radioButtonProject.TabIndex = 2;
      this.radioButtonProject.Text = "Entire project";
      this.radioButtonProject.UseVisualStyleBackColor = true;
      // 
      // radioButtonView
      // 
      this.radioButtonView.AutoSize = true;
      this.radioButtonView.Checked = true;
      this.radioButtonView.Location = new System.Drawing.Point( 10, 37 );
      this.radioButtonView.Name = "radioButtonView";
      this.radioButtonView.Size = new System.Drawing.Size( 84, 17 );
      this.radioButtonView.TabIndex = 1;
      this.radioButtonView.TabStop = true;
      this.radioButtonView.Text = "Current view";
      this.radioButtonView.UseVisualStyleBackColor = true;
      // 
      // radioButtonSelection
      // 
      this.radioButtonSelection.AutoSize = true;
      this.radioButtonSelection.Location = new System.Drawing.Point( 10, 19 );
      this.radioButtonSelection.Name = "radioButtonSelection";
      this.radioButtonSelection.Size = new System.Drawing.Size( 104, 17 );
      this.radioButtonSelection.TabIndex = 0;
      this.radioButtonSelection.Text = "Current selection";
      this.radioButtonSelection.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point( 102, 362 );
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size( 75, 23 );
      this.btnOk.TabIndex = 11;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler( this.btnOk_Click );
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point( 10, 362 );
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size( 86, 23 );
      this.btnCancel.TabIndex = 10;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // cmbParameter
      // 
      this.cmbParameter.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cmbParameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbParameter.FormattingEnabled = true;
      this.cmbParameter.Location = new System.Drawing.Point( 10, 112 );
      this.cmbParameter.Name = "cmbParameter";
      this.cmbParameter.Size = new System.Drawing.Size( 228, 21 );
      this.cmbParameter.TabIndex = 5;
      // 
      // lblParameter
      // 
      this.lblParameter.AutoSize = true;
      this.lblParameter.Location = new System.Drawing.Point( 7, 95 );
      this.lblParameter.Name = "lblParameter";
      this.lblParameter.Size = new System.Drawing.Size( 91, 13 );
      this.lblParameter.TabIndex = 4;
      this.lblParameter.Text = "Built-in parameter:";
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.toolStripMenuItem1,
            this.displayLogFileToolStripMenuItem} );
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size( 153, 114 );
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size( 152, 22 );
      this.aboutToolStripMenuItem.Text = "About...";
      this.aboutToolStripMenuItem.Click += new System.EventHandler( this.aboutToolStripMenuItem_Click );
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size( 152, 22 );
      this.helpToolStripMenuItem.Text = "Help...";
      this.helpToolStripMenuItem.Click += new System.EventHandler( this.helpToolStripMenuItem_Click );
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size( 152, 22 );
      this.toolStripMenuItem1.Text = "Run Test Suite";
      this.toolStripMenuItem1.Click += new System.EventHandler( this.toolStripMenuItem1_Click );
      // 
      // displayLogFileToolStripMenuItem
      // 
      this.displayLogFileToolStripMenuItem.Name = "displayLogFileToolStripMenuItem";
      this.displayLogFileToolStripMenuItem.Size = new System.Drawing.Size( 152, 22 );
      this.displayLogFileToolStripMenuItem.Text = "Display Log File";
      this.displayLogFileToolStripMenuItem.Click += new System.EventHandler( this.displayLogFileToolStripMenuItem_Click );
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add( this.radioButtonProject );
      this.groupBox2.Controls.Add( this.radioButtonSelection );
      this.groupBox2.Controls.Add( this.radioButtonView );
      this.groupBox2.Location = new System.Drawing.Point( 10, 274 );
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size( 166, 79 );
      this.groupBox2.TabIndex = 7;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Selection";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point( 11, 397 );
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size( 135, 13 );
      this.label3.TabIndex = 12;
      this.label3.Text = "Right click for more options";
      // 
      // SearchForm
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size( 253, 421 );
      this.ContextMenuStrip = this.contextMenuStrip1;
      this.Controls.Add( this.groupBox2 );
      this.Controls.Add( this.label3 );
      this.Controls.Add( this.cmbParameter );
      this.Controls.Add( this.lblParameter );
      this.Controls.Add( this.btnCancel );
      this.Controls.Add( this.btnOk );
      this.Controls.Add( this.groupBox1 );
      this.Controls.Add( this.cmbCategory );
      this.Controls.Add( this.cmbSearchString );
      this.Controls.Add( this.label2 );
      this.Controls.Add( this.label1 );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SearchForm";
      this.Text = "Revit Element Parameter String Search";
      this.Load += new System.EventHandler( this.SearchForm_Load );
      this.groupBox1.ResumeLayout( false );
      this.groupBox1.PerformLayout();
      this.contextMenuStrip1.ResumeLayout( false );
      this.groupBox2.ResumeLayout( false );
      this.groupBox2.PerformLayout();
      this.ResumeLayout( false );
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbSearchString;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cmbCategory;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chkRegex;
    private System.Windows.Forms.CheckBox chkWholeWord;
    private System.Windows.Forms.CheckBox chkMatchCase;
    private System.Windows.Forms.CheckBox chkElementType;
    private System.Windows.Forms.CheckBox chkBuiltInParams;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.ComboBox cmbParameter;
    private System.Windows.Forms.Label lblParameter;
    private System.Windows.Forms.RadioButton radioButtonProject;
    private System.Windows.Forms.RadioButton radioButtonView;
    private System.Windows.Forms.RadioButton radioButtonSelection;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem displayLogFileToolStripMenuItem;
    private System.Windows.Forms.Label label3;
  }
}