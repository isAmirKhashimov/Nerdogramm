using System;
using System.Collections.Generic;
using System.Text;

namespace Nerdogramm
{
    public class Group
    {
        public int id { get; set; }
        public String header { get; set; }

        public List<Tag> tags { get; set; }
        public Group()
        {
            tags = new List<Tag>();
        }
        
    }
}
