using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG.Class.Data;

namespace TextRPG.Class.Database.MonsterData
{
    public class Monster : DefaultCharacter
    {

        public Monster
            (int lv, string name, string job, int str, int plusStr, int armorPoint, int plusArmorPoint, int maxHp, int gold, int criticalRate, int dodgeRate, int exp)
        {
            _lv = lv;
            _name = name;
            _job = job;
            _str = str;
            this.plusStr = plusStr;
            _armorPoint = armorPoint;
            this.plusArmorPoint = plusArmorPoint;
            _maxHp = maxHp;
            _hp = maxHp;
            _gold = gold;
            _criticalRate = criticalRate;
            _dodgeRate = dodgeRate;
            this._exp = exp;
        }
        public Monster(Monster other)
        {
            _lv = other.Lv;
            _name = other.Name;
            _job = other.Job;
            _str = other.Str;
            this.plusStr = other.plusStr;
            _armorPoint = other.ArmorPoint;
            this.plusArmorPoint = other.plusArmorPoint;
            _maxHp = other.MaxHp;
            _hp = other.Hp;
            _plusHp = other._plusHp;
            _gold = other.Gold;
            _criticalRate = other.CriticalRate;
            _dodgeRate = other.DodgeRate;
        }
    }
}
