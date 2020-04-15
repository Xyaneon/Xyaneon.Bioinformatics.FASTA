using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

namespace Xyaneon.Bioinformatics.FASTA.Test
{
    [TestClass]
    public class SequenceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullDescriptionIdentifiersCollection()
        {
            _ = new Sequence(null, "ATCG");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullData()
        {
            Identifier identifier = new LocalIdentifier("value");
            _ = new Sequence(new Identifier[] { identifier }, null);
        }
    }
}