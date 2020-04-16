using System;

namespace Xyaneon.Bioinformatics.FASTA
{
    /// <summary>
    /// Represents a regular text description in a FASTA file sequence header.
    /// </summary>
    /// <seealso cref="Identifiers.Identifier"/>
    public sealed class Description : HeaderItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Description"/> class.
        /// </summary>
        /// <param name="text">The description text.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="text"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="text"/> contains a pipe ("|") character.
        /// </exception>
        public Description(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text), "The description text cannot be null.");
            }

            if (text.Contains("|"))
            {
                throw new ArgumentException("The pipe (\"|\") character is not permitted in the description.");
            }

            Text = text;
        }

        /// <summary>
        /// Gets the text in the description.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Returns a string representation of this description.
        /// </summary>
        /// <returns>A string representation of this description.</returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
