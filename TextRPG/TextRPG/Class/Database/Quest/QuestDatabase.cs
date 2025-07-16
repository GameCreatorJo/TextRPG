using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.QuestData;



namespace TextRPG.Class.Database.QuestData
{
    public class QuestDatabase
    {
        

        //전체 퀘스트 목록
        private Dictionary<int, QuestData> _questDictionary;
        public Dictionary<int, QuestData> QuestDictionary
        {
            get { return _questDictionary; }
        }

        //진행중인 퀘스트들
        private Dictionary<int, QuestData> _acceptedQuests = new Dictionary<int, QuestData>();

        public Dictionary<int, QuestData> AcceptedQuests
        {
            get { return _acceptedQuests; }
        }

        // 퀘스트 객체 미리 정의
        QuestData quest1 = new QuestData(1, "첫 번째 퀘스트", "미니언 5마리 처치", 5, 0);
        QuestData quest2 = new QuestData(2, "두 번째 퀘스트", "대포 미니언 처치", 1, 0);
        QuestData quest3 = new QuestData(3, "고양이 찾기", "잃어버린 고양이를 찾아주세요.", 0, 0);

        public QuestDatabase()
        {
            _questDictionary = new Dictionary<int, QuestData>();
            _acceptedQuests = new Dictionary<int, QuestData>();
        }

        public void CreateQuest()
        {
            AddQuest(quest1);
            AddQuest(quest2);
            AddQuest(quest3);
            // 필요시 더 추가 가능!
        }

        public void AddQuest(QuestData quest)
        {
            if (!_questDictionary.ContainsKey(quest.Id))
            {
                _questDictionary.Add(quest.Id, quest);
            }
            else
            {
                Console.WriteLine($"⚠️ 퀘스트 ID {quest.Id}는 이미 존재합니다.");
            }
        }

        public QuestData GetQuestById(int id)
        {
            if (_questDictionary.TryGetValue(id, out QuestData quest))
            {
                return quest;
            }
            else
            {
                Console.WriteLine($"⚠️ 퀘스트 ID {id}를 찾을 수 없습니다.");
                return null;
            }
        }

        public Dictionary<int, QuestData> GetAllQuests()
        {
            return _questDictionary;
        }

        

        public QuestData GetActiveQuest(int id)
        {
            return _acceptedQuests.TryGetValue(id, out QuestData quest) ? quest : null;
        }

        public Dictionary<int, QuestData> GetAcceptedQuests()
        {
            return _acceptedQuests;
        }
        public bool IsQuestAccepted(int id)
        {
            return _acceptedQuests.ContainsKey(id);
        }
        


    }

}
