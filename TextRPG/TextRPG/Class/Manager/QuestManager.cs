using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Class.Database.QuestData;

namespace TextRPG.Class.Manager
{

    public class QuestManager
    {
        private static QuestManager _instance;
        private static readonly object _lock = new object();

        public static QuestManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new QuestManager();
                    }
                    return _instance;
                }
            }

        }
        private QuestData _activeQuest;
        private QuestDatabase _questDatabase;


        public void Initialize(QuestDatabase database)
        {
            _questDatabase = database;

        }
        //퀘스트 수락
        public void SelectQuest(int questId)
        {
            QuestData quest = _questDatabase.GetQuestById(questId);
            if (quest != null)
            {
                _activeQuest = quest;
                Console.WriteLine($"퀘스트 '{_activeQuest.Title}'을(를) 수락했습니다.");
            }
            else
            {
                Console.WriteLine("퀘스트를 찾을 수 없습니다.");
            }
        }
        //퀘스트 포기
        public void AbandonQuest()
        {
            if (_activeQuest != null && !_activeQuest.IsCompleted)
            {
                Console.WriteLine($"퀘스트 '{_activeQuest.Title}'을(를) 포기했습니다.");
                _activeQuest = null;
            }
            else if (_activeQuest?.IsCompleted == true)
            {
                Console.WriteLine("완료된 퀘스트는 포기할 수 없습니다.");
            }
            else
            {
                Console.WriteLine("진행 중인 퀘스트가 없습니다.");
            }
        }

        //퀘스트 진행도
        public void UpdateQuestKillCount()
        {
            if (_activeQuest != null && !_activeQuest.IsCompleted)
            {
                _activeQuest.UpdateKill();
                Console.WriteLine(_activeQuest.GetQuestInfo());
            }
            else
            {
                Console.WriteLine("진행 중인 퀘스트가 없습니다.");
            }

        }
        public QuestData GetActiveQuest()
        {
            return _activeQuest;
        }

    }

}
