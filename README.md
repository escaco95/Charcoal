# Charcoal
C# library including Task(multi-threading) etc.

using Charcoal.Extensions;
  // StringExtensions
    Path.GetFileName( string ) --> string.GetFileName()
  // Int32Extensions
    bool = (int != 0) --> bool = int.ToBoolean()
  // BooleanExtensions
    int = (bool) ? 1 : 0 --> int = bool.ToInt()
  // FileStreamExtensions
    FileStream.WriteInt32( int )
    int = FileStream.ReadInt32()
    FileStream.WriteString( string, System.Text.Encoding )

using Charcoal.IO;
  class FileHolder;

using Charcoal.Python;
  class PythonExecutor;

using Charcoal.Task;
  class ControlTask;
  class Task;
  class TaskManager;
