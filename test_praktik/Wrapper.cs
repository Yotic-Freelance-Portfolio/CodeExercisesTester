using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_praktik
{
    internal sealed class Wrapper
    {
        internal static string path;
        internal static Point location;
        internal static List<Form> exercisesForm = new List<Form>();
        internal static List<Exercise> exercises = new List<Exercise>();
    }
}
