using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSlider.Models
{
        public class ImageModel
        {
            // Required
            public int UserID { get; set; }

            // Optional user info
            public string? UserAlias { get; set; }

            // Image locations
            public string? Image_Location { get; set; }
            public string? Image_Location_Orig { get; set; }
            public string? Image_Location_Small { get; set; }

            // Descriptive fields
            public string? Image_Comment { get; set; }
            public string? Image_Description { get; set; }
            public DateTime? Image_Date { get; set; }

            // Image properties
            public string? Image_Type { get; set; }
            public string? Image_Rotation { get; set; }

            // Category
            public int? Image_Category_ID { get; set; }
            public string? Image_Category { get; set; }

            // Album
            public int? Image_Album_ID { get; set; }
            public string? Image_Album_Name { get; set; }

            // Reference / flags
            public string? Image_Reference { get; set; }
            public int? ProfileCover { get; set; }
            public int? Random { get; set; }
            public int? Showcase { get; set; }
            public int? MediaPacketID { get; set; }

            // Media grouping
            public int? Image_Media_Id { get; set; }
            public string? Image_Media_Name { get; set; }

            // NEW: Image size & dimensions
            public int? Image_Size { get; set; }
            public int? Image_Width { get; set; }
            public int? Image_Height { get; set; }
            public string? Image_Dimentions { get; set; }

            // Output (returned from stored procedure)
            public int VB_Image_Key { get; set; }
        }
    }
