namespace StringSearch
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
      this.label1 = new System.Windows.Forms.Label();
      this.cmbSearchString = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmbCategory = new System.Windows.Forms.ComboBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.chkCurrentView = new System.Windows.Forms.CheckBox();
      this.chkNonStringValued = new System.Windows.Forms.CheckBox();
      this.chkStringValued = new System.Windows.Forms.CheckBox();
      this.chkBuiltInParams = new System.Windows.Forms.CheckBox();
      this.chkNonElementType = new System.Windows.Forms.CheckBox();
      this.chkElementType = new System.Windows.Forms.CheckBox();
      this.chkRegex = new System.Windows.Forms.CheckBox();
      this.chkWholeWord = new System.Windows.Forms.CheckBox();
      this.chkMatchCase = new System.Windows.Forms.CheckBox();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.cmdParameter = new System.Windows.Forms.ComboBox();
      this.lblParameter = new System.Windows.Forms.Label();
      this.btnHelp = new System.Windows.Forms.Button();
      this.btnAbout = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
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
      this.cmbSearchString.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cmbSearchString.FormattingEnabled = true;
      this.cmbSearchString.Location = new System.Drawing.Point( 10, 24 );
      this.cmbSearchString.Name = "cmbSearchString";
      this.cmbSearchString.Size = new System.Drawing.Size( 267, 21 );
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
      this.cmbCategory.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbCategory.FormattingEnabled = true;
      this.cmbCategory.Location = new System.Drawing.Point( 10, 66 );
      this.cmbCategory.Name = "cmbCategory";
      this.cmbCategory.Size = new System.Drawing.Size( 267, 21 );
      this.cmbCategory.TabIndex = 3;
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.groupBox1.Controls.Add( this.chkCurrentView );
      this.groupBox1.Controls.Add( this.chkNonStringValued );
      this.groupBox1.Controls.Add( this.chkStringValued );
      this.groupBox1.Controls.Add( this.chkBuiltInParams );
      this.groupBox1.Controls.Add( this.chkNonElementType );
      this.groupBox1.Controls.Add( this.chkElementType );
      this.groupBox1.Controls.Add( this.chkRegex );
      this.groupBox1.Controls.Add( this.chkWholeWord );
      this.groupBox1.Controls.Add( this.chkMatchCase );
      this.groupBox1.Location = new System.Drawing.Point( 10, 144 );
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size( 270, 224 );
      this.groupBox1.TabIndex = 6;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Find options";
      // 
      // chkCurrentView
      // 
      this.chkCurrentView.AutoSize = true;
      this.chkCurrentView.Location = new System.Drawing.Point( 4, 85 );
      this.chkCurrentView.Name = "chkCurrentView";
      this.chkCurrentView.Size = new System.Drawing.Size( 107, 17 );
      this.chkCurrentView.TabIndex = 3;
      this.chkCurrentView.Text = "Current view only";
      this.chkCurrentView.UseVisualStyleBackColor = true;
      // 
      // chkNonStringValued
      // 
      this.chkNonStringValued.AutoSize = true;
      this.chkNonStringValued.Location = new System.Drawing.Point( 4, 200 );
      this.chkNonStringValued.Name = "chkNonStringValued";
      this.chkNonStringValued.Size = new System.Drawing.Size( 164, 17 );
      this.chkNonStringValued.TabIndex = 8;
      this.chkNonStringValued.Text = "Non-string-valued parameters";
      this.chkNonStringValued.UseVisualStyleBackColor = true;
      // 
      // chkStringValued
      // 
      this.chkStringValued.AutoSize = true;
      this.chkStringValued.Checked = true;
      this.chkStringValued.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkStringValued.Location = new System.Drawing.Point( 4, 177 );
      this.chkStringValued.Name = "chkStringValued";
      this.chkStringValued.Size = new System.Drawing.Size( 143, 17 );
      this.chkStringValued.TabIndex = 7;
      this.chkStringValued.Text = "String-valued parameters";
      this.chkStringValued.UseVisualStyleBackColor = true;
      // 
      // chkBuiltInParams
      // 
      this.chkBuiltInParams.AutoSize = true;
      this.chkBuiltInParams.Location = new System.Drawing.Point( 4, 154 );
      this.chkBuiltInParams.Name = "chkBuiltInParams";
      this.chkBuiltInParams.Size = new System.Drawing.Size( 148, 17 );
      this.chkBuiltInParams.TabIndex = 6;
      this.chkBuiltInParams.Text = "Search built-in parameters";
      this.chkBuiltInParams.UseVisualStyleBackColor = true;
      this.chkBuiltInParams.CheckedChanged += new System.EventHandler( this.chkBuiltInParams_CheckedChanged );
      // 
      // chkNonElementType
      // 
      this.chkNonElementType.AutoSize = true;
      this.chkNonElementType.Checked = true;
      this.chkNonElementType.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkNonElementType.Location = new System.Drawing.Point( 4, 131 );
      this.chkNonElementType.Name = "chkNonElementType";
      this.chkNonElementType.Size = new System.Drawing.Size( 151, 17 );
      this.chkNonElementType.TabIndex = 5;
      this.chkNonElementType.Text = "Search non-ElementTypes";
      this.chkNonElementType.UseVisualStyleBackColor = true;
      // 
      // chkElementType
      // 
      this.chkElementType.AutoSize = true;
      this.chkElementType.Location = new System.Drawing.Point( 4, 108 );
      this.chkElementType.Name = "chkElementType";
      this.chkElementType.Size = new System.Drawing.Size( 130, 17 );
      this.chkElementType.TabIndex = 4;
      this.chkElementType.Text = "Search ElementTypes";
      this.chkElementType.UseVisualStyleBackColor = true;
      // 
      // chkRegex
      // 
      this.chkRegex.AutoSize = true;
      this.chkRegex.Location = new System.Drawing.Point( 4, 62 );
      this.chkRegex.Name = "chkRegex";
      this.chkRegex.Size = new System.Drawing.Size( 138, 17 );
      this.chkRegex.TabIndex = 2;
      this.chkRegex.Text = "Use regular expressions";
      this.chkRegex.UseVisualStyleBackColor = true;
      // 
      // chkWholeWord
      // 
      this.chkWholeWord.AutoSize = true;
      this.chkWholeWord.Location = new System.Drawing.Point( 4, 39 );
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
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point( 203, 380 );
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size( 75, 23 );
      this.btnOk.TabIndex = 10;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler( this.btnOk_Click );
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point( 138, 380 );
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size( 54, 23 );
      this.btnCancel.TabIndex = 9;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // cmdParameter
      // 
      this.cmdParameter.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cmdParameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmdParameter.FormattingEnabled = true;
      this.cmdParameter.Location = new System.Drawing.Point( 10, 112 );
      this.cmdParameter.Name = "cmdParameter";
      this.cmdParameter.Size = new System.Drawing.Size( 267, 21 );
      this.cmdParameter.TabIndex = 5;
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
      // btnHelp
      // 
      this.btnHelp.Location = new System.Drawing.Point( 74, 380 );
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size( 53, 23 );
      this.btnHelp.TabIndex = 8;
      this.btnHelp.Text = "&Help";
      this.btnHelp.UseVisualStyleBackColor = true;
      // 
      // btnAbout
      // 
      this.btnAbout.Location = new System.Drawing.Point( 10, 380 );
      this.btnAbout.Name = "btnAbout";
      this.btnAbout.Size = new System.Drawing.Size( 53, 23 );
      this.btnAbout.TabIndex = 7;
      this.btnAbout.Text = "&About";
      this.btnAbout.UseVisualStyleBackColor = true;
      this.btnAbout.Click += new System.EventHandler( this.btnAbout_Click );
      // 
      // SearchForm
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size( 292, 414 );
      this.Controls.Add( this.btnAbout );
      this.Controls.Add( this.btnHelp );
      this.Controls.Add( this.cmdParameter );
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
      this.Text = "SearchForm";
      this.groupBox1.ResumeLayout( false );
      this.groupBox1.PerformLayout();
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
    private System.Windows.Forms.CheckBox chkNonElementType;
    private System.Windows.Forms.CheckBox chkBuiltInParams;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.CheckBox chkNonStringValued;
    private System.Windows.Forms.CheckBox chkStringValued;
    private System.Windows.Forms.CheckBox chkCurrentView;
    private System.Windows.Forms.ComboBox cmdParameter;
    private System.Windows.Forms.Label lblParameter;
    private System.Windows.Forms.Button btnHelp;
    private System.Windows.Forms.Button btnAbout;
  }
}