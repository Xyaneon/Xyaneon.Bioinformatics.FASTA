using System;

namespace Xyaneon.Bioinformatics.FASTA.Identifiers
{
    /// <summary>
    /// A FASTA file identifier.
    /// This class cannot be directly instantiated.
    /// </summary>
    public abstract class Identifier : HeaderItem
    {
        /// <summary>
        /// The character used to separate parts of an identifier.
        /// </summary>
        public const char IdentifierPartSeparator = '|';

        /// <summary>
        /// Initializes a new instance of the <see cref="Identifier"/> class.
        /// </summary>
        /// <param name="code">The code indicating which type of identifier this is.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="code"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="code"/> is empty or all whitespace.
        /// </exception>
        public Identifier(string code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code), "The identifier code cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("The identifier code cannot be empty or all whitespace.", nameof(code));
            }

            Code = code.Trim();
        }

        /// <summary>
        /// Gets the code indicating which type of identifier this is, as
        /// would be shown in the FASTA file header line.
        /// </summary>
        public string Code { get; }
    }
}
