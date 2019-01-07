using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary;
using Xunit;

namespace DBLibrary.tests
{
    public class UnitTests
    {
        //[Fact]
        //tests to see if string entered is within a specified limit
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

        //tests to see if the values found in database for the highest lowest and average are within 0 - 100
        [Fact]
        public void TestStatistics_ValuesShouldBeWithinRange()
        {
            //Arrange
            TestMethods methods = new TestMethods();
            List<Result> results = new List<Result>();
            results.Add(new Result { result_mark = 78 });
            results.Add(new Result { result_mark = 56 });
            results.Add(new Result { result_mark = 90 });
            double sum = 0;
            foreach (var studentResult in results)
            {
                sum = sum + (double)studentResult.result_mark;
            }
            //Actual
            bool actual = methods.checkStatistics(results, sum);
            //Assert
            Assert.True(actual);
        }
    }
}
