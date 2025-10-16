using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                         break;
               }
          }

          public override void Show()
          {
               Console.WriteLine("인벤토리");

               Console.WriteLine("1. 장착 관리 메뉴");
               Console.WriteLine("0. 돌아가기");
          }
     }
}
