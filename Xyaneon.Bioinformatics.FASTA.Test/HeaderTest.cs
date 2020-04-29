using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.Identifiers.GenInfo;

namespace Xyaneon.Bioinformatics.FASTA.Test
{
    [TestClass]
    public class HeaderTest
    {
        private const string DescriptionText1 = "Description 1";
        private const string DescriptionText2 = "Description 2";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullHeaderItem()
        {
            _ = new Header((HeaderItem)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullHeaderItemsCollection()
        {
            _ = new Header((IEnumerable<HeaderItem>)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_ShouldRejectNullText()
        {
            _ = Header.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_ShouldRejectAllWhitespaceText()
        {
            _ = Header.Parse("     ");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_ShouldRejectTextNotStartingWithLineStartDelimiter()
        {
            _ = Header.Parse("Sample description goes here");
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithOnlyDescription()
        {
            Header header = Header.Parse(">Sample description goes here");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(Description));
            var description = (Description)header.Items[0];
            Assert.AreEqual("Sample description goes here", description.Text);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithBackboneMolTypeIdentifier()
        {
            Header header = Header.Parse(">bbm|123");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(BackboneMolTypeIdentifier));
            var identifier = (BackboneMolTypeIdentifier)header.Items[0];
            Assert.AreEqual("bbm", identifier.Code);
            Assert.AreEqual(123, identifier.Value);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithBackboneSeqIdIdentifier()
        {
            Header header = Header.Parse(">bbs|123");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(BackboneSeqIdIdentifier));
            var identifier = (BackboneSeqIdIdentifier)header.Items[0];
            Assert.AreEqual("bbs", identifier.Code);
            Assert.AreEqual(123, identifier.Value);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithEMBLIdentifier()
        {
            Header header = Header.Parse(">emb|ACCESSION|LOCUS");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(EMBLIdentifier));
            var identifier = (EMBLIdentifier)header.Items[0];
            Assert.AreEqual("emb", identifier.Code);
            Assert.AreEqual("ACCESSION", identifier.Accession);
            Assert.AreEqual("LOCUS", identifier.Locus);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithGenBankIdentifier()
        {
            Header header = Header.Parse(">gb|123|456");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(GenBankIdentifier));
            var identifier = (GenBankIdentifier)header.Items[0];
            Assert.AreEqual("gb", identifier.Code);
            Assert.AreEqual("123", identifier.Accession);
            Assert.AreEqual("456", identifier.Locus);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithImportIdIdentifier()
        {
            Header header = Header.Parse(">gim|123");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(ImportIdIdentifier));
            var identifier = (ImportIdIdentifier)header.Items[0];
            Assert.AreEqual("gim", identifier.Code);
            Assert.AreEqual(123, identifier.Value);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithLocalIdentifier()
        {
            Header header = Header.Parse(">lcl|123");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(LocalIdentifier));
            var identifier = (LocalIdentifier)header.Items[0];
            Assert.AreEqual("lcl", identifier.Code);
            Assert.AreEqual("123", identifier.Value);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithPIRIdentifier()
        {
            Header header = Header.Parse(">pir|ACCESSION|NAME");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(PIRIdentifier));
            var identifier = (PIRIdentifier)header.Items[0];
            Assert.AreEqual("pir", identifier.Code);
            Assert.AreEqual("ACCESSION", identifier.Accession);
            Assert.AreEqual("NAME", identifier.Name);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithSWISSPROTIdentifier()
        {
            Header header = Header.Parse(">sp|ACCESSION|NAME");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(SWISSPROTIdentifier));
            var identifier = (SWISSPROTIdentifier)header.Items[0];
            Assert.AreEqual("sp", identifier.Code);
            Assert.AreEqual("ACCESSION", identifier.Accession);
            Assert.AreEqual("NAME", identifier.Name);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithLocalIdentifierAndDescription()
        {
            Header header = Header.Parse(">lcl|123|Sample description goes here");

            Assert.IsNotNull(header);
            Assert.AreEqual(2, header.Items.Count);

            Assert.IsInstanceOfType(header.Items[0], typeof(LocalIdentifier));
            var identifier = (LocalIdentifier)header.Items[0];
            Assert.AreEqual("lcl", identifier.Code);
            Assert.AreEqual("123", identifier.Value);

            Assert.IsInstanceOfType(header.Items[1], typeof(Description));
            var description = (Description)header.Items[1];
            Assert.AreEqual("Sample description goes here", description.Text);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithLocalIdentifierAndGenBankIdentifier()
        {
            Header header = Header.Parse(">lcl|123|gb|123|456");

            Assert.IsNotNull(header);
            Assert.AreEqual(2, header.Items.Count);

            Assert.IsInstanceOfType(header.Items[0], typeof(LocalIdentifier));
            var identifier1 = (LocalIdentifier)header.Items[0];
            Assert.AreEqual("lcl", identifier1.Code);
            Assert.AreEqual("123", identifier1.Value);

            Assert.IsInstanceOfType(header.Items[1], typeof(GenBankIdentifier));
            var identifier2 = (GenBankIdentifier)header.Items[1];
            Assert.AreEqual("gb", identifier2.Code);
            Assert.AreEqual("123", identifier2.Accession);
            Assert.AreEqual("456", identifier2.Locus);
        }

        [TestMethod]
        public void ToString_ShouldProduceExpectedOutputForSingleItem()
        {
            string expected = $">{DescriptionText1}";
            var header = new Header(new List<HeaderItem>(1) { new Description(DescriptionText1) });

            Assert.AreEqual(expected, header.ToString());
        }

        [TestMethod]
        public void ToString_ShouldProduceExpectedOutputForMultipleItems()
        {
            string expected = $">{DescriptionText1}|{DescriptionText2}";
            var header = new Header(new List<HeaderItem>(2) { new Description(DescriptionText1), new Description(DescriptionText2) });

            Assert.AreEqual(expected, header.ToString());
        }
    }
}
