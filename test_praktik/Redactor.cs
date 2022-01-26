using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_praktik
{
    public partial class Redactor : Form
    {
        #region Misc
        public void Controls_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                ((Control)sender).Capture = false;
                var m = Message.Create(Handle, 0xa1, new IntPtr(0x2), IntPtr.Zero);
                WndProc(ref m);
                Wrapper.location = Location;
            }
        }
        #endregion
        public Redactor()
        {
            InitializeComponent();
            Location = Wrapper.location;
            MouseDown += Controls_MouseDown;
            new GetControls().Get(this, new Type[] { typeof(Label), typeof(Panel), typeof(PictureBox) }).ToList().ForEach(x => x.MouseDown += Controls_MouseDown);
            Tabs menuTabs = new Tabs(ref panel1, ref panel2);
            menuTabs.AddTab();
        }
        List<Form> 
        private void button3_Click(object sender, EventArgs e) => Process.GetCurrentProcess().Kill();
        private void button1_Click(object sender, EventArgs e)
        {
            new Main() { Location = Wrapper.location }.Show();
            Hide();
        }

        private void Redactor_Load(object sender, EventArgs e)
        {

        }
    }
}
