using System;
using System.Collections.Generic;

namespace Xyaneon.Bioinformatics.FASTA
{
    /// <summary>
    /// Contains all of the data stored in a single FASTA file.
    /// </summary>
    public sealed class FileData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileData"/> class.
        /// </summary>
        /// <param name="sequences">The collection of all sequences stored in the file.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequences"/> is <see langword="null"/>.
        /// </exception>
        public FileData(IEnumerable<Sequence> sequences)
        {
            if (sequences == null)
            {
                throw new ArgumentNullException(nameof(sequences), "The collection of sequences cannot be null.");
            }

            Sequences = new List<Sequence>(sequences).AsReadOnly();
        }

        /// <summary>
        /// Gets a read-only list of all sequences stored in this file.
        /// </summary>
        public IReadOnlyList<Sequence> Sequences { get; }
    }
}
