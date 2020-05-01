using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.Identifiers.GenInfo;

namespace Xyaneon.Bioinformatics.FASTA.Test.Identifiers
{
    [TestClass]
    public class IdentifierParserTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_ShouldRejectNullText()
        {
            _ = IdentifierParser.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_ShouldRejectEmptyText()
        {
            _ = IdentifierParser.Parse("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_ShouldRejectAllWhitespaceText()
        {
            _ = IdentifierParser.Parse("     ");
        }

        [TestMethod]
        public void Parse_ShouldParseBackboneMolTypeIdentifier()
        {
            var expected = new BackboneMolTypeIdentifier(123);
            var actual = IdentifierParser.Parse("bbm|123") as BackboneMolTypeIdentifier;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Value, actual.Value);
        }

        [TestMethod]
        public void Parse_ShouldParseBackboneSeqIdIdentifier()
        {
            var expected = new BackboneSeqIdIdentifier(123);
            var actual = IdentifierParser.Parse("bbs|123") as BackboneSeqIdIdentifier;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Value, actual.Value);
        }

        [TestMethod]
        public void Parse_ShouldParseGenBankIdentifier()
        {
            var expected = new GenBankIdentifier("M73307", "AGMA13GT");
            var actual = IdentifierParser.Parse("gb|M73307|AGMA13GT") as GenBankIdentifier;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Accession, actual.Accession);
            Assert.AreEqual(expected.Locus, actual.Locus);
        }

        [TestMethod]
        public void Parse_ShouldParseEMBLIdentifier()
        {
            var expected = new EMBLIdentifier("M73307", "AGMA13GT");
            var actual = IdentifierParser.Parse("emb|M73307|AGMA13GT") as EMBLIdentifier;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Accession, actual.Accession);
            Assert.AreEqual(expected.Locus, actual.Locus);
        }

        [TestMethod]
        public void Parse_ShouldParseImportIdIdentifier()
        {
            var expected = new ImportIdIdentifier(123);
            var actual = IdentifierParser.Parse("gim|123") as ImportIdIdentifier;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Value, actual.Value);
        }

        [TestMethod]
        public void Parse_ShouldParseIntegratedDatabaseIdentifier()
        {
            var expected = new IntegratedDatabaseIdentifier(123);
            var actual = IdentifierParser.Parse("gi|123") as IntegratedDatabaseIdentifier;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Value, actual.Value);
        }

        [TestMethod]
        public void Parse_ShouldParseLocalIdentifier()
        {
            var expected = new LocalIdentifier("123");
            var actual = IdentifierParser.Parse("lcl|123") as LocalIdentifier;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Value, actual.Value);
        }

        [TestMethod]
        public void Parse_ShouldParsePatentIdentifier()
        {
            var expected = new PatentIdentifier("US", "RE33188", "1");
            var actual = IdentifierParser.Parse("pat|US|RE33188|1") as PatentIdentifier;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.Patent, actual.Patent);
            Assert.AreEqual(expected.SequenceNumber, actual.SequenceNumber);
        }

        [TestMethod]
        public void Parse_ShouldParsePIRIdentifier()
        {
            var expected = new PIRIdentifier("M73307", "AGMA13GT");
            var actual = IdentifierParser.Parse("pir|M73307|AGMA13GT") as PIRIdentifier;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Accession, actual.Accession);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod]
        public void Parse_ShouldParsePreGrantPatentIdentifier()
        {
            var expected = new PreGrantPatentIdentifier("EP", "0238993", "7");
            var actual = IdentifierParser.Parse("pgp|EP|0238993|7") as PreGrantPatentIdentifier;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.ApplicationNumber, actual.ApplicationNumber);
            Assert.AreEqual(expected.SequenceNumber, actual.SequenceNumber);
        }

        [TestMethod]
        public void Parse_ShouldParseRefSeqIdentifier()
        {
            var expected = new RefSeqIdentifier("M73307", "AGMA13GT");
            var actual = IdentifierParser.Parse("ref|M73307|AGMA13GT") as RefSeqIdentifier;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Accession, actual.Accession);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod]
        public void Parse_ShouldParseSWISSPROTIdentifier()
        {
            var expected = new SWISSPROTIdentifier("M73307", "AGMA13GT");
            var actual = IdentifierParser.Parse("sp|M73307|AGMA13GT") as SWISSPROTIdentifier;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Accession, actual.Accession);
            Assert.AreEqual(expected.Name, actual.Name);
        }
    }
}
