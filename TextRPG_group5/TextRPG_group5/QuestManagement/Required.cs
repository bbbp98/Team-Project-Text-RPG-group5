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

        public bool IsComplete() => Current >= Count;
    }
}
