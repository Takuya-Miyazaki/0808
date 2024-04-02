using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Amazon.Lambda.Core;
using Amazon.Lambda.Annotations.APIGateway;

namespace TestServerlessApp
{
    public class CustomizeResponseExamples_NotFoundResponseWithHeaderV1Async_Generated
    {
        private readonly CustomizeResponseExamples customizeResponseExamples;
        private readonly Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer serializer;

        public CustomizeResponseExamples_NotFoundResponseWithHeaderV1Async_Generated()
        {
            SetExecutionEnvironment();
            customizeResponseExamples = new CustomizeResponseExamples();
            serializer = new Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer();
        }

        public async System.Threading.Tasks.Task<System.IO.Stream> NotFoundResponseWithHeaderV1Async(Amazon.Lambda.APIGatewayEvents.APIGatewayProxyRequest __request__, Amazon.Lambda.Core.ILambdaContext __context__)
        {
            var validationErrors = new List<string>();

            var x = default(int);
            if (__request__.PathParameters?.ContainsKey("x") == true)
            {
                try
                {
                    x = (int)Convert.ChangeType(__request__.PathParameters["x"], typeof(int));
                }
                catch (Exception e) when (e is InvalidCastException || e is FormatException || e is OverflowException || e is ArgumentException)
                {
                    validationErrors.Add($"Value {__request__.PathParameters["x"]} at 'x' failed to satisfy constraint: {e.Message}");
                }
            }

            // return 400 Bad Request if there exists a validation error
            if (validationErrors.Any())
            {
                var errorResult = new Amazon.Lambda.APIGatewayEvents.APIGatewayProxyResponse
                {
                    Body = @$"{{""message"": ""{validationErrors.Count} validation error(s) detected: {string.Join(",", validationErrors)}""}}",
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json"},
                        {"x-amzn-ErrorType", "ValidationException"}
                    },
                    StatusCode = 400
                };
                var errorStream = new System.IO.MemoryStream();
                serializer.Serialize(errorResult, errorStream);
                errorStream.Position = 0;
                return errorStream;
            }

            var httpResults = await customizeResponseExamples.NotFoundResponseWithHeaderV1Async(x, __context__);
            HttpResultSerializationOptions.ProtocolFormat serializationFormat = HttpResultSerializationOptions.ProtocolFormat.HttpApi;
            HttpResultSerializationOptions.ProtocolVersion serializationVersion = HttpResultSerializationOptions.ProtocolVersion.V1;
            System.Text.Json.Serialization.JsonSerializerContext jsonContext = null;
            var serializationOptions = new HttpResultSerializationOptions { Format = serializationFormat, Version = serializationVersion, JsonContext = jsonContext };
            var response = httpResults.Serialize(serializationOptions);
            return response;
        }

        private static void SetExecutionEnvironment()
        {
            const string envName = "AWS_EXECUTION_ENV";

            var envValue = new StringBuilder();

            // If there is an existing execution environment variable add the annotations package as a suffix.
            if(!string.IsNullOrEmpty(Environment.GetEnvironmentVariable(envName)))
            {
                envValue.Append($"{Environment.GetEnvironmentVariable(envName)}_");
            }

            envValue.Append("amazon-lambda-annotations_1.2.0.0");

            Environment.SetEnvironmentVariable(envName, envValue.ToString());
        }
    }
}