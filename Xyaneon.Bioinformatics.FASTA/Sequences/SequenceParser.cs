using System;
using System.Collections.Generic;

namespace Xyaneon.Bioinformatics.FASTA.Sequences
{
    /// <summary>
    /// Provides functionality for parsing FASTA sequence data.
    /// </summary>
    public static class SequenceParser
    {
        /// <summary>
        /// Parses the provided FASTA sequence data.
        /// </summary>
        /// <param name="data">The string to parse.</param>
        /// <returns>
        /// A new <see cref="ISequence"/> object parsed from the provided
        /// data string. Depending on the data passed in, this could either be
        /// an <see cref="AminoAcidSequence"/> object or a
        /// <see cref="NucleicAcidSequence"/> object. If the sequence is
        /// ambiguous, the latter type is assumed and returned.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="data"/> is empty or all whitespace.
        /// -or-
        /// <paramref name="data"/> is in an incorrect format.
        /// </exception>
        public static ISequence Parse(string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "The sequence data to parse cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(data))
            {
                throw new FormatException("The sequence data to parse cannot be empty or all whitespace.");
            }

            if (NucleicAcidSequence.TryParse(data, out NucleicAcidSequence nucleicAcidSequenceData))
            {
                return nucleicAcidSequenceData;
            }
            else if (AminoAcidSequence.TryParse(data, out AminoAcidSequence aminoAcidSequenceData))
            {
                return aminoAcidSequenceData;
            }
            else
            {
                throw new FormatException("The provided string is not a valid FASTA amino or nucleic acid sequence.");
            }
        }

        /// <summary>
        /// Parses the provided FASTA sequence data.
        /// </summary>
        /// <param name="lines">The collection of lines to parse.</param>
        /// <returns>
        /// A new <see cref="ISequence"/> object parsed from the provided
        /// data string. Depending on the data passed in, this could either be
        /// an <see cref="AminoAcidSequence"/> object or a
        /// <see cref="NucleicAcidSequence"/> object. If the sequence is
        /// ambiguous, the latter type is assumed and returned.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="lines"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="lines"/> is empty or all whitespace.
        /// -or-
        /// <paramref name="lines"/> is in an incorrect format.
        /// </exception>
        public static ISequence Parse(IEnumerable<string> lines)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines), "The collection of sequence data lines to parse cannot be null.");
            }

            try
            {
                return Parse(string.Join(Environment.NewLine, lines));
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message, ex);
            }
        }
    }
}
