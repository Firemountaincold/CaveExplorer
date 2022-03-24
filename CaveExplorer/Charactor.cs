using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveExplorer
{
    /// <summary>
    /// 角色职业：斗士、工程师、信徒
    /// </summary>
    public enum Jobs
    {
        Fighter=0,
        Engineer,
        Believer
    }
    /// <summary>
    /// 角色心情：开心、生气、伤心、天神下凡
    /// </summary>
    public enum Mood
    {
        Happy=0,
        Angry,
        Sad,
        God,
        None
    }
    /// <summary>
    /// 角色类
    /// </summary>
    public class Charactor
    {
        //职业
        public string name = "你";
        public Jobs job { get; }
        //背包
        public int maxbag;
        public List<Items> bag=new List<Items>();
        public List<Events> events = new List<Events>();
        //血量
        public int maxhp;
        public int hp;
        //属性
        public int atk;
        public int agi;
        public int def;
        public int luck;
        //心情
        public Mood mood;
        //数据
        public DataStats stats;
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="j">职业</param>
        /// <param name="features">属性</param>
        public Charactor(Jobs j,int[] features)
        {
            job = j;
            maxhp = features[0];
            hp = maxhp;
            maxbag = 10;
            if (j == Jobs.Engineer)
            {
                maxbag = 12;
            }
            atk = features[1];
            agi = features[2];
            def = features[3];
            luck = features[4];
            mood = Mood.Happy;
            stats = new DataStats();
        }

        public Charactor(string chaline, string dataline)
        {
            string[] temp = chaline.Split(',');
            name = temp[0];
            hp = Convert.ToInt32(temp[1]);
            maxhp = Convert.ToInt32(temp[2]);
            atk = Convert.ToInt32(temp[3]);
            agi = Convert.ToInt32(temp[4]);
            def = Convert.ToInt32(temp[5]);
            luck = Convert.ToInt32(temp[6]);
            if(temp[7] == "Fighter")
            {
                job = Jobs.Fighter;
            }
            else if(temp[7] == "Engineer")
            {
                job = Jobs.Engineer;
            }
            else if(temp[7] == "Believer")
            {
                job = Jobs.Believer;
            }
            if (temp[8] == "Happy")
            {
                mood = Mood.Happy;
            }
            else if (temp[8] == "Angry")
            {
                mood = Mood.Angry;
            }
            else if (temp[8] == "Sad")
            {
                mood = Mood.Sad;
            }
            else if (temp[8] == "God")
            {
                mood = Mood.God;
            }
            maxbag = Convert.ToInt32(temp[9]);
            stats = new DataStats(dataline);
        }
        /// <summary>
        /// 下一回合
        /// </summary>
        public string Next()
        {
            string info = "";
            for(int i = 0; i < bag.Count; i++)
            {
                if(bag[i].type==ItemType.Hold)
                {
                    if(bag[i].result.timelast==1)
                    {
                        info += bag[i].RemoveItem(this);
                        bag.RemoveAt(i);
                    }
                    else
                    {
                        bag[i].result.timelast--;
                    }
                }
            }
            for (int i = 0; i < events.Count; i++)
            {
                if (events[i].result.timelast == 1)
                {
                    info += events[i].RemoveResults(this);
                    events.RemoveAt(i);
                }
                else
                {
                    events[i].result.timelast--;
                }
            }
            return info;
        }
        /// <summary>
        /// 获取背包字符串
        /// </summary>
        /// <returns>背包字符串</returns>
        public void BagString(List<System.Windows.Forms.Label> labels, System.Windows.Forms.Label buff)
        {
            for (int i = 0; i < bag.Count; i++)
            {
                labels[i].Text = bag[i].name;
                if (bag[i].type == ItemType.Use)
                {
                }
                else if (bag[i].type == ItemType.Auto)
                {
                    labels[i].Text += "(装备)";
                }
                else if (bag[i].type == ItemType.Hold)
                {
                    labels[i].Text += "(" + bag[i].result.timelast.ToString() + ")";
                }
            }
            for(int i = bag.Count; i < maxbag; i++)
            {
                labels[i].Text = "(空)";
            }
            buff.Text = "";
            for (int i = 0; i < events.Count; i++)
            {
                buff.Text += events[i].name;
                buff.Text += "(" + events[i].result.timelast.ToString() + ")";
                buff.Text += "\r\n";
            }
        }
        /// <summary>
        /// 获取物品
        /// </summary>
        /// <param name="item">物品</param>
        /// <param name="situation">场景，0为掉落，1为发现</param>
        /// <returns>物品信息字符串</returns>
        public string GetItem(Items item, int situation)
        {
            stats.items++;
            string info = "";
            Items i = new Items(item);
            for(int j = 0; j < bag.Count; j++)
            {
                if (item.name == bag[j].name)
                {
                    if (item.type == ItemType.Auto)
                    {
                        if (situation == 0)
                        {
                            info += "[战斗信息]从怪物的尸体上";
                        }
                        else if (situation == 1)
                        {
                            info += "[探索信息]在洞穴中探索时";
                        }
                        info += "获得了物品：" + i.name + "，因为不能重复装备同种物品，所以自动丢弃了。\r\n";
                        return info;
                    }
                    else if (item.type == ItemType.Hold)
                    {
                        if (situation == 0)
                        {
                            info += "[战斗信息]从怪物的尸体上";
                        }
                        else if (situation == 1)
                        {
                            info += "[探索信息]在洞穴中探索时";
                        }
                        info += "获得了物品：" + i.name + "，延长了当前状态的时间。\r\n";
                        bag[j].result.timelast += item.result.timelast;
                        return info;
                    }
                }
            }
            if (bag.Count < maxbag)
            {
                bag.Add(i);
                if (i.type != ItemType.Use)
                {
                    if (situation == 0)
                    {
                        info += "[战斗信息]从怪物的尸体上发现了物品！";
                    }
                    else if (situation == 1)
                    {
                        info += "[探索信息]在洞穴中探索时发现了物品！";
                    }
                    info += i.GetItem(this);
                }
                else
                {
                    if (situation == 0)
                    {
                        info += "[战斗信息]从怪物的尸体上";
                    }
                    else if (situation == 1)
                    {
                        info += "[探索信息]在洞穴中探索时";
                    }
                    info += "获得了物品：" + i.name + "。\r\n";
                }
            }
            else
            {
                if (situation == 0)
                {
                    info += "[战斗信息]从怪物的尸体上";
                }
                else if (situation == 1)
                {
                    info += "[探索信息]在洞穴中探索时";
                }
                info += "发现了物品" + i.name + "，但背包已满，所以扔掉了。\r\n";
            }
            return info;
        }
        /// <summary>
        /// 使用物品
        /// </summary>
        /// <param name="itemname">物品名</param>
        /// <returns>物品信息</returns>
        public string UseItem(int index)
        {
            string info = "[物品信息]";
            info += "使用了物品：" + bag[index].GetItem(this);
            bag.RemoveAt(index);
            return info;
        }
        /// <summary>
        /// 存档用字符串
        /// </summary>
        /// <returns>存档字符串</returns>
        public string SaveString()
        {
            string save = name + "," + hp.ToString() + "," + maxhp.ToString() + "," + atk.ToString() + "," +
                agi.ToString() + "," + def.ToString() + "," + luck.ToString() + "," + job.ToString() + "," +
                mood.ToString() + "," + maxbag.ToString() + "\r\n";
            save += stats.SaveString();
            foreach(var i in bag)
            {
                save += i.SaveString();
            }
            save += "1995\r\n";
            foreach(var i in events)
            {
                save += i.SaveString();
            }
            return save;
        }
    }
    /// <summary>
    /// 物品种类：可使用、自动使用、携带生效
    /// </summary>
    public enum ItemType
    {
        Use=0,
        Auto,
        Hold
    }
    /// <summary>
    /// 角色装备、道具
    /// </summary>
    public class Items
    {
        public int ID { get; }
        public string name { get; }
        public ItemType type { get; }
        public ItemResult result { get; }
        /// <summary>
        /// 复制用构造函数
        /// </summary>
        /// <param name="i"></param>
        public Items(Items i)
        {
            ID = i.ID;
            name = i.name;
            type = i.type;
            result = new ItemResult(i.result);
        }
        /// <summary>
        /// 从数据读取创建
        /// </summary>
        /// <param name="itemline"></param>
        public Items(string itemline)
        {
            string[] g = itemline.Split(',');
            ID = Convert.ToInt32(g[0]);
            name = g[1];
            if (g[2] == "Use")
            {
                type = ItemType.Use;
            }
            else if (g[2] == "Auto")
            {
                type = ItemType.Auto;
            }
            else if (g[2] == "Hold") 
            {
                type= ItemType.Hold;
            }
            result = new ItemResult();
            result.hpchange = Convert.ToInt32(g[3]);
            result.atkchange = Convert.ToInt32(g[4]);
            result.agichange = Convert.ToInt32(g[5]);
            result.defchange = Convert.ToInt32(g[6]);
            result.luckchange = Convert.ToInt32(g[7]);
            result.maxhpchange = Convert.ToInt32(g[8]);
            result.maxbagchange = Convert.ToInt32(g[9]);
            result.timelast = Convert.ToInt32(g[10]);
        }
        /// <summary>
        /// 物品生效函数
        /// </summary>
        /// <param name="player">角色</param>
        /// <returns>物品使用结果信息</returns>
        public string GetItem(Charactor player)
        {
            string info = player.name + "的";
            if(result.maxhpchange>0)
            {
                player.maxhp += result.maxhpchange;
                info += "生命上限增加了" + result.maxhpchange.ToString() + "点，";
            }
            if(result.hpchange>0)
            {
                info += "生命";
                if (player.maxhp - player.hp > result.hpchange)
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
            if(result.atkchange>0)
            {
                player.atk += result.atkchange;
                info += "攻击增加了" + result.atkchange.ToString() + "点，";
            }
            if(result.agichange>0)
            {
                player.agi += result.agichange;
                info += "敏捷增加了" + result.agichange.ToString() + "点，";
            }
            if(result.defchange>0)
            {
                player.def += result.defchange;
                info += "防御增加了" + result.defchange.ToString() + "点，";
            }
            if(result.luckchange>0)
            {
                player.luck += result.luckchange;
                info += "幸运增加了" + result.luckchange.ToString() + "点，";
            }   
            if (result.maxbagchange > 0)
            {
                player.maxbag += result.maxbagchange;
                info += "背包上限增加了" + result.maxbagchange.ToString() + "格，";
            }
            if (result.timelast > 0)
            {
                info += "持续时间为" + result.timelast + "回合，";
            }
            info += "效果来自物品：" + name + "。\r\n";
            return info;
        }
        /// <summary>
        /// 物品效果移除
        /// </summary>
        /// <param name="player">角色</param>
        /// <returns>物品信息</returns>
        public string RemoveItem(Charactor player)
        {
            player.hp -= result.maxbagchange;
            player.atk -= result.atkchange;
            player.agi -= result.agichange;
            player.def -= result.defchange;
            player.luck -= result.luckchange;
            player.maxhp -= result.maxhpchange;
            if (player.hp > player.maxhp)
            {
                player.hp = player.maxhp;
            }
            player.maxbag -= result.maxbagchange;
            return "[物品信息]" + name + "的效果消失了。\r\n";
        }
        /// <summary>
        /// 说明用字符串
        /// </summary>
        /// <returns>字符串</returns>
        public string TooltipString()
        {
            string info = name + "\r\n";
            if (result.maxhpchange > 0)
            {
                info += "生命上限+" + result.maxhpchange.ToString() + "\r\n";
            }
            if (result.hpchange > 0)
            {
                info += "生命值+" + result.hpchange.ToString() + "\r\n";
            }
            if (result.atkchange > 0)
            {
                info += "攻击+" + result.atkchange.ToString() + "\r\n";
            }
            if (result.agichange > 0)
            {
                info += "敏捷+" + result.agichange.ToString() + "\r\n";
            }
            if (result.defchange > 0)
            {
                info += "防御+" + result.defchange.ToString() + "\r\n";
            }
            if (result.luckchange > 0)
            {
                info += "幸运+" + result.luckchange.ToString() + "\r\n";
            }
            if (result.maxbagchange > 0)
            {
                info += "背包上限+" + result.maxbagchange.ToString() + "格\r\n";
            }
            if (result.timelast > 0)
            {
                info += "持续" + result.timelast + "回合\r\n";
            }
            info += "物品ID：" + ID.ToString();
            return info;
        }
        /// <summary>
        /// 存档用字符串
        /// </summary>
        /// <returns>存档字符串</returns>
        public string SaveString()
        {
            string save = ID + "," + name + "," + type + "," + result.hpchange + "," + result.atkchange +
                "," + result.agichange + "," + result.defchange + "," + result.luckchange + "," +
                result.maxhpchange + "," + result.maxbagchange + "," + result.timelast + "\r\n";
            return save;
        }
    }
    /// <summary>
    /// 物品效果
    /// </summary>
    public class ItemResult
    {
        public int hpchange;
        public int atkchange;
        public int agichange;
        public int defchange;
        public int luckchange;
        public int maxhpchange;
        public int maxbagchange;
        public int timelast;

        public ItemResult() { }

        public ItemResult(ItemResult ir)
        {
            hpchange = ir.hpchange;
            atkchange = ir.atkchange;
            agichange = ir.agichange;
            defchange = ir.defchange;
            luckchange = ir.luckchange;
            maxhpchange = ir.maxhpchange;
            maxbagchange = ir.maxbagchange;
            timelast = ir.timelast;
        }
    }
    /// <summary>
    /// 冒险数据统计
    /// </summary>
    public class DataStats
    {
        public int items = 0;
        public int useitems = 0;
        public int dropitems = 0;
        public int events = 0;
        public List<string> enemy = new List<string>();
        public List<int> enemykill = new List<int>();

        public DataStats() { }
        /// <summary>
        /// 字符串初始化
        /// </summary>
        /// <param name="dataline">存档字符串</param>
        public DataStats(string dataline)
        {
            string[] temp = dataline.Split(',');
            items = Convert.ToInt32(temp[0]);
            useitems = Convert.ToInt32(temp[1]);
            dropitems = Convert.ToInt32(temp[2]);
            events = Convert.ToInt32(temp[3]);
            for(int i = 4; i < temp.Length - 1 && temp[i + 1].Length > 0; i = i + 2)
            {
                enemy.Add(temp[i]);
                enemykill.Add(Convert.ToInt32(temp[i + 1]));
            }
        }
        /// <summary>
        /// 添加敌人数据
        /// </summary>
        /// <param name="fightcave">洞穴数据</param>
        public void AddEnemy(Caves fightcave)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                if (fightcave.fights.demonname == enemy[i])
                {
                    enemykill[i]++;
                    return;
                }
            }
            enemy.Add(fightcave.fights.demonname);
            enemykill.Add(1);
        }
        /// <summary>
        /// 存档字符串
        /// </summary>
        /// <returns>字符串</returns>
        public string SaveString()
        {
            string info = items.ToString() + "," + useitems.ToString() + "," + dropitems.ToString()
                + "," + events.ToString();
            for (int i = 0; i < enemy.Count; i++)
            {
                info += "," + enemy[i] + "," + enemykill[i];
            }
            info += "\r\n";
            return info;
        }
        /// <summary>
        /// 数据字符串
        /// </summary>
        /// <returns>字符串</returns>
        public string StatsString()
        {
            string info = "获得物品：" + items.ToString() + "\r\n使用物品：" + useitems.ToString() +
                "\r\n丢弃物品：" + dropitems.ToString() + "\r\n遭遇事件：" + events.ToString() +
                "\r\n遭遇敌人：";
            if (enemy.Count > 0)
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    info += "\r\n    " + enemy[i] + "：" + enemykill[i];
                }
            }
            else
            {
                info += "暂无";
            }
            return info;
        }
    }
}
