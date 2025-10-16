using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.ItemManage;

namespace TextRPG_group5.Scenes
{
     internal class InventoryScene : Scene
     {
          private Player player;

          public InventoryScene(Player player)
          {
               this.player = player;
          }

          public override void HandleInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         Program.SetScene(new MainScene(player)); 
                         break;
                    case 1:
                         Program.SetScene(new InventorySortScene(player));
                         break;
                    case 2:
                         // load InventoryEquipScene
                         Program.SetScene(new EquipmentScene(player));
                         break;
                    default:
                         Console.WriteLine("잘못된 입력입니다.\n");
                         break;
               }
          }

          public override void Show()
          {
               Console.WriteLine("인벤토리");

               player.Inventory.Show();

               Console.WriteLine("\n1. 인벤토리 정렬");
               Console.WriteLine("2. 장착 관리 메뉴");
               Console.WriteLine("0. 돌아가기");
          }
     }
}
