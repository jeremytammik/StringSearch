#region Copyright
// (C) Copyright 2011-2014 by Autodesk, Inc. 
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
using System.IO;
using System.Windows.Media.Imaging;
using System.Reflection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
#endregion

namespace ADNPlugin.Revit.StringSearch
{
  /// <summary>
  /// String search external application implementation 
  /// compatible with both Revit 2011 and 2012 API.
  /// </summary>
  [Regeneration( RegenerationOption.Manual )] // 2011
  class App : IExternalApplication
  {
    const string _name = "StringSearch";

    const string _tooltip_long_description
      = "Search for a given string within element parameter values."
      + "\r\n\r\nSpecify the target string to search for and various options to select the elements and parameters to examine."
      + "\r\n\r\nFor more information, right click on the search form and select 'Help'.";

    static string _text = AboutBox.AssemblyProduct;

    static string _namespace_prefix 
      = typeof( App ).Namespace + ".";

    #region Revit window handle
    /// <summary>
    /// Revit application window handle, used
    /// as parent for modeless dialogue.
    /// </summary>
    static JtWindowHandle _hWndRevit = null;
    #endregion // Revit window handle

    #region Load bitmap from embedded resources
    /// <summary>
    /// Load a new icon bitmap from embedded resources.
    /// For the BitmapImage, make sure you reference 
    /// WindowsBase and PresentationCore, and import 
    /// the System.Windows.Media.Imaging namespace. 
    /// </summary>
    BitmapImage NewBitmapImage( 
      Assembly a, 
      string imageName )
    {
      Stream s = a.GetManifestResourceStream(
         _namespace_prefix + imageName );

      BitmapImage img = new BitmapImage();

      img.BeginInit();
      img.StreamSource = s;
      img.EndInit();

      return img;
    }
    #endregion // Load bitmap from embedded resources

    static UIControlledApplication _a;

    public Result OnStartup( UIControlledApplication a )
    {
      _a = a;

      #region Revit window handle
      // Set up IWin32Window instance encapsulating 
      // main Revit application window handle:

      Process process = Process.GetCurrentProcess();

      IntPtr h = process.MainWindowHandle;

      _hWndRevit = new JtWindowHandle( h );
      #endregion // Revit window handle

      #region Create ribbon panel
      Assembly exe = AboutBox.ExecutingAssembly;
      string path = exe.Location;

      string className = GetType().FullName.Replace(
        "App", "Command" );

      RibbonPanel rp = a.CreateRibbonPanel( _text );

      PushButtonData d = new PushButtonData(
        _name, _text, path, className );

      d.ToolTip = AboutBox.AssemblyDescription
        ?? _text;

      d.Image = NewBitmapImage( exe, "ImgStringSearch16.png" );
      d.LargeImage = NewBitmapImage( exe, "ImgStringSearch32.png" );
      d.LongDescription = _tooltip_long_description;

      rp.AddItem( d );
      #endregion // Create ribbon panel

      return Result.Succeeded;
    }

    public Result OnShutdown( UIControlledApplication a )
    {
      SearchHitNavigator.Shutdown();

      Unsubscribe();

      return Result.Succeeded;
    }

    #region Display modeless search result navigator form
    /// <summary>
    /// Display a modeless dialogue with entries for all search hits.
    /// Pass in the Revit application window handle and a delegate 
    /// method allowing the dialogue to pass back a pending element 
    /// id, which will be an element that the user wishes to zoom to.
    /// Subscribe to the Idling event.
    /// The modeless dialogue passes back the pending element id.
    /// In the event handler, the pending element id is picked up
    /// and displayed to the user on the Revit graphics screen.
    /// </summary>
    public static void ShowForm(
      SortableBindingList<SearchHit> data )
    {
      SearchHitNavigator.Show( data,
        new SetElementId( SetPendingElementId ),
        _hWndRevit );

      Subscribe();
    }
    #endregion // Display modeless search result navigator form

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

    #region OnIdling
    /// <summary>
    /// Keep track of our subscription status.
    /// </summary>
    static bool _subscribing = false;

    /// <summary>
    /// Subscribe to the Idling event if not yet already done.
    /// </summary>
    static void Subscribe()
    {
      if( !_subscribing )
      {
        _a.Idling
          += new EventHandler<IdlingEventArgs>(
            OnIdling );

        _subscribing = true;
      }
    }

    /// <summary>
    /// Unsubscribe from the Idling event 
    /// if we are currently subscribed.
    /// </summary>
    static void Unsubscribe()
    {
      if( _subscribing )
      {
        _a.Idling
          -= new EventHandler<IdlingEventArgs>(
            OnIdling );

        _subscribing = false;
      }
    }

    /// <summary>
    /// Revit Idling event handler.
    /// Whenever the user has selected an element to 
    /// zoom to in the modeless dialogue, the pending
    /// element id is set. The event handler picks it
    /// up and zooms to it. We are not modifying the
    /// Revit document, so it seems we can get away 
    /// with not starting a transaction.
    /// </summary>
    static void OnIdling(
      object sender,
      IdlingEventArgs ea )
    {
      if( !SearchHitNavigator.IsShowing )
      {
        Unsubscribe();
      }

      int id = _pending_element_id;

      if( 0 != id )
      {
        // Support both 2011, where sender is an 
        // Application instance, and 2012, where 
        // it is a UIApplication instance:

        UIApplication uiapp
          = sender is UIApplication
          ? sender as UIApplication // 2012
          : new UIApplication(
              sender as Application ); // 2011

        UIDocument uidoc
          = uiapp.ActiveUIDocument;

        Document doc
          = uidoc.Document;

        ElementId eid = new ElementId( id );
        Element e = doc.GetElement( eid );

        Debug.Print(
          "Element id {0} requested --> {1}",
          id, new ElementData( e ) );

        // No transaction required:

        //uidoc.Selection.Elements.Clear();
        //uidoc.Selection.Elements.Add( e );
        //uidoc.ShowElements( e );

        List<ElementId> ids = new List<ElementId>( 1 );
        ids.Add( eid );
        uidoc.Selection.SetElementIds( ids );
        uidoc.ShowElements( ids );

        _pending_element_id = 0;
      }
    }
    #endregion // OnIdling
  }
}
