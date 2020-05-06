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
                return ParseIdentifierParts(identifierParts);
            }
            catch (Exception ex)
            {
                throw new FormatException("The supplied string could not be parsed as a valid FASTA identifier.", ex);
            }
        }

        private static ArgumentException CreateIdentifierPartsCountException(string code, int expected, int actual)
        {
            string message = $"The number of identifier parts supplied does not match what is needed for the provided identifier code \"{code}\" (expected {expected}, got {actual}).";
            return new ArgumentException(message);
        }

        private static Identifier ParseIdentifierParts(IList<string> identifierParts)
        {
            if (identifierParts.Count < 1)
            {
                throw new ArgumentException("There must be at least one identifier part supplied for parsing (the identifier code).");
            }

            switch (identifierParts[0])
            {
                case Constants.Codes.BackboneMolType:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.BackboneMolType, 2, identifierParts);
                    return new BackboneMolTypeIdentifier(int.Parse(identifierParts[1]));
                case Constants.Codes.BackboneSeqID:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.BackboneSeqID, 2, identifierParts);
                    return new BackboneSeqIdIdentifier(int.Parse(identifierParts[1]));
                case Constants.Codes.GenBank:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.GenBank, 3, identifierParts);
                    return new GenBankIdentifier(identifierParts[1], identifierParts[2]);
                case Constants.Codes.DDBJ:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.DDBJ, 3, identifierParts);
                    return new DDBJIdentifier(identifierParts[1], identifierParts[2]);
                case Constants.Codes.EMBL:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.EMBL, 3, identifierParts);
                    return new EMBLIdentifier(identifierParts[1], identifierParts[2]);
                case Constants.Codes.GeneralDatabaseReference:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.GeneralDatabaseReference, 3, identifierParts);
                    return new GeneralDatabaseReferenceIdentifier(identifierParts[1], identifierParts[2]);
                case Constants.Codes.GenInfoIntegratedDatabase:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.GenInfoIntegratedDatabase, 2, identifierParts);
                    return new IntegratedDatabaseIdentifier(int.Parse(identifierParts[1]));
                case Constants.Codes.ImportId:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.ImportId, 2, identifierParts);
                    return new ImportIdIdentifier(int.Parse(identifierParts[1]));
                case Constants.Codes.Local:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.Local, 2, identifierParts);
                    return new LocalIdentifier(identifierParts[1]);
                case Constants.Codes.Patent:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.Patent, 4, identifierParts);
                    return new PatentIdentifier(identifierParts[1], identifierParts[2], identifierParts[3]);
                case Constants.Codes.PDB:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.PDB, 3, identifierParts);
                    return new PDBIdentifier(identifierParts[1], identifierParts[2]);
                case Constants.Codes.PIR:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.PIR, 3, identifierParts);
                    return new PIRIdentifier(identifierParts[1], identifierParts[2]);
                case Constants.Codes.PRF:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.PRF, 3, identifierParts);
                    return new PRFIdentifier(identifierParts[1], identifierParts[2]);
                case Constants.Codes.PreGrantPatent:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.PreGrantPatent, 4, identifierParts);
                    return new PreGrantPatentIdentifier(identifierParts[1], identifierParts[2], identifierParts[3]);
                case Constants.Codes.RefSeq:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.RefSeq, 3, identifierParts);
                    return new RefSeqIdentifier(identifierParts[1], identifierParts[2]);
                case Constants.Codes.SWISSPROT:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.SWISSPROT, 3, identifierParts);
                    return new SWISSPROTIdentifier(identifierParts[1], identifierParts[2]);
                case Constants.Codes.ThirdPartyDDBJ:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.ThirdPartyDDBJ, 3, identifierParts);
                    return new ThirdPartyDDBJIdentifier(identifierParts[1], identifierParts[2]);
                case Constants.Codes.ThirdPartyEMBL:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.ThirdPartyEMBL, 3, identifierParts);
                    return new ThirdPartyEMBLIdentifier(identifierParts[1], identifierParts[2]);
                case Constants.Codes.ThirdPartyGenBank:
                    ThrowIfWrongNumberOfPartsForIdentifier(Constants.Codes.ThirdPartyGenBank, 3, identifierParts);
                    return new ThirdPartyGenBankIdentifier(identifierParts[1], identifierParts[2]);
                default:
                    throw new NotSupportedException($"\"{identifierParts[0]}\" is not a recognized identifier code.");
            }
        }

        private static List<string> SplitIdentifierStringIntoParts(string identifierString)
        {
            return identifierString.Split(Separators, StringSplitOptions.None)
                .Select(str => str.Trim())
                .ToList();
        }

        private static void ThrowIfWrongNumberOfPartsForIdentifier(string code, int expectedCount, IList<string> identifierParts)
        {
            if (identifierParts.Count != expectedCount)
            {
                throw CreateIdentifierPartsCountException(code, expectedCount, identifierParts.Count);
            }
        }
    }
}
