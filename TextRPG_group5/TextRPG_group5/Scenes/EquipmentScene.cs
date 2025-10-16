using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.ItemManage;

namespace TextRPG_group5.Scenes
{
     internal class EquipmentScene : Scene
     {
          private Player player;

          public EquipmentScene(Player player)
          {
               this.player = player;
          }

          public override void HandleInput(byte input)
          {
               if (input == 0)
               {
                    Program.SetScene(new InventoryScene(player));
                    return;
               }
               else if (input > 0 && input <= player.Inventory.GetCount())
               {
                    player.Inventory.Equip(input);
                    return;
               }
               else
               {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
               }
          }

          public override void Show()
          {
               // 장착되어 있는 아이템 선택 시 해제
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine("인벤토리 - 아이템 장착/해제");
               Console.ForegroundColor = ConsoleColor.White;

               Console.WriteLine("\n장착하려는 아이템을 선택하면 아이템이 장착됩니다.");
               Console.WriteLine("해제하려는 아이템을 선택하면 아이템이 해제됩니다.");

               player.Inventory.Show();

               Console.WriteLine();
               Console.WriteLine("0. 인벤토리로 돌아가기");

          }
     }
}
