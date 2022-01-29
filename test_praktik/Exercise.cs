using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace test_praktik
{
    internal class Exercise
    {
        public string Name { get; set; }
        public string Desription { get; set; }
        public string StartCode { get; set; }
        public string[] Answers { get; set; }
        public int GoodAnswer { get; set; }
    }
}