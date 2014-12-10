#region Namespaces
using Autodesk.Revit.DB;
#endregion // Namespaces

namespace StringSearch
{
  /// <summary>
  /// Manage data for a Revit element.
  /// This is also used in the loose connector navigator.
  /// </summary>
  public class ElementData
  {
    public string Class { get; set; }
    public string Category { get; set; }
    public string Family { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public int Id { get; set; }

    public ElementData(
      Element e,
      Document doc )
    {
      Class = e.GetType().Name;

      Category = ( null == e.Category )
        ? string.Empty
        : e.Category.Name;

      ElementId typeId = e.GetTypeId();

      ElementType elementType = ( null == typeId )
        ? null
        : doc.get_Element( typeId ) as ElementType;

      FamilyInstance fi = e as FamilyInstance;

      //Duct duct = e as Duct;
      //if( null != duct )
      //{
      //  string s = duct.DuctType.Name;
      //}

      Family = ( null != fi )
        ? fi.Symbol.Family.Name
        : string.Empty;

      Symbol = ( null != fi ) ? fi.Symbol.Name
        : ( ( null != elementType ) ? elementType.Name
        : string.Empty );

      Name = e.Name;

      // this is not valid for electrical panels, which 
      // may have something like "EP-2" versus "400 A":
      //
      //Debug.Assert( Name.Equals( Symbol ), 
      //  "expected element name to equal symbol name" );

      Id = e.Id.IntegerValue;
    }

    public override string ToString()
    {
      string c = ( 0 == Category.Length )
        ? Class
        : Category;

      string fam = ( 0 == Family.Length )
        ? string.Empty
        : "'" + Family + "' ";

      return string.Format(
        "{0} {1}<{2} '{3}'>",
        c, fam, Id, Name );
    }
  }

  /// <summary>
  /// Manage data for a string search hit.
  /// </summary>
  public class SearchHit : ElementData
  {
    public string BipName { get; set; }
    public string ParameterName { get; set; }
    public string ParameterValue { get; set; }
    public string ParameterString { get; set; }

    public SearchHit( 
      Element e, 
      Document doc,
      string bipName,
      string parameterName,
      string parameterValue, 
      string parameterString )
      : base( e, doc )
    {
      BipName = bipName;
      ParameterName = parameterName;
      ParameterValue = parameterValue;
      ParameterString = parameterString;
    }

    public override string ToString()
    {
      return string.Format(
        "Parameter '{0}' {1} = {2} ({3}) on {4}",
        BipName, ParameterName, ParameterValue, 
        ParameterString, base.ToString() );
    }
  }
}
