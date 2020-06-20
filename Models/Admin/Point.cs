using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildWebWithDotNetCore.Models.Admin
{
    public class Point
    {
        public double y;
        public string label;

        public Point() { }
        public Point(string L, double Y)
        {
            label = L;
            y = Y;
        }
    }
}
