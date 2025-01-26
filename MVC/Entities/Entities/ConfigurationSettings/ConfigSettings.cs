using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ConfigurationSettings
{
    public static class ConfigSettings
    {
        public static string ConnectionString { get; } = "data source=.;initial catalog= NOTE-SOLUTION-WEBSITE;Integrated security=true;TrustServerCertificate=True";
    }
}
