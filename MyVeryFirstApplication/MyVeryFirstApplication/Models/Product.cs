using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyVeryFirstApplication
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime expires { get; set; }
        public int expiresIn { get; set; }
        public string unit { get; set; }
        public int amount { get; set; }
        public string category { get; set; }


        public override string ToString()
        {
            return name;
        }
    }
}
