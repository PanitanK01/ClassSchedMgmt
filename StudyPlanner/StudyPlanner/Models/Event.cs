using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace StudyPlanner.Models
{
    public class Event
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; } = new TimeSpan(23, 59, 59);
        public string Location { get; set; }
        public string Description { get; set; }
        public string Organizer { get; set; }


        public string ShortSummary { get => $"Time : {StartTime.ToString(@"hh\:mm")} - {EndTime.ToString(@"hh\:mm")}"; }
        public string LongSummary { get =>  $"Description :{Description}\nEnd :{EndDate.ToString("dd MMMM yyyyy")}\nTime : {StartTime.ToString(@"hh\:mm")} - {EndTime.ToString(@"hh\:mm")}\nLocation : {Location}\nOrganizer :{Organizer}"; }
    }
}
