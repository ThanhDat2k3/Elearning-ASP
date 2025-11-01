using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elearning.Utilities
{
    public class Functions
    {
        public static string GiaoVienSlugGeneration(string type,long id,string? title)
        {
            return type + "-"+id.ToString() + "-" + SlugGenerator.SlugGenerator.GenerateSlug(title) + ".html";
        }
    }
}