using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextRPG_group5
{
    internal class Exit
    {
        public enum ExitAction
        {
            QuitWithoutSaving,
            ReturnToMainMenu
        }

        public async Task<ExitAction> HandleExitAsync()
        {
            Console.WriteLine("1: 게임 종료  ");
            Console.WriteLine("2: 메인 메뉴로 돌아가기");
            Console.Write("옵션을 선택하세요: ");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();


            switch (choice)
            {
                case '1':

                    Console.WriteLine("정말 종료하시겠습니까? (Y/N)");
                    if (Confirm())
                    {
                        Console.WriteLine("게임을 종료합니다.");
                        await Pause();
                        Environment.Exit(0);
                        return ExitAction.QuitWithoutSaving;                        
                    }
                    break;

                case '2':
                    Console.WriteLine("메인 메뉴로 돌아갑니다.");
                    await Pause();
                    return ExitAction.ReturnToMainMenu;

                default:
                    Console.WriteLine("올바른 옵션을 선택하지 않았습니다.");
                    await Pause();
                    break;
            }
            return 0;
        }



        private bool Confirm()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Console.WriteLine();
            return keyInfo.Key == ConsoleKey.Y;
        }

        private async Task Pause()
        {
            await Task.Delay(1000);
        }
    }
}
public class GameProgress
{
    public override string ToString()
    {
        return "현재 게임 진행 상황";
    }
}


