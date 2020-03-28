using System;
using System.IO;

namespace Devscord.Progressor
{
    public class ChartFile : IDisposable
    {
        public string Path { get; private set; }

        internal ChartFile(string path)
        {
            this.Path = path;
            var directory = System.IO.Path.GetDirectoryName(path);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public StreamReader GetStreamReader() => new StreamReader(this.Path);

        public void Dispose()
        {
            if(!string.IsNullOrWhiteSpace(this.Path) && File.Exists(this.Path))
            {
                File.Delete(this.Path);
            }
        }
    }
}
