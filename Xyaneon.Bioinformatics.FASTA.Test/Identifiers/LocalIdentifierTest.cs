using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

namespace Xyaneon.Bioinformatics.FASTA.Test.Identifiers
{
    [TestClass]
    public class LocalIdentifierTest
    {
        private const string Code = "lcl";
        private const string Value = "value";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullValue()
        {
            _ = new LocalIdentifier(null);
        }

        [TestMethod]
        public void Code_ShouldReturnCorrectValue()
        {
            Identifier identifier = new LocalIdentifier(Value);
            Assert.AreEqual(Code, identifier.Code);
        }

        [TestMethod]
        public void ToString_ShouldFormatCorrectly()
        {
            Identifier identifier = new LocalIdentifier(Value);
            Assert.AreEqual($"{Code}|{Value}", identifier.ToString());
        }
    }
}