using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPG.Class.Database.PlayerData;
using TextRPG.Class.Database.MonsterData;
using TextRPG.Class.Database.QuestData; //아래에 Batle Result 메서드에 퀘스트 진행 로직 추가시 활성화할것
using TextRPG.Class.Data;

namespace TextRPG.Class.Manager
{
    
    //배틀메니저
    //11:46
    public class BattleManager
    {
        //4:22
        private List<Monster> monsters = new List<Monster>();
        private Random random = new Random();
        private Player player;
        private int totalGoldReward = 0;
        private int totalExpReward = 0;
        //난이도 별 보정치
        private Dictionary<Monster, (int extraHp, int extraStr, int extraLv, int extraMaxHp)> difficultyModifiers = new();
        private Dictionary<Monster, int> currentHpDict = new();
        public void SearchHP(Player player, Monster monster)
        {
            Console.WriteLine($"{player.Name}의 HP: {player.Hp}/{player.MaxHp}");
            Console.WriteLine($"{monster.Name}의 HP: {GetEffectiveMonsterHp(monster)}/{GetEffectiveMonsterMaxHp(monster)}");
        }

        public void StartDungeonBattle(Player initPlayer)
        {
            player = initPlayer;

            string input;
            int difficulty = -1;

            while (true)
            {
                Console.Write(">> ");
                input = Console.ReadLine();

                if (int.TryParse(input, out difficulty) && difficulty >= 0 && difficulty <= 3)
                {
                    break;
                }
                Console.WriteLine("잘못된 입력입니다. 0~3 사이의 숫자를 입력하세요");
            }

            if (difficulty == 0)
            {
                Console.WriteLine("마을로 돌아갑니다.");
                return;
            }

            monsters = GenerateDungeonMonsters(difficulty);

        }

        public void Battle()
        {/*
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }*/

            /*몬스터와 플레이어 HP 확인(테스트용
            Console.WriteLine($"몬스터 수: {monsters.Count}");
            foreach (var m in monsters)
            {
                Console.WriteLine($"{m.Name} HP: {m.Hp}/{m.MaxHp}");
            }
            Console.WriteLine($"플레이어 HP: {player.Hp}/{player.MaxHp}");
            */

            //Console.Clear();
            Console.WriteLine("\n전투 시작!\n");
            SearchHP(player, monsters[0]);
            while (player.Hp > 0 && monsters.Any(m => GetEffectiveMonsterHp(m) > 0))
            {
                ShowBattleStatus();
                PlayerTurn();

                if (monsters.All(m => GetEffectiveMonsterHp(m) <= 0))
                {
                    break;
                }

                EnemyTurn();

            }

            BattleResult();
        }

        public List<Monster> GenerateDungeonMonsters(int difficulty)
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

                //난이도 별 추가 스텟
                int extraHp = 10 * (difficulty - 1);
                int extraStr = 5 * (difficulty - 1);
                int extraLv = 1 * (difficulty - 1);
                int extraMaxHp = 10 * (difficulty - 1);

                difficultyModifiers[clone] = (extraHp, extraStr, extraLv, extraMaxHp);

                int effectiveMaxHp = clone.MaxHp + extraMaxHp;
                currentHpDict[clone] = effectiveMaxHp;

                Console.WriteLine($"몬스터 생성: {clone.Name} (Lv.{GetEffectiveMonsterLv(clone)}) {GetEffectiveMonsterHp(clone)}/{GetEffectiveMonsterMaxHp(clone)}");

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
                if (GetEffectiveMonsterHp(m) <= 0)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{i + 1}. Lv.{GetEffectiveMonsterLv(m)} {m.Name} {(GetEffectiveMonsterHp(m) <= 0 ? "Dead" : $"HP {GetEffectiveMonsterHp(m)}/{GetEffectiveMonsterMaxHp(m)}")}");
                Console.ResetColor();
            }

            Console.WriteLine($"\n[내정보");
            Console.WriteLine($"Lv.{player.Lv} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
            Console.WriteLine($"MP {player.Mp}/{player.MaxMp}");
        }


        public void Attack()
        {
            Console.WriteLine("\n대상을 선택하세요");
            for (int i = 0; i < monsters.Count; i++)
            {
                if (GetEffectiveMonsterHp(monsters[i]) > 0)
                    Console.WriteLine($"{i + 1}. {monsters[i].Name} (HP: {GetEffectiveMonsterHp(monsters[i])}/{GetEffectiveMonsterMaxHp(monsters[i])})");
            }
            Console.Write(">> ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > monsters.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                Attack();
                return;
            }

            Monster target = monsters[index - 1];
            if (GetEffectiveMonsterHp(target) <= 0)
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

            DecreaseMonsterHp(target, damage);

            Console.WriteLine($"\n{player.Name}의 공격!");
            Console.WriteLine($"{target.Name}에게 {damage}의 데미지를 입혔습니다!");

            if (GetEffectiveMonsterHp(target) <= 0)
            {
                Console.WriteLine($"{target.Name} 을(를) 처치했습니다!");
                totalGoldReward += target.Gold;
                totalExpReward += target.Exp;
                QuestManager.Instance.PromptKillProgress(target);
            }



        }

        public void SkillMenu()
        {
            Console.WriteLine("\n1.알파 스트라이크- MP 10");
            Console.WriteLine("   공격력 * 2 로 하나의 적을 공격합니다.");
            Console.WriteLine("2. 더블 스트라이크- MP 15");
            Console.WriteLine("   공격력* 1.5 로 2명의 적을 랜덤으로 공격합니다.");
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
            Console.WriteLine("\n대상을 선택하세요");
            for(int i = 0; i < monsters.Count; i++)
            {
                if(GetEffectiveMonsterHp(monsters[i]) > 0)
                {
                    Console.WriteLine($"{i + 1}. {monsters[i].Name} (HP: {GetEffectiveMonsterHp(monsters[i])}/{GetEffectiveMonsterMaxHp(monsters[i])})");
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
            if(GetEffectiveMonsterHp(target) <= 0)
            {
                Console.WriteLine("이미 죽은 몬스터입니다.");
                AlphaStrike();
                return;
            }


            int damage = (int)(player.Str * 2);

            DecreaseMonsterHp(target, damage);

            Console.WriteLine($"\n스킬 : 알파 스트라이크을(를) 사용했다!");
            Console.WriteLine($"{target.Name}에게 {damage}의 피해를 입혔다!");

            if (GetEffectiveMonsterHp(target) <= 0)
            {
                Console.WriteLine($"{target.Name} 을(를) 처치했습니다!");
                totalGoldReward += target.Gold;
                totalExpReward += target.Exp;
                QuestManager.Instance.PromptKillProgress(target);
            }
                
        }

        public void DoubleStrike()
        {
            var aliveMonsters = monsters.Where(m => GetEffectiveMonsterHp(m) > 0).ToList();
            if (aliveMonsters.Count == 0)
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

                DecreaseMonsterHp(target, damage);

                Console.WriteLine($"{target.Name}에게 {damage}의 피해를 입혔다.");

                if(GetEffectiveMonsterHp(target) <= 0)
                {
                    Console.WriteLine($"{target.Name} 을(를) 처치했습니다!");
                    aliveMonsters.RemoveAt(index);
                    totalGoldReward += target.Gold;
                    totalExpReward += target.Exp;
                    QuestManager.Instance.PromptKillProgress(target);
                }
            }
        }
        
        private void PlayerTurn()
        {
            Console.WriteLine("\n1. 공격");
            Console.WriteLine("\n2. 스킬");
            Console.WriteLine("\n0. 대기");

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
                if (GetEffectiveMonsterHp(monster) <= 0) continue;

                int damage = GetEffectiveMonsterStr(monster);

                player.TakeDamage(damage, monster.CriticalRate);
                Console.WriteLine($"\n{monster.Name}의 공격 {player.Name}에게 {damage}의 데미지");
            }
        }

        //난이도 보정치를 적용한 몬스터 스텟
        private int GetEffectiveMonsterHp(Monster monster)
        {
            if (currentHpDict.TryGetValue(monster, out int hp))
                return hp;

            if (difficultyModifiers.TryGetValue(monster, out var mod))
                return monster.Hp + mod.extraHp;

            return monster.Hp;
        }

        private int GetEffectiveMonsterMaxHp(Monster monster)
        {
            if (difficultyModifiers.TryGetValue(monster, out var mod))
                return monster.MaxHp + mod.extraMaxHp;
            return monster.MaxHp;
        }

        private int GetEffectiveMonsterStr(Monster monster)
        {
            if (difficultyModifiers.TryGetValue(monster, out var mod))
                return monster.Str + mod.extraStr;
            return monster.Str;
        }

        private int GetEffectiveMonsterLv(Monster monster)
        {
            if (difficultyModifiers.TryGetValue(monster, out var mod))
                return monster.Lv + mod.extraLv;
            return monster.Lv;
        }

        // 몬스터 HP 감소 (데미지 적용)
        private void DecreaseMonsterHp(Monster monster, int damage)
        {
            if (!currentHpDict.ContainsKey(monster))
            {
                int effectiveMaxHp = monster.MaxHp + (difficultyModifiers.TryGetValue(monster, out var mod) ? mod.extraMaxHp : 0);
                currentHpDict[monster] = effectiveMaxHp;
            }

            currentHpDict[monster] = Math.Max(0, currentHpDict[monster] - damage);
        }


        public void BattleResult()
        {
            //Console.Clear();
            Console.WriteLine("\n전투결과\n");

            int defeatedCount = monsters.Count(m => GetEffectiveMonsterHp(m) <= 0);

            if (player.Hp <= 0)
            {
                Console.WriteLine("패배했습니다.");
            }
            else
            {
                Console.WriteLine("승리");
                Console.WriteLine($"던전에서 몬스터 {defeatedCount}마리를 잡았습니다.\n");
                //퀘스트 처치수 업데이트

                foreach (var monster in monsters.Where(m => GetEffectiveMonsterHp(m) <= 0))
                {
                    QuestManager.Instance.UpdateQuestKillCount(monster.Name);
                }



                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"Lv.{player.Lv} {player.Name}");
                Console.WriteLine($"HP {player.MaxHp} -> {player.Hp}");
                Console.WriteLine($"MP {player.MaxMp} -> {player.Mp}");

                Console.WriteLine("[획득한 보상]");
                Console.WriteLine($"{totalGoldReward} Gold");
                Console.WriteLine($"{totalExpReward} Exp\n");

                //전투가 끝나고 한번에 골드와 경험치가 들어감
                player.SpendGold(-totalGoldReward);
                player.TakeEXP(totalExpReward);
            }


           

            Console.WriteLine("\n0. 다음");
            Console.ReadLine();
        }
    }
}

