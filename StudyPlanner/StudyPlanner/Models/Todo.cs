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

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
