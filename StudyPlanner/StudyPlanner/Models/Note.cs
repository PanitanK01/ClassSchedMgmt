using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SQLite;

namespace StudyPlanner.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string TagColor { get; set; } = "#FFF14235";
        public string Class { get; set; } = "None";
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageList { get; set; }
    }
}
