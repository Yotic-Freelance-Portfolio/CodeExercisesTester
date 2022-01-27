using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace test_praktik
{
    [Serializable]
    internal class Exercise
    {
        internal string Name { get; set; }
        internal string Disription { get; set; }
        internal string StartCode { get; set; }
        internal string[] Answers { get; set; }
        internal int GoodAnswer { get; set; }
        public string hui;
    }
}