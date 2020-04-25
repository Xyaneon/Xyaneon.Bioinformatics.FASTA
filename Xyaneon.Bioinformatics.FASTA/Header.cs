using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.Identifiers.GenInfo;
using Xyaneon.Bioinformatics.FASTA.Utility;

namespace Xyaneon.Bioinformatics.FASTA
{
    /// <summary>
    /// Represents a sequence header in a FASTA file.
    /// </summary>
    public sealed class Header
    {
        /// <summary>
        /// The character which denotes the start of the header line.
        /// </summary>
        public const char HeaderStartCharacter = '>';

        /// <summary>
        /// The separator between individual header items.
        /// </summary>
        public const string ItemsSeparator = "|";

        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        /// <param name="headerItem">The item from this header.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="headerItem"/> is <see langword="null"/>.
        /// </exception>
        public Header(HeaderItem headerItem)
        {
            if (headerItem == null)
            {
                throw new ArgumentNullException(nameof(headerItem), "The header item cannot be null.");
            }

            Items = ListUtility.CreateSingleElementList(headerItem).AsReadOnly();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        /// <param name="headerItems">The list of items from this header.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="headerItems"/> is <see langword="null"/>.
        /// </exception>
        public Header(IEnumerable<HeaderItem> headerItems)
        {
            if (headerItems == null)
            {
                throw new ArgumentNullException(nameof(headerItems), "The collection of header items cannot be null.");
            }

            Items = new List<HeaderItem>(headerItems).AsReadOnly();
        }

        /// <summary>
        /// Gets a read-only list of all items in this header.
        /// </summary>
        public IReadOnlyList<HeaderItem> Items { get; }

        /// <summary>
        /// Parses the provided FASTA sequence header line.
        /// </summary>
        /// <param name="headerLine">The header line to parse.</param>
        /// <returns>
        /// A new <see cref="Header"/> object parsed from the provided header
        /// line string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="headerLine"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="headerLine"/> is empty or all whitespace.
        /// -or-
        /// <paramref name="headerLine"/> is in an incorrect format.
        /// </exception>
        public static Header Parse(string headerLine)
        {
            if (headerLine == null)
            {
                throw new ArgumentNullException(nameof(headerLine), "The header line to parse cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(headerLine))
            {
                throw new FormatException("The header line to parse cannot be empty or all whitespace.");
            }

            if (!headerLine.StartsWith($"{HeaderStartCharacter}"))
            {
                throw new FormatException("The header line to parse does not start with the required start character.");
            }

            IList<string> headerParts = headerLine.TrimStart(HeaderStartCharacter)
                .Trim()
                .Split(ItemsSeparator[0])
                .Select(str => str.Trim())
                .ToList();

            IEnumerable<HeaderItem> headerItems = ParseHeaderItems(headerParts);

            return new Header(headerItems);
        }

        /// <summary>
        /// Returns a string representation of this header.
        /// </summary>
        /// <returns>A string representation of this header.</returns>
        public override string ToString()
        {
            return $"{HeaderStartCharacter}{string.Join(ItemsSeparator, Items)}";
        }

        private static IEnumerable<HeaderItem> ParseHeaderItems(IList<string> headerParts)
        {
            int index = 0;

            while (index < headerParts.Count)
            {
                switch (headerParts[index])
                {
                    case "bbm":
                        yield return new BackboneMolTypeIdentifier(int.Parse(headerParts[++index]));
                        break;
                    case "bbs":
                        yield return new BackboneSeqIdIdentifier(int.Parse(headerParts[++index]));
                        break;
                    case "gim":
                        yield return new ImportIdIdentifier(int.Parse(headerParts[++index]));
                        break;
                    case "gb":
                        string accession = headerParts[++index];
                        string locus = headerParts[++index];
                        yield return new GenBankIdentifier(accession, locus);
                        break;
                    case "lcl":
                        yield return new LocalIdentifier(headerParts[++index]);
                        break;
                    default:
                        yield return new Description(headerParts[index]);
                        break;
                }

                index++;
            }
        }
    }
}
