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
        public PotionType Type { get; protected set; }

        public Potion(string name, string description, int healAmount, int mpAmount, int type, int price)
        {
            Name = name;
            HealAmount = healAmount;
            MpAmount = mpAmount;
            Type = (PotionType)type;
            Price = price;
            if(HealAmount != 0)
            {
                this.Description = $"{HealAmount}만큼 체력을 보충합니다.";
            }
            else
            {
                this.Description = $"{MpAmount}만큼 마나를 보충합니다.";
            }
        }
        public override void UseItem(Player player, Potion potion)
        {
            if (potion.Type == PotionType.HealthPotion)
            {
                if(player.MaxHp < player.NowHp + potion.HealAmount)
                {
                    player.NowHp = player.MaxHp;
                    Console.WriteLine("HP를 완전히 회복했습니다.");
                    return;
                }
                player.NowHp += potion.HealAmount;
                player.Inventory.RemoveItem(potion);
                Console.WriteLine($"{Description} | 현재 MP : {player.NowMp}");
            }
            else
            {
                if (player.MaxMp < player.NowMp + potion.MpAmount)
                {
                    player.NowHp = player.MaxHp;
                    Console.WriteLine("MP를 완전히 회복했습니다.");
                    return;
                }
                player.NowMp += potion.MpAmount;
                player.Inventory.RemoveItem(potion);
                Console.WriteLine($"{Description} | 현재 MP : {player.NowMp}");
            }

            
        }
    }
}
