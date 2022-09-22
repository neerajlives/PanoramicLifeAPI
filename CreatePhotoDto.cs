using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanoramicLifeAPI
{
    public class CreatePhotoDto
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public string Location { get; set; }

        public string Url { get; set; }

        public int NumberOfFaces { get; set; }

        public double ImageSize { get; set; }

        public double FaceRatio { get; set; }

        public string FaceRatioType { get; set; }
    }
}
