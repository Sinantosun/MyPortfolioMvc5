using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolioAspNetMvc5.DAL
{
    public static class ExtensionValidator
    {
        public static bool checkExtension(string ex)
        {
            if (ex == ".jpg" || ex == ".png" || ex == ".jpeg")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}