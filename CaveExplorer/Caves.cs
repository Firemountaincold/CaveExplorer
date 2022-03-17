using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveExplorer
{
    public enum CaveType
    {
        Fight=0,
        Find,
        Event,
    }
    /// <summary>
    /// 洞穴类
    /// </summary>
    public class Caves
    {
        public CaveType type;
        public Fights fights;
        public Find find;
        public Events events;

        public Caves(Caves cave)
        {
            type = cave.type;
            if (cave.fights != null)
            {
                fights = new Fights(cave.fights);
            }
            find = cave.find;
            events = cave.events;
        }

        public Caves(CaveType type, string dataline)
        {
            this.type = type;
            if(type == CaveType.Fight)
            {
                fights = new Fights(dataline);
            }
            else if(type == CaveType.Find)
            {
                find = new Find(dataline);
            }
            else if(type==CaveType.Event)
            {
                events = new Events(dataline);
            }
        }
    }
    /// <summary>
    /// 战斗类
    /// </summary>
    public class Fights
    {
        string demonname;
        int hp;
        int atk;
        int agi;
        int def;
        int itemid;
        public int step;

        public Fights(Fights fights)
        {
            demonname = fights.demonname;
            hp = fights.hp;
            atk = fights.atk;
            agi = fights.agi;
            def = fights.def;
            itemid = fights.itemid;
            step = fights.step;
        }

        public Fights(string fightline)
        {
            string[] temp = fightline.Split(',');
            demonname = temp[0];
            hp = Convert.ToInt32(temp[1]);
            atk = Convert.ToInt32(temp[2]);
            agi = Convert.ToInt32(temp[3]);
            def = Convert.ToInt32(temp[4]);
            itemid = Convert.ToInt32(temp[5]);
            step = Convert.ToInt32(temp[6]);
        }

        public string FightWith(Charactor player, List<Items> items)
        {
            string info = "[战斗信息]";
            Random r = new Random();
            int atkchange = 0;
            int agichange = 0;
            int defchange = 0;
            if(player.mood==Mood.Happy)
            {
                atkchange = 2;
            }
            else if(player.mood == Mood.Angry)
            {
                agichange = - player.agi / 2;
                atkchange = 4;
            }
            else if(player.mood==Mood.Sad)
            {
                defchange = - player.def / 2;
            }
            else if(player.mood==Mood.God)
            {
                atkchange = player.atk;
                defchange = player.def;
            }
            if(player.agi + agichange>=agi)
            {
                info += player.name;
            }
            else
            {
                info += demonname;
            }
            info += "先开始攻击。\r\n";
            while (player.hp > 0 || hp > 0)
            {
                if (player.agi + agichange >= agi)
                {
                    int damage = (player.atk + atkchange) * 2 - def + r.Next(0, 5) - 2;
                    damage /= 2;
                    hp -= damage;
                    info += player.name + "对" + demonname + "造成了" + damage.ToString() + "点伤害";
                    if (hp <= 0)
                    {
                        info += "，" + demonname + "死了。\r\n";
                        if (r.Next(0, 50) < player.luck + 10)
                        {
                            info += player.GetItem(items[itemid], 0);
                        }
                        break;
                    }
                    int reverse = atk * 2 - (player.def + defchange) + r.Next(0, 5) - 2;
                    reverse /= 2;
                    player.hp -= reverse;
                    info += "，" + demonname + "的反击造成了" + reverse.ToString() + "点伤害";
                    if(player.hp <= 0)
                    {
                        info += "，" + player.name + "死了。\r\n";
                        break;
                    }
                    info += "。\r\n";
                }
                else
                {
                    int damage = atk * 2 - (player.def + defchange) + r.Next(0, 5) - 2;
                    damage /= 2;
                    player.hp -= damage;
                    info += demonname + "对" + player.name + "造成了" + damage.ToString() + "点伤害";
                    if (player.hp <= 0)
                    {
                        info += "，" + player.name + "死了。\r\n";
                        break;
                    }
                    int reverse = (player.atk + atkchange) * 2 - def + r.Next(0, 5) - 2;
                    reverse /= 2;
                    hp -= reverse;
                    info += "，" + player.name + "的反击造成了" + reverse.ToString() + "点伤害";
                    if (hp <= 0)
                    {
                        info += "，" + demonname + "死了。\r\n";
                        if (r.Next(0, 100) < player.luck + 10)
                        {
                            info += player.GetItem(items[itemid], 0);
                        }
                        break;
                    }
                    info += "。\r\n";
                }
            }
            return info;
        }
    }
    /// <summary>
    /// 寻找宝物类
    /// </summary>
    public class Find
    {
        public int itemID;
        public int step;

        public Find(string findline)
        {
            string[] temp = findline.Split(',');
            itemID = Convert.ToInt32(temp[0]);
            step = Convert.ToInt32(temp[1]);
        }

        public string FindItem(Charactor player, List<Items> itemlist)
        {
            string info = player.GetItem(itemlist[itemID], 1);
            return info;
        }

    }
    /// <summary>
    /// 事件类
    /// </summary>
    public class Events
    {
        string description;
        public int step;

        public Events(string eventline)
        {
            string[] temp = eventline.Split(',');
            description = temp[0];
            step = Convert.ToInt32(temp[1]);
        }

        public string EventRun()
        {
            return "[事件信息]" + description + "\r\n";
        }
    }

    public class CaveResults
    {
        public int hpchange;
        public int atkchange;
        public int agichange;
        public int defchange;
        public int luckchange;
        public int maxhpchange;
        public int maxbagchange;
        public int timelast;
        public Mood moodchange;
    }
}
