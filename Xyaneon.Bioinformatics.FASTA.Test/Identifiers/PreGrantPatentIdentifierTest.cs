using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

namespace Xyaneon.Bioinformatics.FASTA.Test.Identifiers
{
    [TestClass]
    public class PreGrantPatentIdentifierTest
    {
        private const string Code = "pgp";
        private const string Country = "country";
        private const string ApplicationNumber = "applicationNumber";
        private const string SequenceNumber = "sequenceNumber";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullCountry()
        {
            _ = new PreGrantPatentIdentifier(null, ApplicationNumber, SequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyCountry()
        {
            _ = new PreGrantPatentIdentifier("", ApplicationNumber, SequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceCountry()
        {
            _ = new PreGrantPatentIdentifier(" ", ApplicationNumber, SequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullApplicationNumber()
        {
            _ = new PreGrantPatentIdentifier(Country, null, SequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyApplicationNumber()
        {
            _ = new PreGrantPatentIdentifier(Country, "", SequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceApplicationNumber()
        {
            _ = new PreGrantPatentIdentifier(Country, " ", SequenceNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullSequenceNumber()
        {
            _ = new PreGrantPatentIdentifier(Country, ApplicationNumber, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptySequenceNumber()
        {
            _ = new PreGrantPatentIdentifier(Country, ApplicationNumber, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceSequenceNumber()
        {
            _ = new PreGrantPatentIdentifier(Country, ApplicationNumber, " ");
        }

        [TestMethod]
        public void Code_ShouldReturnCorrectValue()
        {
            Identifier identifier = new PreGrantPatentIdentifier(Country, ApplicationNumber, SequenceNumber);
            Assert.AreEqual(Code, identifier.Code);
        }

        [TestMethod]
        public void ToString_ShouldFormatCorrectly()
        {
            Identifier identifier = new PreGrantPatentIdentifier(Country, ApplicationNumber, SequenceNumber);
            Assert.AreEqual($"{Code}|{Country}|{ApplicationNumber}|{SequenceNumber}", identifier.ToString());
        }
    }
}
