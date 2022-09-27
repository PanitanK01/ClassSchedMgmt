using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyPlanner.Models
{
    public class ImageData
    {
        [PrimaryKey]
        public Guid Guid { get; set; } = Guid.NewGuid();
        public byte[] Bytes { get; set; }
    }
}
