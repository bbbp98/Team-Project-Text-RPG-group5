using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.ItemManage
{
    /// <summary>
    /// 추상클래스 Item을 상속한 포션클래스
    /// </summary>
    internal class Potion : UsableItem
    {
        public int HealAmount { get; protected set; }

        public Potion(string name, string Description, int healAmount, int price)
        {
            Name = name;
            HealAmount = healAmount;
            Price = price;
            Description = $"{HealAmount}만큼 체력을 보충합니다.";
        }

        public override void UseItem(Player player)
        {
            Console.WriteLine(Description);
            player.NowHp += HealAmount;
        }
    }
}
