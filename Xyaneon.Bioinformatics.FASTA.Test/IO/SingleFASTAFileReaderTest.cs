using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.IO;
using Xyaneon.Bioinformatics.FASTA.Sequences;
using Xyaneon.Bioinformatics.FASTA.Test.Extensions;

namespace Xyaneon.Bioinformatics.FASTA.Test.IO
{
    [TestClass]
    public class SingleFASTAFileReaderTest
    {
        private static readonly string ValidInputString = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA");

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadFromFile_ShouldRejectNullPath()
        {
            _ = SingleFASTAFileReader.ReadFromFile(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ReadFromFileAsync_ShouldRejectNullPath()
        {
            _ = await SingleFASTAFileReader.ReadFromFileAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadFromStream_ShouldRejectNullStream()
        {
            _ = SingleFASTAFileReader.ReadFromStream(null);
        }

        [TestMethod]
        public void ReadFromStream_ShouldProduceExpectedOutputForStream()
        {
            using Stream stream = ValidInputString.ToStream();

            SingleFASTAFileData sequence = SingleFASTAFileReader.ReadFromStream(stream);

            Assert.IsNotNull(sequence);

            Header header = sequence.Header;
            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            LocalIdentifier identifier = header.Items[0] as LocalIdentifier;
            Assert.IsNotNull(identifier);
            Assert.AreEqual("lcl", identifier.Code);
            Assert.AreEqual("123", identifier.Value);

            NucleicAcidSequence data = (NucleicAcidSequence)sequence.Data;
            Assert.AreEqual("ATCGAAAA", data.Characters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ReadFromStreamAsync_ShouldRejectNullStream()
        {
            _ = await SingleFASTAFileReader.ReadFromStreamAsync(null);
        }

        [TestMethod]
        public async Task ReadFromStreamAsync_ShouldProduceExpectedOutputForStream()
        {
            using Stream stream = ValidInputString.ToStream();

            SingleFASTAFileData sequence = await SingleFASTAFileReader.ReadFromStreamAsync(stream);

            Assert.IsNotNull(sequence);

            Header header = sequence.Header;
            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            LocalIdentifier identifier = header.Items[0] as LocalIdentifier;
            Assert.IsNotNull(identifier);
            Assert.AreEqual("lcl", identifier.Code);
            Assert.AreEqual("123", identifier.Value);

            NucleicAcidSequence data = (NucleicAcidSequence)sequence.Data;
            Assert.AreEqual("ATCGAAAA", data.Characters);
        }
    }
}
