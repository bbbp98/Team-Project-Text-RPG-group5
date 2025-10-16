using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TextRPG_group5.QuestManagement
{
    public enum QuestStatus
    {
        NoProgress,
        InProgress,
        Complete
    }
    public class Quest
    {
        public int QuestID { get; set; } // 퀘스트 id 프로퍼티
        public string QuestTitle { get; set; } // 퀘스트 타이틀 프로퍼티
        public string QuestDescription { get; set; } // 퀘스트 설명 프로퍼티
        public List<Required> objectives { get; set; } // 퀘스트 목표(타입, 타겟, 횟수) 프로퍼티
        public Rewards Rewards { get; set; } // 퀘스트 보상 프로퍼티
        public QuestStatus Status { get; set; } // 퀘스트 진행상황 프로퍼티


        public bool CheckCompletion()
        {
            // Required 클래스의 IsComplete() 메서드가 반환하는 bool 값을 그대로 반환
            return objectives.All(o => o.IsComplete());
        }
    }
}
