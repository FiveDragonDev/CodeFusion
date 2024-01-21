namespace CodeFusion
{
    public class Logger
    {
        protected readonly string _path;
        protected string? _text;

        public Logger(string path = "Log.txt") => _path = path;

        public void Log(string message)
        {
            using StreamWriter writer = new(_path);
            _text += DateTime.UtcNow.ToString("(yyyy-dd-MM, HH:mm:ss.ffff) ") + message + '\n';
            writer.Write(_text);
        }
    }
}
