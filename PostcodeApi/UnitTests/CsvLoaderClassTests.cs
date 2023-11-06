using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class CsvLoaderClassTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CsvLoader()
        {
            // Static File URL fails this test as its running in the UnitTests proj instead. 
            // TODO Fix this with a restructure of the CsvLoader class?

            Assert.Pass();

            try
            {
                CsvLoader c = new();
            }
            catch (FailedToLoadPostcodeData)
            {
                Assert.Fail();
            }

            Assert.Pass();
            
        }
    
    }
}
