using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StringSearch
{
  public partial class SearchForm : Form
  {
    const string _all = "*";
    const string _bic_prefix = "OST_";
    const string _bic_invalid = "INVALID";
    const string _bip_invalid = "INVALID";

    static List<string> _previous_search_strings 
      = new List<string>();

    static List<string> _previous_parameter_names
      = new List<string>();

    public SearchForm()
    {
      InitializeComponent();
      PopulateBuiltInCategories();
      //PopulateBuiltInParameters();
      PopulateParameterNames();
      cmbSearchString.DataSource = _previous_search_strings;
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
      lblParameter.Text = "Built-in parameter:";

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

      cmdParameter.DropDownStyle = ComboBoxStyle.DropDownList;
      cmdParameter.DataSource = bips;
    }

    void PopulateParameterNames()
    {
      lblParameter.Text = "Parameter name:";
      cmdParameter.DropDownStyle = ComboBoxStyle.DropDown;
      cmdParameter.DataSource = _previous_parameter_names;
      cmdParameter.Text = _all;
    }

    public string CategoryName
    {
      get
      {
        string s = cmbCategory.Text;
        return _all == s ? s : _bic_prefix + s;
      }
    }

    public string BuiltInParameterName
    {
      get
      {
        string s = cmdParameter.Text;
        return s;
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
          chkMatchCase.Checked,
          chkWholeWord.Checked,
          chkRegex.Checked,
          //chkElementType.Checked,
          //chkNonElementType.Checked,
          chkBuiltInParams.Checked,
          chkStringValued.Checked,
          chkNonStringValued.Checked );
      }
    }

    public string SearchString
    {
      get { return cmbSearchString.Text; }
    }

    public bool CurrentView
    {
      get { return chkCurrentView.Checked; }
    }

    public bool ElementType
    {
      get { return chkElementType.Checked; }
    }

    public bool NonElementType
    {
      get { return chkNonElementType.Checked; }
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
        && 0 < cmdParameter.Text.Length
        && !cmdParameter.Text.Equals( _all ) )
      {
        _previous_parameter_names.Add( cmdParameter.Text );
      }
    }

    private void btnAbout_Click( object sender, EventArgs e )
    {
      AboutBox a = new AboutBox();
      DialogResult r = a.ShowDialog();
    }

    private void chkBuiltInParams_CheckedChanged( object sender, EventArgs e )
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
  }
}
