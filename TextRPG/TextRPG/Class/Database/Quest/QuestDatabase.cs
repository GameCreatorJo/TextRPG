using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Class.Database.Quest
{
    public class QuestDatabase
    {
        private Dictionary<int, Quest> _questDictionary = new Dictionary<int, Quest>();
        public QuestDatabase()
        {
            // 퀘스트 데이터 초기화
            new Quest(1, "첫 번째 퀘스트", "미니언 5마리 처치", 5, 0);
            new Quest(2, "두 번째 퀘스트", "대포 미니언 처치", 1, 0);

            // 추가적인 퀘스트를 여기에 추가할 수 있습니다.
        }

        // 퀘스트 추가 메서드
        public void AddQuest(Quest quest)
        {
            if (!_questDictionary.ContainsKey(quest.Id))
            {
                _questDictionary.Add(quest.Id, quest);
            }
            else
            {
                Console.WriteLine($"퀘스트 ID {quest.Id}는 이미 존재합니다.");
            }
        }
        public Dictionary<int, Quest> GetAllQuests()
        {
            return _questDictionary;
        }
        public Quest GetQuestById(int id)
        {
            if (_questDictionary.TryGetValue(id, out Quest quest))
            {
                return quest;
            }
            else
            {
                Console.WriteLine($"퀘스트 ID {id}를 찾을 수 없습니다.");
                return null;
            }
        }

    }

}
