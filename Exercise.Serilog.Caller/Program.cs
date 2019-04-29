using Serilog;
using System;
using System.Runtime.CompilerServices;

namespace Exercise.Serilog.Caller
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithCaller()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message} (at {Caller}){NewLine}{Exception}")
                .CreateLogger();

            Log.Logger.Error(new Exception("Amazing error :)"), nameof(Main));

            Log.Information("Hello, world!");

            SayGoodbye();

            Log.CloseAndFlush();

            Console.ReadLine();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void SayGoodbye()
        {
            Log.Information("Goodbye!");
        }
    }
}
