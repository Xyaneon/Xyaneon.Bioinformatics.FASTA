using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.Utility;

namespace Xyaneon.Bioinformatics.FASTA.IO
{
    /// <summary>
    /// Provides functionality for reading FASTA sequences from a file.
    /// </summary>
    /// <seealso cref="SequenceFileWriter"/>
    /// <seealso cref="SequenceStreamReader"/>
    public static class SequenceFileReader
    {
        private const string ArgumentNullException_Path = "The path to the FASTA file cannot be null.";

        /// <summary>
        /// Reads a FASTA file containing a single sequence and returns its
        /// data.
        /// </summary>
        /// <param name="path">The file to read.</param>
        /// <returns>
        /// The data contained in the file as a new
        /// <see cref="Sequence"/> instance.
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
        public static Sequence ReadSingleFromFile(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            IEnumerable<string> fileLines = File.ReadLines(path);
            return Sequence.Parse(fileLines);
        }

        /// <summary>
        /// Reads a FASTA file containing a single sequence and returns its
        /// data asynchronously.
        /// </summary>
        /// <param name="path">The file to read.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <returns>
        /// The data contained in the file as a new
        /// <see cref="Sequence"/> instance.
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
        public static async Task<Sequence> ReadSingleFromFileAsync(string path, CancellationToken cancellationToken = default)
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

            return Sequence.Parse(fileLines);
        }

        /// <summary>
        /// Reads a FASTA file containing multiple sequences and returns its
        /// data.
        /// </summary>
        /// <param name="path">The file to read.</param>
        /// <returns>
        /// The data contained in the file as a new enumerable collection of
        /// <see cref="Sequence"/> instances.
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
        public static IEnumerable<Sequence> ReadMultipleFromFile(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            IEnumerable<string> fileLines = File.ReadLines(path);
            return Sequence.ParseMultiple(fileLines);
        }

        /// <summary>
        /// Reads a FASTA file containing multiple sequences and returns its
        /// data asynchronously.
        /// </summary>
        /// <param name="path">The file to read.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <returns>
        /// The data contained in the file as a new enumerable collection of
        /// <see cref="Sequence"/> instances.
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
        public static async Task<IEnumerable<Sequence>> ReadMultipleFromFileAsync(string path, CancellationToken cancellationToken = default)
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

            return Sequence.ParseMultiple(fileLines);
        }
    }
}
