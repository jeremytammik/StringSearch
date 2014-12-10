#region Copyright
// (C) Copyright 2011-2012 by Autodesk, Inc. 
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
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace ADNPlugin.Revit.StringSearch
{
  class App : IExternalApplication
  {
    const string _name = "StringSearch";

    static string _text = AboutBox.AssemblyProduct;

    static string _namespace_prefix 
      = typeof( App ).Namespace + ".";

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
      // to read from an external file:
      //return new BitmapImage( new Uri(
      //  Path.Combine( _imageFolder, imageName ) ) );

      Stream s = a.GetManifestResourceStream(
          _namespace_prefix + imageName );

      BitmapImage img = new BitmapImage();

      img.BeginInit();
      img.StreamSource = s;
      img.EndInit();

      return img;
    }

    public Result OnStartup( UIControlledApplication a )
    {
      Assembly exe = AboutBox.ExecutingAssembly;
      string path = exe.Location;

      string className = GetType().FullName.Replace( 
        "App", "Command"  );

      RibbonPanel rp = a.CreateRibbonPanel( _text );

      PushButtonData d = new PushButtonData( 
        _name, _text, path, className );

      d.ToolTip = AboutBox.AssemblyDescription 
        ?? _text;

      d.Image = NewBitmapImage( exe, "ImgSherlockHolmes16.png" );
      d.LargeImage = NewBitmapImage( exe, "ImgSherlockHolmes32.png" );
      d.ToolTipImage = NewBitmapImage( exe, "ImgSherlockHolmes.png" );
      d.LongDescription = "This is a longer tooltip description";

      rp.AddItem( d );

      return Result.Succeeded;
    }

    public Result OnShutdown( UIControlledApplication a )
    {
      return Result.Succeeded;
    }
  }
}
