using System;

namespace Xyaneon.Bioinformatics.FASTA.Identifiers
{
    /// <summary>
    /// A patent FASTA identifier.
    /// </summary>
    public sealed class PatentIdentifier : Identifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatentIdentifier"/> class.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="patent">The patent.</param>
        /// <param name="sequenceNumber">The sequence number.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="country"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="patent"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="sequenceNumber"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="country"/> is empty or all whitespace.
        /// -or-
        /// <paramref name="patent"/> is empty or all whitespace.
        /// -or-
        /// <paramref name="sequenceNumber"/> is empty or all whitespace.
        /// </exception>
        public PatentIdentifier(string country, string patent, string sequenceNumber) : base(Constants.Codes.Patent)
        {
            if (country == null)
            {
                throw new ArgumentNullException(nameof(country), "The country cannot be null.");
            }

            if (patent == null)
            {
                throw new ArgumentNullException(nameof(patent), "The patent cannot be null.");
            }

            if (sequenceNumber == null)
            {
                throw new ArgumentNullException(nameof(sequenceNumber), "The sequence number cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(country))
            {
                throw new ArgumentException("The country cannot be empty or all whitespace.", nameof(country));
            }

            if (string.IsNullOrWhiteSpace(patent))
            {
                throw new ArgumentException("The patent cannot be empty or all whitespace.", nameof(patent));
            }

            if (string.IsNullOrWhiteSpace(sequenceNumber))
            {
                throw new ArgumentException("The sequence number cannot be empty or all whitespace.", nameof(sequenceNumber));
            }

            Country = country;
            Patent = patent;
            SequenceNumber = sequenceNumber;
        }

        /// <summary>
        /// Gets the country of this identifier.
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Gets the patent of this identifier.
        /// </summary>
        public string Patent { get; }

        /// <summary>
        /// Gets the sequence number of this identifier.
        /// </summary>
        public string SequenceNumber { get; }

        /// <summary>
        /// Returns the string representation of this identifier, as it would
        /// appear in the FASTA file.
        /// </summary>
        /// <returns>The string representation of this identifier.</returns>
        public override string ToString()
        {
            return $"{Code}|{Country}|{Patent}|{SequenceNumber}";
        }
    }
}
