using System;

namespace Xyaneon.Bioinformatics.FASTA.Identifiers
{
    /// <summary>
    /// A local (i.e., no database reference) FASTA identifier.
    /// </summary>
    public sealed class LocalIdentifier : Identifier
    {
        private const string CodeValue = "lcl";

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalIdentifier"/> class.
        /// </summary>
        /// <param name="value">The identifier value.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> is empty or all whitespace.
        /// </exception>
        public LocalIdentifier(string value) : base(CodeValue)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "The identifier value cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("The identifier value cannot be empty or all whitespace.", nameof(value));
            }

            Value = value;
        }

        /// <summary>
        /// Gets the value of this identifier.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Returns the string representation of this identifier, as it would
        /// appear in the FASTA file.
        /// </summary>
        /// <returns>The string representation of this identifier.</returns>
        public override string ToString()
        {
            return $"{Code}|{Value}";
        }
    }
}
