using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.Managers;
using static TextRPG_group5.Managers.GameProgress;

namespace TextRPG_group5.Scenes
{
     internal class MainScene : Scene
     {
          const string welcomMessage = "스파르타 마을에 오신 여러분 환영합니다.\n" +
               "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n";
          GameProgress gameProgress = new GameProgress();

          public override void HandleInput(byte input)
          {
               switch (input)
               {
                    case 0:
                         Console.WriteLine("게임을 종료합니다.");
                         Exit.Show(gameProgress);
                         break;
                    case 1:
                         Console.WriteLine("캐릭터 정보를 확인합니다.");
                         break;
                    case 2:
                         Console.WriteLine("인벤토리를 확인합니다.");
                         break;
                    case 3:
                         Console.WriteLine("퀘스트를 확인합니다.");
                         break;
                    case 4:
                         Program.SetScene(new DungeonEntranceScene());
                         break;
                    case 5:
                         Console.WriteLine("게임을 저장합니다.");
                         break;
               }
          }

          public override void Show()
          {
               Console.Clear();
               Console.WriteLine("마을");
               Console.WriteLine();
               
               Console.WriteLine(welcomMessage);
               Console.WriteLine("1. 캐릭터 정보 확인");
               Console.WriteLine("2. 인벤토리 확인");
               Console.WriteLine("3. 퀘스트 확인");
               Console.WriteLine("4. 던전 탐험");
               Console.WriteLine("5. 저장하기");
               Console.WriteLine("0. 게임 종료");
          }
     }
}
