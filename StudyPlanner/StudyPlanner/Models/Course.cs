using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace StudyPlanner.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public string PreCode { get; set; }
        public string PreName { get; set; }
        public string Description { get; set; }

        public string LongSummary { get => $"Course Code : {Code}\nCourse Name : {Name}\nCredit : {Credit}\nPrerequisite Course Code : {PreCode}\nPrerequisite Course Name : {PreName}"; }


        public override string ToString()
        {
            return Name;
        }


    }
}
