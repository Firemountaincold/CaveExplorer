using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaveExplorer
{
    public partial class WelcomeForm : Form
    {
        //属性数组
        int[] features = new int[5] { 10, 10, 10, 10, 10 };
        public WelcomeForm()
        {
            InitializeComponent();
        }
        
        private void buttonroll_Click(object sender, EventArgs e)
        {
            //随机属性值
            Random random = new Random();
            features[0] = 2 + random.Next(0, 20);
            features[1] = 2 + random.Next(0, (53 - features[0]) / 4);
            features[2] = 2 + random.Next(0, (52 - features[0] - features[1]) / 3);
            features[3] = 2 + random.Next(0, (51 - features[0] - features[1] - features[2]) / 2);
            features[4] = 2 + random.Next(0, 50 - features[0] - features[1] - features[2] - features[3]);
            string info = " " + features[0].ToString().PadRight(2) +
                "      " + features[1].ToString().PadRight(2) +
                "      " + features[2].ToString().PadRight(2) +
                "      " + features[3].ToString().PadRight(2) +
                "      " + features[4].ToString().PadRight(2);
            labelfeature.Text = info;
        }

        private void buttonload_Click(object sender, EventArgs e)
        {
            //读取存档
            string path = "save.cem";
            if (File.Exists(path))
            {
                string[] read = File.ReadAllLines(path);
                int step = Convert.ToInt32(read[0]);
                Charactor player = new Charactor(read[1]);
                bool item = true;
                for(int i = 2; i < read.Length; i++)
                {
                    if (read[i].Length > 0)
                    {
                        if(read[i]=="1995")
                        {
                            item = false;
                        }
                        else if (item)
                        {
                            player.bag.Add(new Items(read[i]));
                        }
                        else
                        {
                            player.events.Add(new Events(read[i]));
                        }
                    }
                }
                GameForm gf = new GameForm(player);
                gf.steps = step;
                gf.Show();
                Visible = false;

            }
            else
            {
                MessageBox.Show("没有发现存档文件！");
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //开始游戏
            Jobs job = Jobs.Fighter;
            if(rbEngineer.Checked)
            {
                job = Jobs.Engineer;
            }
            else if(rbBeliever.Checked)
            {
                job = Jobs.Believer;
            }
            Charactor player = new Charactor(job, features);
            GameForm gf = new GameForm(player);
            gf.Show();
            Visible = false;
        }
    }
}
