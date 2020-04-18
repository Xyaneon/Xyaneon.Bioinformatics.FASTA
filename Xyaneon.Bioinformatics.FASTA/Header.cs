using System;
using System.Collections.Generic;
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
        /// Returns a string representation of this header.
        /// </summary>
        /// <returns>A string representation of this header.</returns>
        public override string ToString()
        {
            return $"{HeaderStartCharacter}{string.Join(ItemsSeparator, Items)}";
        }
    }
}
