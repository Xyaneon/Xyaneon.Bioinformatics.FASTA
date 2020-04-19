using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Bioinformatics.FASTA.Identifiers.GenInfo;

namespace Xyaneon.Bioinformatics.FASTA.Identifiers
{
    /// <summary>
    /// Provides methods for parsing strings as FASTA identifiers.
    /// </summary>
    public static class IdentifierParser
    {
        private static readonly char[] Separators = new char[] { Identifier.IdentifierPartSeparator };

        /// <summary>
        /// Parses the provided string as a FASTA identifier.
        /// </summary>
        /// <param name="identifierString">The string to parse.</param>
        /// <returns>A new <see cref="Identifier"/> parsed from the provided string.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="identifierString"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// <paramref name="identifierString"/> is in an incorrect format.
        /// Check the <see cref="Exception.InnerException"/> property value
        /// for the underlying error details.
        /// </exception>
        public static Identifier Parse(string identifierString)
        {
            if (identifierString == null)
            {
                throw new ArgumentNullException(nameof(identifierString), "The identifier string to parse cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(identifierString))
            {
                throw new ArgumentException("The identifier string to parse cannot be empty or all whitespace.", nameof(identifierString));
            }

            List<string> identifierParts = SplitIdentifierStringIntoParts(identifierString);

            try
            {
                switch (identifierParts[0])
                {
                    case "bbm":
                        return new BackboneMolTypeIdentifier(int.Parse(identifierParts[1]));
                    case "bbs":
                        return new BackboneSeqIdIdentifier(int.Parse(identifierParts[1]));
                    case "gb":
                        return new GenBankIdentifier(identifierParts[1], identifierParts[2]);
                    case "gim":
                        return new ImportIdIdentifier(int.Parse(identifierParts[1]));
                    case "lcl":
                        return new LocalIdentifier(identifierParts[1]);
                    default:
                        throw new NotSupportedException($"\"{identifierParts[0]}\" is not a recognized identifier code.");
                }
            }
            catch (Exception ex)
            {
                throw new FormatException("The supplied string could not be parsed as a valid FASTA identifier.", ex);
            }
        }

        private static List<string> SplitIdentifierStringIntoParts(string identifierString)
        {
            return identifierString.Split(Separators, StringSplitOptions.None)
                .Select(str => str.Trim())
                .ToList();
        }
    }
}
