using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.Utility;

namespace Xyaneon.Bioinformatics.FASTA.IO
{
    /// <summary>
    /// Provides functionality for reading FASTA sequences from a stream.
    /// </summary>
    /// <seealso cref="SequenceFileReader"/>
    /// <seealso cref="SequenceStreamWriter"/>
    public static class SequenceStreamReader
    {
        private const string ArgumentNullException_Stream = "The stream to read FASTA data from cannot be null.";

        /// <summary>
        /// Reads a stream containing file data for a single-sequence FASTA
        /// file and returns it as an object.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <returns>
        /// The data contained in the stream as a new
        /// <see cref="Sequence"/> instance.
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
        /// <seealso cref="ReadSingleFromStreamAsync(Stream, CancellationToken)"/>
        public static Sequence ReadSingleFromStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            IEnumerable<string> fileLines = StreamUtility.ReadAllLines(stream);
            return Sequence.Parse(fileLines);
        }

        /// <summary>
        /// Asynchronously reads a stream containing file data for a
        /// single-sequence FASTA file and returns it as an object.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <returns>
        /// The data contained in the stream as a new
        /// <see cref="Sequence"/> instance.
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
        /// <seealso cref="ReadSingleFromStream(Stream)"/>
        public static async Task<Sequence> ReadSingleFromStreamAsync(Stream stream, CancellationToken cancellationToken = default)
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

            return Sequence.Parse(fileLines);
        }

        /// <summary>
        /// Reads a stream containing file data for a multi-sequence FASTA
        /// file and returns it as a collection.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <returns>
        /// The data contained in the stream as a new enumerable collection of
        /// <see cref="Sequence"/> objects.
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
        /// <seealso cref="ReadMultipleFromStreamAsync(Stream, CancellationToken)"/>
        public static IEnumerable<Sequence> ReadMultipleFromStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            IEnumerable<string> fileLines = StreamUtility.ReadAllLines(stream);
            return Sequence.ParseMultiple(fileLines);
        }

        /// <summary>
        /// Asynchronously reads a stream containing file data for a
        /// multi-sequence FASTA file and returns it as a collection.
        /// </summary>
        /// <param name="stream">The stream to read the data from.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> which can be used to cancel the ongoing operation.</param>
        /// <returns>
        /// The data contained in the stream as a new enumerable collection of
        /// <see cref="Sequence"/> objects.
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
        /// <seealso cref="ReadMultipleFromStream(Stream)"/>
        public static async Task<IEnumerable<Sequence>> ReadMultipleFromStreamAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), ArgumentNullException_Stream);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Reading multi FASTA file data stream async canceled before read.", cancellationToken);
            }

            IEnumerable<string> fileLines = await StreamUtility.ReadAllLinesAsync(stream, cancellationToken);

            return Sequence.ParseMultiple(fileLines);
        }
    }
}
