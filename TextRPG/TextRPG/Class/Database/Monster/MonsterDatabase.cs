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

        Monster 미니언;
        Monster 대포미니언;
        Monster 공허충;

        public MonsterDatabase()
        {
            미니언 = new Monster(2, "미니언", "enemy", 3, 0, 1, 0, 15, 30, 0, 1);
            대포미니언 = new Monster(5, "대포 미니언", "enemy", 3, 0, 5, 0, 50, 25, 0, 5);
            공허충 = new Monster(3, "공허충", "enemy", 3, 0, 3, 0, 50, 10, 0, 1);

        }

        public void CreateMonster()
        {
            monsterDictionary.Add("minion", 미니언);
            monsterDictionary.Add("siegeMinion", 대포미니언);
            monsterDictionary.Add("siegeMinion2", 공허충);
            Console.WriteLine("몬스터 데이터베이스가 생성되었습니다.");
            Console.WriteLine("몬스터 목록:");
            foreach (var monster in monsterDictionary.Values)
            {
                Console.WriteLine($"- {monster.Name} (Lv.{monster.Lv})");
            }
        }
    }
}
