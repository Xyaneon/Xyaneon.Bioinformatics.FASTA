using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Xyaneon.Bioinformatics.FASTA.ActualSequences;

namespace Xyaneon.Bioinformatics.FASTA.Test.ActualSequences
{
    [TestClass]
    public class ActualSequenceParserTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_ShouldRejectNullText()
        {
            _ = ActualSequenceParser.Parse((string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_ShouldRejectNullLinesCollection()
        {
            _ = ActualSequenceParser.Parse((IEnumerable<string>)null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_ShouldRejectAllWhitespaceText()
        {
            _ = ActualSequenceParser.Parse("     ");
        }

        [TestMethod]
        public void Parse_ShouldParseAminoAcidSequence()
        {
            const string aminoAcidSequenceString = "ABCZ";
            IActualSequence sequenceData = ActualSequenceParser.Parse(aminoAcidSequenceString);

            Assert.IsNotNull(sequenceData);
            Assert.IsInstanceOfType(sequenceData, typeof(AminoAcidSequence));
            var aminoAcidSequenceData = (AminoAcidSequence)sequenceData;
            Assert.AreEqual(aminoAcidSequenceString, aminoAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Parse_ShouldParseNucleicAcidSequence()
        {
            const string nucleicAcidSequenceString = "ATCG";
            IActualSequence sequenceData = ActualSequenceParser.Parse(nucleicAcidSequenceString);

            Assert.IsNotNull(sequenceData);
            Assert.IsInstanceOfType(sequenceData, typeof(NucleicAcidSequence));
            var nucelicAcidSequenceData = (NucleicAcidSequence)sequenceData;
            Assert.AreEqual(nucleicAcidSequenceString, nucelicAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Parse_ShouldAssumeParsingAmbiguousSequenceAsNucleicAcidSequence()
        {
            const string nucleicAcidSequenceString = "ABCD";
            IActualSequence sequenceData = ActualSequenceParser.Parse(nucleicAcidSequenceString);

            Assert.IsNotNull(sequenceData);
            Assert.IsInstanceOfType(sequenceData, typeof(NucleicAcidSequence));
            var nucelicAcidSequenceData = (NucleicAcidSequence)sequenceData;
            Assert.AreEqual(nucleicAcidSequenceString, nucelicAcidSequenceData.Characters);
        }
    }
}
