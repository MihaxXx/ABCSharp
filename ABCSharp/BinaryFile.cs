using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ABCSharp
{
    public class BinaryFile<T> : IDisposable
    {
        public string FilePath { get; }

        public BinaryReader Reader() => 
            new BinaryReader(File.Open(FilePath, FileMode.Open), Encoding.ASCII);

        public BinaryWriter Writer() => 
            new BinaryWriter(File.Open(FilePath, FileMode.Create), Encoding.ASCII);

        public BinaryWriter Appender() => 
            new BinaryWriter(File.Open(FilePath, FileMode.Append), Encoding.ASCII);

        public BinaryFile(string path)
        {
            FilePath = path.Substring(0);
        }

        public override string ToString() => FilePath;

        public void Dispose()
        {
            File.Delete(FilePath);
        }
    }

    public static class BinaryFileReader
    {
        public static IEnumerable<int> Read(this BinaryFile<int> file) =>
            ReadAny(file, r => r.ReadInt32());

        public static IEnumerable<double> Read(this BinaryFile<double> file) =>
            ReadAny(file, r => r.ReadDouble());

        public static IEnumerable<byte> Read(this BinaryFile<byte> file) =>
            ReadAny(file, r => r.ReadByte());

        public static IEnumerable<char> Read(this BinaryFile<char> file) =>
            ReadAny(file, r => r.ReadChar());

        public static IEnumerable<short> Read(this BinaryFile<short> file) =>
            ReadAny(file, r => r.ReadInt16());

        public static IEnumerable<long> Read(this BinaryFile<long> file) =>
            ReadAny(file, r => r.ReadInt64());

        public static IEnumerable<bool> Read(this BinaryFile<bool> file) =>
            ReadAny(file, r => r.ReadBoolean());

        public static IEnumerable<string> Read(this BinaryFile<string> file) =>
            ReadAny(file, r => r.ReadString());

        public static IEnumerable<T> Read<T>(this BinaryFile<T> file) => 
            throw new ArgumentException("Unsupported type");

        private static IEnumerable<T> ReadAny<T>(BinaryFile<T> file, Func<BinaryReader, T> read)
        {
            if (!File.Exists(file.FilePath)) throw new FileNotFoundException();
            using (var reader = file.Reader())
            {
                while (reader.PeekChar() > 0)
                    yield return read(reader);
            }
        }
    }

    public static class BinaryFileWriter
    {
        public static void Write(this BinaryFile<int> file, params int[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<double> file, params double[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<byte> file, params byte[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<bool> file, params bool[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<string> file, params string[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<char> file, params char[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<short> file, params short[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<long> file, params long[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

        public static void Write<T>(this BinaryFile<T> file, params T[] elems) =>
            throw new ArgumentException("Unsupported type");

        private static void WriteAny<T>(BinaryFile<T> file, Action<BinaryWriter, T> write, params T[] elems)
        {
            using (var writer = file.Writer())
            {
                foreach (var x in elems)
                    write(writer, x);
            }
        }
    }

    public static class BinaryFileAppender
    {
        public static void Append(this BinaryFile<int> file, params int[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<double> file, params double[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<byte> file, params byte[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<bool> file, params bool[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<string> file, params string[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<char> file, params char[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<short> file, params short[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

        public static void Write(this BinaryFile<long> file, params long[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

        public static void Write<T>(this BinaryFile<T> file, params T[] elems) =>
            throw new ArgumentException("Unsupported type");

        private static void AppendAny<T>(BinaryFile<T> file, Action<BinaryWriter, T> write, params T[] elems)
        {
            using (var writer = file.Appender())
            {
                foreach (var x in elems)
                    write(writer, x);
            }
        }
    }
}
