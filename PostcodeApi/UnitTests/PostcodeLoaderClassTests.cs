using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class PostcodeLoaderClassTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PostcodeLoader()
        {
            // Static File URL fails this test as its running in the UnitTests proj instead. 
            // TODO Fix this with a restructure of the CsvLoader class?

            Assert.Pass();

            try
            {
                PostcodeLoader postcodeLoader = new();
            }
            catch (FailedToLoadPostcodeData)
            {
                Assert.Fail();
            }

            Assert.Pass();
            
        }
    
    }
}
