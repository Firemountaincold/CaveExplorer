using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
        public List<Label> labels = new List<Label>();
        public List<LinkLabel> links = new List<LinkLabel>();
        public List<string> infos = new List<string>();
        ToolTip toolTip = new ToolTip();
        ToolTip phototip = new ToolTip();
        public int[] linklabelevent = new int[12];
        public int steps = 0;
        public int pageindex = 0;
        public bool isDead = false;
        public bool autostart = false;
        public bool allwin = false;
        WelcomeForm parent;
        public string scorepath = "score.cem";
        public string Lupevent = "升阶,你感觉到了神秘的指引，你对自己的职业的理解更深了！,0,0,0,0,0,0,0,0,0,God";
        public bool isLup = false;
        public string Recoverevent = "决战时刻,你发现了洞穴的尽头，那里栖息着强大的魔王，你休息了一会，做好了最后一战的准备。,999,15,0,0,0,0,0,0,0,None";

        public GameForm(Charactor p, WelcomeForm parent)
        {
            InitializeComponent();
            pictureBoxPlayer.BackColor = Color.Transparent;
            pictureBoxEnemy.BackColor = Color.Transparent;
            pictureBoxAttack.BackColor = Color.Transparent;
            checkBoxauto.BackColor = Color.Transparent;
            labelPhp.BackColor = Color.Transparent;
            labelEhp.BackColor = Color.Transparent;
            player = p;
            this.parent = parent;
        }

        public void LoadSave()
        {
            //读存档时的操作
            if(player.job==Jobs.Fighter2|| player.job == Jobs.Engineer2|| player.job == Jobs.Believer2)
            {
                isLup = true;
            }
            if (steps > 0)
            {
                for(int i = 0; i < steps; i++)
                {
                    infos.Add("这一页在历史的风沙中遗失了……");
                }
            }

        }

        public void LoadLinkLabel()
        {
            //创建列表
            labels.Add(label1);
            labels.Add(label2);
            labels.Add(label3);
            labels.Add(label4);
            labels.Add(label5);
            labels.Add(label6);
            labels.Add(label7);
            labels.Add(label8);
            labels.Add(label9);
            labels.Add(label10);
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 100;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            toolTip.IsBalloon = true;
            links.Add(linkLabel1);
            links.Add(linkLabel2);
            links.Add(linkLabel3);
            links.Add(linkLabel4);
            links.Add(linkLabel5);
            links.Add(linkLabel6);
            links.Add(linkLabel7);
            links.Add(linkLabel8);
            links.Add(linkLabel9);
            links.Add(linkLabel10);
            for (int i = 0; i < linklabelevent.Length; i++) 
            {
                linklabelevent[i] = 0;
            }
            if (player.job == Jobs.Engineer || player.job == Jobs.Engineer2)
            {
                labels.Add(labele1);
                labels.Add(labele2);
                labele1.Visible = true;
                labele2.Visible = true;
                links.Add(linkLabele1);
                links.Add(linkLabele2);
            }
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
            phototip.RemoveAll();
            switch(player.job)
            {
                case Jobs.Fighter:
                    charactorPhoto.Image = Properties.Resources.fighter;
                    phototip.SetToolTip(charactorPhoto, "斗士：\r\n基础暴击率从15%提升至25%。");
                    break;
                case Jobs.Engineer:
                    charactorPhoto.Image = Properties.Resources.engineer;
                    phototip.SetToolTip(charactorPhoto, "工程师：\r\n背包从10格提升至12格。");
                    break;
                case Jobs.Believer:
                    charactorPhoto.Image = Properties.Resources.believer;
                    phototip.SetToolTip(charactorPhoto, "信徒：\r\n每走一步回复1点血量。");
                    break;
                case Jobs.Fighter2:
                    charactorPhoto.Image = Properties.Resources.fighter2;
                    phototip.SetToolTip(charactorPhoto, "血斗士：\r\n斗士的转职。\r\n基础暴击率从15%提升至25%，并有10%的吸血。");
                    break;
                case Jobs.Engineer2:
                    charactorPhoto.Image = Properties.Resources.engineer2;
                    phototip.SetToolTip(charactorPhoto, "机械师：\r\n工程师的转职。\r\n背包从10格提升至12格，每次丢弃物品或失去buff会恢复20点血量。");
                    break;
                case Jobs.Believer2:
                    charactorPhoto.Image = Properties.Resources.believer2;
                    phototip.SetToolTip(charactorPhoto, "圣徒：\r\n信徒的转职。\r\n每走一步回复1点血量，并增加1点随机属性。");
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
                ClearAllEvents();
                hpBar.Value = 0;
                isDead = true;
                SaveScore(false);
                buttonNext.Text = "重新开始";
            }
        }

        public void ClearAllEvents()
        {
            // 清除事件绑定的函数
            for(int i = 0; i < linklabelevent.Length; i++)
            {
                if (linklabelevent[i] == 1)
                {
                    links[i].Click -= new EventHandler(UseItem);
                    linklabelevent[i] = 0;
                }
                else if (linklabelevent[i] == 2)
                {
                    links[i].Click -= new EventHandler(ThrowItem);
                    linklabelevent[i] = 0;
                }
            }
        }

        public void FreshBag()
        {
            //刷新背包
            player.BagString(labels, labelbuff);
            ClearAllEvents();                
            foreach(var l in links)
            {
                l.Visible = false;
            }
            for (int i = 0; i < player.bag.Count; i++)
            {
                if(player.bag[i].type==ItemType.Use)
                {
                    links[i].Text = "使用";
                    links[i].Click += new EventHandler(UseItem);
                    linklabelevent[i] = 1;
                }
                else if(player.bag[i].type==ItemType.Auto)
                {
                    links[i].Text = "丢弃";
                    links[i].Click += new EventHandler(ThrowItem);
                    linklabelevent[i] = 2;
                }
                else if (player.bag[i].type == ItemType.Hold)
                {
                    links[i].Text = "放弃";
                    links[i].Click += new EventHandler(ThrowItem);
                    linklabelevent[i] = 2;
                }
                links[i].Visible = true;
            }
            AddToolTips();
        }

        public void FreshPage(int page)
        {
            //刷新信息页数
            int pages = infos.Count;
            if (pages == 0)
            {
                buttonllastpage.Enabled = false;
                buttonextpage.Enabled = false;
            }
            else if (page <= 1)
            {
                buttonllastpage.Enabled = false;
                buttonextpage.Enabled = true;
            }
            else if(page >= pages)
            {
                buttonllastpage.Enabled = true;
                buttonextpage.Enabled = false;
            }
            else
            {
                buttonllastpage.Enabled = true;
                buttonextpage.Enabled = true;
            }
            if (page > 0)
            {
                infomation.Text = infos[page - 1];
            }
            textBoxpage.Text = page.ToString() + "/" + pages.ToString();
        }

        public void FreshStep(int[] caveindex)
        {
            //刷新步数
            if (labelstep.BackColor != Color.Transparent)
            {
                labelstep.BackColor = Color.Transparent;
            }
            string info = "步数：" + steps.ToString();
            info += "\r\n\r\n目前可遇到的洞穴：\r\n    战斗：" + caveindex[0] + "\r\n    探索：" +
                caveindex[1] + "\r\n    遭遇：" + caveindex[2];
            labelstep.Text = info;
        }

        public void AddToolTips()
        {
            //生成物品说明
            toolTip.RemoveAll();
            for(int i = 0; i < player.bag.Count; i++)
            {               
                toolTip.SetToolTip(labels[i], player.bag[i].TooltipString());
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

        public void SaveScore(bool win)
        {
            //保存最高分
            int score = player.hp * 5 + player.atk * 15 + player.agi * 10 + player.def * 20 + player.luck * 15;
            score += player.stats.items * 3 + player.stats.useitems * 5 - player.stats.dropitems * 2;
            score += player.stats.events * 5 + player.stats.enemykill.Sum() * 2;
            score += player.stats.enemy.Count * 10;
            if (allwin)
            {
                score += 5000;
            }
            string info = "";
            if (win)
            {
                info += "1,";
            }
            else
            {
                info += "0,";
            }
            info += score + "," + steps + "," + player.job.ToString() + ",";
            info += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
            File.AppendAllText(scorepath, info);
            SortScore();
        }

        public void SortScore()
        {
            //高分榜排序
            string[] filestr = File.ReadAllLines(scorepath);
            int[] scores = new int[filestr.Length];
            int k = 0;
            for(int i = 0; i < filestr.Length; i++)
            {
                if (filestr[i].Length > 0)
                {
                    scores[i] = Convert.ToInt32(filestr[i].Split(',')[1]);
                    k++;
                }
                else
                {
                    scores[i] = 0;
                }
            }
            if (k > 10)
            {
                k = 10;
            }
            string[] newfile = new string[k];
            for (int j = 0; j < k; j++)
            {
                int index = 0;
                for (int i = 0; i < scores.Length; i++)
                {
                    if (scores[i] > scores[index])
                    {
                        index = i;
                    }
                }
                scores[index] = 0;
                newfile[j] = filestr[index];
            }
            File.Delete(scorepath);
            File.AppendAllLines(scorepath, newfile);
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //关闭窗口时整个退出
            if (!parent.Visible)
            {
                Application.Exit();
            }
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //退出询问
            if (!isDead)
            {
                if (MessageBox.Show("是否退出游戏？", "退出游戏", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (MessageBox.Show("是否保存角色存档？", "保存游戏", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        SaveGame();
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        public async Task NextStep()
        {
            //下一步
            steps++;
            pageindex = steps;
            infomation.Text = "               ~洞穴探险进行中！第" + steps.ToString() + "步~\r\n";
            infomation.AppendText(player.Next());
            Random rr = new Random();
            int index = rr.Next(0, 100);
            int[] caveindex = GetCaveIndex(steps);
            Caves newcave;
            if (index < 60)
            {
                newcave = new Caves(fightcaves[rr.Next(0, caveindex[0])]);
                player.stats.AddEnemy(newcave);
            }
            else if (index < 85)
            {
                newcave = new Caves(eventcaves[rr.Next(0, caveindex[2])]);
                player.stats.events++;
            }
            else
            {
                newcave = new Caves(findcaves[rr.Next(0, caveindex[1])]);
            }
            if (!isLup && steps > 100)
            {
                if (rr.Next(100) > 96)
                {
                    newcave = new Caves(CaveType.Event, Lupevent);
                    if (player.job == Jobs.Fighter)
                    {
                        player.job = Jobs.Fighter2;
                    }
                    else if (player.job == Jobs.Engineer)
                    {
                        player.job = Jobs.Engineer2;
                    }
                    else if (player.job == Jobs.Believer)
                    {
                        player.job = Jobs.Believer2;
                    }
                }
            }
            if (steps == 499)
            {
                //boss战准备
                newcave = new Caves(CaveType.Event, Recoverevent);
                player.stats.events++;
            }
            else if (steps == 500)
            {
                //boss战
                int bosshp = 2000 + rr.Next(1000);
                int bossatk = player.def / 2 + 5 + rr.Next(15);
                int bossdef = player.atk * 2 - 10 - rr.Next(30);
                newcave = new Caves(CaveType.Fight, "多元虚空魔王," + bosshp + "," + bossatk + ",1000," + bossdef + ",0,0");
            }
            ItemEventPanel iep = new ItemEventPanel(true, panelbattle, pictureBoxAttack, labelbattle);
            buttonNext.Enabled = false;
            if (newcave.type == CaveType.Fight)
            {
                Battle b = new Battle(player, newcave, panelbattle, labelPhp, labelEhp, labelbattle, pictureBoxPlayer, pictureBoxEnemy, pictureBoxAttack);
                infomation.AppendText(await newcave.fights.FightWith(player, itemlist, b, iep));
            }
            else if (newcave.type == CaveType.Find)
            {
                infomation.AppendText(await newcave.find.FindItem(player, itemlist, iep));
            }
            else if (newcave.type == CaveType.Event)
            {
                iep = new ItemEventPanel(false, panelbattle, pictureBoxAttack, labelbattle);
                infomation.AppendText(await newcave.events.EventRun(player, iep));
            }
            buttonNext.Enabled = true;
            if (!isLup && player.job == Jobs.Fighter2)
            {
                FreshPhoto();
                isLup = true;
                infomation.AppendText("[职业信息]你已转职成为——血斗士！你的攻击附带10%吸血。");
            }
            else if (!isLup && player.job == Jobs.Engineer2)
            {
                FreshPhoto();
                isLup = true;
                infomation.AppendText("[职业信息]你已转职成为——机械师！你每次丢弃物品或失去buff可以恢复20点血量。");
            }
            else if (!isLup && player.job == Jobs.Believer2)
            {
                FreshPhoto();
                isLup = true;
                infomation.AppendText("[职业信息]你已转职成为——圣徒！你每回合随机增长一点属性。");
            }
            if (player.job == Jobs.Believer)
            {
                if (player.hp < player.maxhp)
                {
                    player.hp++;
                    infomation.AppendText("[信徒]你回复了1点血量。\r\n");
                }
                else
                {
                    infomation.AppendText("[信徒]你的血量已满，无法恢复。\r\n");
                }
            }
            if (player.job == Jobs.Believer2)
            {
                infomation.AppendText("[圣徒]你增长了1点");
                switch (rr.Next(5))
                {
                    case 0:
                        player.maxhp++;
                        infomation.AppendText("血量上限");
                        break;
                    case 1:
                        player.atk++;
                        infomation.AppendText("攻击");
                        break;
                    case 2:
                        player.agi++;
                        infomation.AppendText("敏捷");
                        break;
                    case 3:
                        player.def++;
                        infomation.AppendText("防御");
                        break;
                    case 4:
                        player.luck++;
                        infomation.AppendText("幸运");
                        break;
                }
                if (player.hp < player.maxhp)
                {
                    player.hp++;
                    infomation.AppendText("，你回复了1点血量。\r\n");
                }
                else
                {
                    infomation.AppendText("，你的血量已满，无法恢复。\r\n");
                }
            }
            FreshBag();
            FreshStatus();
            FreshStep(caveindex);
            if (steps == 500 && player.hp > 0)
            {
                infomation.AppendText("你打败了魔王！你已成功通关！");
                allwin = true;
                SaveScore(allwin);
            }
            infos.Add(infomation.Text);
            FreshPage(pageindex);
            if (allwin)
            {
                if (MessageBox.Show("是否直接退出？", "您已通关！", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    isDead = true;
                    Close();
                }
                else
                {
                    isDead = true;
                    buttonNext.Text = "重新挑战";
                }
            }
        }

        public void AutoUseItems()
        {
            //自动使用物品
            FreshBag();
            for(int i = 0; i < links.Count; i++)
            {
                if (links[i].Text == "使用"&&links[i].Visible)
                {
                    UseItem(links[i], new EventArgs());
                }
            }
        }

        public async Task AutoNextStep()
        {
            //自动前进
            while (autostart)
            {
                if (allwin)
                {
                    return;
                }
                await NextStep();
                if (player.hp <= 0)
                {
                    return;
                }
                AutoUseItems();
                await Task.Delay(1000);
            }
        }

        private async void buttonNext_Click(object sender, EventArgs e)
        {
            //下一回合
            if (isDead)
            {
                parent.Visible = true;
                Close();
                return;
            }
            if (checkBoxauto.Checked)
            {
                if (buttonNext.Text == "前进")
                {
                    buttonNext.Text = "停止";
                    autostart = true;
                    await AutoNextStep();
                }
                else if(buttonNext.Text == "停止")
                {
                    buttonNext.Text = "前进";
                    autostart = false;
                }
            }
            else
            {
                await NextStep();
            }
        }

        private void UseItem(object sender, EventArgs e)
        {
            //使用物品
            player.stats.useitems++;
            int index = links.IndexOf((LinkLabel)sender);
            infomation.AppendText(player.UseItem(index));
            FreshBag();
            FreshStatus();
        }

        private void ThrowItem(object sender, EventArgs e)
        {
            //丢弃物品
            player.stats.dropitems++;
            int index = links.IndexOf((LinkLabel)sender);
            infomation.AppendText(player.bag[index].RemoveItem(player));
            player.bag.RemoveAt(index);
            FreshBag();
            FreshStatus();
        }

        private void infomation_TextChanged(object sender, EventArgs e)
        {
            //自动滚动
            infomation.ScrollToCaret();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            //窗口载入
            LoadLinkLabel();
            LoadItems();
            LoadCaves();
            LoadSave();
            FreshPhoto();
            FreshStatus();
            FreshBag();
            int[] caveindex = GetCaveIndex(0);
            FreshStep(caveindex);
            FreshPage(0);
        }

        private void buttondata_Click(object sender, EventArgs e)
        {
            //查看数据
            MessageBox.Show(player.stats.StatsString());
        }

        private void buttonllastpage_Click(object sender, EventArgs e)
        {
            //上一页
            pageindex--;
            FreshPage(pageindex);
        }

        private void buttonextpage_Click(object sender, EventArgs e)
        {
            //下一页
            pageindex++;
            FreshPage(pageindex);
        }
    }
}
