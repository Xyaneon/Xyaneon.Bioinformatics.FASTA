using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Xyaneon.Bioinformatics.FASTA.Sequences;

namespace Xyaneon.Bioinformatics.FASTA.Test.Data
{
    [TestClass]
    public class SequenceParserTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_ShouldRejectNullText()
        {
            _ = SequenceParser.Parse((string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_ShouldRejectNullLinesCollection()
        {
            _ = SequenceParser.Parse((IEnumerable<string>)null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_ShouldRejectAllWhitespaceText()
        {
            _ = SequenceParser.Parse("     ");
        }

        [TestMethod]
        public void Parse_ShouldParseAminoAcidSequence()
        {
            const string aminoAcidSequenceString = "ABCZ";
            ISequence sequenceData = SequenceParser.Parse(aminoAcidSequenceString);

            Assert.IsNotNull(sequenceData);
            Assert.IsInstanceOfType(sequenceData, typeof(AminoAcidSequence));
            var aminoAcidSequenceData = (AminoAcidSequence)sequenceData;
            Assert.AreEqual(aminoAcidSequenceString, aminoAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Parse_ShouldParseNucleicAcidSequence()
        {
            const string nucleicAcidSequenceString = "ATCG";
            ISequence sequenceData = SequenceParser.Parse(nucleicAcidSequenceString);

            Assert.IsNotNull(sequenceData);
            Assert.IsInstanceOfType(sequenceData, typeof(NucleicAcidSequence));
            var nucelicAcidSequenceData = (NucleicAcidSequence)sequenceData;
            Assert.AreEqual(nucleicAcidSequenceString, nucelicAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Parse_ShouldAssumeParsingAmbiguousSequenceAsNucleicAcidSequence()
        {
            const string nucleicAcidSequenceString = "ABCD";
            ISequence sequenceData = SequenceParser.Parse(nucleicAcidSequenceString);

            Assert.IsNotNull(sequenceData);
            Assert.IsInstanceOfType(sequenceData, typeof(NucleicAcidSequence));
            var nucelicAcidSequenceData = (NucleicAcidSequence)sequenceData;
            Assert.AreEqual(nucleicAcidSequenceString, nucelicAcidSequenceData.Characters);
        }
    }
}
