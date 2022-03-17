using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CaveExplorer
{
    public partial class GameForm : Form
    {
        //角色
        public Charactor player;
        public List<Items> itemlist=new List<Items>();
        public List<Caves> fightcaves=new List<Caves>();
        public List<Caves> findcaves = new List<Caves>();
        public List<Caves> eventcaves = new List<Caves>();
        public int steps = 0;
        public GameForm(Charactor p)
        {
            InitializeComponent();
            player = p;
            LoadItems();
            LoadCaves();
            FreshPhoto();
            FreshStatus();
            FreshBag();
        }

        public void LoadItems()
        {
            //载入物品
            string filepath = "data/items.csv";
            string[] itemstemp = File.ReadAllLines(filepath);
            for (int i = 0; i < itemstemp.Length; i++)
            {
                if (itemstemp[i].Length>9)
                {
                    itemlist.Add(new Items(itemstemp[i]));
                }
            }
        }

        public void LoadCaves()
        {
            //载入洞穴
            string[] filepath = { "data/fights.csv", "data/finds.csv", "data/events.csv" };
            string[] cavetemp1 = File.ReadAllLines(filepath[0]);
            for (int i = 0; i < cavetemp1.Length; i++)
            {
                if (cavetemp1[i].Length > 0)
                {
                    fightcaves.Add(new Caves(CaveType.Fight, cavetemp1[i]));
                }
            }
            string[] cavetemp2 = File.ReadAllLines(filepath[1]);
            for (int i = 0; i < cavetemp2.Length; i++)
            {
                if (cavetemp2[i].Length > 0)
                {
                    findcaves.Add(new Caves(CaveType.Find, cavetemp2[i]));
                }
            }
            string[] cavetemp3 = File.ReadAllLines(filepath[2]);
            for (int i = 0; i < cavetemp3.Length; i++)
            {
                if (cavetemp3[i].Length > 0)
                {
                    eventcaves.Add(new Caves(CaveType.Event, cavetemp3[i]));
                }
            }
        }

        public int[] GetCaveIndex(int step)
        {
            //获取可出现的洞穴编号
            int[] caveindex = new int[3];
            caveindex[0] = fightcaves.Count;
            caveindex[1] = findcaves.Count;
            caveindex[2] = eventcaves.Count;
            for (int i = 0; i < fightcaves.Count; i++) 
            {
                if(fightcaves[i].fights.step>step)
                {
                    caveindex[0] = i;
                    break;
                }
            }
            for (int i = 0; i < findcaves.Count; i++)
            {
                if (findcaves[i].find.step > step)
                {
                    caveindex[1] = i;
                    break;
                }
            }
            for (int i = 0; i < eventcaves.Count; i++)
            {
                if (eventcaves[i].events.step > step)
                {
                    caveindex[2] = i;
                    break;
                }
            }
            return caveindex;
        }

        public void FreshPhoto()
        {
            //刷新角色图
            switch(player.job)
            {
                case Jobs.Fighter:
                    charactorPhoto.Image = Properties.Resources.fighter;
                    break;
                case Jobs.Engineer:
                    charactorPhoto.Image = Properties.Resources.engineer;
                    break;
                case Jobs.Believer:
                    charactorPhoto.Image = Properties.Resources.believer;
                    break;
            }
            if (player.hp <= 0)
            {
                charactorPhoto.Image = WhiteAndBlack((Bitmap)charactorPhoto.Image);
            }
        }

        public void FreshStatus()
        {
            //刷新角色状态
            if (player.hp > 0)
            {
                string status_now = "生命：" + player.hp.ToString().PadLeft(3) + "/" + player.maxhp.ToString().PadLeft(3) +
                    "\r\n攻击：" + player.atk.ToString().PadLeft(7) + "\r\n敏捷：" + player.agi.ToString().PadLeft(7) +
                    "\r\n防御：" + player.def.ToString().PadLeft(7) + "\r\n幸运：" + player.luck.ToString().PadLeft(7) +
                    "\r\n状态：";
                switch (player.mood)
                {
                    case Mood.Happy:
                        status_now += "开心";
                        break;
                    case Mood.Angry:
                        status_now += "愤怒";
                        break;
                    case Mood.Sad:
                        status_now += "悲伤";
                        break;
                    case Mood.God:
                        status_now += "天神下凡";
                        break;
                }
                labelstatus.Text = status_now;
                hpBar.Maximum = player.maxhp;
                hpBar.Value = player.hp;
            }
            else
            {
                MessageBox.Show(player.name + "已死亡！");
                string status_now = "生命：" + player.hp.ToString().PadLeft(3) + "/" + player.maxhp.ToString().PadLeft(3) +
                    "\r\n攻击：" + player.atk.ToString().PadLeft(7) + "\r\n敏捷：" + player.agi.ToString().PadLeft(7) +
                    "\r\n防御：" + player.def.ToString().PadLeft(7) + "\r\n幸运：" + player.luck.ToString().PadLeft(7) +
                    "\r\n状态：已死亡";
                labelstatus.Text = status_now;
                FreshPhoto();
                buttonNext.Enabled = false;
            }
        }

        public void FreshBag()
        {
            //刷新背包
            labelbagstr.Text = player.BagString();
            comboBoxitem.Items.Clear();
            comboBoxitem.SelectedItem = null;
            comboBoxitem.Text = null;
            foreach(var i in player.bag)
            {
                if(i.type==ItemType.Use)
                {
                    comboBoxitem.Items.Add(i.name);
                }
            }
        }

        public Bitmap WhiteAndBlack(Bitmap image)
        {
            //生成黑白图像
            int width = image.Width;
            int height = image.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    int value = (color.R + color.G + color.B) / 3;
                    image.SetPixel(x, y, Color.FromArgb(value, value, value));
                }
            }
            return image;
        }

        public void SaveGame()
        {
            //保存存档
            string path = "save.cem";
            string save = steps.ToString() + "\r\n" + player.SaveString();
            File.WriteAllText(path, save);
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {           
            //关闭窗口时整个退出
            Application.Exit();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //退出询问
            if (MessageBox.Show("是否退出游戏？", "退出游戏", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (player.hp > 0)
                {
                    if (MessageBox.Show("是否保存角色存档？", "保存游戏", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        SaveGame();
                    }
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            //下一回合
            steps++;
            Text = "洞穴探险进行中！第" + steps.ToString() + "步";
            infomation.AppendText(player.Next());
            Random rr = new Random();
            int index = rr.Next(0, 100);
            int[] caveindex = GetCaveIndex(steps);
            Caves newcave;
            if(index < 60)
            {
                newcave = new Caves(fightcaves[rr.Next(0, caveindex[0])]);
            }
            else if(index<85)
            {
                newcave = new Caves(eventcaves[rr.Next(0, caveindex[1])]);
            }
            else
            {
                newcave = new Caves(findcaves[rr.Next(0, caveindex[2])]);
            }
            if (newcave.type == CaveType.Fight)
            {
                infomation.AppendText(newcave.fights.FightWith(player, itemlist));
            }
            else if(newcave.type == CaveType.Find)
            {
                infomation.AppendText(newcave.find.FindItem(player, itemlist));
            }
            else if (newcave.type == CaveType.Event)
            {
                infomation.AppendText(newcave.events.EventRun());
            }
            FreshBag();
            FreshStatus();
        }

        private void buttonUseItem_Click(object sender, EventArgs e)
        {
            //使用物品
            if (comboBoxitem.Items.Count!=0 && comboBoxitem.Items[comboBoxitem.SelectedIndex] != null)
            {
                infomation.AppendText(player.UseItem(comboBoxitem.Items[comboBoxitem.SelectedIndex].ToString()));
            }
            FreshBag();
            FreshStatus();
        }

        private void infomation_TextChanged(object sender, EventArgs e)
        {
            //自动滚动
            infomation.ScrollToCaret();
        }
    }
}
