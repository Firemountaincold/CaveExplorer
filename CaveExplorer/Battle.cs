using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaveExplorer
{
    public class Battle
    {
        Charactor player;
        Caves enemy;
        Panel panel;
        Label Php;
        Label Ehp;
        Label battle;
        PictureBox Pplayer;
        PictureBox Penemy;
        PictureBox Pattack;
        int maxhp;

        public Battle(Charactor player, Caves enemy, Panel panel, Label Php, Label Ehp, Label battle, PictureBox Pplayer, PictureBox Penemy, PictureBox Pattack)
        {
            this.player = player;
            this.enemy = enemy;
            this.panel = panel;
            this.Php = Php;
            this.Ehp = Ehp;
            this.battle = battle;
            this.Pplayer = Pplayer;
            this.Penemy = Penemy;
            this.Pattack = Pattack;
            maxhp = enemy.fights.hp;
            this.Ehp.ForeColor = Color.White;
            this.Php.ForeColor = Color.White;
        }

        public void LoadImage()
        {
            //加载贴图
            Pplayer.Image = Properties.Resources.player;
            Penemy.Image = Properties.Resources.enemy;
        }

        public async Task PlayerAttack(string battlestr, int Ehp)
        {
            //我方攻击动画
            Point l = Pplayer.Location;
            Pplayer.Location = new Point(l.X + 5, l.Y + 5);
            await Task.Delay(100);
            Pplayer.Location = new Point(l.X - 5, l.Y - 5);
            await Task.Delay(100);
            Pplayer.Location = l;
            battle.Text = battlestr;
            Pattack.Image = Properties.Resources.attack1_1;
            await Task.Delay(100);
            Pattack.Image = Properties.Resources.attack1_2;
            await Task.Delay(100);
            Pattack.Image = null;
            this.Ehp.Text = Ehp + "/" + maxhp;
            if (Ehp < maxhp / 3)
            {
                this.Ehp.ForeColor = Color.Red;
            }
            l = Penemy.Location;
            Penemy.Location = new Point(l.X + 5, l.Y + 5);
            await Task.Delay(100);
            Penemy.Location = new Point(l.X - 5, l.Y - 5);
            await Task.Delay(100);
            Penemy.Location = l;
            await Task.Delay(200);
        }

        public async Task EnemyAttack(string battlestr)
        {
            //敌方攻击动画
            Point l = Penemy.Location;
            Penemy.Location = new Point(l.X + 5, l.Y + 5);
            await Task.Delay(100);
            Penemy.Location = new Point(l.X - 5, l.Y - 5);
            await Task.Delay(100);
            Penemy.Location = l;
            battle.Text = battlestr;
            Pattack.Image = Properties.Resources.attack2_1;
            await Task.Delay(100);
            Pattack.Image = Properties.Resources.attack2_2;
            await Task.Delay(100);
            Pattack.Image = null;
            this.Php.Text = player.hp + "/" + player.maxhp;
            if(player.hp < player.maxhp / 3)
            {
                Php.ForeColor = Color.Red;
            }
            l = Pplayer.Location;
            Pplayer.Location = new Point(l.X + 5, l.Y + 5);
            await Task.Delay(100);
            Pplayer.Location = new Point(l.X - 5, l.Y - 5);
            await Task.Delay(100);
            Pplayer.Location = l;
            await Task.Delay(200);
        }

        public async Task BattleStart()
        {
            //战斗开始
            LoadImage();
            Php.Text = player.hp + "/" + player.maxhp;
            Ehp.Text = enemy.fights.hp + "/" + enemy.fights.hp;
            battle.Text = "遭遇了怪物——" + enemy.fights.demonname + "！";
            panel.Visible = true;
            await Task.Delay(500);
        }

        public async Task BattleEnd(bool win)
        {
            //结束动画
            if (win)
            {
                Pattack.Image = Properties.Resources.win;
            }
            else
            {
                Pattack.Image = Properties.Resources.lost;
            }
            await Task.Delay(800);
            panel.Visible = false;
            Pattack.Image = null;
        }
    }
}
