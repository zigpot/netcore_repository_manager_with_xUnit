using System;
using Services;
using Repository;
using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace xUnitTest
{
    


    public class UnitTest1
    {
        static IFormulatrix formulatrix;
        private readonly ITestOutputHelper output;


        public UnitTest1(ITestOutputHelper output){
            formulatrix = new FormulatrixRepository();
            formulatrix.Initialize();
            this.output = output;
        }

        [Theory]
        [InlineData("item1","{\"to\":\"tru3.d3v@gmail.com\",\"title\":\"Greetings\",\"body\":\"Hi there!\"}",1)]
        [InlineData("item2", "<to>tru3</to><title>Greetings</title><body>Hithere</body>", 2)]
        public void ReturnTrue_GivenParameters_RegisteringJSONXML(string itemName,string itemContent,int itemType)
        {
            bool result = false;
            try
            {
                formulatrix.Register(itemName,itemContent , itemType);
                result = true;
            }
            catch(Exception exc){
                result = false;
                this.output.WriteLine(exc.Message);
            }
            finally{

                string outputStr = $"Item:{itemName} should be True when successfully registered";
                Assert.True(result,outputStr);
               
            }
        }


        [Fact]
        public void ReturnFalse_GivenDuplicatedItemNameAndItemType_RegisteringJSON()
        {
            bool result = false;
            try
            {
                // Duplicating itemType and itemName
                formulatrix.Register("item1", "{\"to\":\"tru3.d3v@gmail.com\",\"title\":\"Duplicated\",\"body\":\"Hi there!\"}", 1);
                formulatrix.Register("item1", "{\"to\":\"tru3.d3v@gmail.com\",\"title\":\"Duplicated\",\"body\":\"Hi there i am duplicated!\"}", 1);

                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                Assert.False(result, $"Should be False when Duplicated");
            }
        }

        [Fact]
        public void ReturnFalse_GivenDuplicatedItemNameAndItemType_RegisteringXML()
        {
            bool result = false;
            try
            {
                // Duplicating itemType and itemName
                formulatrix.Register("item2", "<to>tru3</to><title>Greetings</title><body>Hithere</body>", 2);
                formulatrix.Register("item2", "<to>tru3</to><title>Greetings</title><body>Hithere</body>", 2);

                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                Assert.False(result, $"Should be False when Duplicated");
            }
        }


        [Fact]
        public void ReturnFalse_GivenNoInputParameters_Registering()
        {
            bool result = false;
            try
            {
                // Duplicating itemType and itemName
                formulatrix.Register("", "{\"to\":\"tru3.d3v@gmail.com\",\"title\":\"Duplicated\",\"body\":\"Hi there!\"}", 1);
                formulatrix.Register("item1", "", 2);
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                Assert.False(result, $"Should be False when no input parameters");
            }
        }


        [Fact]
        public void Return1_GivenitemName_getType()
        {
            formulatrix.Register("item1", "{\"to\":\"tru3.d3v@gmail.com\",\"title\":\"Duplicated\",\"body\":\"Hi there!\"}", 1);
            Assert.Equal(1,formulatrix.GetType("item1"));
        }

        [Fact]
        public void Return2_GivenitemName_getType()
        {
            formulatrix.Register("item2", "<to>tru3</to><title>Greetings</title><body>Hithere</body>", 2);
            Assert.Equal(2, formulatrix.GetType("item2"));

        }

        [Fact]
        public void Return0_GivenitemNameWrong_getType()
        {
            formulatrix.Register("item2", "<to>tru3</to><title>Greetings</title><body>Hithere</body>", 2);
            Assert.Equal(0, formulatrix.GetType("item22222"));

        }


        [Fact]
        public void ReturnStringXML_GivenitemName_Retrieve()
        {
            formulatrix.Register("item2", "<to>tru3</to><title>Greetings</title><body>Hithere</body>", 2);
            Assert.Equal("<root><to>tru3</to><title>Greetings</title><body>Hithere</body></root>", formulatrix.Retrieve("item2"));

        }

        [Fact]
        public void ReturnStringJson_GivenitemName_Retrieve()
        {
            formulatrix.Register("item1", "{\"to\":\"tru3.d3v@gmail.com\",\"title\":\"Hi there\",\"body\":\"Hi there!\"}", 1);
            Assert.Equal("{\"to\":\"tru3.d3v@gmail.com\",\"title\":\"Hi there\",\"body\":\"Hi there!\"}", formulatrix.Retrieve("item1"));

        }


        [Fact]
        public void ReturnStringEmpty_GivenitemNameWrong_Retrieve()
        {
            formulatrix.Register("item2", "<to>tru3</to><title>Greetings</title><body>Hithere</body>", 2);
            Assert.Empty( formulatrix.Retrieve("item222"));

        }

        [Fact]
        public void Deregister_GivenitemName()
        {
            formulatrix.Register("item2", "<to>tru3</to><title>Greetings</title><body>Hithere</body>", 2);
            formulatrix.Deregister("item2");

            Assert.Equal(0, formulatrix.GetType("item2"));

        }

    }
}
