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

        public FileStream Stream() =>
            new FileStream(FilePath,FileMode.Open);

        public BinaryFile(string path)
        {
            FilePath = path.Substring(0);
        }

        public override string ToString() => FilePath;

        public void Dispose()
        {
            if (File.Exists(FilePath)) File.Delete(FilePath);
        }
    }

    public static class BinaryFileReader
    {
		/// <summary>
		/// Returns file content as IEnumerable
		/// </summary>
        public static IEnumerable<int> Read(this BinaryFile<int> file) =>
            ReadAny(file, r => r.ReadInt32());

	    /// <summary>
	    /// Returns file content as IEnumerable
	    /// </summary>
		public static IEnumerable<double> Read(this BinaryFile<double> file) =>
            ReadAny(file, r => r.ReadDouble());

	    /// <summary>
	    /// Returns file content as IEnumerable
	    /// </summary>
		public static IEnumerable<byte> Read(this BinaryFile<byte> file) =>
            ReadAny(file, r => r.ReadByte());

	    /// <summary>
	    /// Returns file content as IEnumerable
	    /// </summary>
		public static IEnumerable<char> Read(this BinaryFile<char> file) =>
            ReadAny(file, r => r.ReadChar());

	    /// <summary>
	    /// Returns file content as IEnumerable
	    /// </summary>
		public static IEnumerable<short> Read(this BinaryFile<short> file) =>
            ReadAny(file, r => r.ReadInt16());

	    /// <summary>
	    /// Returns file content as IEnumerable
	    /// </summary>
		public static IEnumerable<long> Read(this BinaryFile<long> file) =>
            ReadAny(file, r => r.ReadInt64());

	    /// <summary>
	    /// Returns file content as IEnumerable
	    /// </summary>
		public static IEnumerable<bool> Read(this BinaryFile<bool> file) =>
            ReadAny(file, r => r.ReadBoolean());

	    /// <summary>
	    /// Returns file content as IEnumerable
	    /// </summary>
		public static IEnumerable<string> Read(this BinaryFile<string> file) =>
            ReadAny(file, r => r.ReadString());

	    /// <summary>
	    /// Content of this type cannot be readen
	    /// </summary>
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
		/// <summary>
		/// Rewrites the file with provided data
		/// </summary>
        public static void Write(this BinaryFile<int> file, params int[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Rewrites the file with provided data
	    /// </summary>
		public static void Write(this BinaryFile<double> file, params double[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Rewrites the file with provided data
	    /// </summary>
		public static void Write(this BinaryFile<byte> file, params byte[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Rewrites the file with provided data
	    /// </summary>
		public static void Write(this BinaryFile<bool> file, params bool[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Rewrites the file with provided data
	    /// </summary>
		public static void Write(this BinaryFile<string> file, params string[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Rewrites the file with provided data
	    /// </summary>
		public static void Write(this BinaryFile<char> file, params char[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Rewrites the file with provided data
	    /// </summary>
		public static void Write(this BinaryFile<short> file, params short[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Rewrites the file with provided data
	    /// </summary>
		public static void Write(this BinaryFile<long> file, params long[] elems) =>
            WriteAny(file, (w, x) => w.Write(x), elems);

		/// <summary>
		/// Type is not supported. Will raise an exception.
		/// </summary>
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
		/// <summary>
		/// Adds elements to the end of file
		/// </summary>
        public static void Append(this BinaryFile<int> file, params int[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Adds elements to the end of file
	    /// </summary>
		public static void Append(this BinaryFile<double> file, params double[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Adds elements to the end of file
	    /// </summary>
		public static void Append(this BinaryFile<byte> file, params byte[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Adds elements to the end of file
	    /// </summary>
		public static void Append(this BinaryFile<bool> file, params bool[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Adds elements to the end of file
	    /// </summary>
		public static void Append(this BinaryFile<string> file, params string[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Adds elements to the end of file
	    /// </summary>
		public static void Append(this BinaryFile<char> file, params char[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Adds elements to the end of file
	    /// </summary>
		public static void Append(this BinaryFile<short> file, params short[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Adds elements to the end of file
	    /// </summary>
		public static void Append(this BinaryFile<long> file, params long[] elems) =>
            AppendAny(file, (w, x) => w.Write(x), elems);

	    /// <summary>
	    /// Type is not supported. Will raise exception
	    /// </summary>
		public static void Append<T>(this BinaryFile<T> file, params T[] elems) =>
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

    public static class BinaryFileTruncater
    {
	    /// <summary>
	    /// Truncate the file by cutting off elements which number is more than <paramref name="n"/>.
	    /// </summary>
	    /// <param name="file">File.</param>
	    /// <param name="n">New number of elements in the file.</param>
		public static void Truncate(this BinaryFile<int> file, int n) =>
            TruncateAny(file, n);

	    /// <summary>
	    /// Truncate the file by cutting off elements which number is more than <paramref name="n"/>.
	    /// </summary>
	    /// <param name="file">File.</param>
	    /// <param name="n">New number of elements in the file.</param>
		public static void Truncate(this BinaryFile<double> file, int n) =>
            TruncateAny(file, n);

	    /// <summary>
	    /// Truncate the file by cutting off elements which number is more than <paramref name="n"/>.
	    /// </summary>
	    /// <param name="file">File.</param>
	    /// <param name="n">New number of elements in the file.</param>
		public static void Truncate(this BinaryFile<byte> file, int n) =>
            TruncateAny(file, n);

	    /// <summary>
	    /// Truncate the file by cutting off elements which number is more than <paramref name="n"/>.
	    /// </summary>
	    /// <param name="file">File.</param>
	    /// <param name="n">New number of elements in the file.</param>
		public static void Truncate(this BinaryFile<bool> file, int n) =>
            TruncateAny(file, n);

	    /// <summary>
	    /// Truncate the file by cutting off elements which number is more than <paramref name="n"/>.
	    /// </summary>
	    /// <param name="file">File.</param>
	    /// <param name="n">New number of elements in the file.</param>
		public static void Truncate(this BinaryFile<string> file, int n) =>
            TruncateAny(file, n);

	    /// <summary>
	    /// Truncate the file by cutting off elements which number is more than <paramref name="n"/>.
	    /// </summary>
	    /// <param name="file">File.</param>
	    /// <param name="n">New number of elements in the file.</param>
		public static void Truncate(this BinaryFile<char> file, int n) =>
            TruncateAny(file, n);

	    /// <summary>
	    /// Truncate the file by cutting off elements which number is more than <paramref name="n"/>.
	    /// </summary>
	    /// <param name="file">File.</param>
	    /// <param name="n">New number of elements in the file.</param>
		public static void Truncate(this BinaryFile<short> file, int n) =>
            TruncateAny(file, n);

	    /// <summary>
	    /// Truncate the file by cutting off elements which number is more than <paramref name="n"/>.
	    /// </summary>
	    /// <param name="file">File.</param>
	    /// <param name="n">New number of elements in the file.</param>
		public static void Truncate(this BinaryFile<long> file, int n) =>
            TruncateAny(file, n);

        /// <summary>
        /// Truncate the file by cutting off elements which number is more than <paramref name="n"/>.
        /// </summary>
        /// <param name="file">File.</param>
        /// <param name="n">New number of elements in the file.</param>
        public static void Truncate<T>(this BinaryFile<T> file, int n) =>
            throw new ArgumentException("Unsupported type");

        private static void TruncateAny<T>(BinaryFile<T> file, int n)
        {
            using (var stream = file.Stream())             {
                var size = System.Runtime.InteropServices.Marshal.SizeOf<T>();
                if (stream.Length >= n * size)
                {
                    stream.Seek(n * size, SeekOrigin.Begin);
                    stream.SetLength(stream.Position);
                }
                else throw new Exception("File is too short.");             }         }
    }
}
