using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

namespace Xyaneon.Bioinformatics.FASTA.Test.Identifiers
{
    [TestClass]
    public class GeneralDatabaseReferenceIdentifierTest
    {
        private const string Code = "gnl";
        private const string Database = "database";
        private const string Value = "value";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullDatabase()
        {
            _ = new GeneralDatabaseReferenceIdentifier(null, Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyDatabase()
        {
            _ = new GeneralDatabaseReferenceIdentifier("", Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceDatabase()
        {
            _ = new GeneralDatabaseReferenceIdentifier(" ", Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullValue()
        {
            _ = new GeneralDatabaseReferenceIdentifier(Database, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectEmptyValue()
        {
            _ = new GeneralDatabaseReferenceIdentifier(Database, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectWhitespaceValue()
        {
            _ = new GeneralDatabaseReferenceIdentifier(Database, " ");
        }

        [TestMethod]
        public void Code_ShouldReturnCorrectValue()
        {
            Identifier identifier = new GeneralDatabaseReferenceIdentifier(Database, Value);
            Assert.AreEqual(Code, identifier.Code);
        }

        [TestMethod]
        public void ToString_ShouldFormatCorrectly()
        {
            Identifier identifier = new GeneralDatabaseReferenceIdentifier(Database, Value);
            Assert.AreEqual($"{Code}|{Database}|{Value}", identifier.ToString());
        }
    }
}
