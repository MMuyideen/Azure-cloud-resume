using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;


namespace Company.Function
{
    public static class GetResumeCounter
    {
        [FunctionName("getresumecounter")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(databaseName:"DeenAzureResume", containerName: "Counter", Connection = "deenresumeconnectionstring", Id = "1", PartitionKey = "1")] Counter counter, 
            [CosmosDB(databaseName:"DeenAzureResume", containerName: "Counter", Connection = "deenresumeconnectionstring", Id = "1", PartitionKey = "1")] out Counter updatedCounter,
            ILogger log)
        {
            // here is Where the the counter gets updated
            log.LogInformation("C# HTTP trigger function processed a request.");

            counter ??= new Counter();

            updatedCounter = counter;
            updatedCounter.Count += 1;

            var jsonToReturn = JsonConvert.SerializeObject(counter);


            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }
}
