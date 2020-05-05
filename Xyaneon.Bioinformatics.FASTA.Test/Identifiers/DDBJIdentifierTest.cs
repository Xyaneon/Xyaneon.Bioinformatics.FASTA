using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

namespace Xyaneon.Bioinformatics.FASTA.Test.Identifiers
{
    [TestClass]
    public class DDBJIdentifierTest
    {
        private const string Code = "dbj";
        private const string Accession = "accession";
        private const string Locus = "locus";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullAccessionNumber()
        {
            _ = new DDBJIdentifier(null, Locus);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyAccessionNumber()
        {
            _ = new DDBJIdentifier("", Locus);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceAccessionNumber()
        {
            _ = new DDBJIdentifier(" ", Locus);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullLocusNumber()
        {
            _ = new DDBJIdentifier(Accession, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyLocusNumber()
        {
            _ = new DDBJIdentifier(Accession, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceLocusNumber()
        {
            _ = new DDBJIdentifier(Accession, " ");
        }

        [TestMethod]
        public void Code_ShouldReturnCorrectValue()
        {
            Identifier identifier = new DDBJIdentifier(Accession, Locus);
            Assert.AreEqual(Code, identifier.Code);
        }

        [TestMethod]
        public void ToString_ShouldFormatCorrectly()
        {
            Identifier identifier = new DDBJIdentifier(Accession, Locus);
            Assert.AreEqual($"{Code}|{Accession}|{Locus}", identifier.ToString());
        }
    }
}
