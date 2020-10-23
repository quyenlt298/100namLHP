using System;

namespace TVA.COMMON.Library.Convert
{
    public static class ConvertDateTime
    {
        public static DateTime ToVnTime(this DateTime now)
        {
            return now.AddHours(7);
        }
    }
}
