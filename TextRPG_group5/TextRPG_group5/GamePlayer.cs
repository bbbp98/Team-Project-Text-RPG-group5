using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.ItemManage;
using TextRPG_group5.QuestManagement;

namespace TextRPG_group5
{
    internal class Player : Character
    {
        public string Job { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int MaxExp { get; set; }
        public int ReachedStage { get; set; }

        public PlayerSkill Skill { get; set; }
        public Inventory Inventory { get; set; }

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
                    Console.WriteLine("전사, 궁수, 도적, 마법사 중 하나를 선택하시오");
                    job = Console.ReadLine();
                }
            }
            Skill = new PlayerSkill(this);
            NowHp = MaxHp;
            NowMp = MaxMp;
            Inventory = new Inventory(this);
            QuestManager.Instance.player = this;
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
                    MaxHp = 60; Attack = 15; Defence = 25; Critical = 0.1; Evasion = 0.10; MaxMp = 30;
                    return true;
                case "궁수":
                    MaxHp = 45; Attack = 20; Defence = 20; Critical = 0.3; Evasion = 0.20; MaxMp = 40;
                    return true;
                case "도적":
                    MaxHp = 40; Attack = 25; Defence = 15; Critical = 0.2; Evasion = 0.25; MaxMp = 50;
                    return true;
                case "마법사":
                    MaxHp = 35; Attack = 10; Defence = 10; Critical = 0.4; Evasion = 0.15; MaxMp = 60;
                    return true;
                default:
                    return false;
            }
        }
        public override void ShowStatus()
        {
            Console.WriteLine("=============Player status==============");
            Console.WriteLine($"이름:             {Name}");
            Console.WriteLine($"직업:             {Job}");
            Console.WriteLine($"레벨:             {Level}");
            Console.WriteLine($"경험치:           {Exp} / {MaxExp}");
            Console.WriteLine($"체력:             {NowHp} / {MaxHp}");
            Console.WriteLine($"마나:             {NowMp} / {MaxMp}");
            Console.WriteLine($"공격력:           {Attack}");
            Console.WriteLine($"방어력:           {Defence}");
            Console.WriteLine($"치명타 확률:      {(int)(Critical * 100)} %");
            Console.WriteLine($"회피 확률:        {(int)(Evasion * 100)} %");
            Console.WriteLine($"소지금:           {Gold} G");
            Console.WriteLine($"도달 스테이지:    [{ReachedStage}]Stage");
            Console.WriteLine("========================================\n");
            if (Skill != null && Skill.skillBook.Any())
            {
                Console.WriteLine("                    *                   \n");
                Console.WriteLine("==============Player Skill==============\n");
                foreach (var skill in Skill.skillBook)
                {
                    string description = GetSkillDescription(skill.Name);
                    Console.WriteLine($"- {skill.Name} (MP {skill.MpCost})");
                    Console.WriteLine($"  ▶ {description}");
                }
                Console.WriteLine("\n========================================");
            }
        }
        private string GetSkillDescription(string skillName)
        {
            return skillName switch
            {
                "파워 슬래시" => "강력한 베기로 2배의 피해를 입힌다.",
                "파워 스트라이크" => "적을 강타해 3배 피해 및 1턴 스턴 부여.",
                "헤드 샷" => "정확한 조준으로 3배 피해를 입힌다.",
                "포이즌 애로우" => "3배 피해 및 3턴 중독 부여.",
                "더블 스텝" => "2회 연속 공격을 가한다.",
                "블러드 스텝" => "3배 피해 및 3턴 출혈 부여.",
                "파이어 볼" => "4배의 마법 피해를 준다.",
                "프리징 브레스" => "4배 피해 및 3턴 동안 빙결 부여.",
                _ => "스킬 설명이 없습니다."
            };
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
            MaxExp = (int)(MaxExp * 1.05);
            switch (Job)
            {
                case "전사":
                    Attack += 3;
                    Defence += 5;
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
                case "마법사":
                    Attack += 6;
                    Defence += 2;
                    MaxHp += 5;
                    MaxMp += 20;
                    break;
            }
            NowHp = MaxHp; NowMp = MaxMp;
            Console.WriteLine($"레벨 업 하였습니다.\n현재 레벨: {Level} 입니다.\n");
        }
        public PlayerItem Equipment { get; set; }
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

