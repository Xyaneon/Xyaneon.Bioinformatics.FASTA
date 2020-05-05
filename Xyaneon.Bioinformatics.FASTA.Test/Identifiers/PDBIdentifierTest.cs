using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

namespace Xyaneon.Bioinformatics.FASTA.Test.Identifiers
{
    [TestClass]
    public class PDBIdentifierTest
    {
        private const string Code = "pdb";
        private const string Entry = "entry";
        private const string Chain = "chain";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullEntryNumber()
        {
            _ = new PDBIdentifier(null, Chain);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyEntryNumber()
        {
            _ = new PDBIdentifier("", Chain);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceEntryNumber()
        {
            _ = new PDBIdentifier(" ", Chain);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullChain()
        {
            _ = new PDBIdentifier(Entry, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyChain()
        {
            _ = new PDBIdentifier(Entry, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceChain()
        {
            _ = new PDBIdentifier(Entry, " ");
        }

        [TestMethod]
        public void Code_ShouldReturnCorrectValue()
        {
            Identifier identifier = new PDBIdentifier(Entry, Chain);
            Assert.AreEqual(Code, identifier.Code);
        }

        [TestMethod]
        public void ToString_ShouldFormatCorrectly()
        {
            Identifier identifier = new PDBIdentifier(Entry, Chain);
            Assert.AreEqual($"{Code}|{Entry}|{Chain}", identifier.ToString());
        }
    }
}
