using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scenes
{
     internal class PlayerInfoScene : Scene
     {
          private Player player;

          public PlayerInfoScene(Player player)
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
                    default:
                         Console.WriteLine("잘못된 입력입니다.");
                         break;
               }
          }

          public override void Show()
          {
               Console.WriteLine();
               player.ShowStatus();

               Console.WriteLine("0. 돌아가기");
          }
     }
}
