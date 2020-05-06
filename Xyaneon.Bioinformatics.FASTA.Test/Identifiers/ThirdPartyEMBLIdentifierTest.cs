using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

namespace Xyaneon.Bioinformatics.FASTA.Test.Identifiers
{
    [TestClass]
    public class ThirdPartyEMBLIdentifierTest
    {
        private const string Code = "tpe";
        private const string Accession = "accession";
        private const string Name = "name";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullAccessionNumber()
        {
            _ = new ThirdPartyEMBLIdentifier(null, Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyAccessionNumber()
        {
            _ = new ThirdPartyEMBLIdentifier("", Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceAccessionNumber()
        {
            _ = new ThirdPartyEMBLIdentifier(" ", Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullName()
        {
            _ = new ThirdPartyEMBLIdentifier(Accession, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyName()
        {
            _ = new ThirdPartyEMBLIdentifier(Accession, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceName()
        {
            _ = new ThirdPartyEMBLIdentifier(Accession, " ");
        }

        [TestMethod]
        public void Code_ShouldReturnCorrectValue()
        {
            Identifier identifier = new ThirdPartyEMBLIdentifier(Accession, Name);
            Assert.AreEqual(Code, identifier.Code);
        }

        [TestMethod]
        public void ToString_ShouldFormatCorrectly()
        {
            Identifier identifier = new ThirdPartyEMBLIdentifier(Accession, Name);
            Assert.AreEqual($"{Code}|{Accession}|{Name}", identifier.ToString());
        }
    }
}
