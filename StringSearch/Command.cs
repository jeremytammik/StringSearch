#region Copyright
// (C) Copyright 2011-2013 by Autodesk, Inc. 
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
using System.Windows.Forms;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Application = Autodesk.Revit.ApplicationServices.Application;
#endregion

namespace ADNPlugin.Revit.StringSearch
{
  /// <summary>
  /// String search external command implementation 
  /// compatible with both Revit 2011 and 2012 API.
  /// </summary>
  [Regeneration( RegenerationOption.Manual )] // 2011
  [Transaction( TransactionMode.ReadOnly )]
  public class Command : IExternalCommand
  {
    /// <summary>
    /// Display an informational message.
    /// </summary>
    static public void InfoMsg( string msg )
    {
      TaskDialog.Show( 
        AboutBox.AssemblyProduct, 
        msg );
    }

    #region Execute
    /// <summary>
    /// The Revit 2012 API provides the new method
    /// Autodesk.Revit.UI.Selection.GetElementIds
    /// returning an ICollection of ElementId. In 
    /// Revit 2011, we implement it ourselves for 
    /// backwards compatibility.
    /// </summary>
    ICollection<ElementId> GetSelectedElementIds( 
      UIDocument uidoc )
    {
      SelElementSet ss = uidoc.Selection.Elements;

      List<ElementId> ids = new List<ElementId>( 
        ss.Size );

      foreach( Element e in ss )
      {
        ids.Add( e.Id );
      }
      return ids;
    }

    /// <summary>
    /// External command mainline.
    /// Determine Revit application window handle.
    /// Prompt user for search string and options.
    /// Retrieve all requested elements and search their parameters.
    /// List the result in a log file and a data container.
    /// </summary>
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements )
    {
      UIApplication uiapp = commandData.Application;
      UIDocument uidoc = uiapp.ActiveUIDocument;
      Application app = uiapp.Application;
      Document doc = uidoc.Document;

      ICollection<ElementId> selids = GetSelectedElementIds( uidoc );

      try
      {
        using( JtLogFile log = new JtLogFile( "SearchString" ) )
        {
          SortableBindingList<SearchHit> data = null;

          while( null == data || 0 == data.Count )
          {
            // Display search form:

            SearchForm form = new SearchForm( log.Path );

            DialogResult r = form.ShowDialog();

            if( DialogResult.Cancel == r )
            {
              message = string.Empty;
              return Result.Cancelled;
            }

            if( form.CurrentSelection && 0 == selids.Count )
            {
              InfoMsg( "Sorry; you cannot search the current element selection, because it is empty." );
              continue;
            }

            // Run filtered element collector:

            #region Set up filtered element collector
            FilteredElementCollector a
              = form.CurrentView 
                ? new FilteredElementCollector( 
                  doc, doc.ActiveView.Id )
              : form.CurrentSelection 
                ? new FilteredElementCollector( 
                    doc, GetSelectedElementIds( uidoc ) )
              : new FilteredElementCollector( doc );

            if( form.ElementType && form.NonElementType )
            {
              a.WhereElementIsElementType();

              FilteredElementCollector b = form.CurrentView
                ? new FilteredElementCollector( doc, doc.ActiveView.Id )
                : new FilteredElementCollector( doc );

              b.WhereElementIsNotElementType();

              a.UnionWith( b );
            }
            else if( form.ElementType )
            {
              a.WhereElementIsElementType();
            }
            else if( form.NonElementType )
            {
              a.WhereElementIsNotElementType();
            }
            else
            {
              message = "Please select at least one or both of Element type and non-Element type.";
              return Result.Failed;
            }

            if( !form.AllCategories )
            {
              BuiltInCategory bic
                = ( BuiltInCategory ) Enum.Parse(
                  typeof( BuiltInCategory ),
                  form.CategoryName );

              a.OfCategory( bic );
            }
            #endregion // Set up filtered element collector

            // Search element parameter data:

            StringSearcher ss = new StringSearcher(
              a, form.SearchOptions );

            try
            {
              data = ss.Run( log, out message );

              if( 0 == data.Count )
              {
                InfoMsg( 0 < message.Length ? message : 
                  "No occurrences found." );
              }
            }
            catch( ArgumentException ex )
            {
              if( ex.StackTrace.Contains( 
                "RegularExpressions.RegexParser.ScanRegex" ) )
              {
                InfoMsg( "Invalid regular expression. Error message:\r\n"
                  + ex.Message
                  + "\r\nIf you don't know what a regular expression is, don't use it"
                  + "\r\n(cheat sheet: http://regexlib.com/cheatsheet.aspx)." );
              }
              else
              {
                throw ex;
              }
            }
          }

          App.ShowForm( data );

          return Result.Succeeded;
        }
      }
      catch( Exception ex )
      {
        message = ex.Message;
        return Result.Failed;
      }
    }
    #endregion // Execute
  }
}
