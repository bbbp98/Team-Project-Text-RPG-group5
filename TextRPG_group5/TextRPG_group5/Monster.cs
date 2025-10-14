using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
    enum MonsterType
    {
        normal, boss
    }

    internal class Monster : Character
    {
        public MonsterType Type { get; set; }
        public string Msg { get; set; } // 몬스터 공격 메시지
        public int Exp { get; set; } // 처치 시 얻는 경험치 
        public int Gold { get; set; } // 처치 시 얻는 골드
        
        public Monster(string name, string msg, MonsterType type, int hp, int atk, int def, int exp, int gold) : base(name, hp, atk, def)
        {
            Msg = msg; 
            Exp = exp;
            Gold = gold;
        }
    }

    /* 몬스터 작업 규칙
    internal class 이름 : Monster
    {
        public 이름() : base("name", "msg", type, hp, atk, def, exp, gold)
        {
            MaxHp += (HP 성장 공식);
            Attack += (Atk 성장 공식);
            Defence += (방어력 성장 공식);
            Exp += (경험치 성장 공식);
            Gold += (골드 증가 공식);   
        }
    }
    */

    internal class Slime : Monster
    {
        /*
        슬라임: 튜토리얼 몬스터
        레벨이 비례해 덩치가 커진다(체력과 방어력이 증가)
        하지만 레벨이 올라가도 공격 방식은 크게 달라지지 않는다.
        */
        public Slime(int level) : base("슬라임", "슬라임이 말랑거린다.", MonsterType.normal, 3, 5, 7, 9, 11)
        {   
            MaxHp += (int)(Level * 5);      // 레벨이 올라가면 덩치가 커진다는 설정    
            Attack += (int)(Level);         // 레벨에 비례
            Defence += (int)(Level * 2);    // 레벨에 비례
            Exp += (int)(slimeExp);         // 레벨 구간에 따라 결정
            Gold += (int)(Level * 50);      // 슬라임은 돈 잘 안 주는 잡잡몹
        }

        public int slimeExp
        {
            get
            {
                if (Level <= 5)
                    return Level * 5;
                else if (Level <= 10)
                    return 25 + (Level - 5) * 3;
                else
                    return 40 + Level;
            }
        }
    }

    internal class Goblin : Monster
    {
        /*
        고블린: 계단식으로 강해지는 몬스터
        일정 레벨에 도달할 때마다 깨달음을 얻어 강해진다(공격력이 큰 폭으로 증가)
        하지만 신체적 한계를 극복하지는 못했다(체력과 방어력은 레벨에 비례)
        */
        public Goblin(int level) : base("고블린", "고블린이 날카로운 칼날을 휘두른다!", MonsterType.normal, 10, 15, 5, 20, 30)
        {
            MaxHp += (int)(Level * 3);      // 레벨이 올라가면 덩치가 커진다는 설정    
            Attack += (int)(goblinAttack);  // 레벨에 비례
            Defence += (int)(Level);        // 레벨에 비례
            Exp += (int)(goblinExp);        // 레벨 구간에 따라 결정
            Gold += (int)(Level * 100);     // 고블린은 돈 좀 주는 잡몹
        }

        public int goblinAttack
        {
            get
            {
                if (Level <= 5)
                    return Level * 3;
                else if (Level <= 10)
                    return 15 + (Level - 5) * 2;
                else
                    return 25 + Level;
            }
        }

        public int goblinExp
        {
            get
            {
                if (Level <= 5)
                    return Level * 10;
                else if (Level <= 10)
                    return 50 + (Level - 5) * 5;
                else
                    return 75 + Level * 2;
            }
        }
    }
}
