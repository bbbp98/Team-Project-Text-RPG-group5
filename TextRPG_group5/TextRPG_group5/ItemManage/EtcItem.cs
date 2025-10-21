using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.ItemManage
{
    /// <summary>
    /// 향후 퀘스트용 아이템이나 기타 아이템이 추가될 경우를 대비한 클래스
    /// </summary>
    public class EtcItem : ItemManagement
    {
        public EtcItem(string name, string description, string require)
        {
            Name = name;
            Description = $"{require} 에 필요한 아이템 입니다.";
        }
    }
}
