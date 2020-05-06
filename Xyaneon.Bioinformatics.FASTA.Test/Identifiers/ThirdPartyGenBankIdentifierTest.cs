using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

namespace Xyaneon.Bioinformatics.FASTA.Test.Identifiers
{
    [TestClass]
    public class ThirdPartyGenBankIdentifierTest
    {
        private const string Code = "tpg";
        private const string Accession = "accession";
        private const string Name = "name";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullAccessionNumber()
        {
            _ = new ThirdPartyGenBankIdentifier(null, Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyAccessionNumber()
        {
            _ = new ThirdPartyGenBankIdentifier("", Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceAccessionNumber()
        {
            _ = new ThirdPartyGenBankIdentifier(" ", Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullName()
        {
            _ = new ThirdPartyGenBankIdentifier(Accession, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyName()
        {
            _ = new ThirdPartyGenBankIdentifier(Accession, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceName()
        {
            _ = new ThirdPartyGenBankIdentifier(Accession, " ");
        }

        [TestMethod]
        public void Code_ShouldReturnCorrectValue()
        {
            Identifier identifier = new ThirdPartyGenBankIdentifier(Accession, Name);
            Assert.AreEqual(Code, identifier.Code);
        }

        [TestMethod]
        public void ToString_ShouldFormatCorrectly()
        {
            Identifier identifier = new ThirdPartyGenBankIdentifier(Accession, Name);
            Assert.AreEqual($"{Code}|{Accession}|{Name}", identifier.ToString());
        }
    }
}
