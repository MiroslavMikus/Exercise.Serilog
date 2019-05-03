using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Threading;

namespace Exercise.Serilog.Elastic
{
    class Program
    {
        static Random RND = new Random();

        static void Main()
        {
            CreateLogger();

            LogWithContext();

            LogLevels();

            StructuredLogging();

            for (int i = 0; i < 30; i++)
            {
                EnrichersTest.Enrichers(i % 3 == 0 ? "Storno" : "Transport", i.ToString());
            }

            Console.ReadLine();
        }

        private static void LogWithContext()
        {
            var _log = Log.ForContext<Program>();

            _log.Information("Some amazing log with context :)");
        }

        private static void StructuredLogging()
        {
            for (int i = 0; i < 50; i++)
            {
                Thread.CurrentThread.Join(5);

                var user = new { Name = "Miro", Age = RND.Next(1, 100) };

                Log.Information("Log user {@User}", user);
            }
        }

        private static void LogLevels()
        {
            Log.Verbose("This is verbose");
            Log.Debug("This is debug");
            Log.Information("This is information");
            Log.Warning("This is warning");
            Log.Error("This is Error");
            Log.Fatal("This is Fatal");
        }

        private static void CreateLogger()
        {
            Log.Logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .MinimumLevel.Debug()
                            .WriteTo.Console(LogEventLevel.Debug,
                                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message} (at {SourceContext:l}){NewLine}{Exception}")
                            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                            {
                                AutoRegisterTemplate = true,
                                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                            })
                            .CreateLogger();
        }
    }
}
