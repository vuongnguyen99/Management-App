using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Common.Common
{
    public static class DateTimeHelper
    {
        public static DateTime? ConvertDateTimeLocalToUTC(DateTime? dateTime)
        {
            if (dateTime == null)
                return null;
            else 
                return DateTime.SpecifyKind((DateTime)dateTime, DateTimeKind.Utc);
        } 
    }
}
