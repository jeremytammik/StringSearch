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
using System.Diagnostics;
using System.Text.RegularExpressions;
using Autodesk.Revit.DB;
#endregion // Namespaces

namespace ADNPlugin.Revit.StringSearch
{
  /// <summary>
  /// Search for a string in the elements in the 
  /// collector using a given set of search options.
  /// </summary>
  class StringSearcher
  {
    class Counters
    {
      public Counters()
      {
        ElementsSearched = 0;
        ElementsHit = 0;
        ParametersSearched = 0;
        ParametersHit = 0;
        Hits = 0;
      }
      public int ElementsSearched { get; set; }
      public int ElementsHit { get; set; }
      public int ParametersSearched { get; set; }
      public int ParametersHit { get; set; }
      public int Hits { get; set; }
    }

    /// <summary>
    /// Return an English plural suffix 's' or
    /// nothing for the given number of items.
    /// </summary>
    public static string PluralSuffix( int n )
    {
      return 1 == n ? "" : "s";
    }

    FilteredElementCollector _collector;
    SearchOptions _searchOptions;
    //string _logfile_path;
    Counters _counters;

    static Array _bips = Enum.GetValues( 
      typeof( BuiltInParameter ) );

    static string EscapeRegexChars( string s )
    {
      return s;
    }

    static string GetParameterString( 
      Element e, 
      Parameter p )
    {
      string s = null;

      switch( p.StorageType )
      {
        case StorageType.Integer:
          s = p.AsInteger().ToString();
          //Debug.Assert( s.Equals( p.AsValueString() ), 
          //  "expected integer representation to match value string" );
          break;
        case StorageType.Double:
          s = p.AsDouble().ToString();
          //Debug.Assert( s.Equals( p.AsValueString() ), 
          //  "expected double representation to match value string" );
          break;
        case StorageType.String:
          s = p.AsString();
          //Debug.Assert( s.Equals( p.AsValueString() ), 
          //  "expected string raw value to match value string" );
          break;
        case StorageType.ElementId:
          s = p.AsElementId().IntegerValue.ToString();
          //Debug.Assert( s.Equals( p.AsValueString() ), 
          //  "expected element id representation to match value string" );
          break;
      }

      //Debug.Assert( null != s,
      //  "expected as valid parameter value string" );

      return s;
    }

    /// <summary>
    /// Return number of occurrences of search string in parameter value.
    /// </summary>
    bool SearchParameter( 
      SortableBindingList<SearchHit> data,
      Element e, 
      Parameter p,
      string bipName,
      Regex regex )
    {
      if( null == p )
      {
        return false;
      }

      if( StorageType.String != p.StorageType )
      {
        return false;
      }

      string s = GetParameterString( e, p );

      if( null == s || 0 == s.Length )
      {
        return false;
      }

      bool foundOnParam = false;

      MatchCollection matches = regex.Matches( s );

      foreach( Match m in matches )
      {
        ++_counters.Hits;
        foundOnParam = true;

        Definition def = p.Definition;

        InternalDefinition idef = def as InternalDefinition;

        string bipName2 = ( null == idef ) ? null 
          : idef.BuiltInParameter.ToString();

        if( null == bipName )
        {
          bipName = bipName2;
        }
        else
        {
          Debug.Assert( bipName.Equals( bipName2 ), 
            "expected equal built-in parameter name" );
        }
        
        SearchHit hit = new SearchHit(
          e, bipName, def.Name, s, m.Index );

        data.Add( hit );
      }

      ++_counters.ParametersSearched;

      if( foundOnParam )
      {
        ++_counters.ParametersHit;
      }
      return foundOnParam;
    }

    public StringSearcher(
      FilteredElementCollector collector,
      SearchOptions searchOptions )
    {
      _collector = collector;
      _searchOptions = searchOptions;
      _counters = new Counters();
    }

    public SortableBindingList<SearchHit> Run(
      JtLogFile log )
    {
      Parameter p;

      // do we search for some specific parameter?

      Definition def = null;

      string parameterName = _searchOptions.ParameterName;

      if( null != parameterName )
      {
        foreach( Element e in _collector )
        {
          if( _searchOptions.BuiltInParams )
          {
            BuiltInParameter bip = (BuiltInParameter) Enum.Parse( 
              typeof( BuiltInParameter ), parameterName, true );

            p = e.get_Parameter( bip );
          }
          else
          {
            p = e.get_Parameter( parameterName );
          }
          if( null != p )
          {
            def = p.Definition;
            break;
          }
        }
        if( null == def )
        {
          throw new Exception( string.Format( 
            "None of the selected elements have any parameter '{0}'",
            parameterName ) );
        }
      }

      // set up regular expression for search string

      string searchText = _searchOptions.SearchString;
      
      RegexOptions opt = RegexOptions.Compiled;

      if( !_searchOptions.Regex ) { searchText = Regex.Escape( searchText ); }
      if( !_searchOptions.MatchCase ) { opt |= RegexOptions.IgnoreCase; }
      if( _searchOptions.WholeWord ) { searchText = "\\W" + searchText + "\\W"; }

      Regex regex = new Regex( searchText, opt );

      // start searching

      SortableBindingList<SearchHit> data
        = new SortableBindingList<SearchHit>();

      foreach( Element e in _collector )
      {
        log.Log( "Document: " + e.Document.Title );
        break;
      }
      log.Log( "Search string: " + _searchOptions.SearchString );
      log.Log( "Parameter: " + _searchOptions.ParameterName );
      log.Log( "Match case: " + _searchOptions.MatchCase );
      log.Log( "Whole word: " + _searchOptions.WholeWord );
      log.Log( "Regular expression: " + _searchOptions.Regex );
      log.Log( "Built-in parameters: " + _searchOptions.BuiltInParams );
      //log.Log( "String-valued: " + _searchOptions.StringValued );
      //log.Log( "Non-string-valued: " + _searchOptions.NonStringValued );

      bool foundOnElement;

      foreach( Element e in _collector )
      {
        foundOnElement = false;

        if( null != def )
        {
          p = e.get_Parameter( def );
          foundOnElement = SearchParameter( data, e, p, parameterName, regex );
        }
        else if( _searchOptions.BuiltInParams )
        {
          foreach( BuiltInParameter a in _bips )
          {
            p = e.get_Parameter( a );
            foundOnElement |= SearchParameter( data, e, p, a.ToString(), regex );
          }
        }
        else
        {
          ParameterSet pset = e.Parameters;

          foreach( Parameter q in pset )
          {
            foundOnElement |= SearchParameter( data, e, q, null, regex );
          }
        }
        ++_counters.ElementsSearched;

        if( foundOnElement )
        {
          ++_counters.ElementsHit;
        }
      }
      log.Log( string.Format( 
        "{0} element{1} and {2} parameter{3} searched.", 
        _counters.ElementsSearched, 
        PluralSuffix( _counters.ElementsSearched ), 
        _counters.ParametersSearched, 
        PluralSuffix( _counters.ParametersSearched ) ) );

      log.Log( string.Format( 
        "{0} total hit{1} found on {2} element{3} and {4} parameter{5}{6}", 
        _counters.Hits, 
        PluralSuffix( _counters.Hits ),
        _counters.ElementsHit, 
        PluralSuffix( _counters.ElementsHit ), 
        _counters.ParametersHit, 
        PluralSuffix( _counters.ParametersHit ),
        0 == _counters.Hits ? "." : ":" ) );

      foreach( SearchHit h in data )
      {
        log.Log( h.ToString() );
      }

      //_logfile_path = log.Path;

      return data;
    }

    //public string LogfilePath
    //{
    //  get { return _logfile_path; }
    //}
  }
}
