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
        public string demonname;
        public int hp;
        int atk;
        int agi;
        int def;
        int itemid;
        public int step;
        public int itemratio;

        public Fights(Fights fights)
        {
            demonname = fights.demonname;
            hp = fights.hp;
            atk = fights.atk;
            agi = fights.agi;
            def = fights.def;
            itemid = fights.itemid;
            step = fights.step;
            itemratio = fights.itemratio;
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
            itemratio = Convert.ToInt32(temp[7]);
        }

        public async Task<string> FightWith(Charactor player, List<Items> items, Battle battle, ItemEventPanel iep)
        {
            await battle.BattleStart();
            bool win = true;
            string info = "[战斗信息]";
            Random r = new Random();
            int atkchange = 0;
            int agichange = 0;
            int defchange = 0;
            if (player.mood == Mood.Happy)
            {
                atkchange = 2;
            }
            else if (player.mood == Mood.Angry)
            {
                agichange = -player.agi / 2;
                atkchange = 4;
            }
            else if (player.mood == Mood.Sad)
            {
                defchange = -player.def / 2;
            }
            else if (player.mood == Mood.God)
            {
                atkchange = player.atk;
                defchange = player.def;
            }
            else if (player.mood == Mood.Afraid) 
            {
                atkchange = -player.atk / 5;
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
            int critratio = 84;
            if (player.job == Jobs.Fighter || player.job == Jobs.Fighter2) 
            {
                critratio = 64;
            }
            while (player.hp > 0 || hp > 0)
            {
                if (player.agi + agichange >= agi)
                {
                    int damage = (player.atk + atkchange) * 2 - def + r.Next(0, 9) - 4;
                    if (damage < 1) 
                    {
                        damage = 1;
                    }
                    if (r.Next(0, 100) > critratio)
                    {
                        damage += def;
                        hp -= damage;
                        info += player.name + "对" + demonname + "造成了暴击！" + damage.ToString() + "点伤害";
                        string f2 = "";
                        if (player.job == Jobs.Fighter2)
                        {
                            int delta = player.maxhp - player.hp;
                            int rec = damage / 10 + 1;
                            if (rec >= delta)
                            {
                                rec = delta;
                            }
                            player.hp += rec;
                            f2 = "，吸血恢复了" + rec + "点血量";
                            info += f2;
                        }
                        await battle.PlayerAttack(player.name + "对" + demonname + "造成了暴击！" + damage.ToString() + "点伤害" + f2 + "！", hp);
                    }
                    else
                    {
                        damage = (damage + 1) / 2;
                        hp -= damage;
                        info += player.name + "对" + demonname + "造成了" + damage.ToString() + "点伤害";
                        string f2 = "";
                        if (player.job == Jobs.Fighter2)
                        {
                            int delta = player.maxhp - player.hp;
                            int rec = damage / 10 + 1;
                            if (rec >= delta)
                            {
                                rec = delta;
                            }
                            player.hp += rec;
                            f2 = "，吸血恢复了" + rec + "点血量";
                            info += f2;
                        }
                        await battle.PlayerAttack(player.name + "对" + demonname + "造成了" + damage.ToString() + "点伤害" + f2 + "。", hp);
                    }
                    if (hp <= 0)
                    {
                        await battle.BattleEnd(win);
                        info += "，" + demonname + "死了。\r\n";
                        if (itemratio + player.luck / 2 > r.Next(100))
                        {
                            info += await player.GetItem(items[itemid], iep, 0);
                        }
                        break;
                    }
                    int reverse = atk * 2 - (player.def + defchange) + r.Next(0, 9) - 4;
                    if (reverse < 1)
                    {
                        reverse = 1;
                    }
                    if (r.Next(0, 100) > 90)
                    {
                        reverse = atk + r.Next(0, 9) - 4;
                        player.hp -= reverse;
                        info += "，" + demonname + "的反击造成了暴击！" + reverse.ToString() + "点伤害";
                        await battle.EnemyAttack(demonname + "的反击造成了暴击！" + reverse.ToString() + "点伤害！");
                    }
                    else
                    {
                        reverse = (reverse + 1) / 2;
                        player.hp -= reverse;
                        await battle.EnemyAttack(demonname + "的反击造成了" + reverse.ToString() + "点伤害。");
                        info += "，" + demonname + "的反击造成了" + reverse.ToString() + "点伤害";
                    }
                    if(player.hp <= 0)
                    {
                        await battle.BattleEnd(win);
                        info += "，" + player.name + "死了。\r\n";
                        win = false;
                        break;
                    }
                    info += "。\r\n";
                }
                else
                {
                    int damage = atk * 2 - (player.def + defchange) + r.Next(0, 9) - 4;
                    if (damage < 1)
                    {
                        damage = 1;
                    }
                    if (r.Next(0, 100) > 90)
                    {
                        damage = atk + r.Next(0, 9) - 4;
                        player.hp -= damage;
                        info += demonname + "对" + player.name + "造成了暴击！" + damage.ToString() + "点伤害";
                        await battle.EnemyAttack(demonname + "对" + player.name + "造成了暴击！" + damage.ToString() + "点伤害！");
                    }
                    else
                    {
                        damage = (damage + 1) / 2;
                        player.hp -= damage;
                        info += demonname + "对" + player.name + "造成了" + damage.ToString() + "点伤害";
                        await battle.EnemyAttack(demonname + "对" + player.name + "造成了" + damage.ToString() + "点伤害。");
                    }
                    if (player.hp <= 0)
                    {
                        await battle.BattleEnd(win);
                        info += "，" + player.name + "死了。\r\n";
                        win = false;
                        break;
                    }
                    int reverse = (player.atk + atkchange) * 2 - def + r.Next(0, 9) - 4;
                    if (reverse < 1) 
                    {
                        reverse = 1;
                    }
                    
                    if (r.Next(0, 100) > critratio)
                    {
                        reverse += def;
                        hp -= reverse;
                        info += "，" + player.name + "的反击造成了暴击！" + reverse.ToString() + "点伤害";
                        string f2 = "";
                        if (player.job == Jobs.Fighter2)
                        {
                            int delta = player.maxhp - player.hp;
                            int rec = reverse / 10 + 1;
                            if (rec >= delta)
                            {
                                rec = delta;
                            }
                            player.hp += rec;
                            f2 = "，吸血恢复了" + rec + "点血量";
                            info += f2;
                        }
                        await battle.PlayerAttack(player.name + "的反击造成了暴击！" + reverse.ToString() + "点伤害" + f2 + "！", hp);
                    }
                    else
                    {
                        reverse = (reverse + 1) / 2;
                        hp -= reverse;
                        info += "，" + player.name + "的反击造成了" + reverse.ToString() + "点伤害";
                        string f2 = "";
                        if (player.job == Jobs.Fighter2)
                        {
                            int delta = player.maxhp - player.hp;
                            int rec = reverse / 10 + 1;
                            if (rec >= delta)
                            {
                                rec = delta;
                            }
                            player.hp += rec;
                            f2 = "，吸血恢复了" + rec + "点血量";
                            info += f2;
                        }
                        await battle.PlayerAttack(player.name + "的反击造成了" + reverse.ToString() + "点伤害" + f2 + "。", hp);
                    }
                    if (hp <= 0)
                    {
                        await battle.BattleEnd(win);
                        info += "，" + demonname + "死了。\r\n";
                        if (itemratio + player.luck / 2 > r.Next(100)) 
                        {
                            info += await player.GetItem(items[itemid], iep, 0);
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

        public async Task<string> FindItem(Charactor player, List<Items> itemlist, ItemEventPanel iep)
        {
            string info = await player.GetItem(itemlist[itemID], iep, 1);
            return info;
        }

    }
    /// <summary>
    /// 事件类
    /// </summary>
    public class Events
    {
        public string name;
        string description;
        public int step;
        public CaveResults result;

        public Events(Events eve)
        {   
            name = eve.name;
            description = eve.description;
            step = eve.step;
            result = new CaveResults(eve.result);
        }
        public Events(string eventline)
        {
            string[] temp = eventline.Split(',');
            name = temp[0];
            description = temp[1];
            step = Convert.ToInt32(temp[2]);
            result = new CaveResults();
            result.hpchange = Convert.ToInt32(temp[3]);
            result.atkchange = Convert.ToInt32(temp[4]);
            result.agichange = Convert.ToInt32(temp[5]);
            result.defchange = Convert.ToInt32(temp[6]);
            result.luckchange = Convert.ToInt32(temp[7]);
            result.maxhpchange = Convert.ToInt32(temp[8]);
            result.maxbagchange = Convert.ToInt32(temp[9]);
            result.timelast = Convert.ToInt32(temp[10]);
            if (temp[11] == "Happy")
            {
                result.moodchange = Mood.Happy;
            }
            else if(temp[11] == "Angry")
            {
                result.moodchange = Mood.Angry;
            }
            else if (temp[11] == "Sad")
            {
                result.moodchange = Mood.Sad;
            }
            else if (temp[11] == "God")
            {
                result.moodchange = Mood.God;
            }
            else if (temp[11] == "Afraid")
            {
                result.moodchange = Mood.Afraid;
            }
            else
            {
                result.moodchange = Mood.None;
            }
        }

        public async Task<string> EventRun(Charactor player, ItemEventPanel iep)
        {
            await iep.ShowP(description);
            int i = 0;
            string info = player.name + "的";
            if (result.maxhpchange > 0)
            {
                i++;
                player.maxhp += result.maxhpchange;
                info += "生命上限增加了" + result.maxhpchange.ToString() + "点，";
            }
            else if (result.maxhpchange < 0) 
            {
                i++;
                int ttt = player.maxhp;
                player.maxhp += result.maxhpchange;
                if(player.maxhp < 0)
                {
                    player.maxhp = 1;
                }
                if (player.hp > player.maxhp)
                {
                    player.hp = player.maxhp;
                }
                info += "生命上限减少了" + (ttt - player.maxhp).ToString() + "点，";
            }
            if (result.hpchange != 0)
            {
                i++;
                info += "生命";
                if (result.hpchange < 0)
                {
                    player.hp += result.hpchange;
                    info += "减少了" + (-result.hpchange).ToString() + "点，";
                }
                else if (player.maxhp - player.hp > result.hpchange)
                {
                    player.hp += result.hpchange;
                    info += "恢复了" + result.hpchange.ToString() + "点，";
                }
                else if (player.maxhp - player.hp <= result.hpchange && player.maxhp != player.hp)
                {
                    info += "恢复了" + (player.maxhp - player.hp).ToString() + "点，";
                    player.hp = player.maxhp;
                }
                else
                {
                    info += "已满，无法恢复，";
                }
            }
            if (result.atkchange != 0)
            {
                i++;
                player.atk += result.atkchange;
                if (result.atkchange > 0)
                {
                    info += "攻击增加了" + result.atkchange.ToString() + "点，";
                }
                else
                {
                    info += "攻击减少了" + (-result.atkchange).ToString() + "点，";
                }
            }
            if (result.agichange != 0)
            {
                i++;
                player.agi += result.agichange;
                if (result.agichange > 0)
                {
                    info += "敏捷增加了" + result.agichange.ToString() + "点，";
                }
                else
                {
                    info += "敏捷减少了" + (-result.agichange).ToString() + "点，";
                }
            }
            if (result.defchange != 0)
            {
                i++;
                player.def += result.defchange;
                if (result.defchange > 0)
                {
                    info += "防御增加了" + result.defchange.ToString() + "点，";
                }
                else
                {
                    info += "防御减少了" + (-result.defchange).ToString() + "点，";
                }
            }
            if (result.luckchange != 0)
            {
                i++;
                player.luck += result.luckchange;
                if (result.luckchange > 0)
                {
                    info += "幸运增加了" + result.luckchange.ToString() + "点，";
                }
                else
                {
                    info += "幸运减少了" + (-result.luckchange).ToString() + "点，";
                }
            }
            if (result.maxbagchange > 0)
            {
                i++;
                player.maxbag += result.maxbagchange;
                info += "背包上限增加了" + result.maxbagchange.ToString() + "格，";
            }
            if (result.timelast > 0)
            {
                player.events.Add(new Events(this));
                info += "持续时间为" + result.timelast + "回合，";
            }
            if (result.moodchange != player.mood && result.moodchange != Mood.None) 
            {
                i++;
                info += player.name + "的心情变成了";
                player.mood = result.moodchange;
                switch (result.moodchange)
                {
                    case Mood.Happy:
                        info += "开心（攻击小幅提升）,";
                        break;
                    case Mood.Angry:
                        info += "愤怒（攻击提升，敏捷下降）,";
                        break;
                    case Mood.Sad:
                        info += "悲伤（防御下降）,";
                        break;
                    case Mood.God:
                        info += "天神下凡（攻击、防御大幅提升）,";
                        break;
                    case Mood.Afraid:
                        info += "恐惧（攻击下降），";
                        break;
                }
            }
            if (i != 0)
            {
                info += "效果来自事件：" + name + "。\r\n";
            }
            else
            {
                info = "";
            }
            return "[事件信息]" + description + "\r\n" + info;
        }
        /// <summary>
        /// 去除事件效果
        /// </summary>
        /// <param name="player">角色</param>
        public string RemoveResults(Charactor player)
        {
            player.atk -= result.atkchange;
            player.agi -= result.agichange;
            player.def -= result.defchange;
            player.luck -= result.luckchange;
            if (player.hp > player.maxhp)
            {
                player.hp = player.maxhp;
            }
            player.maxbag -= result.maxbagchange;
            return "[事件信息]" + name + "事件的效果消失了。\r\n";
        }
        /// <summary>
        /// 存档用字符串
        /// </summary>
        /// <returns>存档字符串</returns>
        public string SaveString()
        {
            string save = name + "," + description + "," + step.ToString() + "," + result.hpchange + "," + result.atkchange +
                "," + result.agichange + "," + result.defchange + "," + result.luckchange + "," + result.maxhpchange +
                "," + result.maxbagchange + "," + result.timelast + "," + result.moodchange.ToString() + "\r\n";
            return save;
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

        public CaveResults() { }

        public CaveResults(CaveResults ir)
        {
            hpchange = ir.hpchange;
            atkchange = ir.atkchange;
            agichange = ir.agichange;
            defchange = ir.defchange;
            luckchange = ir.luckchange;
            maxhpchange = ir.maxhpchange;
            maxbagchange = ir.maxbagchange;
            timelast = ir.timelast;
            moodchange = ir.moodchange;
        }
    }
}
