using System;

namespace Xyaneon.Bioinformatics.FASTA.Identifiers
{
    /// <summary>
    /// A pre-grant patent FASTA identifier.
    /// </summary>
    public sealed class PreGrantPatentIdentifier : Identifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreGrantPatentIdentifier"/> class.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="applicationNumber">The patent application number.</param>
        /// <param name="sequenceNumber">The sequence number.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="country"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="applicationNumber"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="sequenceNumber"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="country"/> is empty or all whitespace.
        /// -or-
        /// <paramref name="applicationNumber"/> is empty or all whitespace.
        /// -or-
        /// <paramref name="sequenceNumber"/> is empty or all whitespace.
        /// </exception>
        public PreGrantPatentIdentifier(string country, string applicationNumber, string sequenceNumber) : base(Constants.Codes.PreGrantPatent)
        {
            if (country == null)
            {
                throw new ArgumentNullException(nameof(country), "The country cannot be null.");
            }

            if (applicationNumber == null)
            {
                throw new ArgumentNullException(nameof(applicationNumber), "The patent application number cannot be null.");
            }

            if (sequenceNumber == null)
            {
                throw new ArgumentNullException(nameof(sequenceNumber), "The sequence number cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(country))
            {
                throw new ArgumentException("The country cannot be empty or all whitespace.", nameof(country));
            }

            if (string.IsNullOrWhiteSpace(applicationNumber))
            {
                throw new ArgumentException("The patent application number cannot be empty or all whitespace.", nameof(applicationNumber));
            }

            if (string.IsNullOrWhiteSpace(sequenceNumber))
            {
                throw new ArgumentException("The sequence number cannot be empty or all whitespace.", nameof(sequenceNumber));
            }

            Country = country;
            ApplicationNumber = applicationNumber;
            SequenceNumber = sequenceNumber;
        }

        /// <summary>
        /// Gets the country of this identifier.
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Gets the patent application number of this identifier.
        /// </summary>
        public string ApplicationNumber { get; }

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
            return $"{Code}|{Country}|{ApplicationNumber}|{SequenceNumber}";
        }
    }
}
