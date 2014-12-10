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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
#endregion // Namespaces

namespace ADNPlugin.Revit.StringSearch
{
  /// <summary>
  /// From http://www.timvw.be/presenting-the-sortablebindinglistt-take-two
  /// </summary>
  public class PropertyComparer<T> : IComparer<T>
  {
    private readonly IComparer comparer;
    private PropertyDescriptor propertyDescriptor;
    private int reverse;

    public PropertyComparer( PropertyDescriptor property, ListSortDirection direction )
    {
      this.propertyDescriptor = property;
      Type comparerForPropertyType = typeof( Comparer<> ).MakeGenericType( property.PropertyType );
      this.comparer = ( IComparer ) comparerForPropertyType.InvokeMember( "Default", BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.Public, null, null, null );
      this.SetListSortDirection( direction );
    }

    #region IComparer<T> Members

    public int Compare( T x, T y )
    {
      return this.reverse * this.comparer.Compare( this.propertyDescriptor.GetValue( x ), this.propertyDescriptor.GetValue( y ) );
    }

    #endregion

    private void SetPropertyDescriptor( PropertyDescriptor descriptor )
    {
      this.propertyDescriptor = descriptor;
    }

    private void SetListSortDirection( ListSortDirection direction )
    {
      this.reverse = direction == ListSortDirection.Ascending ? 1 : -1;
    }

    public void SetPropertyAndDirection( PropertyDescriptor descriptor, ListSortDirection direction )
    {
      this.SetPropertyDescriptor( descriptor );
      this.SetListSortDirection( direction );
    }
  }
}
