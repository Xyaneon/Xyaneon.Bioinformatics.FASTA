using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.ActualSequences;
using Xyaneon.Bioinformatics.FASTA.Utility;

namespace Xyaneon.Bioinformatics.FASTA.IO
{
    /// <summary>
    /// Provides functionality for writing FASTA sequences to a file.
    /// </summary>
    /// <seealso cref="SequenceFileReader"/>
    /// <seealso cref="SequenceStreamWriter"/>
    public static class SequenceFileWriter
    {
        private const string ArgumentNullException_Sequence = "The FASTA data to write cannot be null.";
        private const string ArgumentNullException_Sequences = "The collection of FASTA data to write cannot be null.";
        private const string ArgumentNullException_Path = "The path to the FASTA file cannot be null.";

        /// <summary>
        /// Writes interleaved (multiline) data for a single FASTA sequence
        /// to the file at the given <paramref name="path"/>.
        /// </summary>
        /// <param name="sequence">The sequence data to write.</param>
        /// <param name="path">The file to write.</param>
        /// <param name="lineLength">An optional maximum length for each line in the sequence. If omitted, this defaults to <see cref="Constants.DefaultLineLength"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequence"/> is <see langword="null"/>.
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
        /// <seealso cref="WriteToInterleavedFileAsync(Sequence, string, int, CancellationToken)"/>
        /// <seealso cref="WriteToSequentialFile(Sequence, string)"/>
        public static void WriteToInterleavedFile(Sequence sequence, string path, int lineLength = Constants.DefaultLineLength)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence), ArgumentNullException_Sequence);
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            if (lineLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lineLength), lineLength, SequenceUtility.ArgumentOutOfRangeException_LineLengthLessThanOne);
            }

            File.WriteAllLines(path, sequence.ToInterleavedLines(lineLength));
        }

        /// <summary>
        /// Writes interleaved (multiline) data for a single FASTA sequence
        /// to the file at the given <paramref name="path"/> asynchronously.
        /// </summary>
        /// <param name="sequence">The sequence data to write.</param>
        /// <param name="path">The file to write.</param>
        /// <param name="lineLength">An optional maximum length for each line in the sequence. If omitted, this defaults to <see cref="Constants.DefaultLineLength"/>.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <returns>A <see cref="Task"/> representing the ongoing operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequence"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="lineLength"/> is less than one.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is a zero-length string, contains only
        /// white space, or contains one or more invalid characters defined
        /// by the <see cref="Path.GetInvalidPathChars"/> method.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid, (for example, it is on an unmapped
        /// drive).
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined
        /// maximum length.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> is in an invalid format.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission.
        /// -or-
        /// <paramref name="path"/> specified a read-only file or directory.
        /// </exception>
        /// <seealso cref="WriteToInterleavedFile(Sequence, string, int)"/>
        /// <seealso cref="WriteToSequentialFileAsync(Sequence, string, CancellationToken)"/>
        public static async Task WriteToInterleavedFileAsync(Sequence sequence, string path, int lineLength = Constants.DefaultLineLength, CancellationToken cancellationToken = default)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence), ArgumentNullException_Sequence);
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            if (lineLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lineLength), lineLength, SequenceUtility.ArgumentOutOfRangeException_LineLengthLessThanOne);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Writing single interleaved FASTA file data stream async canceled before write.", cancellationToken);
            }

            await FileUtility.WriteAllLinesAsync(path, sequence.ToInterleavedLines(lineLength), cancellationToken);
        }

        /// <summary>
        /// Writes interleaved (multiline) data for multiple FASTA sequences
        /// to the file at the given <paramref name="path"/>.
        /// </summary>
        /// <param name="sequences">The collection of sequence data to write.</param>
        /// <param name="path">The file to write.</param>
        /// <param name="lineLength">An optional maximum length for each line in the sequence. If omitted, this defaults to <see cref="Constants.DefaultLineLength"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequences"/> is <see langword="null"/>.
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
        /// <seealso cref="WriteToInterleavedFileAsync(IEnumerable{Sequence}, string, int, CancellationToken)"/>
        /// <seealso cref="WriteToSequentialFile(IEnumerable{Sequence}, string)"/>
        public static void WriteToInterleavedFile(IEnumerable<Sequence> sequences, string path, int lineLength = Constants.DefaultLineLength)
        {
            if (sequences == null)
            {
                throw new ArgumentNullException(nameof(sequences), ArgumentNullException_Sequences);
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            if (lineLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lineLength), lineLength, SequenceUtility.ArgumentOutOfRangeException_LineLengthLessThanOne);
            }

            File.WriteAllLines(path, sequences.SelectMany(sequence => sequence.ToInterleavedLines(lineLength)));
        }

        /// <summary>
        /// Writes interleaved (multiline) data for multiple FASTA sequences
        /// to the file at the given <paramref name="path"/> asynchronously.
        /// </summary>
        /// <param name="sequences">The collection of sequence data to write.</param>
        /// <param name="path">The file to write.</param>
        /// <param name="lineLength">An optional maximum length for each line in the sequence. If omitted, this defaults to <see cref="Constants.DefaultLineLength"/>.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <returns>A <see cref="Task"/> representing the ongoing operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequences"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="lineLength"/> is less than one.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is a zero-length string, contains only
        /// white space, or contains one or more invalid characters defined
        /// by the <see cref="Path.GetInvalidPathChars"/> method.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid, (for example, it is on an unmapped
        /// drive).
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined
        /// maximum length.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> is in an invalid format.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission.
        /// -or-
        /// <paramref name="path"/> specified a read-only file or directory.
        /// </exception>
        /// <seealso cref="WriteToInterleavedFile(IEnumerable{Sequence}, string, int)"/>
        /// <seealso cref="WriteToSequentialFileAsync(IEnumerable{Sequence}, string, CancellationToken)"/>
        public static async Task WriteToInterleavedFileAsync(IEnumerable<Sequence> sequences, string path, int lineLength = Constants.DefaultLineLength, CancellationToken cancellationToken = default)
        {
            if (sequences == null)
            {
                throw new ArgumentNullException(nameof(sequences), ArgumentNullException_Sequences);
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            if (lineLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lineLength), lineLength, SequenceUtility.ArgumentOutOfRangeException_LineLengthLessThanOne);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Writing multiple interleaved FASTA file data stream async canceled before write.", cancellationToken);
            }

            await FileUtility.WriteAllLinesAsync(path, sequences.SelectMany(sequence => sequence.ToInterleavedLines(lineLength)), cancellationToken);
        }

        /// <summary>
        /// Writes sequential (single-line) data for a single FASTA sequence
        /// to the file at the given <paramref name="path"/>.
        /// </summary>
        /// <param name="sequence">The sequence data to write.</param>
        /// <param name="path">The file to write.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequence"/> is <see langword="null"/>.
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
        /// <seealso cref="WriteToInterleavedFile(Sequence, string, int)"/>
        /// <seealso cref="WriteToSequentialFileAsync(Sequence, string, CancellationToken)"/>
        public static void WriteToSequentialFile(Sequence sequence, string path)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence), ArgumentNullException_Sequence);
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            File.WriteAllLines(path, sequence.ToSequentialLines());
        }

        /// <summary>
        /// Writes sequential (single-line) data for a single FASTA sequence
        /// to the file at the given <paramref name="path"/> asynchronously.
        /// </summary>
        /// <param name="sequence">The sequence data to write.</param>
        /// <param name="path">The file to write.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequence"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is a zero-length string, contains only
        /// white space, or contains one or more invalid characters defined
        /// by the <see cref="Path.GetInvalidPathChars"/> method.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid, (for example, it is on an unmapped
        /// drive).
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined
        /// maximum length.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> is in an invalid format.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission.
        /// -or-
        /// <paramref name="path"/> specified a read-only file or directory.
        /// </exception>
        /// <seealso cref="WriteToInterleavedFileAsync(Sequence, string, int, CancellationToken)"/>
        /// <seealso cref="WriteToSequentialFile(Sequence, string)"/>
        public static async Task WriteToSequentialFileAsync(Sequence sequence, string path, CancellationToken cancellationToken = default)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence), ArgumentNullException_Sequence);
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            await FileUtility.WriteAllLinesAsync(path, sequence.ToSequentialLines(), cancellationToken);
        }

        /// <summary>
        /// Writes sequential (single-line) data for multiple FASTA sequences
        /// to the file at the given <paramref name="path"/>.
        /// </summary>
        /// <param name="sequences">The collection of sequence data to write.</param>
        /// <param name="path">The file to write.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequences"/> is <see langword="null"/>.
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
        /// <seealso cref="WriteToInterleavedFile(IEnumerable{Sequence}, string, int)"/>
        /// <seealso cref="WriteToSequentialFileAsync(IEnumerable{Sequence}, string, CancellationToken)"/>
        public static void WriteToSequentialFile(IEnumerable<Sequence> sequences, string path)
        {
            if (sequences == null)
            {
                throw new ArgumentNullException(nameof(sequences), ArgumentNullException_Sequences);
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            File.WriteAllLines(path, sequences.SelectMany(sequence => sequence.ToSequentialLines()));
        }

        /// <summary>
        /// Writes sequential (single-line) data for multiple FASTA sequences
        /// to the file at the given <paramref name="path"/> asynchronously.
        /// </summary>
        /// <param name="sequences">The collection of sequence data to write.</param>
        /// <param name="path">The file to write.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequences"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is a zero-length string, contains only
        /// white space, or contains one or more invalid characters defined
        /// by the <see cref="Path.GetInvalidPathChars"/> method.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// The specified path is invalid, (for example, it is on an unmapped
        /// drive).
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined
        /// maximum length.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> is in an invalid format.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission.
        /// -or-
        /// <paramref name="path"/> specified a read-only file or directory.
        /// </exception>
        /// <seealso cref="WriteToInterleavedFileAsync(IEnumerable{Sequence}, string, int, CancellationToken)"/>
        /// <seealso cref="WriteToSequentialFile(IEnumerable{Sequence}, string)"/>
        public static async Task WriteToSequentialFileAsync(IEnumerable<Sequence> sequences, string path, CancellationToken cancellationToken = default)
        {
            if (sequences == null)
            {
                throw new ArgumentNullException(nameof(sequences), ArgumentNullException_Sequences);
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path), ArgumentNullException_Path);
            }

            await FileUtility.WriteAllLinesAsync(path, sequences.SelectMany(sequence => sequence.ToSequentialLines()), cancellationToken);
        }
    }
}
