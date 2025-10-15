using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.Managers;

namespace TextRPG_group5.Scenes
{
     internal class DungeonEntranceScene : Scene
     {
          int selectedStage = byte.MaxValue;
          int maxStage = 6;

          public override void HandleInput(byte input)
          {
               if (input == 0)
               {
                    // go to MainScene
                    //Program.SetScene(new StartScene());

                    Console.WriteLine("마을로 돌아갑니다."); // log
               }
               else if (input > maxStage)
               {
                    Console.WriteLine("잘못된 입력입니다.\n");
               }
               else
               {
                    // go to BattleScene
                    //Program.SetScene(new BattleScene()); // stage 정보 전달
                    Program.SetScene(new DungeonResultScene(true)); // test
                    Console.WriteLine($"{input}층 던전 입장");   // log
               }


               //switch (input)
               //{
               //     case 0:
               //          // go to MainScene
               //          //Program.SetScene(new StartScene());
               //          break;
               //     case 1:
               //          // go to StageScene
               //          Program.SetScene(new DungeonResultScene(true));
               //          break;
               //     default:
               //          Console.WriteLine("잘못된 입력입니다.\n");
               //          break;
               //}
          }


          public override void Show()
          {
               Console.WriteLine("던전 입구\n");
               Console.WriteLine("그 누구도 끝까지 도달하지 못한 곳. 이제 당신이 그 전설이 될 차례다.");
               Console.WriteLine("탑의 꼭대기에는 전설의 보스가 잠들어 있다고 한다.");
               Console.WriteLine("한 층, 또 한 층. 당신의 용기만이 길을 밝혀줄 것이다.");

               Console.WriteLine();
               int stage = maxStage;
               while (stage > 0)
               {
                    Console.WriteLine($"{stage}. {stage}층 던전 입장");
                    stage--;
               }
               //Console.WriteLine($"1. 던전 입장 (현재 스테이지: {stage})");
               Console.WriteLine("0. 마을로 돌아가기");
          }
     }
}
