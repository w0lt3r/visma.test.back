using System;
using System.Collections.Generic;
using System.Text;
using TimeZoneConverter;

namespace SharedKernel.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime GetCurrentPeruvianDateTime()
        {
            var info = TZConvert.GetTimeZoneInfo("America/Lima");
            //var info = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            DateTimeOffset localServerTime = DateTimeOffset.Now;
            return TimeZoneInfo.ConvertTime(localServerTime, info).DateTime;
        }
    }
}