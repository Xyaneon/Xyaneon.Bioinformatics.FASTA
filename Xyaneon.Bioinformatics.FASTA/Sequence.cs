using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Bioinformatics.FASTA.ActualSequences;
using Xyaneon.Bioinformatics.FASTA.Extensions;

namespace Xyaneon.Bioinformatics.FASTA
{
    /// <summary>
    /// Represents one sequence and associated description data in a FASTA file.
    /// </summary>
    /// <remarks>
    /// This is not to be confused with the <see cref="IActualSequence"/>
    /// interface, which contains only the actual sequence data and not the
    /// description data.
    /// </remarks>
    public sealed class Sequence
    {
        private const string ArgumentNullException_Lines = "The collection of lines to parse cannot be null.";

        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence"/> class.
        /// </summary>
        /// <param name="header">This sequence's header.</param>
        /// <param name="data">The actual sequence data.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="header"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="data"/> is <see langword="null"/>.
        /// </exception>
        public Sequence(Header header, IActualSequence data)
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
        public IActualSequence Data { get; }

        /// <summary>
        /// Parses the provided string as a new <see cref="Sequence"/>
        /// instance.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <returns>A new <see cref="Sequence"/> instance parsed from <paramref name="s"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="s"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="s"/> is not of the correct format.
        /// </exception>
        public static Sequence Parse(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s), "The sequence string to parse cannot be null.");
            }

            return ParseBase(s.SplitIntoNonBlankLines());
        }

        /// <summary>
        /// Parses the provided collection of strings as a new
        /// <see cref="Sequence"/> instance.
        /// </summary>
        /// <param name="lines">The collection of lines to parse.</param>
        /// <returns>A new <see cref="Sequence"/> instance parsed from <paramref name="lines"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="lines"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="lines"/> is not of the correct format.
        /// </exception>
        public static Sequence Parse(IEnumerable<string> lines)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines), "The collection of sequence lines to parse cannot be null.");
            }

            return ParseBase(lines);
        }

        /// <summary>
        /// Parses the provided FASTA sequence data for a multiple sequences.
        /// </summary>
        /// <param name="lines">The collection of lines to parse.</param>
        /// <returns>
        /// A new enumerable collection of the parsed
        /// <see cref="Sequence"/> objects.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="lines"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="lines"/> is empty or all whitespace.
        /// -or-
        /// <paramref name="lines"/> is in an incorrect format.
        /// </exception>
        public static IEnumerable<Sequence> ParseMultiple(IEnumerable<string> lines)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines), ArgumentNullException_Lines);
            }

            try
            {
                return ParseMultipleBase(lines);
            }
            catch (FormatException ex)
            {
                throw new FormatException("The collection of sequence lines are not in the correct format.", ex);
            }
        }

        /// <summary>
        /// Gets the interleaved (multiline sequence) representation of this
        /// <see cref="Sequence"/> object.
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
        /// <see cref="Sequence"/> object.
        /// </summary>
        /// <returns>The sequential file lines as a collection of strings.</returns>
        /// <seealso cref="ToInterleavedLines(int)"/>
        public IEnumerable<string> ToSequentialLines()
        {
            yield return Header.ToString();
            yield return Data.ToString();
        }

        private static Sequence ParseBase(IEnumerable<string> lines)
        {
            try
            {
                IEnumerable<string> nonBlankLines = lines.Where(line => !string.IsNullOrWhiteSpace(line));

                Header header = Header.Parse(nonBlankLines.First());
                IActualSequence sequenceData = ActualSequenceParser.Parse(nonBlankLines.Skip(1));

                return new Sequence(header, sequenceData);
            }
            catch (FormatException ex)
            {
                throw new FormatException("The collection of sequence lines are not in the correct format.", ex);
            }
        }

        private static IEnumerable<Sequence> ParseMultipleBase(IEnumerable<string> lines)
        {
            IEnumerable<IEnumerable<string>> lineGroups = SplitByHeaderLines(RetrieveNonBlankLines(lines));
            int sequenceNumber = 1;

            foreach (IEnumerable<string> lineGroup in lineGroups)
            {
                Sequence singleFASTAFileData;

                try
                {
                    singleFASTAFileData = Sequence.Parse(lineGroup);
                }
                catch (FormatException ex)
                {
                    throw new FormatException($"Sequence {sequenceNumber:N0} is in an incorrect format.", ex);
                }

                sequenceNumber++;
                yield return singleFASTAFileData;
            }
        }

        private static IEnumerable<string> RetrieveNonBlankLines(IEnumerable<string> lines)
        {
            return lines.Where(line => !string.IsNullOrWhiteSpace(line));
        }

        private static IEnumerable<IEnumerable<string>> SplitByHeaderLines(IEnumerable<string> lines)
        {
            List<string> lineGroup = new List<string>();

            foreach (string line in lines)
            {
                if (line.StartsWith($"{Header.HeaderStartCharacter}") && lineGroup.Count > 0)
                {
                    IEnumerable<string> lineGroupToReturn = new List<string>(lineGroup);
                    lineGroup = new List<string>() { line };

                    yield return lineGroupToReturn;
                }
                else
                {
                    lineGroup.Add(line);
                }
            }

            if (lineGroup.Count > 0)
            {
                yield return lineGroup;
            }
        }
    }
}
