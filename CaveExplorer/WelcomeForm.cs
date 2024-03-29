﻿using System;
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
        GameForm gf;
        public WelcomeForm()
        {
            InitializeComponent();
            groupBoxfeature.BackColor = Color.Transparent;
            groupBoxjob.BackColor = Color.Transparent;
        }

        public void FreshFeatures()
        {
            //刷新属性值
            string info = " " + features[0].ToString().PadRight(2) +
                "      " + features[1].ToString().PadRight(2) +
                "      " + features[2].ToString().PadRight(2) +
                "      " + features[3].ToString().PadRight(2) +
                "      " + features[4].ToString().PadRight(2);
            labelfeature.Text = info;
        }

        private void buttonroll_Click(object sender, EventArgs e)
        {
            //随机属性值
            Random random = new Random();
            int basenum = 3;
            if (rbEngineer.Checked)
            {
                basenum = 5;
            }
            features[0] = basenum + random.Next(0, 17);
            features[1] = basenum + random.Next(0, (56 - features[0]) / 2);
            features[2] = basenum + random.Next(0, (56 - features[0] - features[1]) * 2 / 3);
            features[3] = basenum + random.Next(0, (56 - features[0] - features[1] - features[2]));
            features[4] = basenum + 56 - features[0] - features[1] - features[2] - features[3];
            FreshFeatures();
        }

        private void buttonload_Click(object sender, EventArgs e)
        {
            //读取存档
            string path = "save.cem";
            if (File.Exists(path))
            {
                string[] read = File.ReadAllLines(path);
                int step = Convert.ToInt32(read[0]);
                Charactor player = new Charactor(read[1], read[2]);
                bool item = true;
                for(int i = 3; i < read.Length; i++)
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
                GameForm gf = new GameForm(player, this);
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
            gf = new GameForm(player, this);
            gf.Show();
            Visible = false;
        }

        private void buttonScore_Click(object sender, EventArgs e)
        {
            //高分榜
            HighScore highScore = new HighScore();
            highScore.ShowDialog();
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            //关于
            About about = new About();
            about.ShowDialog();
        }

        private void rbFighter_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFighter.Checked)
            {
                labeldescription.Text = "斗士：基础暴击率从15%提升至35%。";
            }
        }

        private void rbEngineer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEngineer.Checked)
            {
                labeldescription.Text = "工程师：背包从10格提升至12格，10点属性奖励。";
                for (int i = 0; i < features.Length; i++)
                {
                    features[i] += 2;
                }
            }
            else
            {
                for(int i = 0; i < features.Length; i++)
                {
                    features[i] -= 2;
                }
            }
            FreshFeatures();
        }

        private void rbBeliever_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBeliever.Checked)
            {
                labeldescription.Text = "信徒：每走一步回复3点血量。";
            }
        }
    }
}
