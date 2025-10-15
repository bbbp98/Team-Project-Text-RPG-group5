using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TextRPG_group5.QuestManagement
{
    public class QuestManager
    {
        public int QuestID { get; protected set; }
        public string QuestTitle { get; protected set; }
        public string QuestDescription { get; protected set; }
        public List<Required> objectives { get; protected set; }


    }
}
