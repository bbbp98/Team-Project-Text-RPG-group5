using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.ItemManage;

namespace TextRPG_group5
{
    internal class Player : Character
    {
        public string Job { get; private set; }
        public int Gold { get; set; }
        public int Exp { get; private set; }
        public int MaxExp { get; private set; }
        public int ReachedStage { get; set; }

        public Inventory Inventory { get; private set; }

        internal Player(string name, string job)
              : base(name, 0, 0, 0)
        {
            Level = 1;
            Critical = 0.1;
            Evasion = 0.2;
            Gold = 500;
            Exp = 0;
            MaxExp = 100;
            Equipment = new PlayerItem(this);
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
            Inventory = new Inventory(this);
        }
        public Player()
        {
            Equipment = new PlayerItem(this);
        }
        public Player Clone()
        {
            return new Player
            {
                Name = this.Name,
                Level = this.Level,
                MaxHp = this.MaxHp,
                NowHp = this.NowHp,
                MaxMp = this.MaxMp,
                NowMp = this.NowMp,
                Attack = this.Attack,
                Defence = this.Defence,
                Gold = this.Gold,
                Exp = this.Exp,
                ReachedStage = this.ReachedStage,
                Critical = this.Critical,
                Evasion = this.Evasion,
            };
        }


        private bool StatbyJob(string job)
        {
            switch (job)
            {
                case "전사":
                    MaxHp = 120; Attack = 30; Defence = 50; Critical = 0.1; Evasion = 0.1; MaxMp = 60;
                    return true;
                case "궁수":
                    MaxHp = 90; Attack = 40; Defence = 30; Critical = 0.4; Evasion = 0.2; MaxMp = 80;
                    return true;
                case "도적":
                    MaxHp = 80; Attack = 50; Defence = 20; Critical = 0.3; Evasion = 0.3; MaxMp = 90;
                    return true;
                case "법사":
                    MaxHp = 60; Attack = 20; Defence = 20; Critical = 0.5; Evasion = 0.1; MaxMp = 120;
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
            Console.WriteLine($"치명타 확률: {Critical * 100}");
            Console.WriteLine($"회피확률: {Evasion * 100}");
            Console.WriteLine($"소지금: {Gold}");
            Console.WriteLine($"도달 스테이지: [{ReachedStage}]Stage");
            Console.WriteLine("===================================\n");
        }
        public void GainExp(int amount)
        {
            Exp += amount;
            Console.WriteLine($"경험치 {amount} 획득.");
            while (Exp >= MaxExp)
            {
                Exp -= MaxExp;
                LevelUp();
            }
        }
        public void LevelUp()
        {
            Level++;
            MaxExp = (int)(MaxExp * 1.2);
            switch (Job)
            {
                case "전사":
                    Attack += 4;
                    Defence += 4;
                    MaxHp += 20;
                    MaxMp += 5;
                    break;
                case "궁수":
                    Attack += 4;
                    Defence += 4;
                    MaxHp += 15;
                    MaxMp += 10;
                    break;
                case "도적":
                    Attack += 5;
                    Defence += 3;
                    MaxHp += 10;
                    MaxMp += 15;
                    break;
                case "법사":
                    Attack += 7;
                    Defence += 2;
                    MaxHp += 5;
                    MaxMp += 20;
                    break;
            }
            NowHp = MaxHp; NowMp = MaxMp;
            Console.WriteLine($"레벨 업 하였습니다.\n 현재 레벨 : {Level} 입니다.");
        }
        public PlayerItem Equipment { get; private set; }
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

