using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xyaneon.Bioinformatics.FASTA.Data;
using Xyaneon.Bioinformatics.FASTA.Identifiers;

namespace Xyaneon.Bioinformatics.FASTA.Test
{
    [TestClass]
    public class SequenceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullHeaderItemsCollection()
        {
            _ = new Sequence(null, new NucleicAcidSequenceData("ATCG"));
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