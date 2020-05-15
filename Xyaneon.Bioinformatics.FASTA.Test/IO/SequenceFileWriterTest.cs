using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.ActualSequences;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.IO;

namespace Xyaneon.Bioinformatics.FASTA.Test.IO
{
    [TestClass]
    public class SequenceFileWriterTest
    {
        private const string Path = @"C:\some\file\path";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedFile_SingleSequence_ShouldRejectNullData()
        {
            SequenceFileWriter.WriteToInterleavedFile((Sequence)null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedFile_SingleSequence_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);

            SequenceFileWriter.WriteToInterleavedFile(sequence, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedFile_SingleSequence_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);

            SequenceFileWriter.WriteToInterleavedFile(sequence, Path, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedFile_SingleSequence_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);

            SequenceFileWriter.WriteToInterleavedFile(sequence, Path, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedFileAsync_SingleSequence_ShouldRejectNullData()
        {
            await SequenceFileWriter.WriteToInterleavedFileAsync((Sequence)null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedFileAsync_SingleSequence_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);

            await SequenceFileWriter.WriteToInterleavedFileAsync(sequence, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedFileAsync_SingleSequence_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);

            await SequenceFileWriter.WriteToInterleavedFileAsync(sequence, Path, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedFileAsync_SingleSequence_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);

            await SequenceFileWriter.WriteToInterleavedFileAsync(sequence, Path, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialFile_SingleSequence_ShouldRejectNullData()
        {
            SequenceFileWriter.WriteToSequentialFile((Sequence)null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialFile_SingleSequence_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);

            SequenceFileWriter.WriteToSequentialFile(sequence, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialFileAsync_SingleSequence_ShouldRejectNullData()
        {
            await SequenceFileWriter.WriteToSequentialFileAsync((Sequence)null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialFileAsync_SingleSequence_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);

            await SequenceFileWriter.WriteToSequentialFileAsync(sequence, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedFile_MultipleSequences_ShouldRejectNullData()
        {
            SequenceFileWriter.WriteToInterleavedFile((IEnumerable<Sequence>)null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedFile_MultipleSequences_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            SequenceFileWriter.WriteToInterleavedFile(sequences, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedFile_MultipleSequences_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            SequenceFileWriter.WriteToInterleavedFile(sequences, Path, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedFile_MultipleSequences_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            SequenceFileWriter.WriteToInterleavedFile(sequences, Path, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedFileAsync_MultipleSequences_ShouldRejectNullData()
        {
            await SequenceFileWriter.WriteToInterleavedFileAsync((IEnumerable<Sequence>)null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedFileAsync_MultipleSequences_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            await SequenceFileWriter.WriteToInterleavedFileAsync(sequences, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedFileAsync_MultipleSequences_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            await SequenceFileWriter.WriteToInterleavedFileAsync(sequences, Path, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedFileAsync_MultipleSequences_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            await SequenceFileWriter.WriteToInterleavedFileAsync(sequences, Path, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialFile_MultipleSequences_ShouldRejectNullData()
        {
            SequenceFileWriter.WriteToSequentialFile((IEnumerable<Sequence>)null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialFile_MultipleSequences_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            SequenceFileWriter.WriteToSequentialFile(sequences, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialFileAsync_MultipleSequences_ShouldRejectNullData()
        {
            await SequenceFileWriter.WriteToSequentialFileAsync((IEnumerable<Sequence>)null, Path);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialFileAsync_MultipleSequences_ShouldRejectNullPath()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            await SequenceFileWriter.WriteToSequentialFileAsync(sequences, null);
        }
    }
}
