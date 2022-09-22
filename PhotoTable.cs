using Microsoft.Azure.Cosmos.Table;
using System;

public class PhotoTable : TableEntity
{
    public DateTime CreatedTime { get; set; }

    public string Location { get; set; }

    public string Url { get; set; }

    public int NumberOfFaces { get; set; }

    public double ImageSize { get; set; }

    public double FaceRatio { get; set; }

    public string FaceRatioType { get; set; }
}