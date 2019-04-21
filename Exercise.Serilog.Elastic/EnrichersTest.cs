using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Serilog.Elastic
{
    public static class EnrichersTest
    {
        public class ProcessInfo
        {
            public string ProcessName { get; set; }
            public string ProcessId { get; set; }
            public string System { get; set; }
        }

        public static void Enrichers(string processName, string processId)
        {
            LogIncommingTransportOrder(processName, processId);

            LogTransportOrderProcessing(processName, processId);

            LogFromAGV(processName, processId);

            LogFinishTransportOrderFromServer(processName, processId);

            LogFinistTransportOrderApi(processName, processId);
        }

        private static void LogFromAGV(string processName, string processId)
        {
            var info = new ProcessInfo
            {
                ProcessName = processName,
                ProcessId = processId,
                System = "AGV"
            };

            using (LogContext.PushProperty("Process", info, true))
            {
                Businesslogic();
                Log.Logger.Information("Received transport order");
            }

            using (LogContext.PushProperty("Process", info, true))
            {
                Log.Logger.Information("Transport order was executed");
            }
        }

        private static void LogFinistTransportOrderApi(string processName, string processId)
        {
            var info = new ProcessInfo
            {
                ProcessName = processName,
                ProcessId = processId,
                System = "SapService"
            };

            using (LogContext.PushProperty("Process", info, true))
            {
                Businesslogic();
                Log.Logger.Information("Received acknowledgment from Server. Finishing Transport Process");
            }
        }

        private static void LogIncommingTransportOrder(string processName, string processId)
        {
            var info = new ProcessInfo
            {
                ProcessName = processName,
                ProcessId = processId,
                System = "SapService"
            };

            using (LogContext.PushProperty("Process", info, true))
            {
                Log.Logger.Information("Received transport order");
            }

            using (LogContext.PushProperty("Process", info, true))
            {
                Log.Logger.Information("Sending transport order to Server with port: {Port}", 55);
            }
        }

        private static void LogTransportOrderProcessing(string processName, string processId)
        {
            var info = new ProcessInfo
            {
                ProcessName = processName,
                ProcessId = processId,
                System = "Server"
            };

            using (LogContext.PushProperty("Process", info, true))
            {
                Log.Logger.Information("Received transport order from Api service");
            }

            using (LogContext.PushProperty("Process", info, true))
            {
                Log.Logger.Information("Sending transport order to AGV: {AgvId}", 63);
            }
        }

        private static void LogFinishTransportOrderFromServer(string processName, string processId)
        {
            var info = new ProcessInfo
            {
                ProcessName = processName,
                ProcessId = processId,
                System = "Server"
            };

            using (LogContext.PushProperty("Process", info, true))
            {
                Log.Logger.Information("Sending Transport order completed to Api service");
            }
        }

        private static void Businesslogic()
        {
            Log.Logger.Information("I'm just the poor worker :`(");
        }
    }
}
