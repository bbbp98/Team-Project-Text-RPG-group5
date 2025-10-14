using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scenes
{
     internal class DungeonEntranceScene : Scene
     {
          public override void HandleInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         // go to MainScene
                         //Program.SetScene(new MainScene());
                         break;
                    case 1:
                         // go to StageScene
                         break;
                    default:
                         Console.WriteLine("잘못된 입력입니다.\n");
                         break;
               }
          }


          public override void Show()
          {
               int stage = 3;

               Console.WriteLine("던전 입구\n");
               Console.WriteLine("그 누구도 끝까지 도달하지 못한 곳. 이제 당신이 그 전설이 될 차례다.");
               Console.WriteLine("지하로 향하는 길이 열린다. 위험과 보물이 함께 기다리고 있다.");
               Console.WriteLine("탑의 꼭대기에는 전설의 보스가 잠들어 있다고 한다.");
               Console.WriteLine("한 층, 또 한 층. 당신의 용기만이 길을 밝혀줄 것이다.");

               Console.WriteLine();
               Console.WriteLine($"1. 던전 입장 (현재 스테이지: {stage})");
               Console.WriteLine("0. 마을로 돌아가기");





               // 아라님 이런식으로 가져다가 사용하시면 됩니다.
               StageManager sm = new StageManager();
               List<Monster> monsters = sm.CreateMonsters(stage);
          }
     }
}
