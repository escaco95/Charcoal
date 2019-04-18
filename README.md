# Charcoal
C# library including Task(multi-threading) etc.

using Charcoal.Extensions;<br/>
　*StringExtensions*<br/>
　　Path.GetFileName( string ) --> string.GetFileName()<br/>
　*Int32Extensions*<br/>
　　bool = (int != 0) --> bool = int.ToBoolean()<br/>
　*BooleanExtensions*<br/>
　　int = (bool) ? 1 : 0 --> int = bool.ToInt()<br/>
　*FileStreamExtensions*<br/>
　　FileStream.WriteInt32( int )<br/>
　　int = FileStream.ReadInt32()<br/>
　　FileStream.WriteString( string, System.Text.Encoding )<br/>

using Charcoal.IO;<br/>
　class FileHolder;<br/>

using Charcoal.Python;<br/>
　class PythonExecutor;<br/>

using Charcoal.Task;<br/>
　class ControlTask;<br/>
　class Task;<br/>
　class TaskManager;<br/>
