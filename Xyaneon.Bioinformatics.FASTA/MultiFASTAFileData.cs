using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Bioinformatics.FASTA.Extensions;
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

        /// <summary>
        /// Parses the provided string as a new <see cref="MultiFASTAFileData"/>
        /// instance.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <returns>A new <see cref="MultiFASTAFileData"/> instance parsed from <paramref name="s"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="s"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="s"/> is not of the correct format.
        /// </exception>
        public static MultiFASTAFileData Parse(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s), "The string to parse cannot be null.");
            }

            return ParseBase(s.SplitIntoNonBlankLines());
        }

        /// <summary>
        /// Parses the provided collection of strings as a new
        /// <see cref="MultiFASTAFileData"/> instance.
        /// </summary>
        /// <param name="lines">The collection of lines to parse.</param>
        /// <returns>A new <see cref="MultiFASTAFileData"/> instance parsed from <paramref name="lines"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="lines"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="lines"/> is not of the correct format.
        /// </exception>
        public static MultiFASTAFileData Parse(IEnumerable<string> lines)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines), "The collection of lines to parse cannot be null.");
            }

            return ParseBase(lines);
        }

        private bool AllSequencesAreOfType(Type type)
        {
            return SingleFASTASequences.All(sequence => sequence.Data.GetType() == type);
        }

        private static MultiFASTAFileData ParseBase(IEnumerable<string> lines)
        {
            try
            {
                IEnumerable<string> nonBlankLines = lines.Where(line => !string.IsNullOrWhiteSpace(line));
                IEnumerable<SingleFASTAFileData> sequences = ParseSequences(nonBlankLines.ToList());

                return new MultiFASTAFileData(sequences);
            }
            catch (FormatException ex)
            {
                throw new FormatException("The collection of sequence lines are not in the correct format.", ex);
            }
        }

        private static IEnumerable<SingleFASTAFileData> ParseSequences(IEnumerable<string> lines)
        {
            IEnumerable<IEnumerable<string>> lineGroups = SplitByHeaderLines(lines);
            int sequenceNumber = 1;

            foreach (IEnumerable<string> lineGroup in lineGroups)
            {
                SingleFASTAFileData singleFASTAFileData;

                try
                {
                    singleFASTAFileData = SingleFASTAFileData.Parse(lineGroup);
                }
                catch (FormatException ex)
                {
                    throw new FormatException($"Sequence {sequenceNumber:N0} is in an incorrect format.", ex);
                }

                sequenceNumber++;
                yield return singleFASTAFileData;
            }
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
