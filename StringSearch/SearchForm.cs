#region Copyright
// (C) Copyright 2011 by Autodesk, Inc. 
//
// Permission to use, copy, modify, and distribute this software
// in object code form for any purpose and without fee is hereby
// granted, provided that the above copyright notice appears in
// all copies and that both that copyright notice and the limited
// warranty and restricted rights notice below appear in all
// supporting documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK,
// INC. DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL
// BE UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is
// subject to restrictions set forth in FAR 52.227-19 (Commercial
// Computer Software - Restricted Rights) and DFAR 252.227-7013(c)
// (1)(ii)(Rights in Technical Data and Computer Software), as
// applicable.
#endregion // Copyright

#region Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
#endregion // Namespaces

namespace ADNPlugin.Revit.StringSearch
{
  public partial class SearchForm : Form
  {
    const string _all = "*";
    const string _bic_prefix = "OST_";
    const string _bic_invalid = "INVALID";
    const string _bip_invalid = "INVALID";
    string _log_path;
    bool _ok_clicked;

    static List<string> _previous_search_strings 
      = new List<string>();

    static List<string> _previous_parameter_names
      = new List<string>();

    public SearchForm( string log_path )
    {
      _log_path = log_path;
      _ok_clicked = false;
      InitializeComponent();
      PopulateBuiltInCategories();
      //PopulateBuiltInParameters();
      PopulateParameterNames();
      cmbSearchString.DataSource = _previous_search_strings;
      //this.Validating += new CancelEventHandler( SearchForm_Validating );
      this.FormClosing += new FormClosingEventHandler( SearchForm_FormClosing );
    }

    void SearchForm_FormClosing( object sender, FormClosingEventArgs e )
    {
      if( _ok_clicked )
      {
        if( 0 == cmbSearchString.Text.Length )
        {
          e.Cancel = true;
          _ok_clicked = false;
          MessageBox.Show(
            "Please specify a search string.",
            AboutBox.AssemblyProduct );
        }
        //else if( !ElementType && !NonElementType )
        //{
        //  e.Cancel = true;
        //  _ok_clicked = false;
        //  MessageBox.Show(
        //    "Please select at least one of element type and non-element type.",
        //    AboutBox.AssemblyProduct );
        //}
        else if( ElementType && (CurrentSelection || CurrentView) )
        {
          e.Cancel = true;
          _ok_clicked = false;
          MessageBox.Show(
            "Element types can only be searched across the entire project, not when specifying the current selection or current view.",
            AboutBox.AssemblyProduct );
        }
        //else if( !chkStringValued.Checked && !chkNonStringValued.Checked )
        //{
        //  e.Cancel = true;
        //  MessageBox.Show(
        //    "Please select at least one of string-valued and non-string-valued parameters.",
        //    AboutBox.AssemblyProduct );
        //}
      }
    }

    void PopulateBuiltInCategories()
    {
      Type t = typeof( Autodesk.Revit.DB.BuiltInCategory );

      string[] names = Enum.GetNames( t );

      List<string> bics = new List<string>( names.Length );

      foreach( string s in names )
      {
        Debug.Assert( s.Equals( _bic_invalid ) 
          || s.Substring( 0, 4 ).Equals( _bic_prefix ),
          "Expected all BuiltInCategory enum names to start with OST_" );

        bics.Add( s.Equals( _bic_invalid )
          ? _all
          : s.Substring( 4 ) );
      }

      bics.Sort();

      Debug.Assert( names.Length == bics.Count,
        "Expected all BuiltInCategory enum names to be added" );

      cmbCategory.DataSource = bics;
    }

    void PopulateBuiltInParameters()
    {
      lblParameter.Text = "Built-in parameter or * for all (slow):";

      Type t = typeof( Autodesk.Revit.DB.BuiltInParameter );

      string[] names = Enum.GetNames( t );

      List<string> bips = new List<string>( names.Length );

      foreach( string s in names )
      {
        bips.Add( _bip_invalid.Equals( s )
          ? _all
          : s );
      }

      bips.Sort();

      Debug.Assert( names.Length == bips.Count,
        "Expected all BuiltInCategory enum names to be added" );

      cmbParameter.DropDownStyle = ComboBoxStyle.DropDownList;
      cmbParameter.DataSource = bips;
    }

    void PopulateParameterNames()
    {
      lblParameter.Text = "Parameter name or * for all:";
      cmbParameter.DropDownStyle = ComboBoxStyle.DropDown;
      cmbParameter.DataSource = _previous_parameter_names;
      cmbParameter.Text = _all;
    }

    public string CategoryName
    {
      get
      {
        string s = cmbCategory.Text;
        return _all == s ? s : _bic_prefix + s;
      }
    }

    public string ParameterName
    {
      get
      {
        string s = cmbParameter.Text;
        return _all == s ? null : s;
      }
    }

    public bool AllCategories
    {
      get { return cmbCategory.Text.Equals( _all ); }
    }

    public SearchOptions SearchOptions
    {
      get 
      { 
        return new SearchOptions(
          cmbSearchString.Text,
          ParameterName,
          chkMatchCase.Checked,
          chkWholeWord.Checked,
          chkRegex.Checked,
          //chkElementType.Checked,
          //chkNonElementType.Checked,
          chkBuiltInParams.Checked
          //chkStringValued.Checked,
          //chkNonStringValued.Checked 
        );
      }
    }

    public string SearchString
    {
      get { return cmbSearchString.Text; }
    }

    public bool CurrentSelection
    {
      get { return radioButtonSelection.Checked; }
    }

    public bool CurrentView
    {
      //get { return chkCurrentView.Checked; }
      get { return radioButtonView.Checked; }
    }

    public bool ElementType
    {
      //get { return chkElementType.Checked; }
      get { return chkElementType.Checked; }
    }

    public bool NonElementType
    {
      get { return !chkElementType.Checked; }
    }

#if NEED_INDIVIDUAL_FIELDS
    public bool MatchCase
    {
      get { return chkMatchCase.Checked; }
    }

    public bool WholeWord
    {
      get { return chkWholeWord.Checked; }
    }

    public bool Regex
    {
      get { return chkRegex.Checked; }
    }

    public bool BuiltInParams
    {
      get { return chkBuiltInParams.Checked; }
    }
#endif // NEED_INDIVIDUAL_FIELDS

    private void btnOk_Click( object sender, EventArgs e )
    {
      if( 0 < SearchString.Length
        && !_previous_search_strings.Contains( SearchString ) )
      {
        _previous_search_strings.Add( SearchString );
      }
      if( !chkBuiltInParams.Checked
        && 0 < cmbParameter.Text.Length
        && !cmbParameter.Text.Equals( _all ) )
      {
        _previous_parameter_names.Add( cmbParameter.Text );
      }
      _ok_clicked = true;
    }

    private void btnAbout_Click( object sender, EventArgs e )
    {
      AboutBox a = new AboutBox();
      DialogResult r = a.ShowDialog();
    }

    private void chkBuiltInParams_CheckedChanged( 
      object sender, 
      EventArgs e )
    {
      if( chkBuiltInParams.Checked )
      {
        PopulateBuiltInParameters();
      }
      else
      {
        PopulateParameterNames();
      }
    }

    private void chkNonElementType_CheckedChanged( 
      object sender, 
      EventArgs e )
    {
      radioButtonSelection.Checked = false;
      radioButtonView.Checked = false;
      radioButtonProject.Checked = true;
    }

    private void btnHelp_Click( 
      object sender, 
      EventArgs e )
    {
      MessageBox.Show( AboutBox.AssemblyDescription
        + "\r\n\r\nHelp still under construction.",
        AboutBox.AssemblyProduct );
    }

    private void aboutToolStripMenuItem_Click( object sender, EventArgs e )
    {
      AboutBox a = new AboutBox();
      DialogResult r = a.ShowDialog();
    }

    private void helpToolStripMenuItem_Click( object sender, EventArgs e )
    {
      MessageBox.Show( AboutBox.AssemblyDescription
        + "\r\n\r\nHelp still under construction.",
        AboutBox.AssemblyProduct );
    }

    private void toolStripMenuItem1_Click( object sender, EventArgs e )
    {
      MessageBox.Show( 
        "The test suite is still under construction.",
        AboutBox.AssemblyProduct );
    }

    private void displayLogFileToolStripMenuItem_Click( object sender, EventArgs e )
    {
      Process.Start( _log_path );
    }

    private void SearchForm_Load( object sender, EventArgs e )
    {

    }

    //private void toolStripMenuItem1_Click( object sender, EventArgs e )
    //{
    //}
  }
}
