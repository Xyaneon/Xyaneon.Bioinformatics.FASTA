using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xyaneon.Bioinformatics.FASTA.Extensions;

namespace Xyaneon.Bioinformatics.FASTA.ActualSequences
{
    /// <summary>
    /// Contains nucleic acid sequence data for a FASTA file sequence.
    /// </summary>
    /// <seealso cref="AminoAcidSequence"/>
    public readonly struct NucleicAcidSequence: IActualSequence
    {
        private static readonly Regex ValidCharactersRegex = new Regex(@"^[ACGTURYKMSWBDHVN-]*$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="NucleicAcidSequence"/> class.
        /// </summary>
        /// <param name="characters">The characters in the sequence data.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="characters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="characters"/> contains invalid characters.
        /// </exception>
        private NucleicAcidSequence(string characters)
        {
            if (characters == null)
            {
                throw new ArgumentNullException(nameof(characters), "The characters string for the nucleic acid sequence data cannot be null.");
            }

            string cleanedUpCharacters = characters.RemoveAllWhitespace();

            if (!IsValidSequence(cleanedUpCharacters))
            {
                throw new ArgumentException("The supplied characters are not a valid nucleic acid sequence.", nameof(characters));
            }

            Characters = cleanedUpCharacters.ToUpperInvariant();
        }

        #region ISequenceData

        /// <summary>
        /// Gets a string containing all characters in the sequence data.
        /// </summary>
        public string Characters { get; }

        /// <summary>
        /// Returns this sequence as an enumerable collection of lines.
        /// </summary>
        /// <param name="lineLength">The maximum length of each line. If omitted, this is <see cref="Constants.DefaultLineLength"/>.</param>
        /// <returns>This sequence as an enumerable collection of lines.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="lineLength"/> is less than one.
        /// </exception>
        /// <seealso cref="ToMultilineString(int)"/>
        public IEnumerable<string> ToLines(int lineLength = Constants.DefaultLineLength)
        {
            if (lineLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lineLength), lineLength, SequenceUtility.ArgumentOutOfRangeException_LineLengthLessThanOne);
            }

            return SequenceUtility.ToLines(Characters, lineLength);
        }

        /// <summary>
        /// Returns a multiline string representation of this sequence.
        /// </summary>
        /// <param name="lineLength">The maximum length of each line. If omitted, this is <see cref="Constants.DefaultLineLength"/>.</param>
        /// <returns>A multiline string representation of this sequence.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="lineLength"/> is less than one.
        /// </exception>
        /// <seealso cref="ToLines(int)"/>
        /// <seealso cref="ToString"/>
        public string ToMultilineString(int lineLength = Constants.DefaultLineLength)
        {
            if (lineLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lineLength), lineLength, SequenceUtility.ArgumentOutOfRangeException_LineLengthLessThanOne);
            }

            if (lineLength == 1)
            {
                return ToString();
            }

            return string.Join(Environment.NewLine, SequenceUtility.ToLines(Characters, lineLength));
        }

        #endregion // ISequenceData

        /// <summary>
        /// Converts a string representation of a FASTA file nucelic acid
        /// sequence to its <see cref="NucleicAcidSequence"/> equivalent.
        /// </summary>
        /// <param name="s">A string containing the sequence to convert.</param>
        /// <returns>
        /// A <see cref="NucleicAcidSequence"/> value equivalent of the
        /// sequence contained in <paramref name="s"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="s"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="s"/> is not in the correct format.
        /// </exception>
        /// <seealso cref="TryParse(string, out NucleicAcidSequence)"/>
        public static NucleicAcidSequence Parse(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s), "The characters string for the nucleic acid sequence data cannot be null.");
            }

            string cleanedUpCharacters = s.RemoveAllWhitespace();

            if (!IsValidSequence(cleanedUpCharacters))
            {
                throw new FormatException("The supplied characters are not a valid nucleic acid sequence.");
            }

            return new NucleicAcidSequence(cleanedUpCharacters.ToUpperInvariant());
        }

        /// <summary>
        /// Converts a string representation of a FASTA file nucelic acid
        /// sequence to its <see cref="NucleicAcidSequence"/> equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A string containing the sequence to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the
        /// <see cref="NucleicAcidSequence"/> value equivalent of the
        /// sequence contained in <paramref name="s"/>, if the conversion
        /// succeeded, or an empty sequence if the conversion failed. The
        /// conversion fails if the <paramref name="s"/> parameter is
        /// <see langword="null"/>, empty, all whitespace, or is not of the
        /// correct format. This parameter is passed uninitialized; any value
        /// originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="s"/> was converted
        /// successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <seealso cref="Parse(string)"/>
        public static bool TryParse(string s, out NucleicAcidSequence result)
        {
            if (s == null)
            {
                result = default;
                return false;
            }

            string cleanedUpCharacters = s.RemoveAllWhitespace();

            if (!IsValidSequence(cleanedUpCharacters))
            {
                result = default;
                return false;
            }

            result = new NucleicAcidSequence(cleanedUpCharacters.ToUpperInvariant());
            return true;
        }

        /// <summary>
        /// Returns a single-line string representation of this sequence.
        /// </summary>
        /// <returns>A single-line string representation of this sequence.</returns>
        /// <seealso cref="ToMultilineString(int)"/>
        public override string ToString()
        {
            return Characters;
        }

        private static bool IsValidSequence(string data)
        {
            return ValidCharactersRegex.IsMatch(data);
        }
    }
}
