using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.ItemManage;

namespace TextRPG_group5.Scenes
{
     internal class CreatePlayerScene : Scene
     {
          enum CheckType
          {
               Name,
               Job,
          }

          Player? player;
          string name = "";
          string job = "";

          public override void HandleInput(byte input)
          {
          }

          public override void Show()
          {
               Console.Clear();

               Naming();
               SelectClass();

               player = new Player(name, job);

               Console.Clear();
               Console.WriteLine($"\"{player.Name}\", {player.Job} (이)가 생성되었습니다!");
               Console.WriteLine("Press any key to continue...");
               Console.ReadKey();

               InitializePlayer();

               Console.Clear();
          }

          private bool Checking(CheckType type)
          {
               while (true)
               {
                    if (type == CheckType.Name)
                         Console.WriteLine($"이름을 {name} 으로 하시겠습니까? Y / N");
                    else if (type == CheckType.Job)
                         Console.WriteLine($"직업을 {job} 으로 하시겠습니까? Y / N");

                    string check = Console.ReadLine() ?? "";
                    check = check.ToLower();
                    if (check == "y")
                         return true;
                    else if (check == "n")
                         return false;
                    else continue;
               }
          }

          private void Naming()
          {
               while (name != null)
               {
                    Console.Clear();
                    Console.WriteLine("이름을 입력해주세요.");
                    Console.Write("입력: ");
                    name = Console.ReadLine() ?? "";

                    if (name == "")
                    {
                         Console.WriteLine("이름을 다시 입력해주세요.");
                         Thread.Sleep(1000);
                         continue;
                    }

                    if (Checking(CheckType.Name))
                         break;
               }
          }

          private void SelectClass()
          {
               while (job != null)
               {
                    Console.Clear();
                    Console.WriteLine("직업을 선택하시오.\n1. 전사\n2. 궁수\n3. 도적\n4. 마법사");
                    Console.Write("입력: ");
                    string input = Console.ReadLine() ?? "";

                    switch (input)
                    {
                         case "1":
                              job = "전사";
                              break;
                         case "2":
                              job = "궁수";
                              break;
                         case "3":
                              job = "도적";
                              break;
                         case "4":
                              job = "마법사";
                              break;
                         default:
                              Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                              Thread.Sleep(1000);
                              continue;
                    }

                    if (Checking(CheckType.Job))
                         break;
               }
          }

          private void InitializePlayer()
          {
               player = new Player(name, job);

               Program.Initialize(player);
          }
     }
}
