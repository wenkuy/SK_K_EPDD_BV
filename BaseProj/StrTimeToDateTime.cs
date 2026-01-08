using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseProj
{
    public class StrTimeToDateTime
    {
        public static DateTime StrToDateTime(string StrTime)
        {
            //1.时间格式检查 "yyyy-MM-dd HH:mm:ss" 2026-12-31 23:59:59
            // @"\d{4}(-\d{2}){2} (\d{2}:){2}\d{2}"

            string rule = @"\d{4}(-\d{2}){2} (\d{2}:){2}\d{2}";
            if (!Regex.IsMatch(StrTime, rule))
            {
                throw new FormatException("时间字符串格式不正确，应为yyyy-MM-dd HH:mm:ss");
            }

            //2.字符串转DateTime
            return DateTime.ParseExact(StrTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
