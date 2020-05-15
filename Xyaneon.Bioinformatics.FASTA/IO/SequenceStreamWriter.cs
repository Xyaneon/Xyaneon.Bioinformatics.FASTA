using System;
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
    /// Provides functionality for writing FASTA sequences to a stream.
    /// </summary>
    /// <seealso cref="SequenceFileWriter"/>
    /// <seealso cref="SequenceStreamReader"/>
    public static class SequenceStreamWriter
    {
        private const string ArgumentNullException_Sequence = "The FASTA data to write cannot be null.";
        private const string ArgumentNullException_Sequences = "The collection of FASTA data to write cannot be null.";
        private const string ArgumentNullException_Stream = "The stream to write the FASTA data to cannot be null.";

        /// <summary>
        /// Writes interleaved (multiline) data for a single FASTA sequence
        /// to the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="sequence">The sequence data to write.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="lineLength">An optional maximum length for each line in the sequence. If omitted, this defaults to <see cref="Constants.DefaultLineLength"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequence"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="lineLength"/> is less than one.
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
        /// <seealso cref="WriteToInterleavedStreamAsync(Sequence, Stream, int, CancellationToken)"/>
        /// <seealso cref="WriteToSequentialStream(Sequence, Stream)"/>
        public static void WriteToInterleavedStream(Sequence sequence, Stream stream, int lineLength = Constants.DefaultLineLength)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence), ArgumentNullException_Sequence);
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            StreamUtility.WriteAllLines(stream, sequence.ToInterleavedLines(lineLength));
        }

        /// <summary>
        /// Writes interleaved (multiline) data for a single FASTA sequence
        /// to the given <paramref name="stream"/> asynchronously.
        /// </summary>
        /// <param name="sequence">The sequence data to write.</param>
        /// <param name="stream">The stream to write to.</param>
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
        /// <seealso cref="WriteToInterleavedStream(Sequence, Stream, int)"/>
        /// <seealso cref="WriteToSequentialStreamAsync(Sequence, Stream, CancellationToken)"/>
        public static async Task WriteToInterleavedStreamAsync(Sequence sequence, Stream stream, int lineLength = Constants.DefaultLineLength, CancellationToken cancellationToken = default)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence), ArgumentNullException_Sequence);
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            await StreamUtility.WriteAllLinesAsync(stream, sequence.ToInterleavedLines(lineLength), cancellationToken);
        }

        /// <summary>
        /// Writes interleaved (multiline) data for multiple FASTA sequences
        /// to the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="sequences">The collection of sequence data to write.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="lineLength">An optional maximum length for each line in the sequence. If omitted, this defaults to <see cref="Constants.DefaultLineLength"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequences"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="lineLength"/> is less than one.
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
        /// <seealso cref="WriteToInterleavedStreamAsync(IEnumerable{Sequence}, Stream, int, CancellationToken)"/>
        /// <seealso cref="WriteToSequentialStream(IEnumerable{Sequence}, Stream)"/>
        public static void WriteToInterleavedStream(IEnumerable<Sequence> sequences, Stream stream, int lineLength = Constants.DefaultLineLength)
        {
            if (sequences == null)
            {
                throw new ArgumentNullException(nameof(sequences), ArgumentNullException_Sequences);
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            StreamUtility.WriteAllLines(stream, sequences.SelectMany(sequence => sequence.ToInterleavedLines(lineLength)));
        }

        /// <summary>
        /// Writes interleaved (multiline) data for multiple FASTA sequences
        /// to the given <paramref name="stream"/> asynchronously.
        /// </summary>
        /// <param name="sequences">The sequence data to write.</param>
        /// <param name="stream">The stream to write to.</param>
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
        /// <seealso cref="WriteToInterleavedStream(IEnumerable{Sequence}, Stream, int)"/>
        /// <seealso cref="WriteToSequentialStreamAsync(IEnumerable{Sequence}, Stream, CancellationToken)"/>
        public static async Task WriteToInterleavedStreamAsync(IEnumerable<Sequence> sequences, Stream stream, int lineLength = Constants.DefaultLineLength, CancellationToken cancellationToken = default)
        {
            if (sequences == null)
            {
                throw new ArgumentNullException(nameof(sequences), ArgumentNullException_Sequences);
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            await StreamUtility.WriteAllLinesAsync(stream, sequences.SelectMany(sequence => sequence.ToInterleavedLines(lineLength)), cancellationToken);
        }

        /// <summary>
        /// Writes sequential (single-line) data for a single FASTA sequence
        /// to the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="sequence">The sequence data to write.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequence"/> is <see langword="null"/>.
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
        /// <seealso cref="WriteToInterleavedStream(Sequence, Stream, int)"/>
        /// <seealso cref="WriteToSequentialStreamAsync(Sequence, Stream, CancellationToken)"/>
        public static void WriteToSequentialStream(Sequence sequence, Stream stream)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence), ArgumentNullException_Sequence);
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            StreamUtility.WriteAllLines(stream, sequence.ToSequentialLines());
        }

        /// <summary>
        /// Writes sequential (single-line) data for a single FASTA sequence
        /// to the given <paramref name="stream"/> asynchronously.
        /// </summary>
        /// <param name="sequence">The sequence data to write.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequence"/> is <see langword="null"/>.
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
        /// <seealso cref="WriteToInterleavedStreamAsync(Sequence, Stream, int, CancellationToken)"/>
        /// <seealso cref="WriteToSequentialStream(Sequence, Stream)"/>
        public static async Task WriteToSequentialStreamAsync(Sequence sequence, Stream stream, CancellationToken cancellationToken = default)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence), ArgumentNullException_Sequence);
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            await StreamUtility.WriteAllLinesAsync(stream, sequence.ToSequentialLines(), cancellationToken);
        }

        /// <summary>
        /// Writes sequential (single-line) data for multiple FASTA sequences
        /// to the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="sequences">The collection of sequence data to write.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequences"/> is <see langword="null"/>.
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
        /// <seealso cref="WriteToInterleavedStream(IEnumerable{Sequence}, Stream, int)"/>
        /// <seealso cref="WriteToSequentialStreamAsync(IEnumerable{Sequence}, Stream, CancellationToken)"/>
        public static void WriteToSequentialStream(IEnumerable<Sequence> sequences, Stream stream)
        {
            if (sequences == null)
            {
                throw new ArgumentNullException(nameof(sequences), ArgumentNullException_Sequences);
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            StreamUtility.WriteAllLines(stream, sequences.SelectMany(sequence => sequence.ToSequentialLines()));
        }

        /// <summary>
        /// Writes sequential (single-line) data for multiple FASTA sequences
        /// to the given <paramref name="stream"/> asynchronously.
        /// </summary>
        /// <param name="sequences">The collection of sequence data to write.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequences"/> is <see langword="null"/>.
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
        /// <seealso cref="WriteToInterleavedStreamAsync(IEnumerable{Sequence}, Stream, int, CancellationToken)"/>
        /// <seealso cref="WriteToSequentialStream(IEnumerable{Sequence}, Stream)"/>
        public static async Task WriteToSequentialStreamAsync(IEnumerable<Sequence> sequences, Stream stream, CancellationToken cancellationToken = default)
        {
            if (sequences == null)
            {
                throw new ArgumentNullException(nameof(sequences), ArgumentNullException_Sequences);
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            await StreamUtility.WriteAllLinesAsync(stream, sequences.SelectMany(sequence => sequence.ToSequentialLines()), cancellationToken);
        }
    }
}
