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
        public void Parse_ShouldParseHeaderWithDDBJIdentifier()
        {
            Header header = Header.Parse(">dbj|ACCESSION|LOCUS");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(DDBJIdentifier));
            var identifier = (DDBJIdentifier)header.Items[0];
            Assert.AreEqual("dbj", identifier.Code);
            Assert.AreEqual("ACCESSION", identifier.Accession);
            Assert.AreEqual("LOCUS", identifier.Locus);
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
        public void Parse_ShouldParseHeaderWithGeneralDatabaseReferenceIdentifier()
        {
            Header header = Header.Parse(">gnl|DATABASE|VALUE");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(GeneralDatabaseReferenceIdentifier));
            var identifier = (GeneralDatabaseReferenceIdentifier)header.Items[0];
            Assert.AreEqual("gnl", identifier.Code);
            Assert.AreEqual("DATABASE", identifier.Database);
            Assert.AreEqual("VALUE", identifier.Value);
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
        public void Parse_ShouldParseHeaderWithIntegratedDatabaseIdentifier()
        {
            Header header = Header.Parse(">gi|123");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(IntegratedDatabaseIdentifier));
            var identifier = (IntegratedDatabaseIdentifier)header.Items[0];
            Assert.AreEqual("gi", identifier.Code);
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
        public void Parse_ShouldParseHeaderWithPatentIdentifier()
        {
            Header header = Header.Parse(">pat|COUNTRY|PATENT|SEQUENCENUMBER");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(PatentIdentifier));
            var identifier = (PatentIdentifier)header.Items[0];
            Assert.AreEqual("pat", identifier.Code);
            Assert.AreEqual("COUNTRY", identifier.Country);
            Assert.AreEqual("PATENT", identifier.Patent);
            Assert.AreEqual("SEQUENCENUMBER", identifier.SequenceNumber);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithPDBIdentifier()
        {
            Header header = Header.Parse(">pdb|ENTRY|CHAIN");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(PDBIdentifier));
            var identifier = (PDBIdentifier)header.Items[0];
            Assert.AreEqual("pdb", identifier.Code);
            Assert.AreEqual("ENTRY", identifier.Entry);
            Assert.AreEqual("CHAIN", identifier.Chain);
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
        public void Parse_ShouldParseHeaderWithPRFIdentifier()
        {
            Header header = Header.Parse(">prf|ACCESSION|NAME");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(PRFIdentifier));
            var identifier = (PRFIdentifier)header.Items[0];
            Assert.AreEqual("prf", identifier.Code);
            Assert.AreEqual("ACCESSION", identifier.Accession);
            Assert.AreEqual("NAME", identifier.Name);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithPreGrantPatentIdentifier()
        {
            Header header = Header.Parse(">pgp|COUNTRY|APPLICATIONNUMBER|SEQUENCENUMBER");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(PreGrantPatentIdentifier));
            var identifier = (PreGrantPatentIdentifier)header.Items[0];
            Assert.AreEqual("pgp", identifier.Code);
            Assert.AreEqual("COUNTRY", identifier.Country);
            Assert.AreEqual("APPLICATIONNUMBER", identifier.ApplicationNumber);
            Assert.AreEqual("SEQUENCENUMBER", identifier.SequenceNumber);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithRefSeqIdentifier()
        {
            Header header = Header.Parse(">ref|ACCESSION|NAME");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(RefSeqIdentifier));
            var identifier = (RefSeqIdentifier)header.Items[0];
            Assert.AreEqual("ref", identifier.Code);
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
        public void Parse_ShouldParseHeaderWithThirdPartyDDBJIdentifier()
        {
            Header header = Header.Parse(">tpd|ACCESSION|NAME");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(ThirdPartyDDBJIdentifier));
            var identifier = (ThirdPartyDDBJIdentifier)header.Items[0];
            Assert.AreEqual("tpd", identifier.Code);
            Assert.AreEqual("ACCESSION", identifier.Accession);
            Assert.AreEqual("NAME", identifier.Name);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithThirdPartyEMBLIdentifier()
        {
            Header header = Header.Parse(">tpe|ACCESSION|NAME");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(ThirdPartyEMBLIdentifier));
            var identifier = (ThirdPartyEMBLIdentifier)header.Items[0];
            Assert.AreEqual("tpe", identifier.Code);
            Assert.AreEqual("ACCESSION", identifier.Accession);
            Assert.AreEqual("NAME", identifier.Name);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithThirdPartyGenBankIdentifier()
        {
            Header header = Header.Parse(">tpg|ACCESSION|NAME");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(ThirdPartyGenBankIdentifier));
            var identifier = (ThirdPartyGenBankIdentifier)header.Items[0];
            Assert.AreEqual("tpg", identifier.Code);
            Assert.AreEqual("ACCESSION", identifier.Accession);
            Assert.AreEqual("NAME", identifier.Name);
        }

        [TestMethod]
        public void Parse_ShouldParseHeaderWithTrEMBLIdentifier()
        {
            Header header = Header.Parse(">tr|ACCESSION|NAME");

            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            Assert.IsInstanceOfType(header.Items[0], typeof(TrEMBLIdentifier));
            var identifier = (TrEMBLIdentifier)header.Items[0];
            Assert.AreEqual("tr", identifier.Code);
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
