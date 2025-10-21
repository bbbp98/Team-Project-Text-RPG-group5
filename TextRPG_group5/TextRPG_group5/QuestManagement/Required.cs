using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.QuestManagement
{
    public class Required
    {
        public string Type { get; set; } // 퀘스트 조건의 종류
        public string Target { get; set; } // 퀘스트 조건의 대상
        public int Count { get; set; } // 퀘스트 조건을 충족하기 위한 대상의 수
        public int Current { get; set; } // 퀘스트의 현재 진행상황


        /// <summary>
        /// 퀘스트의 진행상황을 체크하여 완료여부를 판단
        /// </summary>
        /// <returns></returns>
        public bool IsComplete() => Current >= Count;
    }
}
