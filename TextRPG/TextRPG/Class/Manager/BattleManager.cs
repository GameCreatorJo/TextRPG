using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPG.Class.Database.Player;
using TextRPG.Class.Database.Monster;
using TextRPG.Class.Data;

namespace TextRPG.Class.Manager
{
    //BattleManager 2:33
    internal class BattleManager
    {
        private List<Monster> monsters = new List<Monster>();
        private Random random = new Random();
        private Player player;
        

        

        private void Battle(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            this.player = player;
            monsters = GenerateDungeonMonsters();

            Console.Clear();
            Console.WriteLine("전투 시작\n");

            while (player.Hp > 0 && monsters.Any(m => m.Hp > 0))
            {
                ShowBattleStatus();
                PlayerTurn();

                if (monsters.All(m => m.Hp <= 0))
                {
                    break;
                }

                EnemyTurn();

            }

            BattleResult();
        }

        private List<Monster> GenerateDungeonMonsters()
        {
            var dungeonMonsters = new List<Monster>();
            var monsterDict = GameManager.Instance.CreateManager.MonsterDatabase.MonsterDictionary;
            var monsterKeys = monsterDict.Keys.ToList();

            int count = random.Next(1, 5);

            for (int i = 0; i < count; i++)
            {
                string key = monsterKeys[random.Next(monsterKeys.Count)];
                
                Monster original = monsterDict[key];

               
                dungeonMonsters.Add(original);
            }

            return dungeonMonsters;
        }


        private void ShowBattleStatus()
        {
            Console.Clear();
            Console.WriteLine("전투 상태\n");

            for (int i = 0; i < monsters.Count; i++)
            {
                var m = monsters[i];
                if (m.Hp <= 0)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{i + 1}. Lv.{m.Lv} {m.Name} {(m.Hp <= 0 ? "Dead" : $"HP {m.Hp}")}");
                Console.ResetColor();
            }

            Console.WriteLine($"\n[내정보]");
            Console.WriteLine($"Lv.{player.Lv} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
        }


        private void Attack()
        {
            Console.WriteLine("\n대상을 선택하세요.");
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].Hp > 0)
                    Console.WriteLine($"{i + 1}. {monsters[i].Name} (HP: {monsters[i].Hp})");
            }
            Console.Write(">> ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > monsters.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                Attack();
                return;
            }

            Monster target = monsters[index - 1];
            if (target.Hp <= 0)
            {
                Console.WriteLine("이미 죽은 몬스터입니다.");
                Attack();
                return;
            }

            //오차가 소수점이면 올림처리
            double offset = Math.Ceiling(player.Str * 0.1);

            int minDamage = (int)(player.Str - offset);
            int maxDamage = (int)(player.Str + offset);

            //공격력 10% 오차처리
            int damage = random.Next(minDamage, maxDamage + 1);

            target.TakeDamage(damage);

            Console.WriteLine($"\n{player.Name}의 공격!");
            Console.WriteLine($"{target.Name}에게 {damage}의 데미지를 입혔습니다!");

            if (target.Hp <= 0)
                Console.WriteLine($"{target.Name} 을(를) 처치했습니다!");


        }

        private void PlayerTurn()
        {
            Console.WriteLine("\n1. 공격");
            Console.WriteLine("0. 대기");

            Console.Write("\n행동을 선택하세요 >> ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Attack();
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

        private void EnemyTurn()
        {
            foreach (var monster in monsters)
            {
                if (monster.Hp <= 0) continue;

                int damage = monster.Str;
                
                player.TakeDamage(damage);
                Console.WriteLine($"\n{monster.Name}의 공격 {player.Name}에게 {damage}의 데미지");
            }
        }

        private void BattleResult()
        {
            Console.Clear();
            Console.WriteLine("\n전투결과\n");

            int defeatedCount = monsters.Count(m => m.Hp <= 0);

            if (player.Hp <= 0)
            {
                Console.WriteLine("패배했습니다.");
            }
            else
            {
                Console.WriteLine("승리");
                Console.WriteLine($"던전에서 몬스터 {defeatedCount}마리를 잡았습니다.\n");
            }


            Console.WriteLine($"Lv.{player.Lv} {player.Name}");
            Console.WriteLine($"HP {player.MaxHp} -> {player.Hp}");


            Console.WriteLine("\n0. 다음");
            Console.ReadLine();
        }

    }



}

