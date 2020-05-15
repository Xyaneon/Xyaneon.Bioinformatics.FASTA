using System.Collections.Generic;

namespace Xyaneon.Bioinformatics.FASTA.ActualSequences
{
    /// <summary>
    /// Interface for objects representing the actual FASTA sequence data.
    /// </summary>
    public interface IActualSequence
    {
        /// <summary>
        /// Gets a string containing all characters in the sequence data.
        /// </summary>
        string Characters { get; }

        /// <summary>
        /// Returns this sequence as an enumerable collection of lines.
        /// </summary>
        /// <param name="lineLength">The maximum length of each line. If omitted, this is <see cref="Constants.DefaultLineLength"/>.</param>
        /// <returns>This sequence as an enumerable collection of lines.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="lineLength"/> is less than one.
        /// </exception>
        /// <seealso cref="ToMultilineString(int)"/>
        IEnumerable<string> ToLines(int lineLength = Constants.DefaultLineLength);

        /// <summary>
        /// Returns a multiline string representation of this sequence.
        /// </summary>
        /// <param name="lineLength">The maximum length of each line. If omitted, this is <see cref="Constants.DefaultLineLength"/>.</param>
        /// <returns>A multiline string representation of this sequence.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="lineLength"/> is less than one.
        /// </exception>
        /// <seealso cref="ToLines(int)"/>
        string ToMultilineString(int lineLength = Constants.DefaultLineLength);
    }
}
