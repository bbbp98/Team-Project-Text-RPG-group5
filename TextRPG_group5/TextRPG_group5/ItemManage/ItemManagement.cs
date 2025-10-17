using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5;

namespace TextRPG_group5.ItemManage
{
    // 아이템 전체의 관리하는 클래스
    public class ItemManagement
    {
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public int Price { get; set; }
        public bool IsEquip { get; set; }
        public int ItemCounts { get; set; }
    }
}
