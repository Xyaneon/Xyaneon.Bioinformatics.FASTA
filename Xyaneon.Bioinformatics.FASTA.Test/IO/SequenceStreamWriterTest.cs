using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.ActualSequences;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.IO;

namespace Xyaneon.Bioinformatics.FASTA.Test.IO
{
    [TestClass]
    public class SequenceStreamWriterTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedStream_SingleSequence_ShouldRejectNullData()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            SequenceStreamWriter.WriteToInterleavedStream((Sequence)null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedStream_SingleSequence_ShouldRejectNullStream()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);

            SequenceStreamWriter.WriteToInterleavedStream(sequence, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedStream_SingleSequence_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            var stream = new MemoryStream(new byte[] { }, true);

            SequenceStreamWriter.WriteToInterleavedStream(sequence, stream, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedStream_SingleSequence_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            var stream = new MemoryStream(new byte[] { }, true);

            SequenceStreamWriter.WriteToInterleavedStream(sequence, stream, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedStreamAsync_SingleSequence_ShouldRejectNullData()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            await SequenceStreamWriter.WriteToInterleavedStreamAsync((Sequence)null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedStreamAsync_SingleSequence_ShouldRejectNullStream()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);

            await SequenceStreamWriter.WriteToInterleavedStreamAsync(sequence, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedStreamAsync_SingleSequence_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            var stream = new MemoryStream(new byte[] { }, true);

            await SequenceStreamWriter.WriteToInterleavedStreamAsync(sequence, stream, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedStreamAsync_SingleSequence_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            var stream = new MemoryStream(new byte[] { }, true);

            await SequenceStreamWriter.WriteToInterleavedStreamAsync(sequence, stream, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedStream_MultipleSequences_ShouldRejectNullData()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            SequenceStreamWriter.WriteToInterleavedStream((IEnumerable<Sequence>)null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToInterleavedStream_MultipleSequences_ShouldRejectNullStream()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            SequenceStreamWriter.WriteToInterleavedStream(sequences, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedStream_MultipleSequences_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            var stream = new MemoryStream(new byte[] { }, true);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            SequenceStreamWriter.WriteToInterleavedStream(sequences, stream, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WriteToInterleavedStream_MultipleSequences_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            var stream = new MemoryStream(new byte[] { }, true);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            SequenceStreamWriter.WriteToInterleavedStream(sequences, stream, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedStreamAsync_MultipleSequences_ShouldRejectNullSequenceCollection()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            await SequenceStreamWriter.WriteToInterleavedStreamAsync((IEnumerable<Sequence>)null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToInterleavedStreamAsync_MultipleSequences_ShouldRejectNullStream()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            await SequenceStreamWriter.WriteToInterleavedStreamAsync(sequences, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedStreamAsync_MultipleSequences_ShouldRejectLineLengthOfZero()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            var stream = new MemoryStream(new byte[] { }, true);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            await SequenceStreamWriter.WriteToInterleavedStreamAsync(sequences, stream, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task WriteToInterleavedStreamAsync_MultipleSequences_ShouldRejectNegativeLineLength()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            var stream = new MemoryStream(new byte[] { }, true);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            await SequenceStreamWriter.WriteToInterleavedStreamAsync(sequences, stream, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialStream_SingleSequence_ShouldRejectNullData()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            SequenceStreamWriter.WriteToSequentialStream((Sequence)null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialStream_SingleSequence_ShouldRejectNullStream()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);

            SequenceStreamWriter.WriteToSequentialStream(sequence, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialStreamAsync_SingleSequence_ShouldRejectNullSequence()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            await SequenceStreamWriter.WriteToSequentialStreamAsync((Sequence)null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialStreamAsync_SingleSequence_ShouldRejectNullStream()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);

            await SequenceStreamWriter.WriteToSequentialStreamAsync(sequence, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialStream_MultipleSequences_ShouldRejectNullSequenceCollection()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            SequenceStreamWriter.WriteToSequentialStream((IEnumerable<Sequence>)null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteToSequentialStream_MultipleSequences_ShouldRejectNullStream()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            SequenceStreamWriter.WriteToSequentialStream(sequences, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialStreamAsync_MultipleSequences_ShouldRejectNullSequenceCollection()
        {
            var stream = new MemoryStream(new byte[] { }, true);

            await SequenceStreamWriter.WriteToSequentialStreamAsync((IEnumerable<Sequence>)null, stream);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WriteToSequentialStreamAsync_MultipleSequences_ShouldRejectNullStream()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            IActualSequence actualSequence = NucleicAcidSequence.Parse("ATCG");
            var sequence = new Sequence(header, actualSequence);
            IEnumerable<Sequence> sequences = new Sequence[] { sequence };

            await SequenceStreamWriter.WriteToSequentialStreamAsync(sequences, null);
        }
    }
}
