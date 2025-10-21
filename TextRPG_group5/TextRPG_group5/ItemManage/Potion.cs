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
        public int Amount { get; set; }
        public PotionType Type { get; set; }

        /// <summary>
        /// 포션의 정보를 포션타입에 따라 구별
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="amount"></param>
        /// <param name="type"></param>
        /// <param name="price"></param>
        public Potion(string name, string description, int amount, int type, int price)
        {
            Name = name;
            Amount = amount;
            Type = (PotionType)type;


            Price = price;
            if(Type == PotionType.HealthPotion)
            {
                this.Description = $"{Amount}만큼 체력을 보충합니다.";
            }
            else
            {
                this.Description = $"{Amount}만큼 마나를 보충합니다.";
            }
        }
        /// <summary>
        /// 포션 사용시 포션타입에 따라 효과를 구분
        /// </summary>
        /// <param name="player"></param>
        /// <param name="potion"></param>
        public override void UseItem(Player player, Potion potion)
        {
            if (potion.Type == PotionType.HealthPotion)
            {
                if(player.MaxHp < player.NowHp + potion.Amount)
                {
                    player.NowHp = player.MaxHp;
                    Console.WriteLine("HP를 완전히 회복했습니다.");
                    return;
                }
                player.NowHp += potion.Amount;
                player.Inventory.RemoveItem(potion);
                Console.WriteLine($"{Description} | 현재 MP : {player.NowMp}");
            }
            else
            {
                if (player.MaxMp < player.NowMp + potion.Amount)
                {
                    player.NowMp = player.MaxMp;
                    Console.WriteLine("MP를 완전히 회복했습니다.");
                    return;
                }
                player.NowMp += potion.Amount;
                player.Inventory.RemoveItem(potion);
                Console.WriteLine($"{Description} | 현재 MP : {player.NowMp}");
            }

            
        }
    }
}
