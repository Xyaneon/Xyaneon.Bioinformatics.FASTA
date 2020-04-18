using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Bioinformatics.FASTA.Data;
using Xyaneon.Bioinformatics.FASTA.Utility;

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
        /// <param name="sequence">The sequence stored in the file.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequence"/> is <see langword="null"/>.
        /// </exception>
        public FileData(Sequence sequence)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence), "The sequence cannot be null.");
            }

            Sequences = ListUtility.CreateSingleElementList(sequence).AsReadOnly();
        }

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

        /// <summary>
        /// Returns a value indicating whether this file only contains amino
        /// acid sequences.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if all sequences in this file are amino acid
        /// sequences; otherwise, <see langword="false"/>.
        /// </returns>
        /// <seealso cref="ContainsOnlyNucleicAcidSequenes"/>
        public bool ContainsOnlyAminoAcidSequences()
        {
            return AllSequencesAreOfType(typeof(AminoAcidSequenceData));
        }

        /// <summary>
        /// Returns a value indicating whether this file only contains nucleic
        /// acid sequences.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if all sequences in this file are nucleic
        /// acid sequences; otherwise, <see langword="false"/>.
        /// </returns>
        /// <seealso cref="ContainsOnlyAminoAcidSequences"/>
        public bool ContainsOnlyNucleicAcidSequences()
        {
            return AllSequencesAreOfType(typeof(NucleicAcidSequenceData));
        }

        private bool AllSequencesAreOfType(Type type)
        {
            return Sequences.All(sequence => sequence.Data.GetType() == type);
        }
    }
}
