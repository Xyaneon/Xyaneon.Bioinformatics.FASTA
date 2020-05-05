using System;

namespace Xyaneon.Bioinformatics.FASTA.Identifiers
{
    /// <summary>
    /// A PDB FASTA identifier.
    /// </summary>
    public sealed class PDBIdentifier : Identifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PDBIdentifier"/> class.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="chain">The chain.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="entry"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="chain"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="entry"/> is empty or all whitespace.
        /// -or-
        /// <paramref name="chain"/> is empty or all whitespace.
        /// </exception>
        public PDBIdentifier(string entry, string chain) : base(Constants.Codes.PDB)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry), "The accession number cannot be null.");
            }

            if (chain == null)
            {
                throw new ArgumentNullException(nameof(chain), "The name cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(entry))
            {
                throw new ArgumentException("The accession number cannot be empty or all whitespace.", nameof(entry));
            }

            if (string.IsNullOrWhiteSpace(chain))
            {
                throw new ArgumentException("The name cannot be empty or all whitespace.", nameof(chain));
            }

            Entry = entry;
            Chain = chain;
        }

        /// <summary>
        /// Gets the entry of this identifier.
        /// </summary>
        public string Entry { get; }

        /// <summary>
        /// Gets the chain of this identifier.
        /// </summary>
        public string Chain { get; }

        /// <summary>
        /// Returns the string representation of this identifier, as it would
        /// appear in the FASTA file.
        /// </summary>
        /// <returns>The string representation of this identifier.</returns>
        public override string ToString()
        {
            return $"{Code}|{Entry}|{Chain}";
        }
    }
}
