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

            panel1.VerticalScroll.Enabled = true;
            panel1.VerticalScroll.Visible = false;
            panel1.VerticalScroll.Maximum = 0;
            panel1.AutoScroll = true;

            Location = Wrapper.location;
            MouseDown += Controls_MouseDown;
            new GetControls().Get(this, new Type[] { typeof(Label), typeof(Panel), typeof(PictureBox) }).ToList().ForEach(x => x.MouseDown += Controls_MouseDown);
            menuTabs = new Tabs(ref panel1, ref panel2);
            
            files = Directory.GetFiles(Wrapper.path);
            if(exercises.Count == 0)
            {
                test.Answers = new List<Answer>();
                for (int i = 0; i < files.Length; i++)
                {
                    if (i > 25) break;
                    exercises.Add(JsonSerializer.Deserialize<Exercise>(File.ReadAllText(files[i], Encoding.UTF8)));
                }
                for (int i = 0; i < exercises.Count; i = i + 1)
                {
                    Answer answer = new Answer 
                    {
                        GoodAnswer = exercises[i].GoodAnswer,
                        SelectAnswer = -1,
                        Index = i,
                    };
                    test.Answers.Add(answer);
                }
                test.MaxAnswers = exercises.Count - 1;
                for (int i = 0; i < exercises.Count; i = i + 1)
                {
                    Form form = new Form
                    {
                        Size = new Size(635, 418),
                        FormBorderStyle = FormBorderStyle.None,
                        BackColor = Color.FromArgb(40, 40, 40),                        
                    };
                    Panel code = new Panel
                    {
                        Location = new Point(8, 8),
                        Size = new Size(620, 229),
                        BackColor = Color.FromArgb(47, 47, 47),
                    };
                    Panel preCode = new Panel
                    {
                        Location = new Point(0, 0),
                        BackColor = Color.FromArgb(47, 47, 47),
                    };
                    code.Controls.Add(preCode);
                    form.MouseDown += FormControls_MouseDown;
                    string xDTEMFUCKEDCSHARP = exercises[i].Name;
                    int indexF = i;
                    form.Paint += new PaintEventHandler((object sender, PaintEventArgs e) => {
                        label1.Text = xDTEMFUCKEDCSHARP;
                        Graphics g = preCode.CreateGraphics();
                        string[] codeLines = exercises[indexF].StartCode.Replace("\r", "").Split('\n');
                        preCode.Size = new Size(620, 12 * codeLines.Length + 10);
                        g.Clear(preCode.BackColor);
                        for (int q = 0; q < codeLines.Length; q += 1)
                            g.DrawString(codeLines[q], new Font("Open Sans Semibold", 7F, FontStyle.Regular, GraphicsUnit.Point, 204), new SolidBrush(Color.Gray), new PointF(0, q * 10));
                        code.Paint += ((object key, PaintEventArgs e2) => {
                            g.Clear(preCode.BackColor);
                            for (int q = 0; q < codeLines.Length; q += 1)
                                g.DrawString(codeLines[q], new Font("Open Sans Semibold", 7F, FontStyle.Regular, GraphicsUnit.Point, 204), new SolidBrush(Color.Gray), new PointF(0, q * 10));
                        });
                        code.MouseWheel += ((object key, MouseEventArgs e2) => {
                            g.Clear(preCode.BackColor);
                            for (int q = 0; q < codeLines.Length; q += 1)
                                g.DrawString(codeLines[q], new Font("Open Sans Semibold", 7F, FontStyle.Regular, GraphicsUnit.Point, 204), new SolidBrush(Color.Gray), new PointF(0, q * 10));
                        });
                    });
                    SharePath.SetRoundedShape(code, 20, true, true, true, true);
                    string xDTEMFUCKEDCSHARP2 = exercises[i].Desription;
                    Label Description = new Label
                    {
                        AutoSize = true,
                        Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 204),
                        ForeColor = Color.Silver,
                        Location = new Point(8, 240),
                        Name = "desc" + i,
                        Size = new Size(128, 20),
                        TabIndex = 8,
                        Text = xDTEMFUCKEDCSHARP2,
                    };
                    for(int o = 0; o < exercises[i].Answers.Length; o += 1)
                    {
                        string xDTEMFUCKEDCSHARP3 = exercises[i].Answers[o];
                        Label answer = new Label
                        {
                            Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, 204),
                            ForeColor = Color.Silver,
                            Size = new Size(600, 24),
                            Location = new Point(8, 280 + o * 34),
                            Name = "answ" + i + o,
                            BackColor = Color.FromArgb(47, 47, 47),
                            TabIndex = o + 256,
                            Text = xDTEMFUCKEDCSHARP3,                                                    
                        };
                        SharePath.SetRoundedShape(answer, 10, true, true, true, true);
                        answer.MouseEnter += new EventHandler((object sender, EventArgs e) => {
                            answer.Font = new Font("Open Sans Semibold", 12.1F, FontStyle.Bold, GraphicsUnit.Point, 204);
                            if(!(answer.BackColor == Color.FromArgb(47, 100, 47)) && !(answer.BackColor == Color.FromArgb(100, 47, 47)))
                                answer.BackColor = Color.FromArgb(50, 50, 50);
                        });
                        answer.MouseLeave += new EventHandler((object sender, EventArgs e) => {
                            answer.Font = new Font("Open Sans Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
                            if (!(answer.BackColor == Color.FromArgb(47, 100, 47)) && !(answer.BackColor == Color.FromArgb(100, 47, 47)))
                                answer.BackColor = Color.FromArgb(47, 47, 47);
                        });
                        int ix = i;
                        int ox = o;
                        answer.Click += new EventHandler((object sender, EventArgs e) => {
                            if(test.Answers[ix].SelectAnswer == -1)
                            {
                                test.Answers[ix].SelectAnswer = ox;
                                if (test.Answers[ix].SelectAnswer == test.Answers[ix].GoodAnswer)
                                {
                                    answer.BackColor = Color.FromArgb(47, 100, 47);
                                    test.GoodAnsers += 1;
                                }
                                else answer.BackColor = Color.FromArgb(100, 47, 47);
                                menuTabs.Tabses[ix].CancelBackColor(answer.BackColor);
                                menuTabs.Select(ix + 1);
                                test.AnswersCount += 1;
                                if(test.MaxAnswers == test.AnswersCount)
                                {
                                    test.End = true;
                                    Save();
                                    TopMost = true;
                                }
                            }
                        });
                        form.Controls.Add(answer);
                    }
                    code.VerticalScroll.Enabled = true;
                    code.VerticalScroll.Visible = false;
                    code.VerticalScroll.Maximum = 0;
                    code.VerticalScroll.LargeChange = 5;
                    code.VerticalScroll.SmallChange = 1;
                    code.AutoScroll = true;
                    form.Controls.Add(code);
                    form.Controls.Add(Description);
                    exercisesForm.Add(form);
                }
                for (int i = 0; i < exercisesForm.Count; i = i + 1)
                    menuTabs.AddTab(new Tabs.Tab($"#{i} {exercises[i].Name}", exercisesForm[i], i, new Size(165, 30)));
            }
            menuTabs.Init();
        }
        private List<Form> exercisesForm = new List<Form>();
        private List<Exercise> exercises = new List<Exercise>();
        private Test test = new Test();
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
        private void button3_Click(object sender, EventArgs e)
        {
            if(!TopMost) Process.GetCurrentProcess().Kill();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!TopMost)
            {
                Wrapper.main.Location = Location;
                Wrapper.main.Show();
                Hide();
            }
        }        
        private void Save() => File.WriteAllText(Wrapper.pathToSave + DateTime.Now.ToString("dddd, dd MMMM yyyy HH,mm,ss") + ".json", JsonSerializer.Serialize(test), Encoding.UTF8);
    }
}
