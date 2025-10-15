using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.QuestManagement
{
    public class Rewards
    {
        public int Exp { get; protected set; }
        public int Gold { get; protected set; }
        public List<string> items { get; protected set; }
    }
}
