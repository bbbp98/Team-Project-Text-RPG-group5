using TextRPG_group5.Scene;

namespace TextRPG_group5
{
     internal class Program
     {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello, World!");

            Character player = new Character("Chad", 100, 10, 10, 1);

            Character mon1 = new Character("미니언", 15, 5, 5, 2);
            Character mon2 = new Character("대포미니언", 25, 5, 5, 5);
            Character mon3 = new Character("공허충", 10, 5, 5, 3);

            BattleScene bts = new BattleScene(player, mon1, mon2, mon3);

            while (true)
            {
                bts.Show();
                Console.WriteLine();

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                byte input;
                bool parseResult = byte.TryParse(Console.ReadLine(), out input);
                bts.HandleInput(input);
            }
        }
     }
}
