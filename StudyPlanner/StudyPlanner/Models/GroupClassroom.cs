using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StudyPlanner.Models
{
    public class GroupClassroom : List<Classroom>
    {
        public string Title { get; private set; }
        public Color TitleColor { get; private set; }

        public GroupClassroom(DayOfWeek day, List<Classroom> classrooms) : base(classrooms)
        {
            Color[] dayColor = Application.Current.Resources["DayColor"] as Color[];

            Title = day.ToString();
            TitleColor = dayColor[(int)day];
        }
    }
}
