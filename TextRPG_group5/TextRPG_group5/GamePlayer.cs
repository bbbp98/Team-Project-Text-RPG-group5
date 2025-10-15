using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
    internal class GamePlayer
    {
        internal class Player : Character
        {
            public string Job { get; private set; }
            public new int Level { get; private set; } = 1;
            public int MaxMp { get; private set; }
            public int NowMp { get; private set; }
            public int MagicPower { get; private set; }
            public int Criticalrate { get; private set; }
            public int Evasionrate { get; private set; }
            public int Gold { get; private set; }
            public int Exp { get; private set; }
            public int MaxExp { get; private set; }

            public Player(string name, string job)
                : base(name, 0, 0, 0)
            {
                Gold = 500;
                Exp = 0;
                MaxExp = 100;
                while (true)
                {
                    if (StatbyJob(job))
                    {
                        Job = job;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("전사, 궁수, 도적, 법사 중 하나를 선택하시오");
                        job = Console.ReadLine();
                    }
                }
                NowHp = MaxHp;
                NowMp = MaxMp;
            }

            private bool StatbyJob(string job)
            {
                switch (job)
                {
                    case "전사":
                        Attack = 30; Defence = 50; MaxHp = 120; MaxMp = 60; MagicPower = 10; Criticalrate = 10; Evasionrate = 5;
                        return true;
                    case "궁수":
                        Attack = 40; Defence = 30; MaxHp = 90; MaxMp = 80; MagicPower = 30; Criticalrate = 40; Evasionrate = 20;
                        return true;
                    case "도적":
                        Attack = 50; Defence = 20; MaxHp = 80; MaxMp = 90; MagicPower = 25; Criticalrate = 30; Evasionrate = 25;
                        return true;
                    case "법사":
                        Attack = 20; Defence = 20; MaxHp = 60; MaxMp = 120; MagicPower = 50; Criticalrate = 15; Evasionrate = 10;
                        return true;
                    default:
                        return false;
                }
            }
            public override void ShowStatus()
            {
                Console.WriteLine("===========Player status===========");
                Console.WriteLine($"이름: {Name}");
                Console.WriteLine($"직업: {Job}");
                Console.WriteLine($"레벨: {Level}");
                Console.WriteLine($"경험치: {Exp}/{MaxExp}");
                Console.WriteLine($"체력: {NowHp}/{MaxHp}");
                Console.WriteLine($"마나: {NowMp}/{MaxMp}");
                Console.WriteLine($"공격력: {Attack}");
                Console.WriteLine($"방어력: {Defence}");
                Console.WriteLine($"마력: {MagicPower}");
                Console.WriteLine($"치명타 확률: {Criticalrate}");
                Console.WriteLine($"회피확률: {Evasionrate}");
                Console.WriteLine($"소지금: {Gold}");
                Console.WriteLine("===================================\n");
            }
            public void GainExp(int amount)
            {
                Exp += amount;
                Console.WriteLine($"경험치 {amount} 획득.");
                while(Exp >= MaxExp)
                {
                    Exp -= MaxExp;
                    LevelUp();
                }
            }
            public void LevelUp()
            {
                Level++;
                MaxExp = (int)(MaxExp * 1.2);
                Attack += 2; Defence += 2; MagicPower += 1; MaxHp += 10; MaxMp += 5;
                NowHp = MaxHp; NowMp = MaxMp;
                Console.WriteLine($"레벨 업 하였습니다.\n 현재 레벨 : {Level} 입니다.");
            }
        }
    }
}
