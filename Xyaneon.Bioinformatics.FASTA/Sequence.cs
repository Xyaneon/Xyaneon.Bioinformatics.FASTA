using System;
using System.Collections.Generic;
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
        /// <param name="headerItems">The list of items from this sequence's header line.</param>
        /// <param name="data">The actual sequence data.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="headerItems"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="data"/> is <see langword="null"/>.
        /// </exception>
        public Sequence(IEnumerable<HeaderItem> headerItems, SequenceData data)
        {
            if (headerItems == null)
            {
                throw new ArgumentNullException(nameof(headerItems), "The collection of header items cannot be null.");
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "The sequence data cannot be null.");
            }

            HeaderItems = new List<HeaderItem>(headerItems).AsReadOnly();
            Data = data;
        }

        /// <summary>
        /// Gets a read-only list of items from this sequence's header line.
        /// </summary>
        public IReadOnlyList<HeaderItem> HeaderItems { get; }

        /// <summary>
        /// Gets the actual sequence data.
        /// </summary>
        public SequenceData Data { get; }
    }
}
