using System;
using System.Linq;

namespace MainDemo.Reports
{
    public static class NamespaceHelper
    {
        public static string Get()
        {
            return typeof(NamespaceHelper).Assembly.GetName().Name;
        }
    }
}
