using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Serilog
{
    class Program
    {
        static void Main()
        {
            CreateLogger();

            var _log = Log.ForContext<Program>();

            _log.Information("Some amazing context log :)");

            Console.ReadKey();

            for (int i = 0; i < 500; i++)
            {
                Console.WriteLine(i);

                SimpleLog();

                LogLevels();

                StructuredLogging();
            }

        }

        private static void StructuredLogging()
        {
            // serilog can deconstruct base types
            var fruit = new Dictionary<string, int> { { "Apple", 1 }, { "Pear", 5 } };
            Log.Information("In my bowl I have {Fruit}", fruit);

            var user = new { Name = "Miro", Age = 123 };

            // user will be deconstructed with @
            Log.Information("Log deconstructed user {@User}", user);

            Log.Information("Log user {User}", user);

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

        private static void SimpleLog()
        {
            Log.Information("Hello, Serilog!");

            int a = 10, b = 0;

            try
            {
                Log.Debug("Dividing {A} by {B}", a, b);

                Console.WriteLine(a / b);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");
            }
        }

        private static void CreateLogger()
        {
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Verbose()
                            .WriteTo.Console(LogEventLevel.Verbose,
                                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message} (at {SourceContext:l}){NewLine}{Exception}")
                            .WriteTo.File(new JsonFormatter(), "Log\\Machine.log")
                            .WriteTo.File("Log\\Human.log")
                            .WriteTo.RollingFile(new JsonFormatter(),
                                "Log\\RollingFile-{Date}.log")
                            .CreateLogger();
        }
    }
}
