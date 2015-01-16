using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkydiverFL.Extensions.CodeSmith.Helpers
{
    public static class Dates
    {
        public static string GetDateString(this DateTime value, string format = "G")
        {
            return value.ToString(format);
        }
    }
}
