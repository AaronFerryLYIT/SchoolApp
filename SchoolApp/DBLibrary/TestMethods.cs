using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class TestMethods
    {
        //takes a string and the limit number to check to see if they go over
        public bool dontExcedeCharLimit(string text, int limit)
        {
            int numOfChar = text.Length;
            if (numOfChar > limit)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool checkStatistics(List<Result> results, double sum)
        {
            int highest = results.Max(h => h.result_mark);
            int lowest = results.Min(l => l.result_mark);
            //the sum value is after looping through a student's results and added them together
            double average = (sum / results.Count);
            average = Math.Round(average, 2);
            if(highest < 0 || highest > 100)
            {
                return false;
            }
            if(lowest < 0 || lowest > 100)
            {
                return false;
            }
            if(average < 0 || average > 100)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
