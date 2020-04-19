﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void Parse_ShouldParseImportIdIdentifier()
        {
            var expected = new ImportIdIdentifier(123);
            var actual = IdentifierParser.Parse("gim|123") as ImportIdIdentifier;

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
    }
}
