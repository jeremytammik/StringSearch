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
using System.Windows.Forms;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
using Application = Autodesk.Revit.ApplicationServices.Application;
#endregion

namespace ADNPlugin.Revit.StringSearch
{
  [Transaction( TransactionMode.ReadOnly )]
  public class Command : IExternalCommand
  {
    static JtLogFile _log = null;
    static SearchHitNavigator _navigator = null;
    //static Process _logFileDisplay = null;

    #region Revit window handle
    /// <summary>
    /// Revit application window handle, used
    /// as parent for modeless dialogue.
    /// </summary>
    static JtWindowHandle _hWndRevit = null;
    #endregion // Revit window handle

    #region Pending element id
    /// <summary>
    /// Pending element id, element to zoom to
    /// next time the Idling event fires.
    /// </summary>
    static int _pending_element_id;

    /// <summary>
    /// Set a pending element id to zoom to
    /// when the Idling event fires.
    /// </summary>
    static void SetPendingElementId( int id )
    {
      _pending_element_id = id;
    }

    /// <summary>
    /// Delegate to set a pending 
    /// element id to zoom to.
    /// </summary>
    public delegate void SetElementId( int id );
    #endregion // Pending element id

    #region Execute
    /// <summary>
    /// External command mainline.
    /// Determine Revit application window handle.
    /// Prompt user for search string and options.
    /// Retrieve all requested elements and search their parameters.
    /// List the result in a log file and a data container.
    /// Display the log file.
    /// Start a modeless dialogue with entries for all search hits.
    /// Pass in the Revit application window handle and a delegate method
    /// allowing the dialogue to pass back a pending element id, which will
    /// be an element that the user wishes to zoom to.
    /// Subscribe to the Idling event.
    /// The modeless dialogue passes back the pending element id.
    /// In the event handler, the pending element id is picked up
    /// and displayed to the user on the Revit graphics screen.
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

      try
      {
        if( null == _log )
        {
          _log = new JtLogFile( "SearchString" );
        }

        // display search form:

        SearchForm form = new SearchForm( _log.Path );

        DialogResult r = form.ShowDialog();

        if( DialogResult.Cancel == r )
        {
          return Result.Cancelled;
        }

        // run filtered element collector:

        #region Set up filtered element collector
        FilteredElementCollector a 
          = form.CurrentView ? new FilteredElementCollector( doc, doc.ActiveView.Id )
          : form.CurrentSelection ? new FilteredElementCollector( doc, uidoc.Selection.GetElementIds() )
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
            = (BuiltInCategory) Enum.Parse(
              typeof( BuiltInCategory ),
              form.CategoryName );

          a.OfCategory( bic );
        }
        #endregion // Set up filtered element collector

        // search element parameter data:

        StringSearcher ss = new StringSearcher(
          a, form.SearchOptions );

        SortableBindingList<SearchHit> data = ss.Run( _log );

        if( 0 == data.Count )
        {
          MessageBox.Show( "No occurrences found.", 
            AboutBox.AssemblyProduct );

          return Result.Succeeded;
        }

        if( null == _navigator )
        {
          // first time around, subscribe to Idling event

          uiapp.Idling += new EventHandler<IdlingEventArgs>(
            OnIdling );

          // display data in modeless form and ensure
          // that the form remains on top of Revit:

          _navigator = new SearchHitNavigator(
            new SetElementId( SetPendingElementId ) );

          _navigator.Disposed += new EventHandler(
            _navigator_Disposed );
        }

        _navigator.SetData( data );

        if( !_navigator.Visible )
        {
          _navigator.Show( _hWndRevit );
        }
        return Result.Succeeded;
      }
      catch( Exception ex )
      {
        message = ex.Message;
        return Result.Failed;
      }
    }

    void _navigator_Disposed( object sender, EventArgs e )
    {
      _navigator = null;
    }
    #endregion // Execute

    #region OnIdling
    /// <summary>
    /// Revit Idling event handler.
    /// Whenever the user has selected an element to 
    /// zoom to in the modeless dialogue, the pending
    /// element id is set. The event handler picks it
    /// up and zooms to it. We are not modifying the
    /// Revit document, so it seems we can get away 
    /// with not starting a transaction.
    /// </summary>
    void OnIdling(
      object sender,
      IdlingEventArgs ea )
    {
      int id = _pending_element_id;

      if( 0 != id )
      {
        UIApplication uiapp
          = sender as UIApplication;

        UIDocument uidoc
          = uiapp.ActiveUIDocument;

        Document doc
          = uidoc.Document;

        ElementId eid = new ElementId( id );
        Element e = doc.get_Element( eid );

        Debug.Print(
          "Element id {0} requested --> {1}",
          id, new ElementData( e ) );

        // look, mom, no transaction required!

        uidoc.Selection.Elements.Clear();
        uidoc.Selection.Elements.Add( e );
        uidoc.ShowElements( e );

        _pending_element_id = 0;
      }
    }
    #endregion // OnIdling
  }
}
