using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.ItemManage
{
    /// <summary>
    /// 1회성 소모품의 추상클래스
    /// </summary>
    internal class UsableItem : ItemManagement
    {
        // 추상클래스 선언
        public virtual void UseItem(Player player, string name)
        {

        }
    }
}
