using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

namespace Xyaneon.Bioinformatics.FASTA.Test.Identifiers
{
    [TestClass]
    public class SWISSPROTIdentifierTest
    {
        private const string Code = "sp";
        private const string Accession = "accession";
        private const string Name = "name";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullAccessionNumber()
        {
            _ = new SWISSPROTIdentifier(null, Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyAccessionNumber()
        {
            _ = new SWISSPROTIdentifier("", Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceAccessionNumber()
        {
            _ = new SWISSPROTIdentifier(" ", Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullName()
        {
            _ = new SWISSPROTIdentifier(Accession, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyName()
        {
            _ = new SWISSPROTIdentifier(Accession, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceName()
        {
            _ = new SWISSPROTIdentifier(Accession, " ");
        }

        [TestMethod]
        public void Code_ShouldReturnCorrectValue()
        {
            Identifier identifier = new SWISSPROTIdentifier(Accession, Name);
            Assert.AreEqual(Code, identifier.Code);
        }

        [TestMethod]
        public void ToString_ShouldFormatCorrectly()
        {
            Identifier identifier = new SWISSPROTIdentifier(Accession, Name);
            Assert.AreEqual($"{Code}|{Accession}|{Name}", identifier.ToString());
        }
    }
}
