using System;
using System.Collections.Generic;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

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
        /// <param name="descriptionIdentifiers">The list of identifiers from this sequence's description line.</param>
        /// <param name="data">The actual sequence data as a string of characters.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="descriptionIdentifiers"/> is <see langword="null"/>.
        /// </exception>
        public Sequence(IEnumerable<Identifier> descriptionIdentifiers, string data)
        {
            if (descriptionIdentifiers == null)
            {
                throw new ArgumentNullException(nameof(descriptionIdentifiers), "The collection of description identifiers cannot be null.");
            }

            DescriptionIdentifiers = new List<Identifier>(descriptionIdentifiers).AsReadOnly();
            Data = data;
        }

        /// <summary>
        /// Gets a read-only list of identifiers from this sequence's
        /// description line.
        /// </summary>
        public IReadOnlyList<Identifier> DescriptionIdentifiers { get; }

        /// <summary>
        /// Gets the actual sequence data as a string of characters.
        /// </summary>
        public string Data { get; }
    }
}
