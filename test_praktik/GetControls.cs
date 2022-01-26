using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

internal class GetControls
{
    internal IEnumerable<Control> Get(Control control, Type[] type)
    {
        var controls = control.Controls.Cast<Control>();
        return controls.Cast<Control>().SelectMany(x => Get(x, type)).Concat(controls).Where(c => type.Contains(c.GetType()));
    }
}
