using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPG.Class.Database.Player;
using TextRPG.Class.Database.Monster;

namespace TextRPG.Class.Manager
{
    internal class BattleManager
    {
        private List<Monster> monsters = new List<Monster>();
        private bool isRun = false;
        private Random random = new Random();

        private Player player;
        /*
        public void Battle(Player player)
        {
            this.player = player;
            monsters = DungeonMonsters();

            Console.Clear();
            Console.WriteLine("전투 시작\n");

            while(player.Hp > 0 && monsters.Any(m => !m.IsDead))
            {
                ShowStatus();
                PlayerTurn();
                if(monsters.All(m => m.IsDead))
                {
                    break;
                }

                EnemyTurn();

            } 

            EndBattle();
        }*/

        private List<Monster> DungeonMonsters()
        {
            int count = random.Next(1, 5);
            List<Monster> List = new List<Monster>();
            for (int i = 0; i < count; i++)
            {
                int type = random.Next(1, 4);
                switch (type)
                {
                    case 1:
                        List.Add(GameManager.Instance.CreateManager.MonsterDatabase.MonsterDictionary["minion"]);
                        break;
                        
                    case 2:
                        List.Add(GameManager.Instance.CreateManager.MonsterDatabase.MonsterDictionary["siegeMinion"]);
                        break;
                    case 3:
                        List.Add(GameManager.Instance.CreateManager.MonsterDatabase.MonsterDictionary["siegeMinion2"]);
                        break;
                }
            }

            return List;
        }
        /*
        private void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("전투 상태\n");

            for (int i = 0; i < monsters.Count; i++)
            {
                var m = monsters[i];
                if (m.IsDead)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{i + 1}. Lv.{m.Level} {m.Name} {(m.IsDead ? "Dead" : $"HP {m.Hp}")}");
                Console.ResetColor();
            }

            Console.WriteLine($"\n[내정보]");
            //Console.WriteLine($"Lv.{player.Lv} {player.Name} ({player.Job})");
            //Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
        }*/
        /*

        private void Attack()
        {
            Console.WriteLine("\n대상을 선택하세요.");
            for (int i = 0; i < monsters.Count; i++)
            {
                if (!monsters[i].IsDead)
                    Console.WriteLine($"{i + 1}. {monsters[i].Name} (HP: {monsters[i].Hp})");
            }
            Console.Write(">> ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > monsters.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            Monster target = monsters[index - 1];
            if (target.IsDead)
            {
                Console.WriteLine("이미 죽은 몬스터입니다.");
                return;
            }

            int damage = player.Str;

            target.TakeDamage(damage);

            Console.WriteLine($"\n{player.Name}의 공격!");
            Console.WriteLine($"{target.Name}에게 {damage}의 데미지를 입혔습니다!");
           
            if (target.IsDead)
                Console.WriteLine($"{target.Name} 을(를) 처치했습니다!");

           
        }
        */
        private void PlayerTurn()
        {
            Console.WriteLine("\n1. 공격");
            Console.WriteLine("0. 대기");

            Console.Write("\n행동을 선택하세요 >> ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    //Attack();
                    break;
                case "0":
                    Console.WriteLine("턴을 넘깁니다.");
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    PlayerTurn();
                    break;
            }
        }
        /*
        private void EnemyTurn()
        {
            foreach (var monster in monsters)
            {
                if (monster.IsDead) continue;

                
                double offset = Math.Ceiling(monster.Attack * 0.1);
                int damage = random.Next((int)(monster.Attack - offset), (int)(monster.Attack + offset) + 1);

                //player.TakeDamage(damage);
                //Console.WriteLine($"\n{monster.Name}의 공격 {player.Name}에게 {damage}의 데미지");
            }
        }
        */
        private void EndBattle()
        {
            Console.Clear();
            Console.WriteLine("\n전투결과\n");

            /*if(player.Hp <= 0)
            {
                Console.WriteLine("패배했습니다.");
            }
            else
            {
                Console.WriteLine("승리");
            }
            */
            Console.WriteLine("\n0. 다음");
            Console.ReadLine();
        }

    }

    
    
}
