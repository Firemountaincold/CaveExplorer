using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaveExplorer
{
    public partial class HighScore : Form
    {
        public string scorepath = "score.cem";
        public HighScore()
        {
            InitializeComponent();
            PrintScore();
        }

        public DataTable GetScore()
        {
            //从文件中读取高分榜
            DataTable data = new DataTable();
            data.Columns.Add("score");
            data.Columns.Add("win");
            data.Columns.Add("step");
            data.Columns.Add("job");
            data.Columns.Add("datatime");
            if (!File.Exists(scorepath))
            {
                FileStream fs = File.Create(scorepath);
                fs.Close();
            }
            string[] scores = File.ReadAllLines(scorepath);
            foreach(string s in scores)
            {
                if (s.Length > 0)
                {
                    string[] vs = s.Split(',');
                    DataRow row = data.NewRow();
                    row[0] = vs[1];
                    row[2] = vs[2];
                    if (vs[3] == "Fighter")
                    {
                        row[3] = "斗士";
                    }
                    else if (vs[3] == "Engineer")
                    {
                        row[3] = "工程师";
                    }
                    else if (vs[3] == "Believer")
                    {
                        row[3] = "信徒";
                    }
                    row[4] = vs[4];
                    if (vs[0] == "0")
                    {
                        row[1] = "未通关";
                    }
                    else if (vs[0] == "1")
                    {
                        row[1] = "已通关";
                    }
                    data.Rows.Add(row);
                }
            }
            return data;
        }

        public void PrintScore()
        {
            //显示高分榜
            DataTable score = GetScore();
            BindingSource bs = new BindingSource();
            bs.DataSource = score;
            dataGridView.DataSource = bs;
        }

        private void buttonok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
