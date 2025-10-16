using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.QuestManagement
{
    public class Required
    {
        public string Type { get; set; }
        public string Target { get; set; }
        public int Count { get; set; }
        public int Current { get; set; }


        // 현재 진행상황이 목표를 달성하였을 때. true반환 달성하지 못하였을때는 false반환
        public bool IsComplete() => Current >= Count;
    }
}
