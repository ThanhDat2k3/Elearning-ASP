using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Elearning.Utilities
{
    public static class Functions
    {
       
        public static string GiaoVienSlugGeneration(string prefix, long id, string? name)
        {
            return $"{prefix}-{id}-{name}";
        }
    }
}