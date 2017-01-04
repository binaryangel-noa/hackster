using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleController
{
    public static class AppInsights
    {
        private static TelemetryClient mClient = new TelemetryClient();

        public static TelemetryClient Client
        {
            get { return mClient; }
        }
    }
}
