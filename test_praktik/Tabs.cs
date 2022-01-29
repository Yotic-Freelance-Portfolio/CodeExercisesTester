using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


internal class Tabs
{
    private int select = 0;
    private bool vertical;
    private Panel TabOwner;
    private Panel Display;
    internal List<Tab> Tabses = new List<Tab>();
    internal Tabs(ref Panel panel, ref Panel formDisplay) : this(ref panel, ref formDisplay, false) { }
    internal Tabs(ref Panel panel, ref Panel formDisplay, bool vertical)
    {
        TabOwner = panel;
        Display = formDisplay;
        this.vertical = vertical;
    }
    internal void AddTab(Tab tab)
    {
        Tabses.Add(tab);
        tab.TabButton.Click += Click;
        TabOwner.Controls.Add(tab.TabButton);
    }
    internal void Init()
    {
        ReloadTab();
    }
    internal void Click(object sender, EventArgs e)
    {
        int preSelect = select;
        select = ((Control)sender).TabIndex - 1000;
        if (preSelect != select)
        {
            Tabses[preSelect].TabButton.TextAlign = ContentAlignment.MiddleLeft;
            ReloadTab();
        }
    }
    internal void Select(int newSelect)
    {
        if(newSelect < Tabses.Count)
        {
            int preSelect = select;
            select = newSelect;
            if (preSelect != select)
            {
                Tabses[preSelect].TabButton.TextAlign = ContentAlignment.MiddleLeft;
                ReloadTab();
            }
        }
    }
    private void ReloadTab()
    {
        foreach (Control control in Display.Controls)
            Display.Controls.Remove(control);
        Tabses[select].TabButton.TextAlign = ContentAlignment.MiddleCenter;
        Tabses[select].ShowForm.TopLevel = false;
        Display.Controls.Add(Tabses[select].ShowForm);
        Tabses[select].ShowForm.Show();
    }
    internal class Tab
    {
        internal Tab(string name, Form form, int index, Size size, bool vertical)
        {
            Name = name;
            ShowForm = form;
            Size = size;
            Vertical = vertical;
            rects = new PointF[4] { new PointF(0, size.Height), new PointF(size.Width, size.Height), new PointF(size.Width, size.Height - 2), new PointF(0, size.Height - 2) };
            TabButton = new Button();
            TabButton.TabIndex = index + 1000;
            TabButton.BackColor = Color.FromArgb(56, 56, 56);
            TabButton.Font = new Font("Open Sans Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            TabButton.ForeColor = Color.Silver;
            TabButton.Location = Vertical ? new Point(size.Width * index, 0) : new Point(0, size.Height * index);
            TabButton.Name = name;
            TabButton.Text = name;
            TabButton.Size = size;
            TabButton.UseVisualStyleBackColor = false;
            TabButton.FlatStyle = FlatStyle.Flat;
            TabButton.FlatAppearance.BorderSize = 0;
            TabButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 62, 62);
            TabButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(69, 69, 69);
            TabButton.TextAlign = ContentAlignment.MiddleLeft;
            g = TabButton.CreateGraphics();
            TabButton.MouseLeave += Leave;
            TabButton.MouseEnter += Enter;
        }
        public void CancelBackColor(Color color)
        {
            TabButton.MouseLeave -= Leave;
            TabButton.MouseEnter -= Enter;
            TabButton.BackColor = color;
            TabButton.FlatAppearance.MouseDownBackColor = color;
            TabButton.FlatAppearance.MouseOverBackColor = color;
            TabButton.FlatAppearance.CheckedBackColor = color;
        }
        private void Enter(object sender, EventArgs e)
        {
            TabButton.Font = new Font("Open Sans Semibold", 12.1F, FontStyle.Bold, GraphicsUnit.Point, 204);
        }
        private void Leave(object sender, EventArgs e)
        {
            TabButton.Font = new Font("Open Sans Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
        }
        internal Tab(string name, Form form, int index, Size size) : this(name, form, index, size, false) { }
        private Graphics g { get; set; }
        internal PointF[] rects { get; set; }
        internal string Name { get; set; }
        internal Button TabButton { get; set; }
        internal Form ShowForm { get; set; }
        internal Size Size { get; set; }
        internal bool Vertical { get; set; }
    }
}