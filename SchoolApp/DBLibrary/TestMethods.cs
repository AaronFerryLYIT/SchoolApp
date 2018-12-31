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

        public double calculateAverage(int curUserID)
        {
            double average = 0;
            int count = 0;
            List<Result> results = results = new List<Result>();
            foreach (var studentResult in db.Results.Where(r => r.user_id == curUserID))
            {
                average = average + (double)studentResult.result_mark;
                count++;
                results.Add(studentResult);
            }
            average = (average / count);
            //average = Math.Round(average, 2);
            return average;
        }
    }
}
