using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Bioinformatics.FASTA.Sequences;

namespace Xyaneon.Bioinformatics.FASTA.Test.Data
{
    [TestClass]
    public class AminoAcidSequenceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_ShouldRejectNullString()
        {
            _ = AminoAcidSequence.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_ShouldRejectInvalidCharacterAtStart()
        {
            _ = AminoAcidSequence.Parse("2ABC");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_ShouldRejectInvalidCharacterAtEnd()
        {
            _ = AminoAcidSequence.Parse("ABC2");
        }

        [TestMethod]
        public void Parse_ShouldConvertLowercaseSequenceToUppercase()
        {
            var aminoAcidSequenceData = AminoAcidSequence.Parse("abc");
            Assert.AreEqual("ABC", aminoAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Parse_ShouldConvertMultilineSequenceToSingleLine()
        {
            var aminoAcidSequenceData = AminoAcidSequence.Parse("ABC\nDEF");
            Assert.AreEqual("ABCDEF", aminoAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Parse_ShouldAcceptGaps()
        {
            string expectedSequence = "ABC-";
            var aminoAcidSequenceData = AminoAcidSequence.Parse(expectedSequence);
            Assert.AreEqual(expectedSequence, aminoAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Parse_ShouldAcceptTranslationStop()
        {
            string expectedSequence = "ABC*";
            var aminoAcidSequenceData = AminoAcidSequence.Parse(expectedSequence);
            Assert.AreEqual(expectedSequence, aminoAcidSequenceData.Characters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToLines_RejectsLineLengthOfZero()
        {
            string sequence = "ABCDEF";

            var aminoAcidSequenceData = AminoAcidSequence.Parse(sequence);
            _ = aminoAcidSequenceData.ToLines(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToLines_RejectsNegativeLineLength()
        {
            string sequence = "ABCDEF";

            var aminoAcidSequenceData = AminoAcidSequence.Parse(sequence);
            _ = aminoAcidSequenceData.ToLines(-1);
        }

        [TestMethod]
        public void ToLines_ProducesExpectedOutput()
        {
            string sequence = "ABCDEF";
            var expectedLines = new string[] { "ABCD", "EF" };

            var aminoAcidSequenceData = AminoAcidSequence.Parse(sequence);
            IEnumerable<string> actualLines = aminoAcidSequenceData.ToLines(4);

            Assert.IsTrue(actualLines.SequenceEqual(expectedLines));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToMultilineString_RejectsLineLengthOfZero()
        {
            string sequence = "ABCDEF";

            var aminoAcidSequenceData = AminoAcidSequence.Parse(sequence);
            _ = aminoAcidSequenceData.ToMultilineString(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToMultilineString_RejectsNegativeLineLength()
        {
            string sequence = "ABCDEF";

            var aminoAcidSequenceData = AminoAcidSequence.Parse(sequence);
            _ = aminoAcidSequenceData.ToMultilineString(-1);
        }

        [TestMethod]
        public void ToMultilineString_ProducesExpectedOutput()
        {
            string sequence = "ABCDEF";
            string expectedLines = $"ABCD{Environment.NewLine}EF";

            var aminoAcidSequenceData = AminoAcidSequence.Parse(sequence);
            string actualLines = aminoAcidSequenceData.ToMultilineString(4);

            Assert.AreEqual(expectedLines, actualLines);
        }

        [TestMethod]
        public void ToString_ProducesExpectedOutput()
        {
            string expectedSequence = "ABC-";
            var aminoAcidSequenceData = AminoAcidSequence.Parse(expectedSequence);
            Assert.AreEqual(expectedSequence, aminoAcidSequenceData.ToString());
        }
    }
}
