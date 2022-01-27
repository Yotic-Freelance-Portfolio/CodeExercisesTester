using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

            Exercise ex = new Exercise
            {
                Answers = new string[] { "Who Trahat" , "ANY"},
                Disription = "Find Hagy-Wagy???",
                GoodAnswer = 0,//"Blue Hagy-Wagy",
                Name = "Hagy-Wagy",
                StartCode = @"SHO
                NUSHO
                Console."
            };
            ex.hui = "qq";
            File.WriteAllText(Wrapper.path + "\\1", JsonSerializer.Serialize(ex, new JsonSerializerOptions
            {
                WriteIndented = true,
            }));

            Location = Wrapper.location;
            MouseDown += Controls_MouseDown;
            new GetControls().Get(this, new Type[] { typeof(Label), typeof(Panel), typeof(PictureBox) }).ToList().ForEach(x => x.MouseDown += Controls_MouseDown);
            menuTabs = new Tabs(ref panel1, ref panel2);
            files = Directory.GetFiles(Wrapper.path);
            if(Wrapper.exercises.Count == 0)
            {
                for (int i = 0; i < files.Length; i += 1)
                    Wrapper.exercises.Add(JsonSerializer.Deserialize<Exercise>(File.ReadAllText(files[i])));
                for (int i = 0; i < Wrapper.exercises.Count; i += 1)
                {
                    Form form = new Form
                    {
                        Size = new Size(635, 418),
                        FormBorderStyle = FormBorderStyle.None,
                        BackColor = Color.FromArgb(40, 40, 40),
                    };
                    form.MouseDown += FormControls_MouseDown;
                }
            }
        }
        public void FormControls_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                ((Control)sender).Capture = false;
                var m = Message.Create(Handle, 0xa1, new IntPtr(0x2), IntPtr.Zero);
                WndProc(ref m);
            }
        }
        private string[] files;
        private Tabs menuTabs;        
        private void button3_Click(object sender, EventArgs e) => Process.GetCurrentProcess().Kill();
        private void button1_Click(object sender, EventArgs e)
        {
            new Main() { Location = Wrapper.location }.Show();
            UnLoad();
            Hide();
        }
        private ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount * 2 };
        private void UnLoad()
        {
            Parallel.For(0, Wrapper.exercises.Count, parallelOptions, i => {
                File.WriteAllText(Wrapper.path + "\\" + i + ".json", JsonSerializer.Serialize(Wrapper.exercises[i]));
            });
        }
    }
}
