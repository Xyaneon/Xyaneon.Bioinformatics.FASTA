using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

namespace Xyaneon.Bioinformatics.FASTA.Test.Identifiers
{
    [TestClass]
    public class PatentIdentifierTest
    {
        private const string Code = "pat";
        private const string Country = "country";
        private const string Patent = "patent";
        private const string SequenceNumber = "sequenceNumber";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullCountry()
        {
            _ = new PatentIdentifier(null, Patent, SequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyCountry()
        {
            _ = new PatentIdentifier("", Patent, SequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceCountry()
        {
            _ = new PatentIdentifier(" ", Patent, SequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullPatent()
        {
            _ = new PatentIdentifier(Country, null, SequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyPatent()
        {
            _ = new PatentIdentifier(Country, "", SequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespacePatent()
        {
            _ = new PatentIdentifier(Country, " ", SequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullSequenceNumber()
        {
            _ = new PatentIdentifier(Country, Patent, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptySequenceNumber()
        {
            _ = new PatentIdentifier(Country, Patent, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceSequenceNumber()
        {
            _ = new PatentIdentifier(Country, Patent, " ");
        }

        [TestMethod]
        public void Code_ShouldReturnCorrectValue()
        {
            Identifier identifier = new PatentIdentifier(Country, Patent, SequenceNumber);
            Assert.AreEqual(Code, identifier.Code);
        }

        [TestMethod]
        public void ToString_ShouldFormatCorrectly()
        {
            Identifier identifier = new PatentIdentifier(Country, Patent, SequenceNumber);
            Assert.AreEqual($"{Code}|{Country}|{Patent}|{SequenceNumber}", identifier.ToString());
        }
    }
}
