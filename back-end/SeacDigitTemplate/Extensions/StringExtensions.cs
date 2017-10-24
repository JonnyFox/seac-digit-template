using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeacDigitTemplate.Extensions
{
    public static class StringExtensions
    {
        public static int? ToNullableInt(this string s) {
            var nullable = new int?();
            if (int.TryParse(s, out int i)) {
                nullable = new int?(i);
            };
            return nullable;
        }
    }
}
