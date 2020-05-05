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

            IList<string> headerParts = ExtractHeaderParts(headerLine);
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

        private static IList<string> ExtractHeaderParts(string headerLine)
        {
            return headerLine.TrimStart(HeaderStartCharacter)
                .Trim()
                .Split(ItemsSeparator[0])
                .Select(str => str.Trim())
                .ToList();
        }

        private static IEnumerable<HeaderItem> ParseHeaderItems(IList<string> headerParts)
        {
            int index = 0;

            while (index < headerParts.Count)
            {
                switch (headerParts[index])
                {
                    case Constants.Codes.BackboneMolType:
                        yield return new BackboneMolTypeIdentifier(int.Parse(headerParts[++index]));
                        break;
                    case Constants.Codes.BackboneSeqID:
                        yield return new BackboneSeqIdIdentifier(int.Parse(headerParts[++index]));
                        break;
                    case Constants.Codes.DDBJ:
                        {
                            string accession = headerParts[++index];
                            string locus = headerParts[++index];
                            yield return new DDBJIdentifier(accession, locus);
                        }
                        break;
                    case Constants.Codes.EMBL:
                        {
                            string accession = headerParts[++index];
                            string locus = headerParts[++index];
                            yield return new EMBLIdentifier(accession, locus);
                        }
                        break;
                    case Constants.Codes.ImportId:
                        yield return new ImportIdIdentifier(int.Parse(headerParts[++index]));
                        break;
                    case Constants.Codes.GenBank:
                        {
                            string accession = headerParts[++index];
                            string locus = headerParts[++index];
                            yield return new GenBankIdentifier(accession, locus);
                        }
                        break;
                    case Constants.Codes.GeneralDatabaseReference:
                        {
                            string database = headerParts[++index];
                            string value = headerParts[++index];
                            yield return new GeneralDatabaseReferenceIdentifier(database, value);
                        }
                        break;
                    case Constants.Codes.GenInfoIntegratedDatabase:
                        yield return new IntegratedDatabaseIdentifier(int.Parse(headerParts[++index]));
                        break;
                    case Constants.Codes.Local:
                        yield return new LocalIdentifier(headerParts[++index]);
                        break;
                    case Constants.Codes.Patent:
                        {
                            string country = headerParts[++index];
                            string patent = headerParts[++index];
                            string sequenceNumber = headerParts[++index];
                            yield return new PatentIdentifier(country, patent, sequenceNumber);
                        }
                        break;
                    case Constants.Codes.PDB:
                        {
                            string accession = headerParts[++index];
                            string name = headerParts[++index];
                            yield return new PDBIdentifier(accession, name);
                        }
                        break;
                    case Constants.Codes.PIR:
                        {
                            string accession = headerParts[++index];
                            string name = headerParts[++index];
                            yield return new PIRIdentifier(accession, name);
                        }
                        break;
                    case Constants.Codes.PRF:
                        {
                            string accession = headerParts[++index];
                            string name = headerParts[++index];
                            yield return new PRFIdentifier(accession, name);
                        }
                        break;
                    case Constants.Codes.PreGrantPatent:
                        {
                            string country = headerParts[++index];
                            string applicationNumber = headerParts[++index];
                            string sequenceNumber = headerParts[++index];
                            yield return new PreGrantPatentIdentifier(country, applicationNumber, sequenceNumber);
                        }
                        break;
                    case Constants.Codes.RefSeq:
                        {
                            string accession = headerParts[++index];
                            string name = headerParts[++index];
                            yield return new RefSeqIdentifier(accession, name);
                        }
                        break;
                    case Constants.Codes.SWISSPROT:
                        {
                            string accession = headerParts[++index];
                            string name = headerParts[++index];
                            yield return new SWISSPROTIdentifier(accession, name);
                        }
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
