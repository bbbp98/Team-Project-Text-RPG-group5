using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5;

namespace TextRPG_group5.ItemManage
{
    /// <summary>
    /// 모든 아이템이 반드시 가지고 있어야하는 정보
    /// </summary>
    public class ItemManagement
    {
        public string? Name { get; set; } // 아이템 이름
        public string? Description { get; set; } // 아이템 설명
        public int Price { get; set; } // 아이템 상점가격
        public bool IsEquip { get; set; } // 아이템 장착 여부
        public int ItemCounts { get; set; } // 아이템 개수
    }
}
