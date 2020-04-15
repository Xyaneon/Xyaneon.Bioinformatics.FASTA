using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Xyaneon.Bioinformatics.FASTA.Test
{
    [TestClass]
    public class FileDataTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullSequencesCollection()
        {
            FileData fileData = new FileData(null);
        }
    }
}
