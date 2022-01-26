using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_praktik
{
    public partial class Main : Form
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
        public Main()
        {
            InitializeComponent();
            if(Wrapper.location != null) Location = Wrapper.location;
            MouseDown += Controls_MouseDown;
            new GetControls().Get(this, new Type[] { typeof(Label), typeof(Panel), typeof(PictureBox) }).ToList().ForEach(x => x.MouseDown += Controls_MouseDown);
            pfc.AddFontFile("Minecraft.ttf");
            font = new Font(pfc.Families[0], 12, FontStyle.Regular);
            bigFont = new Font(pfc.Families[0], 72, FontStyle.Regular);
            for (int i = 0; i < 25; i += 1)
            {
                string line = "";
                for (int o = 0; o < 200; o += 1)
                {
                    line += rnd.Next(-100, 100) > 0 ? "1" : "0";
                }
                CodeLines.Add(line);
            }
            g = panel1.CreateGraphics();
        }
        private List<string> CodeLines = new List<string>();
        private Random rnd = new Random();
        private Pen pen = new Pen(Color.LightGreen);
        private Brush drawBrush = new SolidBrush(Color.Green);
        private Brush fillBrush = new SolidBrush(Color.FromArgb(47, 47, 47));
        private Brush bigBrush = new SolidBrush(Color.DarkGreen);
        private PrivateFontCollection pfc = new PrivateFontCollection();
        private Font font;
        private Font bigFont;
        private Graphics g;
        private void timer1_Tick(object sender, EventArgs e)
        {
            g.FillPolygon(fillBrush, new Point[4] { new Point(0, 0), new Point(0, panel1.Height), new Point(panel1.Width, panel1.Height), new Point(panel1.Width, 0)});
            string line = "";
            CodeLines.RemoveAt(0);
            for(int o = 0; o < 100; o += 1)
                line += rnd.Next(-100, 100) > 0 ? "1" : "0";
            CodeLines.Add(line);
            for (int i = 0; i < 25; i += 1)
                g.DrawString(CodeLines[i], font, drawBrush, 0, i * 25);            
        }
        private void button2_Click(object sender, EventArgs e) => Process.GetCurrentProcess().Kill();
        private void button1_Click(object sender, EventArgs e)
        {
            new Redactor(){ Location = Wrapper.location }.Show();
            Hide();
        }
        
    }
}
