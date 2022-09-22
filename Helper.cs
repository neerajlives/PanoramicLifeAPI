using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanoramicLifeAPI
{
    static class Helper
    {
        public static string BuildFilter(CreatePhotoDto input)
        {
            string pkFilter, rkFilter, urlFilter, nfFilter, isFilter, frFilter, frtFilter, finalFilter;
            pkFilter = rkFilter = urlFilter = nfFilter = isFilter = frFilter = frtFilter = finalFilter = string.Empty;

            if (input != null)
            {
                if (!string.IsNullOrWhiteSpace(input?.PartitionKey))
                {
                    pkFilter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, input.PartitionKey);
                    finalFilter = pkFilter;
                }

                if (!string.IsNullOrWhiteSpace(input?.RowKey))
                {
                    rkFilter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, input.RowKey);
                    finalFilter = TableQuery.CombineFilters(finalFilter, TableOperators.And, rkFilter);
                }

                if (!string.IsNullOrWhiteSpace(input?.Url))
                {
                    urlFilter = TableQuery.GenerateFilterCondition("Url", QueryComparisons.Equal, input.Url);
                    finalFilter = TableQuery.CombineFilters(finalFilter, TableOperators.And, urlFilter);
                }

                if (input?.NumberOfFaces > 0)
                {
                    nfFilter = TableQuery.GenerateFilterCondition("NumberOfFaces", QueryComparisons.Equal, input.NumberOfFaces.ToString());
                    finalFilter = TableQuery.CombineFilters(finalFilter, TableOperators.And, nfFilter);
                }

                if (input?.ImageSize > 0)
                {
                    isFilter = TableQuery.GenerateFilterCondition("Url", QueryComparisons.Equal, input.ImageSize.ToString());
                    finalFilter = TableQuery.CombineFilters(finalFilter, TableOperators.And, isFilter);
                }

                if (input?.FaceRatio > 0)
                {
                    frFilter = TableQuery.GenerateFilterCondition("FaceRatio", QueryComparisons.GreaterThan, input.FaceRatio.ToString());
                    finalFilter = TableQuery.CombineFilters(finalFilter, TableOperators.And, frFilter);
                }

                if (!string.IsNullOrWhiteSpace(input?.FaceRatioType))
                {
                    frtFilter = TableQuery.GenerateFilterCondition("FaceRatioType", QueryComparisons.Equal, input.FaceRatioType);
                    finalFilter = TableQuery.CombineFilters(finalFilter, TableOperators.And, frtFilter);
                }
            }
            else
            {
                finalFilter = null;
            }

            return finalFilter;
        }
    }
}
