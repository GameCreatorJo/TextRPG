using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Data;

namespace TextRPG.Class.Database.Monster
{
    internal class Monster : DefaultCharacter
    {
        public Monster(int lv, string name, string job, int str, int plusStr, int armorPoint, int plusArmorPoint, int maxHp, int hp, int plusHp, int gold)
        {
            _lv = lv;
            _name = name;
            _job = job;
            _str = str;
            this.plusStr = plusStr;
            _armorPoint = armorPoint;
            this.plusArmorPoint = plusArmorPoint;
            _maxHp = maxHp;
            _hp = hp;
            _plusHp = plusHp;
            _gold = gold;
        }
    }
}
