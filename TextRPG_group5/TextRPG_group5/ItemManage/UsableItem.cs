using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.ItemManage
{
    /// <summary>
    /// 포션의 종류를 구분하기위한 Enum
    /// </summary>
    public enum PotionType
    {
        HealthPotion = 1,
        ManaPotion
    }
    /// <summary>
    /// 포션 이외의 소모아이템의 기능 추가를 고려한 가상메서드가 존재
    /// </summary>
    internal class UsableItem : ItemManagement
    {
        // 추상클래스 선언
        public virtual void UseItem(Player player, Potion potion)
        {

        }
    }
}
