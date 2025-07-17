using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPG.Class.Database.PlayerData;
using TextRPG.Class.Database.MonsterData;
using TextRPG.Class.Data;

namespace TextRPG.Class.Manager
{
    
    public class BattleManager
    {
        //4:22
        private List<Monster> monsters = new List<Monster>();
        private Random random = new Random();
        private Player player;
        private int totalGoldReward = 0;
        private int totalExpReward = 0;
   
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
            Console.WriteLine($"MP {player.Mp}/{player.MaxMp}");
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
            int damage = random.Next(minDamage, maxDamage + 1);

            target.TakeDamage(damage, player.CriticalRate);

            Console.WriteLine($"\n{player.Name}의 공격!");
            Console.WriteLine($"{target.Name}에게 {damage}의 데미지를 입혔습니다!");

            if (target.Hp <= 0)
            {
                Console.WriteLine($"{target.Name} 을(를) 처치했습니다!");
                //처치하면 전투결과때 얻을 골드와 경험치가 늘어남
                totalGoldReward += target.Gold;
                totalExpReward += target.Exp;
            }
                


        }

        public void SkillMenu()
        {
            Console.WriteLine("\n1. 알파 스트라이크 - MP 10");
            Console.WriteLine("   공격력 * 2 로 하나의 적을 공격합니다.");
            Console.WriteLine("2. 더블 스트라이크 - MP 15");
            Console.WriteLine("   공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.");
            Console.WriteLine("0. 취소");

            Console.Write("\n원하시는 행동을 입력해주세요 >>");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (player.Mp < 10)
                    {
                        Console.WriteLine("MP가 부족합니다.");
                        return;
                    }
                    player.ConsumeMp(10);
                    AlphaStrike();
                    break;
                case "2":
                    if(player.Mp < 15)
                    {
                        Console.WriteLine("MP가 부족합니다.");
                        return;
                    }
                    player.ConsumeMp(15);
                    DoubleStrike();
                    break;
                case "0":
                    Console.WriteLine("스킬 사용을 취소하셨습니다");
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    SkillMenu();
                    break;

            }
        }

        public void AlphaStrike()
        {
            Console.WriteLine("\n대상을 선택하세요.");
            for(int i = 0; i < monsters.Count; i++)
            {
                if(monsters[i].Hp > 0)
                {
                    Console.WriteLine($"{i + 1}. {monsters[i].Name} (HP: {monsters[i].Hp})");
                }
            }
            Console.Write(">> ");
            if(!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > monsters.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                AlphaStrike();
                return;
            }

            Monster target = monsters[index - 1];
            if(target.Hp <= 0)
            {
                Console.WriteLine("이미 죽은 몬스터입니다.");
                AlphaStrike();
                return;
            }


            int damage = (int)(player.Str * 2);

            target.TakeDamage(damage, player.CriticalRate);

            Console.WriteLine($"\n스킬 : 알파 스트라이크을(를) 사용했다!");
            Console.WriteLine($"{target.Name}에게 {damage}의 피해를 입혔다.");

            if (target.Hp <= 0)
            {
                Console.WriteLine($"{target.Name} 을(를) 처치했습니다!");
                totalGoldReward += target.Gold;
                totalExpReward += target.Exp;
            }
                
        }

        public void DoubleStrike()
        {
            var aliveMonsters = monsters.Where(m => m.Hp > 0).ToList();
            if(aliveMonsters.Count == 0)
            {
                Console.WriteLine("공격할 대상이 없습니다");
                return;
            }

            Console.WriteLine($"\n스킬 : 더블 스트라이크을(를) 사용했다!");

            for(int i = 0; i < 2;  i++)
            {
                if(aliveMonsters.Count == 0)
                {
                    break;
                }
                int index = random.Next(aliveMonsters.Count);
                Monster target = aliveMonsters[index];

                int damage = (int)(player.Str * 1.5);

                target.TakeDamage(damage, player.CriticalRate);

                Console.WriteLine($"{target.Name}에게 {damage}의 피해를 입혔다.");

                if(target.Hp <= 0)
                {
                    Console.WriteLine($"{target.Name} 을(를) 처치했습니다!");
                    aliveMonsters.RemoveAt(index);
                    totalGoldReward += target.Gold;
                    totalExpReward += target.Exp;
                }
            }
        }
        
        private void PlayerTurn()
        {
            Console.WriteLine("\n1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("0. 대기");

            Console.Write("\n행동을 선택하세요 >> ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Attack();
                    break;
                case "2":
                    SkillMenu();
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

        public void UseMP(int Use)
        {

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

                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"Lv.{player.Lv} {player.Name}");
                Console.WriteLine($"HP {player.MaxHp} -> {player.Hp}");
                Console.WriteLine($"MP {player.MaxMp} -> {player.Mp}");

                Console.WriteLine("[획득한 보상]");
                Console.WriteLine($"{totalGoldReward} Gold");
                Console.WriteLine($"{totalExpReward} Exp\n");

                //전투가 끝나고 한번에 골드와 경험치가 들어감
                //player.AddGold(totalGoldReward);
                player.TakeEXP(totalExpReward);
            }


           

            Console.WriteLine("\n0. 다음");
            Console.ReadLine();
        }
    }
}

