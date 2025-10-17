using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scenes
{
    internal class NewTitleScene : Scene
    {
        Player player;
        Load.LoadData load = new Load.LoadData();
        public override void HandleInput(byte input)
        {
            switch (input)
            {
                case 1:
                    // 캐릭터 생성 씬
                    Program.SetScene(new CreatePlayerScene());
                    break;
                case 2:
                    // 캐릭터 로드
                    player = load.Load();
                    Program.SetScene(new MainScene(player));
                    break;
            }
        }

        public override void Show()
        {
            Console.Clear();
            Console.WriteLine("Game Start");
            Console.WriteLine("1.New Game");
            Console.WriteLine("2.Continue");
            Console.WriteLine();
            Console.Write("입력: ");

            byte input = byte.TryParse(Console.ReadLine(), out byte val) ? val : byte.MaxValue;
            HandleInput(input);
        }
    }
}
