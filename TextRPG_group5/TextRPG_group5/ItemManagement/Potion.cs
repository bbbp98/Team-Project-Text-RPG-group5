using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.ItemManagement
{
    /// <summary>
    /// 추상클래스 Item을 상속한 포션클래스
    /// </summary>
    public class Potion : UsableItem
    {
        public int HealAmount { get; protected set; }

        public Potion(string name, string Description, int healAmount, int price)
        {
            Name = name;
            HealAmount = healAmount;
            Price = price;
            Description = $"{HealAmount}만큼 체력을 보충합니다.";
        }

        public override void UseItem() // 매개변수 추가 필요 : 플레이어 메서드 오브젝트
        {
            // 회복 or 버프 로직 추가 필요
        }
    }
}
