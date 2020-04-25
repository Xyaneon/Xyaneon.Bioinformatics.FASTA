using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Bioinformatics.FASTA.Sequences;
using Xyaneon.Bioinformatics.FASTA.Utility;

namespace Xyaneon.Bioinformatics.FASTA
{
    /// <summary>
    /// Contains all of the data stored in a single FASTA file.
    /// </summary>
    public sealed class MultiFASTAFileData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiFASTAFileData"/> class.
        /// </summary>
        /// <param name="sequence">The sequence stored in the file.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequence"/> is <see langword="null"/>.
        /// </exception>
        public MultiFASTAFileData(SingleFASTAFileData sequence)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence), "The sequence cannot be null.");
            }

            SingleFASTASequences = ListUtility.CreateSingleElementList(sequence).AsReadOnly();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiFASTAFileData"/> class.
        /// </summary>
        /// <param name="sequences">The collection of all sequences stored in the file.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sequences"/> is <see langword="null"/>.
        /// </exception>
        public MultiFASTAFileData(IEnumerable<SingleFASTAFileData> sequences)
        {
            if (sequences == null)
            {
                throw new ArgumentNullException(nameof(sequences), "The collection of sequences cannot be null.");
            }

            SingleFASTASequences = new List<SingleFASTAFileData>(sequences).AsReadOnly();
        }

        /// <summary>
        /// Gets a read-only list of all individual FASTA sequences stored in this file.
        /// </summary>
        public IReadOnlyList<SingleFASTAFileData> SingleFASTASequences { get; }

        /// <summary>
        /// Returns a value indicating whether this file only contains amino
        /// acid sequences.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if all sequences in this file are amino acid
        /// sequences; otherwise, <see langword="false"/>.
        /// </returns>
        /// <seealso cref="ContainsOnlyNucleicAcidSequences"/>
        public bool ContainsOnlyAminoAcidSequences()
        {
            return AllSequencesAreOfType(typeof(AminoAcidSequence));
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
            return AllSequencesAreOfType(typeof(NucleicAcidSequence));
        }

        private bool AllSequencesAreOfType(Type type)
        {
            return SingleFASTASequences.All(sequence => sequence.Data.GetType() == type);
        }
    }
}
