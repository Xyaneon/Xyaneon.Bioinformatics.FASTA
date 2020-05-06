using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

namespace Xyaneon.Bioinformatics.FASTA.Test.Identifiers
{
    [TestClass]
    public class ThirdPartyDDBJIdentifierTest
    {
        private const string Code = "tpd";
        private const string Accession = "accession";
        private const string Name = "name";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullAccessionNumber()
        {
            _ = new ThirdPartyDDBJIdentifier(null, Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyAccessionNumber()
        {
            _ = new ThirdPartyDDBJIdentifier("", Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceAccessionNumber()
        {
            _ = new ThirdPartyDDBJIdentifier(" ", Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullName()
        {
            _ = new ThirdPartyDDBJIdentifier(Accession, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyName()
        {
            _ = new ThirdPartyDDBJIdentifier(Accession, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceName()
        {
            _ = new ThirdPartyDDBJIdentifier(Accession, " ");
        }

        [TestMethod]
        public void Code_ShouldReturnCorrectValue()
        {
            Identifier identifier = new ThirdPartyDDBJIdentifier(Accession, Name);
            Assert.AreEqual(Code, identifier.Code);
        }

        [TestMethod]
        public void ToString_ShouldFormatCorrectly()
        {
            Identifier identifier = new ThirdPartyDDBJIdentifier(Accession, Name);
            Assert.AreEqual($"{Code}|{Accession}|{Name}", identifier.ToString());
        }
    }
}
