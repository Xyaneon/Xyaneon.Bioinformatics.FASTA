using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.IO;
using Xyaneon.Bioinformatics.FASTA.Sequences;

namespace Xyaneon.Bioinformatics.FASTA.Test.IO
{
    [TestClass]
    public class MultiFASTAFileWriterTest
    {
        private const string Path = @"C:\some\file\path";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedFile_ShouldRejectNullData()
        {
            MultiFASTAFileWriter.WriteToInterleavedFile(null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedFile_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            MultiFASTAFileWriter.WriteToInterleavedFile(multiFASTAData, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedFile_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            MultiFASTAFileWriter.WriteToInterleavedFile(multiFASTAData, Path, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedFile_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            MultiFASTAFileWriter.WriteToInterleavedFile(multiFASTAData, Path, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedStream_ShouldRejectNullData()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            MultiFASTAFileWriter.WriteToInterleavedStream(null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedStream_ShouldRejectNullStream()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            MultiFASTAFileWriter.WriteToInterleavedStream(multiFASTAData, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedStream_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var stream = new MemoryStream(new byte[] { }, true);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            MultiFASTAFileWriter.WriteToInterleavedStream(multiFASTAData, stream, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedStream_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var stream = new MemoryStream(new byte[] { }, true);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            MultiFASTAFileWriter.WriteToInterleavedStream(multiFASTAData, stream, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedStreamAsync_ShouldRejectNullData()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            await MultiFASTAFileWriter.WriteToInterleavedStreamAsync(null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedStreamAsync_ShouldRejectNullStream()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            await MultiFASTAFileWriter.WriteToInterleavedStreamAsync(multiFASTAData, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedStreamAsync_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var stream = new MemoryStream(new byte[] { }, true);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            await MultiFASTAFileWriter.WriteToInterleavedStreamAsync(multiFASTAData, stream, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedStreamAsync_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var stream = new MemoryStream(new byte[] { }, true);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            await MultiFASTAFileWriter.WriteToInterleavedStreamAsync(multiFASTAData, stream, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedFileAsync_ShouldRejectNullData()
        {
            await MultiFASTAFileWriter.WriteToInterleavedFileAsync(null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedFileAsync_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            await MultiFASTAFileWriter.WriteToInterleavedFileAsync(multiFASTAData, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedFileAsync_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            await MultiFASTAFileWriter.WriteToInterleavedFileAsync(multiFASTAData, Path, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedFileAsync_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            await MultiFASTAFileWriter.WriteToInterleavedFileAsync(multiFASTAData, Path, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialFile_ShouldRejectNullData()
        {
            MultiFASTAFileWriter.WriteToSequentialFile(null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialFile_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            MultiFASTAFileWriter.WriteToSequentialFile(multiFASTAData, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialStream_ShouldRejectNullData()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            MultiFASTAFileWriter.WriteToSequentialStream(null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialStream_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            MultiFASTAFileWriter.WriteToSequentialStream(multiFASTAData, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialFileAsync_ShouldRejectNullData()
        {
            await MultiFASTAFileWriter.WriteToSequentialFileAsync(null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialFileAsync_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            await MultiFASTAFileWriter.WriteToSequentialFileAsync(multiFASTAData, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialStreamAsync_ShouldRejectNullData()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            await MultiFASTAFileWriter.WriteToSequentialStreamAsync(null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialStreamAsync_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            ISequence sequence = NucleicAcidSequence.Parse("ATCG");
            var singleFASTAData = new SingleFASTAFileData(header, sequence);
            var multiFASTAData = new MultiFASTAFileData(singleFASTAData);

            await MultiFASTAFileWriter.WriteToSequentialStreamAsync(multiFASTAData, null);
        }
    }
}
