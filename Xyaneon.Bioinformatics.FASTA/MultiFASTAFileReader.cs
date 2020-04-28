using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.Utility;

namespace Xyaneon.Bioinformatics.FASTA
{
    /// <summary>
    /// Reads multi-sequence FASTA file data.
    /// </summary>
    /// <seealso cref="MultiFASTAFileData"/>
    /// <seealso cref="SingleFASTAFileReader"/>
    public static class MultiFASTAFileReader
    {
        /// <summary>
        /// Reads a FASTA file containing multiple sequences and returns its
        /// data.
        /// </summary>
        /// <param name="path">The file to read.</param>
        /// <returns>
        /// The data contained in the file as a new
        /// <see cref="MultiFASTAFileData"/> instance.
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
        public static MultiFASTAFileData ReadFromFile(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), "The path to the multi-sequence FASTA file cannot be null.");
            }

            IEnumerable<string> fileLines = File.ReadLines(path);
            return MultiFASTAFileData.Parse(fileLines);
        }

        /// <summary>
        /// Reads a FASTA file containing multiple sequences and returns its
        /// data asynchronously.
        /// </summary>
        /// <param name="path">The file to read.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <returns>
        /// The data contained in the file as a new
        /// <see cref="MultiFASTAFileData"/> instance.
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
        public static async Task<MultiFASTAFileData> ReadFromFileAsync(string path, CancellationToken cancellationToken = default)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), "The path to the multi-sequence FASTA file cannot be null.");
            }

            IEnumerable<string> fileLines;

            using (FileStream fileStream = File.OpenRead(path))
            {
                fileLines = await StreamUtility.ReadAllLinesFromStreamAsync(fileStream, cancellationToken);
            }

            return MultiFASTAFileData.Parse(fileLines);
        }

        /// <summary>
        /// Reads a stream containing file data for a multi-sequence FASTA
        /// file and returns it as an object.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <returns>
        /// The data contained in the stream as a new
        /// <see cref="MultiFASTAFileData"/> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <see langword="null"/>.
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
        public static MultiFASTAFileData ReadFromStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), "The stream to read multi-sequence FASTA file data from cannot be null.");
            }

            IEnumerable<string> fileLines = StreamUtility.ReadAllLinesFromStream(stream);
            return MultiFASTAFileData.Parse(fileLines);
        }

        /// <summary>
        /// Asynchronously reads a stream containing file data for a
        /// multi-sequence FASTA file and returns it as an object.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <returns>
        /// The data contained in the stream as a new
        /// <see cref="MultiFASTAFileData"/> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <see langword="null"/>.
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
        public static async Task<MultiFASTAFileData> ReadFromStreamAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), "The stream to read multi-sequence FASTA file data from cannot be null.");
            }

            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Reading multi FASTA file data stream async canceled before read.", cancellationToken);
            }

            IEnumerable<string> fileLines = await StreamUtility.ReadAllLinesFromStreamAsync(stream, cancellationToken);

            return MultiFASTAFileData.Parse(fileLines);
        }
    }
}
