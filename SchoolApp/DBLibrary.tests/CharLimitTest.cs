using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary;
using Xunit;

namespace DBLibrary.tests
{
    public class CharLimitTest
    {
        //[Fact]
        [Theory]
        [InlineData ("bqwertyuiop",10,false)]
        [InlineData ("b",10,true)]
        [InlineData ("1234",3,false)]
        public void TestCharLimit_StringShouldBeInLimit(string text, int limit, bool expected)
        {
            //Arrange
            TestMethods methods = new TestMethods();
            //bool expected = true;
            //Act
            bool actual = methods.dontExcedeCharLimit(text, limit);
            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        //student alice cooper, average should be 67
        [InlineData(1010,67)]
        public void TestAverage_DoubleShouldBeWithinRange(int curUserID, double expected)
        {
            TestMethods methods = new TestMethods();

            double actual = methods.calculateAverage(curUserID);

            Assert.Equal(expected, actual);
        }
    }
}
