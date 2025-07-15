using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Data;

namespace TextRPG.Class.Database.Monster
{
    internal class MonsterDatabase : DefaultCharacter
    {
        List<MonsterDatabase> monsterDatabaseList = new List<MonsterDatabase>();

        Dictionary<string, MonsterDatabase> monsterDictionary = new Dictionary<string, MonsterDatabase>();

        public MonsterDatabase(int lv, string name, string job, int str, int plusStr, int armorPoint, int plusArmorPoint, int maxHp, int plusHp, int gold)
        {
            this._lv = lv;
            this._name = name;
            this._job = job;

            this._str = str;
            this.plusStr = plusStr;

            this._armorPoint = armorPoint;
            this.plusArmorPoint = plusArmorPoint;

            this._maxHp = maxHp;
            this._plusHp = plusHp;
            this._hp = _maxHp;

            this._gold = gold;

            monsterDatabaseList.Add(this);
        }

        public void createMonster()
        {
            monsterDictionary.Add("minion", new MonsterDatabase(2, "미니언", "enemy", 3, 0, 1, 0, 15, 0, 1));
            monsterDictionary.Add("SiegeMinion", new MonsterDatabase(5, "대포 미니언", "enemy", 3, 0, 5, 0, 25, 0, 5));
            monsterDictionary.Add("Voidling", new MonsterDatabase(3, "공허충", "enemy", 3, 0, 3, 0, 10, 0, 1));
        }
    }
}
