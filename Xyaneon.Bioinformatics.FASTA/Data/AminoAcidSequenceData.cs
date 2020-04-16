using System;
using System.Text.RegularExpressions;
using Xyaneon.Bioinformatics.FASTA.Extensions;

namespace Xyaneon.Bioinformatics.FASTA.Data
{
    /// <summary>
    /// Contains amino acid sequence data for a FASTA file sequence.
    /// </summary>
    /// <seealso cref="NucleicAcidSequenceData"/>
    public sealed class AminoAcidSequenceData : SequenceData
    {
        private static readonly Regex ValidCharactersRegex = new Regex(@"^[A-Z*-]*$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="AminoAcidSequenceData"/> class.
        /// </summary>
        /// <param name="characters">The characters in the sequence data.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="characters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="characters"/> contains invalid characters.
        /// </exception>
        public AminoAcidSequenceData(string characters)
        {
            if (characters == null)
            {
                throw new ArgumentNullException(nameof(characters), "The characters string for the amino acid sequence data cannot be null.");
            }

            string cleanedUpCharacters = characters.RemoveAllWhitespace();

            if (!IsValidAminoAcidSequence(cleanedUpCharacters))
            {
                throw new ArgumentException("The supplied characters are not a valid amino acid sequence.", nameof(characters));
            }

            Characters = cleanedUpCharacters.ToUpperInvariant();
        }

        /// <summary>
        /// Gets a string containing all characters in the sequence data.
        /// </summary>
        public override string Characters { get; }

        private static bool IsValidAminoAcidSequence(string data)
        {
            return ValidCharactersRegex.IsMatch(data);
        }
    }
}