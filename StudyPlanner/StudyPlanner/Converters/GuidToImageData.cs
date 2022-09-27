using StudyPlanner.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace StudyPlanner.Converters
{
    public class GuidToImageData : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> lstr = ((string)value)?.Split('|').ToList();
            List<ImageData> images = new List<ImageData>();
            if (lstr != null)
                foreach (string guid in lstr)
                    images.Add(App.Database.GetImage(Guid.Parse(guid)).Result);

            if (images.Count > 5)
            {
                List<ImageData> ranImage = new List<ImageData>();
                while (ranImage.Count < 5)
                {
                    Random random = new Random();
                    int index = random.Next(images.Count);
                    ranImage.Add(images[index]);
                    images.RemoveAt(index);
                }
                return ranImage;
            }
            
            return images;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
