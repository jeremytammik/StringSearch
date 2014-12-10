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
#endregion // Namespaces

namespace ADNPlugin.Revit.StringSearch
{
  /// <summary>
  /// From http://www.timvw.be/presenting-the-sortablebindinglistt-take-two
  /// </summary>
  public class SortableBindingList<T> : BindingList<T>
  {
    private readonly Dictionary<Type, PropertyComparer<T>> comparers;
    private bool isSorted;
    private ListSortDirection listSortDirection;
    private PropertyDescriptor propertyDescriptor;

    public SortableBindingList()
      : base( new List<T>() )
    {
      this.comparers = new Dictionary<Type, PropertyComparer<T>>();
    }

    public SortableBindingList( IEnumerable<T> enumeration )
      : base( new List<T>( enumeration ) )
    {
      this.comparers = new Dictionary<Type, PropertyComparer<T>>();
    }

    protected override bool SupportsSortingCore
    {
      get { return true; }
    }

    protected override bool IsSortedCore
    {
      get { return this.isSorted; }
    }

    protected override PropertyDescriptor SortPropertyCore
    {
      get { return this.propertyDescriptor; }
    }

    protected override ListSortDirection SortDirectionCore
    {
      get { return this.listSortDirection; }
    }

    protected override bool SupportsSearchingCore
    {
      get { return true; }
    }

    protected override void ApplySortCore( PropertyDescriptor property, ListSortDirection direction )
    {
      List<T> itemsList = ( List<T> ) this.Items;

      Type propertyType = property.PropertyType;
      PropertyComparer<T> comparer;
      if( !this.comparers.TryGetValue( propertyType, out comparer ) )
      {
        comparer = new PropertyComparer<T>( property, direction );
        this.comparers.Add( propertyType, comparer );
      }

      comparer.SetPropertyAndDirection( property, direction );
      itemsList.Sort( comparer );

      this.propertyDescriptor = property;
      this.listSortDirection = direction;
      this.isSorted = true;

      this.OnListChanged( new ListChangedEventArgs( ListChangedType.Reset, -1 ) );
    }

    protected override void RemoveSortCore()
    {
      this.isSorted = false;
      this.propertyDescriptor = base.SortPropertyCore;
      this.listSortDirection = base.SortDirectionCore;

      this.OnListChanged( new ListChangedEventArgs( ListChangedType.Reset, -1 ) );
    }

    protected override int FindCore( PropertyDescriptor property, object key )
    {
      int count = this.Count;
      for( int i = 0; i < count; ++i )
      {
        T element = this[i];
        if( property.GetValue( element ).Equals( key ) )
        {
          return i;
        }
      }
      return -1;
    }

    //public void AddIfNotNull( T a )
    //{
    //  if( null != a )
    //  {
    //    Add( a );
    //  }
    //}
  }
}
