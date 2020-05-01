using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.Identifiers.GenInfo;

namespace Xyaneon.Bioinformatics.FASTA.Test.Identifiers.GenInfo
{
    [TestClass]
    public class IntegratedDatabaseIdentifierTest
    {
        private const string Code = "gi";
        private const int Value = 123;

        [TestMethod]
        public void Code_ShouldReturnCorrectValue()
        {
            Identifier identifier = new IntegratedDatabaseIdentifier(Value);
            Assert.AreEqual(Code, identifier.Code);
        }

        [TestMethod]
        public void ToString_ShouldFormatCorrectly()
        {
            Identifier identifier = new IntegratedDatabaseIdentifier(Value);
            Assert.AreEqual($"{Code}|{Value}", identifier.ToString());
        }
    }
}
