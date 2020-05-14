using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.IO;

namespace Xyaneon.Bioinformatics.FASTA.Test.IO
{
    [TestClass]
    public class SequenceFileReaderTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadSingleFromFile_ShouldRejectNullPath()
        {
            _ = SequenceFileReader.ReadSingleFromFile(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ReadSingleFromFileAsync_ShouldRejectNullPath()
        {
            _ = await SequenceFileReader.ReadSingleFromFileAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadMultipleFromFile_ShouldRejectNullPath()
        {
            _ = SequenceFileReader.ReadMultipleFromFile(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ReadMultipleFromFileAsync_ShouldRejectNullPath()
        {
            _ = await SequenceFileReader.ReadMultipleFromFileAsync(null);
        }
    }
}
