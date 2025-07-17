using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using TextRPG.Class.Manager;
using TextRPG.Class.Database.MonsterData;
using TextRPG.Class.Database.QuestData;
using TextRPG.Class.Database.PlayerData;




namespace TextRPG.Class.Test
{
    internal class SystemTest
    {
        public static void Run()
        {
            Console.WriteLine("🧪 [퀘스트 + 전투 시스템 통합 테스트 시작]");

            // 1. 퀘스트 매니저 초기화
            var questDatabase = new QuestDatabase();
            QuestManager.Instance.Initialize(questDatabase);
            Console.WriteLine("🗂️ 퀘스트 매니저 초기화 완료");

            // 2. 퀘스트 생성 및 등록
            var quest = new QuestData(
                id: 1,
                title: "미니언 처치",
                description: "미니언 1마리 처치",
                killTarget: 1,
                killCount: 0
            );
            quest.TargetMonsterKey = "미니언";
            QuestManager.Instance.AcceptQuest(quest.Id);
            Console.WriteLine($"📜 퀘스트 수락: {quest.Title}");

            // 3. 플레이어 생성
            var player = new Player("테스트전사", "1");
            Console.WriteLine($"👤 플레이어 생성: {player.Name} ({player.Job})");

            // 4. 전투 매니저 생성 및 전투 시작
            var battleManager = new BattleManager();
            battleManager.Battle(player); // 실제 몬스터 생성 및 전투 진행

            // 5. 퀘스트 상태 확인
            var activeQuests = QuestManager.Instance.GetActiveQuests();
            foreach (var q in activeQuests.Values)
            {
                Console.WriteLine($"\n📋 퀘스트 상태 확인");
                Console.WriteLine(q.GetQuestInfo());
            }

            Console.WriteLine("\n✅ [퀘스트 + 전투 시스템 통합 테스트 완료]");


        }
    }
}
