using System;

namespace Xyaneon.Bioinformatics.FASTA.Identifiers
{
    /// <summary>
    /// A TrEMBL FASTA identifier.
    /// </summary>
    public sealed class TrEMBLIdentifier : Identifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrEMBLIdentifier"/> class.
        /// </summary>
        /// <param name="accession">The accession number.</param>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="accession"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="name"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="accession"/> is empty or all whitespace.
        /// -or-
        /// <paramref name="name"/> is empty or all whitespace.
        /// </exception>
        public TrEMBLIdentifier(string accession, string name) : base(Constants.Codes.TrEMBL)
        {
            if (accession == null)
            {
                throw new ArgumentNullException(nameof(accession), "The accession number cannot be null.");
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "The name cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(accession))
            {
                throw new ArgumentException("The accession number cannot be empty or all whitespace.", nameof(accession));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name cannot be empty or all whitespace.", nameof(name));
            }

            Accession = accession;
            Name = name;
        }

        /// <summary>
        /// Gets the accession number of this identifier.
        /// </summary>
        public string Accession { get; }

        /// <summary>
        /// Gets the name of this identifier.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Returns the string representation of this identifier, as it would
        /// appear in the FASTA file.
        /// </summary>
        /// <returns>The string representation of this identifier.</returns>
        public override string ToString()
        {
            return $"{Code}|{Accession}|{Name}";
        }
    }
}
