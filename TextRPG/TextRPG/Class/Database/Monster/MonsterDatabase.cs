using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Data;

namespace TextRPG.Class.Database.Monster
{
    internal class MonsterDatabase
    {
        private List<Monster> monsterDatabaseList = new List<Monster>();
        private Dictionary<string, Monster> monsterDictionary = new Dictionary<string, Monster>();
        public Dictionary<string, Monster> MonsterDictionary
        {
            get { return monsterDictionary; }
        }

        public MonsterDatabase()
        {

        }

        public void CreateMonster()
        {
            monsterDictionary.Add("minion", new Monster(2, "미니언", "enemy", 3, 0, 1, 0, 15, 0, 1));
            monsterDictionary.Add("siegeMinion", new Monster(5, "대포 미니언", "enemy", 3, 0, 5, 0, 25, 0, 5));
            monsterDictionary.Add("siegeMinion2", new Monster(3, "공허충", "enemy", 3, 0, 3, 0, 10, 0, 1));
        }
    }
}
