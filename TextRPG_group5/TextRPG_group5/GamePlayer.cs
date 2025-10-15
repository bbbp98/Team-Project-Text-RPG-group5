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
            public new double Critical { get; private set; }
            public new double Evasion { get; private set; }
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
                        Attack = 30; Defence = 50; MaxHp = 120; MaxMp = 60; MagicPower = 10; Critical = 0.1; Evasion = 0.1;
                        return true;
                    case "궁수":
                        Attack = 40; Defence = 30; MaxHp = 90; MaxMp = 80; MagicPower = 30; Critical = 0.4; Evasion = 0.2;
                        return true;
                    case "도적":
                        Attack = 50; Defence = 20; MaxHp = 80; MaxMp = 90; MagicPower = 25; Critical = 0.3; Evasion = 0.3;
                        return true;
                    case "법사":
                        Attack = 20; Defence = 20; MaxHp = 60; MaxMp = 120; MagicPower = 50; Critical = 0.2; Evasion = 0.1;
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
                Console.WriteLine($"치명타 확률: {Critical * 100}");
                Console.WriteLine($"회피확률: {Evasion * 100}");
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
            public PlayerEquip Equipment { get; private set; }
            public void AddCritical(double amount)
            {
                Critical += amount;
            }
            public void AddEvasion(double amount)
            {
                Evasion += amount;
            }
        }
    }
}
