using Acn.Slp;
using NUnit.Framework;
using System.Collections.Generic;

namespace Acn.Test
{
    
    
    /// <summary>
    ///This is a test class for SlpUserAgentTest and is intended
    ///to contain all SlpUserAgentTest Unit Tests
    ///</summary>
    [TestFixture]
    public class SlpUserAgentTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for SplitAttributeList
        ///</summary>
        [Test, Category("Unit")]
        public void SplitAttributeListTest()
        {
            string attributeList = "(Foo=Bar),(Time=Money),(Knowledge=Power),(Tea=Happiness)";
            Dictionary<string, string> expected = new Dictionary<string, string>();
            expected["Foo"] = "Bar";
            expected["Time"] = "Money";
            expected["Knowledge"] = "Power";
            expected["Tea"] = "Happiness";

            Dictionary<string, string> actual;
            actual = SlpUserAgent.SplitAttributeList(attributeList);
            CollectionAssert.AreEquivalent(expected, actual, "Dictionarys didn't match"); 
           
        }
    }
}
