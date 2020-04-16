using System;
using System.Collections.Generic;
using Xyaneon.Bioinformatics.FASTA.Extensions;

namespace Xyaneon.Bioinformatics.FASTA.Data
{
    /// <summary>
    /// Contains the actual data stored in a FASTA file sequence.
    /// This class cannot be directly instantiated.
    /// </summary>
    public abstract class SequenceData
    {
        /// <summary>
        /// The default maximum length of each line of a multiline sequence.
        /// </summary>
        public const int DefaultLineLength = 80;

        private const string ArgumentOutOfRangeException_LineLengthLessThanOne = "The length of each line cannot be less than one.";

        /// <summary>
        /// Gets a string containing all characters in the sequence data.
        /// </summary>
        public abstract string Characters { get; }

        /// <summary>
        /// Returns this sequence as an enumerable collection of lines.
        /// </summary>
        /// <param name="lineLength">The maximum length of each line. If omitted, this is <see cref="DefaultLineLength"/>.</param>
        /// <returns>This sequence as an enumerable collection of lines.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="lineLength"/> is less than one.
        /// </exception>
        /// <seealso cref="ToMultilineString(int)"/>
        public IEnumerable<string> ToLines(int lineLength = DefaultLineLength)
        {
            if (lineLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lineLength), lineLength, ArgumentOutOfRangeException_LineLengthLessThanOne);
            }

            return ToLinesBase(lineLength);
        }

        /// <summary>
        /// Returns a multiline string representation of this sequence.
        /// </summary>
        /// <param name="lineLength">The maximum length of each line. If omitted, this is <see cref="DefaultLineLength"/>.</param>
        /// <returns>A multiline string representation of this sequence.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="lineLength"/> is less than one.
        /// </exception>
        /// <seealso cref="ToLines(int)"/>
        /// <seealso cref="ToString"/>
        public string ToMultilineString(int lineLength = DefaultLineLength)
        {
            if (lineLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lineLength), lineLength, ArgumentOutOfRangeException_LineLengthLessThanOne);
            }

            if (lineLength == 1)
            {
                return ToString();
            }

            return string.Join(Environment.NewLine, ToLinesBase(lineLength));
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

        private IEnumerable<string> ToLinesBase(int lineLength = DefaultLineLength)
        {
            return Characters.SplitBy(lineLength);
        }
    }
}
