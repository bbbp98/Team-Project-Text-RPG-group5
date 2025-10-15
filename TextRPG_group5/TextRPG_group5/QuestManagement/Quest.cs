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
        public int QuestID { get; set; }
        public string QuestTitle { get; set; }
        public string QuestDescription { get; set; }
        public List<Required> objectives { get; set; }
        public Rewards Rewards { get; set; }
        public QuestStatus Status { get; set; }


        public bool CheckCompletion()
        {
            return objectives.All(o => o.IsComplete());
        }
    }
}
