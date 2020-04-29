﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.Sequences;
using Xyaneon.Bioinformatics.FASTA.Test.Extensions;

namespace Xyaneon.Bioinformatics.FASTA.Test
{
    [TestClass]
    public class SingleFASTAFileReaderTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadFromStream_ShouldRejectNullStream()
        {
            _ = SingleFASTAFileReader.ReadFromStream(null);
        }

        [TestMethod]
        public void ReadFromStream_ShouldProduceExpectedOutputForStream()
        {
            string inputString = ">lcl|123" + Environment.NewLine + "ATCG" + Environment.NewLine + "AAAA";
            using Stream stream = inputString.ToStream();

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
            string inputString = ">lcl|123" + Environment.NewLine + "ATCG" + Environment.NewLine + "AAAA";
            using Stream stream = inputString.ToStream();

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