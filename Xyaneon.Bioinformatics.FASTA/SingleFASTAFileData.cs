using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Bioinformatics.FASTA.Sequences;
using Xyaneon.Bioinformatics.FASTA.Extensions;

namespace Xyaneon.Bioinformatics.FASTA
{
    /// <summary>
    /// Represents one sequence and associated description data in a FASTA file.
    /// </summary>
    public sealed class SingleFASTAFileData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleFASTAFileData"/> class.
        /// </summary>
        /// <param name="header">This sequence's header.</param>
        /// <param name="data">The actual sequence data.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="header"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="data"/> is <see langword="null"/>.
        /// </exception>
        public SingleFASTAFileData(Header header, ISequence data)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header), "The header cannot be null.");
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "The sequence data cannot be null.");
            }

            Header = header;
            Data = data;
        }

        /// <summary>
        /// Gets this sequence's header.
        /// </summary>
        public Header Header { get; }

        /// <summary>
        /// Gets the actual sequence data.
        /// </summary>
        public ISequence Data { get; }

        /// <summary>
        /// Parses the provided string as a new <see cref="SingleFASTAFileData"/>
        /// instance.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <returns>A new <see cref="SingleFASTAFileData"/> instance parsed from <paramref name="s"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="s"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="s"/> is not of the correct format.
        /// </exception>
        public static SingleFASTAFileData Parse(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s), "The sequence string to parse cannot be null.");
            }

            return ParseBase(s.SplitIntoNonBlankLines());
        }

        /// <summary>
        /// Parses the provided collection of strings as a new
        /// <see cref="SingleFASTAFileData"/> instance.
        /// </summary>
        /// <param name="lines">The collection of lines to parse.</param>
        /// <returns>A new <see cref="SingleFASTAFileData"/> instance parsed from <paramref name="lines"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="lines"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="lines"/> is not of the correct format.
        /// </exception>
        public static SingleFASTAFileData Parse(IEnumerable<string> lines)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines), "The collection of sequence lines to parse cannot be null.");
            }

            return ParseBase(lines);
        }

        /// <summary>
        /// Gets the interleaved (multiline sequence) representation of this
        /// <see cref="SingleFASTAFileData"/> object.
        /// </summary>
        /// <param name="lineLength">An optional maximum length for each line in the sequence. If omitted, this defaults to <see cref="Constants.DefaultLineLength"/>.</param>
        /// <returns>The interleaved file lines as a collection of strings.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="lineLength"/> is less than one.
        /// </exception>
        /// <seealso cref="ToSequentialLines"/>
        public IEnumerable<string> ToInterleavedLines(int lineLength = Constants.DefaultLineLength)
        {
            if (lineLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lineLength), lineLength, SequenceUtility.ArgumentOutOfRangeException_LineLengthLessThanOne);
            }

            yield return Header.ToString();
            foreach (string line in Data.ToLines(lineLength))
            {
                yield return line;
            }
        }

        /// <summary>
        /// Gets the sequential (single-line sequence) representation of this
        /// <see cref="SingleFASTAFileData"/> object.
        /// </summary>
        /// <returns>The sequential file lines as a collection of strings.</returns>
        /// <seealso cref="ToInterleavedLines(int)"/>
        public IEnumerable<string> ToSequentialLines()
        {
            yield return Header.ToString();
            yield return Data.ToString();
        }

        private static SingleFASTAFileData ParseBase(IEnumerable<string> lines)
        {
            try
            {
                IEnumerable<string> nonBlankLines = lines.Where(line => !string.IsNullOrWhiteSpace(line));

                Header header = Header.Parse(nonBlankLines.First());
                ISequence sequenceData = SequenceParser.Parse(nonBlankLines.Skip(1));

                return new SingleFASTAFileData(header, sequenceData);
            }
            catch (FormatException ex)
            {
                throw new FormatException("The collection of sequence lines are not in the correct format.", ex);
            }
        }
    }
}
