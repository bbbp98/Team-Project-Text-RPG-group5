using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.ItemManage
{
    public class EtcItem : ItemManagement
    {
        public EtcItem(string name, string description, string require)
        {
            Name = name;
            Description = $"{require} 에 필요한 아이템 입니다.";
        }
    }
}
