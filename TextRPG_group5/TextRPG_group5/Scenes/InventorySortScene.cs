using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scenes
{
     internal class InventorySortScene : Scene
     {
          private Player player;

          public InventorySortScene(Player player)
          {
               this.player = player;
          }

          public override void HandleInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         Program.SetScene(new InventoryScene(player));
                         break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                         player.Inventory.Sort(input);
                         break;
                    default:
                         Console.WriteLine("잘못된 입력입니다.\n");
                         break;
               }
          }

          public override void Show()
          {
               Console.WriteLine("인벤토리-정렬");

               player.Inventory.Show();

               Console.WriteLine("\n1. 이름순");
               Console.WriteLine("2. 장착순");
               Console.WriteLine("3. 공격력");
               Console.WriteLine("4. 방어력");
               Console.WriteLine("0. 돌아가기");
          }
     }
}
