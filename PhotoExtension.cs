using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanoramicLifeAPI
{
    public static class PhotoExtensions
    {
        public static PhotoTable ToTable(this Photo photo)
        {
            return new PhotoTable
            {
                PartitionKey = photo.PartitionKey,
                RowKey = photo.RowKey,
                ETag = photo.ETag,
                CreatedTime = photo.CreatedTime,
                Url = photo.Url,
                Location = photo.Location,
                NumberOfFaces = photo.NumberOfFaces,
                ImageSize = photo.ImageSize,
                FaceRatio = photo.FaceRatio,
                FaceRatioType = photo.FaceRatioType
            };
        }

        public static Photo ToPhoto(this PhotoTable photoTable)
        {
            return new Photo
            {
                PartitionKey = photoTable.PartitionKey,
                RowKey = photoTable.RowKey,
                CreatedTime = photoTable.CreatedTime,
                Url = photoTable.Url,
                Location = photoTable.Location,
                NumberOfFaces = photoTable.NumberOfFaces,
                ImageSize = photoTable.ImageSize,
                FaceRatio = photoTable.FaceRatio,
                FaceRatioType = photoTable.FaceRatioType
            };
        }
    }
}
