using System;

namespace Sopfim.CustomControls
{
    public static class SopfimDateTimeExtension
    {
            public static DateTime? ToSopfimDateTime(this DateTime? input)
            {
                DateTime? newDate;
                if (input.HasValue)
                {
                    newDate = new DateTime(input.Value.Year, input.Value.Month, input.Value.Day, input.Value.Hour > 11 ? 17 : 5, 0, 0);
                }
                else
                {
                    newDate = null;
                }
                return newDate;
            }
    }
}