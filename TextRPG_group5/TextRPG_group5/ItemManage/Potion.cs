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
        public int MpAmount { get; protected set; }
        public int CountItem { get; protected set; }

        public Potion(string name, string Description, int healAmount, int mpAmount, int price)
        {
            Name = name;
            HealAmount = healAmount;
            MpAmount = mpAmount;
            Price = price;
            

            if(HealAmount != 0)
            {
                Description = $"{HealAmount}만큼 체력을 보충합니다.";
            }
            else
            {
                Description = $"{MpAmount}만큼 마나를 보충합니다.";
            }
        }

        public override void UseItem(Player player, Potion potion)
        {
            if(potion.HealAmount != 0)
            {
                player.NowHp += potion.HealAmount;
                player.Inventory.RemoveItem(potion);
            }
            else
            {
                player.NowMp += potion.MpAmount;
                player.Inventory.RemoveItem(potion);
            }

            Console.WriteLine(Description);
        }
    }
}
