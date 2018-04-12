using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ABCSharp
{
    public class BinaryFile<T>
    {
        public string FilePath { get; }

        public BinaryFile(string path)
        {
            FilePath = path.Substring(0);
        }

        public override string ToString() =>
            FilePath;
    }

    public static class BinaryFileE
    {
        public static IEnumerable<int> Read(this BinaryFile<int> file)
        {
            if (!File.Exists(file.FilePath)) throw new ArgumentException("File doesn't exist");
            using (var reader = new BinaryReader(File.OpenRead(file.FilePath), Encoding.ASCII))
            {
                while (reader.PeekChar() > 0)
                    yield return reader.ReadInt32();
            }
        }

        public static IEnumerable<double> Read(this BinaryFile<double> file)
        {
            if (!File.Exists(file.FilePath)) throw new ArgumentException("File doesn't exist");
            using (var reader = new BinaryReader(File.OpenRead(file.FilePath), Encoding.ASCII))
            {
                while (reader.PeekChar() > 0)
                    yield return reader.ReadDouble();
            }
        }
    }
}