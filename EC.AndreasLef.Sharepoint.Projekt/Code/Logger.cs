using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Administration;

namespace EC.AndreasLef.Sharepoint.Projekt.Code
{
    static class Logger
    {
        public static void WriteTrace(Exception ex)
        {
            SPDiagnosticsService diagSvc = SPDiagnosticsService.Local;
            diagSvc.WriteTrace(0,
                new SPDiagnosticsCategory("EC.AndreasLef Exception",
                    TraceSeverity.Monitorable,
                    EventSeverity.Error),
                TraceSeverity.Monitorable,
                "An exception occurred: {0}",
                new object[] { ex });
        }

        public static void WriteTrace(string message)
        {
            SPDiagnosticsService diagSvc = SPDiagnosticsService.Local;
            diagSvc.WriteTrace(0,
                new SPDiagnosticsCategory("EC AndreasLef LogEntry",
                    TraceSeverity.Monitorable,
                    EventSeverity.Error),
                TraceSeverity.Monitorable,
                 message);
        }
    }
}
