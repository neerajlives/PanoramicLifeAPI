using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos.Table;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace PanoramicLifeAPI
{
    public static class PhotoApi
    {
        //[FunctionName("Create")]
        //public static async Task<IActionResult> Create(
        //[HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "photo")] HttpRequest req,
        //[Table("photos", Connection = "AzureWebJobsStorage")] IAsyncCollector<PhotoTable> photoTableCollector,
        //ILogger log)
        //{
        //    try
        //    {
        //        log.LogInformation("Adding New Photo Record");

        //        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //        var input = JsonConvert.DeserializeObject<CreatePhotoDto>(requestBody);

        //        if (input.Url == null)
        //        {
        //            return new BadRequestObjectResult("Please provide url");
        //        }

        //        var photo = new Photo
        //        {
        //            PartitionKey = input.PartitionKey,
        //            RowKey = input.RowKey,
        //            Url = input.Url,
        //            NumberOfFaces = input.NumberOfFaces,
        //            ImageSize = input.ImageSize,
        //            FaceRatio = input.FaceRatio,
        //            FaceRatioType = input.FaceRatioType
        //        };

        //        await photoTableCollector.AddAsync(photo.ToTable());

        //        return new OkObjectResult(photo);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        [FunctionName("GetAll")]
        public static async Task<IActionResult> GetAll(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "photo")] HttpRequest req,
        [Table("photos", Connection = "AzureWebJobsStorage")] CloudTable cloudTable,
        ILogger log)
        {
            try
            {
                log.LogInformation("Getting All Photos Records");

                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var input = JsonConvert.DeserializeObject<CreatePhotoDto>(requestBody);

                string filter = Helper.BuildFilter(input);
                filter = "(FaceRatio gt '0.03')";

                TableQuery<PhotoTable> query = new TableQuery<PhotoTable>().Where(filter);
                var segment = await cloudTable.ExecuteQuerySegmentedAsync(query, null);
                var data = segment.Select(PhotoExtensions.ToPhoto);

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

        }

        [FunctionName("Update")]
        public static async Task<IActionResult> Update(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "photo")] HttpRequest req,
        [Table("photos", Connection = "AzureWebJobsStorage")] CloudTable cloudTable,
        ILogger log)
        {
            try
            {
                log.LogInformation("Update Photo Record");

                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var input = JsonConvert.DeserializeObject<CreatePhotoDto>(requestBody);

                var photo = new Photo
                {
                    PartitionKey = input.PartitionKey,
                    RowKey = input.RowKey,
                    ETag = "*",
                    Url = input.Url,
                    Location = input.Location,
                    NumberOfFaces = input.NumberOfFaces,
                    ImageSize = input.ImageSize,
                    FaceRatio = input.FaceRatio,
                    FaceRatioType = input.FaceRatioType
                };

                var operation = TableOperation.Replace(photo.ToTable());
                await cloudTable.ExecuteAsync(operation);

                return new OkObjectResult(photo);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [FunctionName("Delete")]
        public static async Task<IActionResult> Delete(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "photo")] HttpRequest req,
        [Table("photos", Connection = "AzureWebJobsStorage")] CloudTable cloudTable,
        ILogger log)
        {
            try
            {
                log.LogInformation("Delete Photo Record");

                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var input = JsonConvert.DeserializeObject<CreatePhotoDto>(requestBody);

                var photo = new Photo
                {
                    PartitionKey = input.PartitionKey,
                    RowKey = input.RowKey,
                    ETag = "*",
                };

                var operation = TableOperation.Delete(photo.ToTable());
                await cloudTable.ExecuteAsync(operation);

                return new OkObjectResult(photo);
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [FunctionName("Create")]
        public static async Task<IActionResult> Create(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "photo")] HttpRequest req,
        [Table("photos", Connection = "AzureWebJobsStorage")] CloudTable cloudTable,
        ILogger log)
        {
            try
            {
                log.LogInformation("Adding New Photo Record");

                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var input = JsonConvert.DeserializeObject<CreatePhotoDto>(requestBody);

                if (input.Url == null)
                {
                    return new BadRequestObjectResult("Please provide url");
                }

                var photo = new Photo
                {
                    PartitionKey = input.PartitionKey,
                    RowKey = input.RowKey,
                    Url = input.Url,
                    Location = input.Location,
                    NumberOfFaces = input.NumberOfFaces,
                    ImageSize = input.ImageSize,
                    FaceRatio = input.FaceRatio,
                    FaceRatioType = input.FaceRatioType
                };

                var operation = TableOperation.InsertOrReplace(photo.ToTable());
                await cloudTable.ExecuteAsync(operation);

                return new OkObjectResult(photo);
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
