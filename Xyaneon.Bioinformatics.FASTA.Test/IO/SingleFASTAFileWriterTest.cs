using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.IO;
using Xyaneon.Bioinformatics.FASTA.ActualSequences;

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
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);

            SingleFASTAFileWriter.WriteToInterleavedFile(data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedFile_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);

            SingleFASTAFileWriter.WriteToInterleavedFile(data, Path, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedFile_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);

            SingleFASTAFileWriter.WriteToInterleavedFile(data, Path, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedStream_ShouldRejectNullData()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            SingleFASTAFileWriter.WriteToInterleavedStream(null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedStream_ShouldRejectNullStream()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);

            SingleFASTAFileWriter.WriteToInterleavedStream(data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedStream_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);
            var stream = new MemoryStream(new byte[] { }, true);

            SingleFASTAFileWriter.WriteToInterleavedStream(data, stream, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedStream_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);
            var stream = new MemoryStream(new byte[] { }, true);

            SingleFASTAFileWriter.WriteToInterleavedStream(data, stream, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedStreamAsync_ShouldRejectNullData()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            await SingleFASTAFileWriter.WriteToInterleavedStreamAsync(null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedStreamAsync_ShouldRejectNullStream()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);

            await SingleFASTAFileWriter.WriteToInterleavedStreamAsync(data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedStreamAsync_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);
            var stream = new MemoryStream(new byte[] { }, true);

            await SingleFASTAFileWriter.WriteToInterleavedStreamAsync(data, stream, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedStreamAsync_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);
            var stream = new MemoryStream(new byte[] { }, true);

            await SingleFASTAFileWriter.WriteToInterleavedStreamAsync(data, stream, -1);
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
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);

            await SingleFASTAFileWriter.WriteToInterleavedFileAsync(data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedFileAsync_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);

            await SingleFASTAFileWriter.WriteToInterleavedFileAsync(data, Path, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedFileAsync_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);

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
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);

            SingleFASTAFileWriter.WriteToSequentialFile(data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialStream_ShouldRejectNullData()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            SingleFASTAFileWriter.WriteToSequentialStream(null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialStream_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);

            SingleFASTAFileWriter.WriteToSequentialStream(data, null);
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
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);

            await SingleFASTAFileWriter.WriteToSequentialFileAsync(data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialStreamAsync_ShouldRejectNullData()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            await SingleFASTAFileWriter.WriteToSequentialStreamAsync(null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialStreamAsync_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence sequence = NucleicAcidSequence.Parse("ATCG");
            var data = new Sequence(header, sequence);

            await SingleFASTAFileWriter.WriteToSequentialStreamAsync(data, null);
        }
    }
}
