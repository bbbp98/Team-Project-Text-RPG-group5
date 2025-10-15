using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.QuestManagement
{
    public class Rewards
    {
        public int Exp { get; set; } // 보상 경험치 프로퍼티
        public int Gold { get; set; } // 보상 금액 프로퍼티
        public List<string> Items { get; set; } // 보상 아이템 프로퍼티
    }
}
