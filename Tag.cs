using System;
using System.Collections.Generic;
using System.Text;

namespace Nerdogramm
{
    public class Tag
    {
        public int points { get; set; }
        public String name { get; set; }

        public Tag(int points, String name)
        {
            this.points = points;
            this.name = name;
        }
    }
}
