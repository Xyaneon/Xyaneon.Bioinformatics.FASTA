using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.IO;
using Xyaneon.Bioinformatics.FASTA.Sequences;

namespace Xyaneon.Bioinformatics.FASTA.Test.IO
{
    [TestClass]
    public class SingleFASTAFileWriterTest
    {
        private const string Path = @"C:\some\file\path";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedFile_ShouldRejectNullData()
        {
            SingleFASTAFileWriter.WriteToInterleavedFile(null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedFile_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new SingleFASTAFileData(header, sequence);

            SingleFASTAFileWriter.WriteToInterleavedFile(data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedFile_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new SingleFASTAFileData(header, sequence);

            SingleFASTAFileWriter.WriteToInterleavedFile(data, Path, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedFile_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new SingleFASTAFileData(header, sequence);

            SingleFASTAFileWriter.WriteToInterleavedFile(data, Path, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedFileAsync_ShouldRejectNullData()
        {
            await SingleFASTAFileWriter.WriteToInterleavedFileAsync(null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedFileAsync_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new SingleFASTAFileData(header, sequence);

            await SingleFASTAFileWriter.WriteToInterleavedFileAsync(data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedFileAsync_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new SingleFASTAFileData(header, sequence);

            await SingleFASTAFileWriter.WriteToInterleavedFileAsync(data, Path, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedFileAsync_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new SingleFASTAFileData(header, sequence);

            await SingleFASTAFileWriter.WriteToInterleavedFileAsync(data, Path, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialFile_ShouldRejectNullData()
        {
            SingleFASTAFileWriter.WriteToSequentialFile(null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialFile_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new SingleFASTAFileData(header, sequence);

            SingleFASTAFileWriter.WriteToSequentialFile(data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialFileAsync_ShouldRejectNullData()
        {
            await SingleFASTAFileWriter.WriteToSequentialFileAsync(null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialFileAsync_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new SingleFASTAFileData(header, sequence);

            await SingleFASTAFileWriter.WriteToSequentialFileAsync(data, null);
        }
    }
}
