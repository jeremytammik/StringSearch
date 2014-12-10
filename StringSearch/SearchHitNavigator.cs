#region Namespaces
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
#endregion // Namespaces

namespace StringSearch
{
  public partial class SearchHitNavigator : Form
  {
    Command.SetElementId _set_id;

    public SearchHitNavigator(
      SortableBindingList<SearchHit> a,
      Command.SetElementId set_id )
    {
      InitializeComponent();
      dataGridView1.DataSource = a;
      _set_id = set_id;
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
  }
}
