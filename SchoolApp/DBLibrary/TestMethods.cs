using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class TestMethods
    {
        SchoolDBEntities db = new SchoolDBEntities("metadata=res://*/SchoolModel.csdl|res://*/SchoolModel.ssdl|res://*/SchoolModel.msl;provider=System.Data.SqlClient;provider connection string='data source=192.168.1.10;initial catalog=schoolDB;user id=aaron;password=Password16;MultipleActiveResultSets=True;App=EntityFramework'");

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
