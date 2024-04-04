using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Amazon.Lambda.Core;

namespace TestServerlessApp.Sub1
{
    public class FunctionsZipOutput_ToLower_Generated
    {
        private readonly ServiceProvider serviceProvider;

        public FunctionsZipOutput_ToLower_Generated()
        {
            SetExecutionEnvironment();
            var services = new ServiceCollection();

            // By default, Lambda function class is added to the service container using the singleton lifetime
            // To use a different lifetime, specify the lifetime in Startup.ConfigureServices(IServiceCollection) method.
            services.AddSingleton<FunctionsZipOutput>();
            services.AddSingleton<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>();

            var startup = new TestServerlessApp.Startup();
            startup.ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public string ToLower(string text)
        {
            // Create a scope for every request,
            // this allows creating scoped dependencies without creating a scope manually.
            using var scope = serviceProvider.CreateScope();
            var functionsZipOutput = scope.ServiceProvider.GetRequiredService<FunctionsZipOutput>();
            var serializer = scope.ServiceProvider.GetRequiredService<Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer>();

            return functionsZipOutput.ToLower(text);
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

            envValue.Append("amazon-lambda-annotations_1.3.0.0");

            Environment.SetEnvironmentVariable(envName, envValue.ToString());
        }
    }
}