using System;

namespace Xyaneon.Bioinformatics.FASTA.Identifiers
{
    /// <summary>
    /// A general database reference FASTA identifier.
    /// </summary>
    public sealed class GeneralDatabaseReferenceIdentifier : Identifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralDatabaseReferenceIdentifier"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="database"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="value"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="database"/> is empty or all whitespace.
        /// -or-
        /// <paramref name="value"/> is empty or all whitespace.
        /// </exception>
        public GeneralDatabaseReferenceIdentifier(string database, string value) : base(Constants.Codes.GeneralDatabaseReference)
        {
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database), "The database cannot be null.");
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "The value cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(database))
            {
                throw new ArgumentException("The database cannot be empty or all whitespace.", nameof(database));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("The value cannot be empty or all whitespace.", nameof(value));
            }

            Database = database;
            Value = value;
        }

        /// <summary>
        /// Gets the database of this identifier.
        /// </summary>
        public string Database { get; }

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
            return $"{Code}|{Database}|{Value}";
        }
    }
}
