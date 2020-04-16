using System;
using System.Text.RegularExpressions;
using Xyaneon.Bioinformatics.FASTA.Extensions;

namespace Xyaneon.Bioinformatics.FASTA.Data
{
    /// <summary>
    /// Contains nucleic acid sequence data for a FASTA file sequence.
    /// </summary>
    /// <seealso cref="AminoAcidSequenceData"/>
    public sealed class NucleicAcidSequenceData: SequenceData
    {
        private static readonly Regex ValidCharactersRegex = new Regex(@"^[ACGTURYKMSWBDHVN-]*$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="NucleicAcidSequenceData"/> class.
        /// </summary>
        /// <param name="characters">The characters in the sequence data.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="characters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="characters"/> contains invalid characters.
        /// </exception>
        public NucleicAcidSequenceData(string characters)
        {
            if (characters == null)
            {
                throw new ArgumentNullException(nameof(characters), "The characters string for the nucleic acid sequence data cannot be null.");
            }

            string cleanedUpCharacters = characters.RemoveAllWhitespace();

            if (!IsValidNucleicAcidSequence(cleanedUpCharacters))
            {
                throw new ArgumentException("The supplied characters are not a valid nucleic acid sequence.", nameof(characters));
            }

            Characters = cleanedUpCharacters.ToUpperInvariant();
        }

        /// <summary>
        /// Gets a string containing all characters in the sequence data.
        /// </summary>
        public override string Characters { get; }

        private static bool IsValidNucleicAcidSequence(string data)
        {
            return ValidCharactersRegex.IsMatch(data);
        }
    }
}
