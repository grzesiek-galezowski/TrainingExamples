using NUnit.Framework;

namespace XUnitFixtures.MinimalVsStandard
{
    public class FreshStandardFixture
    {
        private string _xml; //not readonly, so the reference may be mutated

        [SetUp]
        public void Initialize() //lower performance, more safety than persistent
        {
            _xml = @"
                    <CATALOG>
                    <CD>
                    <TITLE>Empire Burlesque</TITLE>
                    <ARTIST>Bob Dylan</ARTIST>
                    <COUNTRY>USA</COUNTRY>
                    <COMPANY>Columbia</COMPANY>
                    <PRICE>10.90</PRICE>
                    <YEAR>1985</YEAR>
                    </CD>
                    <CD>
                    <TITLE>Hide your heart</TITLE>
                    <ARTIST>Bonnie Tyler</ARTIST>
                    <COUNTRY>UK</COUNTRY>
                    <COMPANY>CBS Records</COMPANY>
                    <PRICE>9.90</PRICE>
                    <YEAR>1988</YEAR>
                    </CD>
                    <CD>
                    <TITLE>Greatest Hits</TITLE>
                    <ARTIST>Dolly Parton</ARTIST>
                    <COUNTRY>USA</COUNTRY>
                    <COMPANY>RCA</COMPANY>
                    <PRICE>9.90</PRICE>
                    <YEAR>1982</YEAR>
                    </CD>";
        }

        [Test]
        public void Test1()
        {
            StringAssert.Contains("Bonnie Tyler", _xml);
        }

        [Test]
        public void Test2()
        {
            StringAssert.Contains("1985", _xml);
        }

        


    }
}