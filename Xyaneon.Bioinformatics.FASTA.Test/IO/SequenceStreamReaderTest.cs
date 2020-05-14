using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.IO;
using Xyaneon.Bioinformatics.FASTA.ActualSequences;
using Xyaneon.Bioinformatics.FASTA.Test.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Xyaneon.Bioinformatics.FASTA.Test.IO
{
    [TestClass]
    public class SequenceStreamReaderTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadSingleFromStream_ShouldRejectNullStream()
        {
            _ = SequenceStreamReader.ReadSingleFromStream(null);
        }

        [TestMethod]
        public void ReadSingleFromStream_ShouldProduceExpectedOutputForOneSequence()
        {
            Stream stream = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA").ToStream();
            Sequence sequence = SequenceStreamReader.ReadSingleFromStream(stream);

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
        public void ReadSingleFromStream_ShouldProduceExpectedOutputForTwoSequences()
        {
            Stream stream = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA", "", ">lcl|456", "TTTT", "CCCC").ToStream();
            Sequence sequence = SequenceStreamReader.ReadSingleFromStream(stream);

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
        public async Task ReadSingleFromStreamAsync_ShouldRejectNullStream()
        {
            _ = await SequenceStreamReader.ReadSingleFromStreamAsync(null);
        }

        [TestMethod]
        public async Task ReadSingleFromStreamAsync_ShouldProduceExpectedOutputForOneSequence()
        {
            Stream stream = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA").ToStream();
            Sequence sequence = await SequenceStreamReader.ReadSingleFromStreamAsync(stream);

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
        public async Task ReadSingleFromStreamAsync_ShouldProduceExpectedOutputForTwoSequences()
        {
            Stream stream = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA", "", ">lcl|456", "TTTT", "CCCC").ToStream();
            Sequence sequence = await SequenceStreamReader.ReadSingleFromStreamAsync(stream);

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
        public void ReadMultipleFromStream_ShouldRejectNullStream()
        {
            _ = SequenceStreamReader.ReadMultipleFromStream(null);
        }

        [TestMethod]
        public void ReadMultipleFromStream_ShouldProduceExpectedOutputForOneSequence()
        {
            Stream stream = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA").ToStream();
            IList<Sequence> multiFASTAFileData = SequenceStreamReader.ReadMultipleFromStream(stream).ToList();

            Assert.IsNotNull(multiFASTAFileData);
            Assert.AreEqual(1, multiFASTAFileData.Count);

            Sequence sequence = multiFASTAFileData[0];
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
        public void ReadMultipleFromStream_ShouldProduceExpectedOutputForTwoSequences()
        {
            Stream stream = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA", "", ">lcl|456", "TTTT", "CCCC").ToStream();
            IList<Sequence> multiFASTAFileData = SequenceStreamReader.ReadMultipleFromStream(stream).ToList();

            Assert.IsNotNull(multiFASTAFileData);
            Assert.AreEqual(2, multiFASTAFileData.Count);

            {
                Sequence sequence = multiFASTAFileData[0];
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

            {
                Sequence sequence = multiFASTAFileData[1];
                Assert.IsNotNull(sequence);

                Header header = sequence.Header;
                Assert.IsNotNull(header);
                Assert.AreEqual(1, header.Items.Count);
                LocalIdentifier identifier = header.Items[0] as LocalIdentifier;
                Assert.IsNotNull(identifier);
                Assert.AreEqual("lcl", identifier.Code);
                Assert.AreEqual("456", identifier.Value);

                NucleicAcidSequence data = (NucleicAcidSequence)sequence.Data;
                Assert.AreEqual("TTTTCCCC", data.Characters);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ReadMultipleFromStreamAsync_ShouldRejectNullStream()
        {
            _ = await SequenceStreamReader.ReadMultipleFromStreamAsync(null);
        }

        [TestMethod]
        public async Task ReadMultipleFromStreamAsync_ShouldProduceExpectedOutputForOneSequence()
        {
            Stream stream = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA").ToStream();
            IList<Sequence> multiFASTAFileData = (await SequenceStreamReader.ReadMultipleFromStreamAsync(stream)).ToList();

            Assert.IsNotNull(multiFASTAFileData);
            Assert.AreEqual(1, multiFASTAFileData.Count);

            Sequence sequence = multiFASTAFileData[0];
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
        public async Task ReadMultipleFromStreamAsync_ShouldProduceExpectedOutputForTwoSequences()
        {
            Stream stream = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA", "", ">lcl|456", "TTTT", "CCCC").ToStream();
            IList<Sequence> multiFASTAFileData = (await SequenceStreamReader.ReadMultipleFromStreamAsync(stream)).ToList();

            Assert.IsNotNull(multiFASTAFileData);
            Assert.AreEqual(2, multiFASTAFileData.Count);

            {
                Sequence sequence = multiFASTAFileData[0];
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

            {
                Sequence sequence = multiFASTAFileData[1];
                Assert.IsNotNull(sequence);

                Header header = sequence.Header;
                Assert.IsNotNull(header);
                Assert.AreEqual(1, header.Items.Count);
                LocalIdentifier identifier = header.Items[0] as LocalIdentifier;
                Assert.IsNotNull(identifier);
                Assert.AreEqual("lcl", identifier.Code);
                Assert.AreEqual("456", identifier.Value);

                NucleicAcidSequence data = (NucleicAcidSequence)sequence.Data;
                Assert.AreEqual("TTTTCCCC", data.Characters);
            }
        }
    }
}
