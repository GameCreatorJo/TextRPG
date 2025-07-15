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
        private Dictionary<string, Monster> monsterDictionary;
        public Dictionary<string, Monster> MonsterDictionary
        {
            get { return monsterDictionary; }
        }
        Monster 미니언 = new Monster(2, "미니언", "enemy", 3, 0, 1, 0, 15, 0, 1);
        Monster 대포미니언 = new Monster(5, "대포 미니언", "enemy", 3, 0, 5, 0, 25, 0, 5);
        Monster 공허충 = new Monster(3, "공허충", "enemy", 3, 0, 3, 0, 10, 0, 1);


        public MonsterDatabase()
        {
            monsterDictionary = new Dictionary<string, Monster>();
        }


        public void createMonster()
        {
            monsterDictionary.Add("minion",미니언);
            monsterDictionary.Add("siegeMinion",대포미니언);
            monsterDictionary.Add("siegeMinion2", 공허충);
        }
    }
}
