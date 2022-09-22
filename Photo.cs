using System;

public class Photo
{
    public string PartitionKey { get; set; }

    public string RowKey { get; set; } = Guid.NewGuid().ToString();

    public string ETag { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

    public string Location { get; set; }

    public string Url { get; set; }

    public int NumberOfFaces { get; set; }

    public double ImageSize { get; set; }

    public double FaceRatio { get; set; }

    public string FaceRatioType { get; set; }
}