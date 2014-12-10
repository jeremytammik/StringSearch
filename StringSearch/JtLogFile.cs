#region Namespaces
using System;
using System.Diagnostics;
using System.IO;
#endregion // Namespaces

namespace StringSearch
{
  class JtLogFile : IDisposable
  {
    string _path;
    StreamWriter _sw;

    public JtLogFile( string basename )
    {
      _path = System.IO.Path.Combine(
        System.IO.Path.GetTempPath(),
        basename + ".log" );

      //FileStream fs = new FileStream( path, FileMode.OpenOrCreate, FileAccess.Write );
      //StreamWriter streamWriter = new StreamWriter( fs );
      //streamWriter.BaseStream.Seek( 0, SeekOrigin.End );

      _sw = new StreamWriter( _path, true );

      _sw.WriteLine( "\n\rStart analysis {0}\n\r",
        DateTime.Now.ToString( "u" ) );
    }

    public void Dispose()
    {
      _sw.Close();
      _sw.Dispose();
    }

    public void Log( string s )
    {
      _sw.WriteLine( s );
      Debug.WriteLine( s );
    }

    public string Path
    {
      get
      {
        return _path;
      }
    }
  }
}
