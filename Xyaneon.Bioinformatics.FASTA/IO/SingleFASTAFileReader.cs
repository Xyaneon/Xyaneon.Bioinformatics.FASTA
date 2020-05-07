using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.Utility;

namespace Xyaneon.Bioinformatics.FASTA.IO
{
    /// <summary>
    /// Reads single-sequence FASTA file data.
    /// </summary>
    /// <seealso cref="MultiFASTAFileReader"/>
    /// <seealso cref="SingleFASTAFileData"/>
    /// <seealso cref="SingleFASTAFileWriter"/>
    public static class SingleFASTAFileReader
    {
        private const string ArgumentNullException_Path = "The path to the single-sequence FASTA file cannot be null.";
        private const string ArgumentNullException_Stream = "The stream to read single-sequence FASTA file data from cannot be null.";

        /// <summary>
        /// Reads a FASTA file containing a single sequence and returns its
        /// data.
        /// </summary>
        /// <param name="path">The file to read.</param>
        /// <returns>
        /// The data contained in the file as a new
        /// <see cref="SingleFASTAFileData"/> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is a zero-length string, contains only
        /// white space, or contains one or more invalid characters defined
        /// by the <see cref="Path.GetInvalidPathChars"/> method.
        /// </exception>
        /// <exception cref="FormatException">
        /// The file data is in an invalid format.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// <paramref name="path"/> is invalid (for example, it is on an
        /// unmapped drive).
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The file specified by <paramref name="path"/> was not found.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred while opening the file.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// <paramref name="path"/> exceeds the system-defined maximum length.
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <paramref name="path"/> specifies a file that is read-only.
        /// -or-
        /// This operation is not supported on the current platform.
        /// -or-
        /// <paramref name="path"/> is a directory.
        /// -or-
        /// The caller does not have the required permission.
        /// </exception>
        public static SingleFASTAFileData ReadFromFile(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            IEnumerable<string> fileLines = File.ReadLines(path);
            return SingleFASTAFileData.Parse(fileLines);
        }

        /// <summary>
        /// Reads a FASTA file containing a single sequence and returns its
        /// data asynchronously.
        /// </summary>
        /// <param name="path">The file to read.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <returns>
        /// The data contained in the file as a new
        /// <see cref="SingleFASTAFileData"/> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is a zero-length string, contains only
        /// white space, or contains one or more invalid characters defined
        /// by the <see cref="Path.GetInvalidPathChars"/> method.
        /// </exception>
        /// <exception cref="FormatException">
        /// The file data is in an invalid format.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// The operation was canceled.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// <paramref name="path"/> is invalid (for example, it is on an
        /// unmapped drive).
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The file specified by <paramref name="path"/> was not found.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred while opening the file.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// <paramref name="path"/> exceeds the system-defined maximum length.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <paramref name="path"/> is a directory.
        /// -or-
        /// The caller does not have the required permission.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> is in an invalid format.
        /// </exception>
        public static async Task<SingleFASTAFileData> ReadFromFileAsync(string path, CancellationToken cancellationToken = default)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            IEnumerable<string> fileLines;

            using (FileStream fileStream = File.OpenRead(path))
            {
                fileLines = await StreamUtility.ReadAllLinesAsync(fileStream, cancellationToken);
            }

            return SingleFASTAFileData.Parse(fileLines);
        }

        /// <summary>
        /// Reads a stream containing file data for a single-sequence FASTA
        /// file and returns it as an object.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <returns>
        /// The data contained in the stream as a new
        /// <see cref="SingleFASTAFileData"/> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// The file data is in an invalid format.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// The operation was canceled.
        /// </exception>
        /// <exception cref="OutOfMemoryException">
        /// There is insufficient memory to allocate a buffer for one of the
        /// read lines.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurs.
        /// </exception>
        public static SingleFASTAFileData ReadFromStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            IEnumerable<string> fileLines = StreamUtility.ReadAllLines(stream);
            return SingleFASTAFileData.Parse(fileLines);
        }

        /// <summary>
        /// Asynchronously reads a stream containing file data for a
        /// single-sequence FASTA file and returns it as an object.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <returns>
        /// The data contained in the stream as a new
        /// <see cref="SingleFASTAFileData"/> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// The file data is in an invalid format.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// The operation was canceled.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="stream"/> has been disposed.
        /// </exception>
        public static async Task<SingleFASTAFileData> ReadFromStreamAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Reading single FASTA file data stream async canceled before read.", cancellationToken);
            }

            IEnumerable<string> fileLines = await StreamUtility.ReadAllLinesAsync(stream, cancellationToken);

            return SingleFASTAFileData.Parse(fileLines);
        }
    }
}
