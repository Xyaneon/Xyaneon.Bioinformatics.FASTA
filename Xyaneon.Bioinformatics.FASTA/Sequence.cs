using System;
using Xyaneon.Bioinformatics.FASTA.Data;

namespace Xyaneon.Bioinformatics.FASTA
{
    /// <summary>
    /// Represents one sequence and associated description data in a FASTA file.
    /// </summary>
    public sealed class Sequence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence"/> class.
        /// </summary>
        /// <param name="header">This sequence's header.</param>
        /// <param name="data">The actual sequence data.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="header"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="data"/> is <see langword="null"/>.
        /// </exception>
        public Sequence(Header header, SequenceData data)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header), "The header cannot be null.");
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "The sequence data cannot be null.");
            }

            Header = header;
            Data = data;
        }

        /// <summary>
        /// Gets this sequence's header.
        /// </summary>
        public Header Header { get; }

        /// <summary>
        /// Gets the actual sequence data.
        /// </summary>
        public SequenceData Data { get; }
    }
}
