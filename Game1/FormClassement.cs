using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public partial class FormClassement : Form
    {
        private List<KeyValuePair<string, int>> list;

        public FormClassement()
        {
            InitializeComponent();
            list = new List<KeyValuePair<string, int>>();
        }

        private void FormClassement_Load(object sender, EventArgs e)
        {
            TableLayoutPanel panel = new TableLayoutPanel();
            panel.CellPaint += tableLayoutPanel_CellPaint;
            panel.ColumnCount = 3;
            panel.RowCount = 1;
            panel.Size = new Size(740, 500);
            panel.Location = new Point(20, 20);
            panel.AutoScroll = true;
            panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
            panel.Controls.Add(new Label() { Text = "POSITION", BackColor = Color.Transparent, Font = new Font(Font, FontStyle.Bold) }, 0, 0);
            panel.Controls.Add(new Label() { Text = "PSEUDO", BackColor = Color.Transparent, Font = new Font(Font, FontStyle.Bold) }, 1, 0);
            panel.Controls.Add(new Label() { Text = "SCORE", BackColor = Color.Transparent, Font = new Font(Font, FontStyle.Bold) }, 2, 0);

            // This text is added only once to the file.
            if (File.Exists(Game1.CLASSEMENT_PATH))
            {
                using (StreamReader sr = File.OpenText(Game1.CLASSEMENT_PATH))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] values = s.Split(':');
                        list.Add(new KeyValuePair<string, int>(values[0], int.Parse(values[1])));
                    }
                }
            }

            foreach (KeyValuePair<string, int> item in list.OrderByDescending(key => key.Value))
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
                panel.Controls.Add(new Label() { Text = panel.RowCount + "", BackColor = Color.Transparent }, 0, panel.RowCount);
                panel.Controls.Add(new Label() { Text = item.Key, BackColor = Color.Transparent }, 1, panel.RowCount);
                panel.Controls.Add(new Label() { Text = item.Value+"", BackColor = Color.Transparent }, 2, panel.RowCount);
                panel.RowCount++;
            }

            this.Controls.Add(panel);
        }

        private void tableLayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if(e.Row == 0)
                e.Graphics.FillRectangle(Brushes.FloralWhite, e.CellBounds);
            else if (e.Row % 2 == 0)
                e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.CellBounds);
            else
                e.Graphics.FillRectangle(Brushes.GhostWhite, e.CellBounds);
        }
    }
}
