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
using Application = Autodesk.Revit.ApplicationServices.Application;
#endregion // Namespaces

namespace StringSearch
{
  [Transaction( TransactionMode.ReadOnly )]
  [Regeneration( RegenerationOption.Manual )]
  public class Command : IExternalCommand
  {
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

      // display search form:

      SearchForm form = new SearchForm();

      DialogResult r = form.ShowDialog();

      if( DialogResult.Cancel == r )
      {
        return Result.Cancelled;
      }

      // run filtered element collector:

      #region Set up filtered element collector
      FilteredElementCollector a = form.CurrentView
        ? new FilteredElementCollector( doc, doc.ActiveView.Id )
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
        message = "Please select ElementType, NonElementType, or both.";
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

      SortableBindingList<SearchHit> data = ss.Run();

      // display log file:

      Process.Start( ss.LogfilePath );

      // display data in modeless form and ensure
      // that the form remains on top of Revit:

      SearchHitNavigator navigator
        = new SearchHitNavigator(
          data,
          new SetElementId( SetPendingElementId ) );

      navigator.Show( _hWndRevit );

      // subscribe to Idling event:

      uiapp.Idling += new EventHandler<IdlingEventArgs>(
        OnIdling );

      return Result.Succeeded;
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
        Application app
          = sender as Application;

        UIApplication uiapp
          = new UIApplication( app );

        UIDocument uidoc
          = uiapp.ActiveUIDocument;

        Document doc
          = uidoc.Document;

        ElementId eid = new ElementId( id );
        Element e = doc.get_Element( eid );

        Debug.Print(
          "Element id {0} requested --> {1}",
          id, new ElementData( e, doc ) );

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
