using System;

namespace Xyaneon.Bioinformatics.FASTA.Identifiers
{
    /// <summary>
    /// An EMBL FASTA identifier.
    /// </summary>
    public sealed class EMBLIdentifier : Identifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EMBLIdentifier"/> class.
        /// </summary>
        /// <param name="accession">The accession number.</param>
        /// <param name="locus">The locus number.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="accession"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="locus"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="accession"/> is empty or all whitespace.
        /// -or-
        /// <paramref name="locus"/> is empty or all whitespace.
        /// </exception>
        public EMBLIdentifier(string accession, string locus) : base(Constants.Codes.EMBL)
        {
            if (accession == null)
            {
                throw new ArgumentNullException(nameof(accession), "The accession number cannot be null.");
            }

            if (locus == null)
            {
                throw new ArgumentNullException(nameof(locus), "The locus number cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(accession))
            {
                throw new ArgumentException("The accession number cannot be empty or all whitespace.", nameof(accession));
            }

            if (string.IsNullOrWhiteSpace(locus))
            {
                throw new ArgumentException("The locus number cannot be empty or all whitespace.", nameof(locus));
            }

            Accession = accession;
            Locus = locus;
        }

        /// <summary>
        /// Gets the accession number of this identifier.
        /// </summary>
        public string Accession { get; }

        /// <summary>
        /// Gets the locus number of this identifier.
        /// </summary>
        public string Locus { get; }

        /// <summary>
        /// Returns the string representation of this identifier, as it would
        /// appear in the FASTA file.
        /// </summary>
        /// <returns>The string representation of this identifier.</returns>
        public override string ToString()
        {
            return $"{Code}|{Accession}|{Locus}";
        }
    }
}
