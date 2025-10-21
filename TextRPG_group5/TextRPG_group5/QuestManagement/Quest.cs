using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TextRPG_group5.QuestManagement
{
    /// <summary>
    /// 퀘스트의 진행상태를 구분하기 위한 열거형(Enum)
    /// </summary>
    public enum QuestStatus
    {
        NoProgress,
        InProgress,
        Complete,
        Done
    }
    /// <summary>
    /// 퀘스트가 공통적으로 가지고 있어야할 정보를 명시, 사용자정의자료형 프로퍼티는 json 역직렬화 사용시 자동set
    /// </summary>
    public class Quest
    {
        public int QuestID { get; set; } // 퀘스트 id 프로퍼티
        public string QuestTitle { get; set; } // 퀘스트 타이틀 프로퍼티
        public string QuestDescription { get; set; } // 퀘스트 설명 프로퍼티
        public List<Required> objectives { get; set; } // 퀘스트 목표(타입, 타겟, 횟수) 프로퍼티
        public Rewards Rewards { get; set; } // 퀘스트 보상 프로퍼티
        public QuestStatus Status { get; set; } // 퀘스트 진행상황 프로퍼티


        /// <summary>
        /// 퀘스트 완료여부를 반환한다.
        /// </summary>
        /// <returns></returns>
        public bool CheckCompletion()
        {
            // Required 클래스의 IsComplete() 메서드가 반환하는 bool 값을 그대로 반환
            return objectives.All(o => o.IsComplete());
        }
    }
}
