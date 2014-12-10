#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Autodesk.Revit.DB;
#endregion // Namespaces

namespace StringSearch
{
  /// <summary>
  /// Search for a string in the elements in the 
  /// collector using a given set of search options.
  /// </summary>
  class StringSearcher
  {
    FilteredElementCollector _collector;
    SearchOptions _searchOptions;
    string _logfile_path;

    static Array _bips = Enum.GetValues( 
      typeof( BuiltInParameter ) );

    static string EscapeRegexChars( string s )
    {
      return s;
    }

    void Search( Parameter p )
    {
    }

    public StringSearcher(
      FilteredElementCollector collector,
      SearchOptions searchOptions )
    {
      _collector = collector;
      _searchOptions = searchOptions;
    }

    public SortableBindingList<SearchHit> Run()
    {
      string searchText = _searchOptions.SearchString;
      
      RegexOptions opt = RegexOptions.Compiled;

      if( !_searchOptions.Regex ) { searchText = EscapeRegexChars( searchText ); }
      if( !_searchOptions.MatchCase ) { opt |= RegexOptions.IgnoreCase; }
      if( _searchOptions.WholeWord ) { searchText = "\\W" + searchText + "\\W"; }

      Regex regex = new Regex( searchText, opt );

      SortableBindingList<SearchHit> data
        = new SortableBindingList<SearchHit>();

      using( JtLogFile log
        = new JtLogFile( "SearchString" ) )
      {
        int nElementsSearched = 0;
        int nParametersSearched = 0;
        int nSearchHitElements = 0;
        int nSearchHitParameters = 0;

        foreach( Element e in _collector )
        {
          if( _searchOptions.BuiltInParams )
          {
            foreach( BuiltInParameter a in _bips )
            {
              try
              {
                Parameter p = e.get_Parameter( a );

                if( null != p )
                {
                  //string valueString = ( StorageType.ElementId == p.StorageType )
                  //  ? LabUtils.GetParameterValue2( p, doc )
                  //  : p.AsValueString();

                  //data.Add( new ParameterData( a, p, valueString ) );
                }
              }
              catch( Exception ex )
              {
                //Debug.Print( "Exception retrieving built-in parameter {0} on {1}: {1}",
                //  a, ElementDescription( e ), ex );
              }
            }
          }
          else
          {
            ParameterSet pset = e.Parameters;

            foreach( Parameter p in pset )
            {
              Search( p );
            }
          }
          ++nElementsSearched;
        }
        _logfile_path = log.Path;
      }
      return data;
    }

    public string LogfilePath
    {
      get { return _logfile_path; }
    }
  }
}
