using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPG.Class.Data;
using TextRPG.Class.Database.Monster;
using TextRPG.Class.Database.Player;
using static System.Net.Mime.MediaTypeNames;

namespace TextRPG.Class.Manager
{
    //BattleManager 2:33
    internal class BattleManager
    {
        private List<Monster> monsters = new List<Monster>();
        private Random random = new Random();
        private Player player;
   
        

        public void SearchHP(Player player, Monster monster)
        {
            Console.WriteLine($"{player.Name}의 HP: {player.Hp}/{player.MaxHp}");
            Console.WriteLine($"{monster.Name}의 HP: {monster.Hp}/{monster.MaxHp}");
        }
        public void Battle(Player initPlayer)
        {/*
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }*/

            player = initPlayer;
            monsters = GenerateDungeonMonsters();

            /*몬스터와 플레이어 HP 확인(테스트용
            Console.WriteLine($"몬스터 수: {monsters.Count}");
            foreach (var m in monsters)
            {
                Console.WriteLine($"{m.Name} HP: {m.Hp}/{m.MaxHp}");
            }
            Console.WriteLine($"플레이어 HP: {player.Hp}/{player.MaxHp}");
            */

            //Console.Clear();
            Console.WriteLine("전투 시작\n");
            SearchHP(player, monsters[0]);
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

        public List<Monster> GenerateDungeonMonsters()
        {
            var dungeonMonsters = new List<Monster>();
            var monsterDict = GameManager.Instance.CreateManager.MonsterDatabase.MonsterDictionary;
            var monsterKeys = monsterDict.Keys.ToList();

            int count = random.Next(1, 5);

            for (int i = 0; i < count; i++)
            {
                string key = monsterKeys[random.Next(monsterKeys.Count)];
                Monster original = monsterDict[key];
                var clone = new Monster(original);
                Console.WriteLine($"몬스터 생성: {original.Name} (Lv.{original.Lv}){original.Hp},{original.MaxHp}");

                dungeonMonsters.Add(clone);
            }

            return dungeonMonsters;
        }


        public void ShowBattleStatus()
        {
            //Console.Clear();
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


        public void Attack()
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
            int baseDamage = random.Next(minDamage, maxDamage + 1);

            int crit = player.CriticalDamage(player.CriticalRate);
            int avoid = target.AvoidDamage();

            if (avoid == 0)
            {
                Console.WriteLine($"\n{player.Name}의 공격!");
                Console.WriteLine($"Lv.{target.Name}을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            }
            else
            {
                int damage = baseDamage * crit;
                target.TakeDamage(damage, player.CriticalRate);

                Console.WriteLine($"\n{player.Name}의 공격!");
                Console.WriteLine($"Lv.{target.Lv} {target.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");

                if (crit > 1)
                {
                    Console.Write("- 치명타 공격!!");
                }
                   

                if (target.Hp <= 0)
                {
                    Console.WriteLine($"{target.Name} 을(를) 처치했습니다!");
                }
                    
            }
            


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

        public void EnemyTurn()
        {
            foreach (var monster in monsters)
            {
                if (monster.Hp <= 0) continue;

                int damage = monster.Str;
                
                player.TakeDamage(damage, monster.CriticalRate);
                Console.WriteLine($"\n{monster.Name}의 공격 {player.Name}에게 {damage}의 데미지");
            }
        }

        public void BattleResult()
        {
            //Console.Clear();
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

