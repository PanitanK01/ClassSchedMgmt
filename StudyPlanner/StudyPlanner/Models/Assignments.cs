using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace StudyPlanner.Models
{
    public class Todo
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public bool IsComplete { get; set; }
        public string Task { get; set; }
        public bool IsImportant { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; } = new TimeSpan(23, 59, 59);


        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
