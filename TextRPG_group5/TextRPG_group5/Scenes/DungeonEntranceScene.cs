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
          private Player player;

          public DungeonEntranceScene(Player player)
          {
               this.player = player;
          }

          public override void HandleInput(byte input)
          {
               if (input == 0)
               {
                    // go to MainScene
                    Program.SetScene(new MainScene(player));
               }
               else if (input > player.ReachedStage)
               {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.ForegroundColor = ConsoleColor.White;
               }
               else
               {
                    StageManager.Instance.SetPlayer(player);
                    // go to BattleScene
                    Battle selectedStage = new Battle(player, StageManager.Instance.CreateMonsters(input), input);
                    Program.SetScene(new BattleScene(selectedStage));
                    Console.WriteLine($"{input}층 던전에 입장했습니다.\n");   // log
               }
          }

          public override void Show()
          {
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine("던전 입구\n");
               Console.ForegroundColor = ConsoleColor.White;

               Console.WriteLine("어둠이 입구를 삼킨다.");
               Thread.Sleep(800);
               Console.WriteLine("이제, 소원을 향한 여정이 다시 시작된다.");
               Thread.Sleep(800);

               Console.WriteLine();
               int stage = player.ReachedStage;
               stage = stage > Program.maxStage ? Program.maxStage : stage; // 최대 층까지 제한

               while (stage > 0)
               {
                    Console.WriteLine($"{stage}. {stage}층 던전 입장");
                    stage--;
               }
               Console.WriteLine("0. 마을로 돌아가기");
          }
     }
}
