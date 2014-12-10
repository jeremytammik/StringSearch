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
using System.Diagnostics;
using System.Windows.Forms;
#endregion // Namespaces

namespace ADNPlugin.Revit.StringSearch
{
  public partial class SearchHitNavigator : Form
  {
    Command.SetElementId _set_id;

    public SearchHitNavigator(
      //SortableBindingList<SearchHit> a,
      Command.SetElementId set_id )
    {
      InitializeComponent();
      _set_id = set_id;
      this.dataGridView1.CellDoubleClick 
        += new DataGridViewCellEventHandler( 
          dataGridView1_CellDoubleClick );
    }

    public void SetData( 
      SortableBindingList<SearchHit> a )
    {
      dataGridView1.DataSource = a;
    }

    void SetElementIdFromRow(
      int rowIndex,
      bool doubleClick )
    {
      //
      // do something on double click, 
      // except when on the header:
      //
      if( rowIndex > -1 )
      {
        DataGridViewRow row
          = dataGridView1.Rows[rowIndex];

        int n = row.Cells.Count;

        DataGridViewCell cell = row.Cells[n - 1];

        int id = ( int ) cell.Value;

        _set_id( id );

        Debug.Print(
          "{0} click on row {1} --> element id {2}",
          doubleClick ? "Double" : "Single",
          rowIndex, id );
      }
    }

    void dataGridView1_CellDoubleClick( 
      object sender, 
      DataGridViewCellEventArgs e )
    {
      SetElementIdFromRow( e.RowIndex, true );
    }

    /*
     * attempts to select and highlight the search string within the data grid view cell ...
     * 
     * http://social.msdn.microsoft.com/Forums/en-US/vbgeneral/thread/43f6b81f-4cb7-4e8e-bd29-e3645f200734
     * http://www.visualbasicask.com/visual-basic-general/highlight-text-inside-a-datagridview-cell.shtml
     */
  }
}
