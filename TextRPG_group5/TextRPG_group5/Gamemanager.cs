using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
    internal class Gamemanager
    {
        public Player Player { get; set; }
        public int Stage { get; set; } = 1;
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("Game Start");
            Console.WriteLine("1.New Game");
            Console.WriteLine("2.Continue");
            Console.Write("선택: ");
            string input = Console.ReadLine();
            if (input == "1")
                CreatePlayer();
        }


                private void CreatePlayer()
        {
            Console.Clear();
            Console.Write("이름을 입력하시오:  ");
            string name = Console.ReadLine();
            string job = "";

            while (true)
            {
                Console.Write("직업을 선택하시오 (1.전사 / 2.궁수 / 3.도적 / 4.법사):  ");
                string input = Console.ReadLine();

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
                        job = "법사";
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
                        continue;
                }
                break;
            }

            Player = new Player(name, job);

            Console.Clear();
            Console.WriteLine($"{Player.Name}, {Player.Job}(이)가 생성되었습니다!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }


    }
}

