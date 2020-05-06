using System;
using System.IO;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.Sequences;
using Xyaneon.Bioinformatics.FASTA.Utility;

namespace Xyaneon.Bioinformatics.FASTA
{
    /// <summary>
    /// Writes single-sequence FASTA file data.
    /// </summary>
    /// <seealso cref="SingleFASTAFileData"/>
    /// <seealso cref="SingleFASTAFileReader"/>
    public static class SingleFASTAFileWriter
    {
        private const string ArgumentNullException_Data = "The single-sequence FASTA data to write cannot be null.";
        private const string ArgumentNullException_Path = "The path to the single-sequence FASTA file cannot be null.";
        private const string ArgumentNullException_Stream = "The stream to write the single-sequence FASTA data to cannot be null.";

        /// <summary>
        /// Writes interleaved (multiline) data for a single FASTA sequence
        /// to the file at the given <paramref name="path"/>.
        /// </summary>
        /// <param name="data">The sequence data to write.</param>
        /// <param name="path">The file to write.</param>
        /// <param name="lineLength">An optional maximum length for each line in the sequence. If omitted, this defaults to <see cref="Constants.DefaultLineLength"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is a zero-length string, contains only
        /// white space, or contains one or more invalid characters defined
        /// by the <see cref="Path.GetInvalidPathChars"/> method.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="lineLength"/> is less than one.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// <paramref name="path"/> is invalid (for example, it is on an
        /// unmapped drive).
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred while opening the file.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> is in an invalid format.
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
        /// <paramref name="path"/> specified a file that is hidden.
        /// -or-
        /// This operation is not supported on the current platform.
        /// -or-
        /// <paramref name="path"/> is a directory.
        /// -or-
        /// The caller does not have the required permission.
        /// </exception>
        public static void WriteToInterleavedFile(SingleFASTAFileData data, string path, int lineLength = Constants.DefaultLineLength)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), ArgumentNullException_Data);
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            if (lineLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lineLength), lineLength, SequenceUtility.ArgumentOutOfRangeException_LineLengthLessThanOne);
            }

            File.WriteAllLines(path, data.ToInterleavedLines(lineLength));
        }

        public static async Task WriteToInterleavedFileAsync(SingleFASTAFileData data, string path, int lineLength = Constants.DefaultLineLength, CancellationToken cancellationToken = default)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), ArgumentNullException_Data);
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Writing single interleaved FASTA file data stream async canceled before write.", cancellationToken);
            }

            await FileUtility.WriteAllLinesAsync(path, data.ToInterleavedLines(lineLength), cancellationToken);
        }

        public static void WriteToInterleavedStream(SingleFASTAFileData data, Stream stream, int lineLength = Constants.DefaultLineLength)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), ArgumentNullException_Data);
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            StreamUtility.WriteAllLines(stream, data.ToInterleavedLines(lineLength));
        }

        public static async Task WriteToInterleavedStreamAsync(SingleFASTAFileData data, Stream stream, int lineLength = Constants.DefaultLineLength, CancellationToken cancellationToken = default)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), ArgumentNullException_Data);
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            await StreamUtility.WriteAllLinesAsync(stream, data.ToInterleavedLines(lineLength), cancellationToken);
        }

        /// <summary>
        /// Writes sequential (single-line) data for a single FASTA sequence
        /// to the file at the given <paramref name="path"/>.
        /// </summary>
        /// <param name="data">The sequence data to write.</param>
        /// <param name="path">The file to write.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is a zero-length string, contains only
        /// white space, or contains one or more invalid characters defined
        /// by the <see cref="Path.GetInvalidPathChars"/> method.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// <paramref name="path"/> is invalid (for example, it is on an
        /// unmapped drive).
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred while opening the file.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> is in an invalid format.
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
        /// <paramref name="path"/> specified a file that is hidden.
        /// -or-
        /// This operation is not supported on the current platform.
        /// -or-
        /// <paramref name="path"/> is a directory.
        /// -or-
        /// The caller does not have the required permission.
        /// </exception>
        /// <seealso cref="WriteToInterleavedFile(SingleFASTAFileData, string, int)"/>
        /// <seealso cref="WriteToSequentialFileAsync(SingleFASTAFileData, string, CancellationToken)"/>
        public static void WriteToSequentialFile(SingleFASTAFileData data, string path)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), ArgumentNullException_Data);
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            File.WriteAllLines(path, data.ToSequentialLines());
        }

        /// <summary>
        /// Writes sequential (single-line) data for a single FASTA sequence
        /// to the file at the given <paramref name="path"/> asynchronously.
        /// </summary>
        /// <param name="data">The sequence data to write.</param>
        /// <param name="path">The file to write.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        /// <seealso cref="WriteToSequentialFile(SingleFASTAFileData, string)"/>
        public static async Task WriteToSequentialFileAsync(SingleFASTAFileData data, string path, CancellationToken cancellationToken = default)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), ArgumentNullException_Data);
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            await FileUtility.WriteAllLinesAsync(path, data.ToSequentialLines(), cancellationToken);
        }

        /// <summary>
        /// Writes sequential (single-line) data for a single FASTA sequence
        /// to the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="data">The sequence data to write.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="stream"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="stream"/> is not writable.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurs.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="stream"/> is closed.
        /// </exception>
        /// <seealso cref="WriteToInterleavedStream(SingleFASTAFileData, Stream, int)"/>
        /// <seealso cref="WriteToSequentialStreamAsync(SingleFASTAFileData, Stream, CancellationToken)"/>
        public static void WriteToSequentialStream(SingleFASTAFileData data, Stream stream)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), ArgumentNullException_Data);
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            StreamUtility.WriteAllLines(stream, data.ToSequentialLines());
        }

        /// <summary>
        /// Writes sequential (single-line) data for a single FASTA sequence
        /// to the given <paramref name="stream"/> asynchronously.
        /// </summary>
        /// <param name="data">The sequence data to write.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="stream"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="stream"/> is not writable.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurs.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// <paramref name="stream"/> is closed.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// The ongoing operation was canceled.
        /// </exception>
        /// <seealso cref="WriteToInterleavedStreamAsync(SingleFASTAFileData, Stream, int, CancellationToken)"/>
        /// <seealso cref="WriteToSequentialStream(SingleFASTAFileData, Stream)"/>
        public static async Task WriteToSequentialStreamAsync(SingleFASTAFileData data, Stream stream, CancellationToken cancellationToken = default)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), ArgumentNullException_Data);
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            await StreamUtility.WriteAllLinesAsync(stream, data.ToSequentialLines(), cancellationToken);
        }
    }
}
