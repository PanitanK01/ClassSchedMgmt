﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace StudyPlanner.Models
{
    public class Classroom
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DayOfWeek Day { get; set; }
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; } = new TimeSpan(23, 59, 59);
        public string Room { get; set; }
        public string Section { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }

        public string ShortSummary { get => $"Time : {StartTime.ToString(@"hh\:mm")} - {EndTime.ToString(@"hh\:mm")}\nRoom : {Room}";  }
        public string LongSummary { get => $"Time : {StartTime.ToString(@"hh\:mm")} - {EndTime.ToString(@"hh\:mm")}\nRoom : {Room}\nSection : {Section}\nNumber : {Number}"; }

        public override string ToString()
        {
            return Name;
        }
    }
}
