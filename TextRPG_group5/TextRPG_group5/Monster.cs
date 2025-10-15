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
        normal, unique, boss
    }

    internal class Monster : Character
    {
        public MonsterType Type { get; set; }
        public string Msg { get; set; } // 몬스터 공격 메시지 예: 슬라임이 말랑거린다. 고블린이 날카로운 칼날을 휘두른다!
        public int Exp { get; set; } // 처치 시 얻는 경험치
        public int Gold { get; set; } // 처치 시 얻는 골드
        
        public Monster(string name, string msg, MonsterType type, int hp, int atk, int def, double critical, double evasion) : base(name, hp, atk, def)
        {
            Msg = msg;
            Critical = critical;
            Evasion = evasion;
        }
    }

    /* 몬스터 작업 규칙
    internal class 이름 : Monster
    {
        public 이름() : base("name", "msg", type, hp, atk, def, exp, gold, critical, evasion)
        {
            MaxHp += (HP 성장 공식);
            Attack += (Atk 성장 공식);
            Defence += (방어력 성장 공식);
            Exp += (경험치 성장 공식);
            Gold += (골드 증가 공식);
            Critical += (치명타 확률 증가 공식);// 필요한 경우에만
            Evasion += (회피 확률 증가 공식);// 필요한 경우에만
        }
    }
    */

    // 몬스터의 수치는 추후 조정 필요
    // 엑셀 파일을 별도로 만들도록 하겠습니다.

    internal class Slime : Monster
    {
        /*
        슬라임: 튜토리얼 몬스터
        레벨이 비례해 덩치가 커진다(체력과 방어력이 증가)
        하지만 레벨이 올라가도 공격 방식은 크게 달라지지 않는다.
        */
        public Slime(int level) : base("슬라임", "슬라임이 말랑거린다.", MonsterType.normal, 15, 2, 5, 0, 0.1)
        {   
            MaxHp += (int)(Level * 5);          // 레벨에 비례    
            Attack += (int)(Level);             // 레벨에 비례
            Defence += (int)(Level * 2);        // 레벨에 비례
            Exp += (int)(slimeExp);             // 레벨 구간에 따라 결정
            Gold += (int)(Level * 3);           // 슬라임은 돈 잘 안 주는 잡몹
        }

        public int slimeExp
        {
            get
            {
                if (Level <= 5)
                    return Level * 3;
                else if (Level <= 10)
                    return 15 + (Level - 5) * 2;
                else
                    return 25 + Level;
                // 레벨이 높아질수록 슬라임을 잡는 것이 비효율적이게끔 설계
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
        public Goblin(int level) : base("고블린", "고블린이 날카로운 칼날을 휘두른다!", MonsterType.normal, 10, 0, 3, 0.1, 0.2)
        {
            MaxHp += (int)(Level * 3);              // 레벨에 비례    
            Attack += (int)(goblinAttack);          // 레벨에 비례
            Defence += (int)(Level);                // 레벨에 비례
            Exp += (int)(Level * 2 + goblinAttack); // 레벨에 비례 + 공격력만큼의 보너스
            Gold += (int)(10 + goblinAttack);       // 고블린은 그 위험도에 비례해 돈을 가지고 있다.
            // 저레벨의 고블린은 위험하지 않은 잡몹이지만, 고레벨의 고블린은 까다로운 몬스터
        }

        public int goblinAttack
        {
            get
            {
                if (Level <= 5)
                    return Level * 2;
                else if (Level <= 10)
                    return 20 + (Level * 2);
                else if(Level <= 20)
                    return 60 + (Level * 3);
                else
                    return 120 + (Level * 4);
            }
            // 어느 구간에 도달할 때마다 공격력이 크게 증가
        }
    }

    internal class HobGoblin : Monster
    {
        /*
        홉고블린: 고블린의 진화체
        진리를 깨우쳐 고블린보다 더 빠르게 강해진다(고블린에 비해 공격력이 더 크게 증가)
        여전히 신체적 한계를 극복하지는 못했다.(체력과 방어력은 레벨에 비례)
        */
        public HobGoblin(int level) : base("고블린", "고블린이 날카로운 칼날을 휘두른다!", MonsterType.normal, 15, 8, 4, 0.1, 0.2)
        {
            MaxHp += (int)(Level * 3);                  // 레벨에 비례    
            Attack += (int)(hobGoblinAttack);           // 레벨에 비례
            Defence += (int)(Level);                    // 레벨에 비례
            Critical += (hobGoblinAttack*0.001);        // 공격력이 올라갈 수록 치명타 확률도 올라간다
            Exp += (int)(Level * 2 + hobGoblinAttack);  // 레벨에 비례 + 공격력만큼의 보너스
            Gold += (int)(15 + hobGoblinAttack);        // 그 위험도에 비례해 돈을 가지고 있다.
            // 저레벨의 고블린은 위험하지 않은 잡몹이지만, 고레벨의 고블린은 까다로운 몬스터
        }

        public int hobGoblinAttack
        {
            get
            {
                if (Level <= 5)
                    return (int)(Level * 3);
                else if (Level <= 10)
                    return 30 + (Level * 4);
                else if (Level <= 20)
                    return 60 + (int)(Level * 5);
                else
                    return 150 + (Level * 6);
            }
            // 어느 구간에 도달할 때마다 공격력이 크게 증가
        }
    }

    internal class Orc : Monster
    {
        /*
        오크: 초반 구간의 악몽
        우월하고 강인한 유전자로 태생부터 강력하지만, 우둔하여 레벨이 올라도 성장폭은 크지 않다.
        강력한 체력, 공격력, 방어력을 가졌지만, 치명타 확률과 회피율은 낮다.
        */
        public Orc(int level) : base("오크", "오크가 거대한 도끼를 휘두른다!", MonsterType.unique, 250, 70, 50, 0.08, 0.05)
        {
            MaxHp += (int)(Level * 1.5);            // 레벨에 비례    
            Attack += (int)(Level);                 // 레벨에 비례
            Defence += (int)(Level * 1.1);          // 레벨에 비례
            Exp += (int)(150 + Level * 4);          // 기본 많은 경험치, 레벨에 비례해 추가
            Gold += (int)(150 + Level * 2);         // 기본 많은 골드, 레벨에 비례해 추가
        }
        // 초반부에 마주치면 두려운 존재지만, 고레벨 때 마주치면 꿀통 몬스터
    }

    internal class Golem: Monster
    {
        /*
        골렘: 더럽게 단단하지만 느린 탱커
        마법을 이용해 돌로 만들어진 거인. 강력한 체력과 방어력을 가졌지만, 방어력에만 치중한 탓에 공격력은 일정하다.
        느리고 둔해서 치명타를 때릴 확률도, 회피할 일도 없다.
        */
        public Golem(int level) : base("골렘", "골-렘-의-무-거-운 주-먹-이-다-가-온-다!", MonsterType.unique, 100, 30, 100, 0, 0)
        {
            MaxHp += (int)(Level * 20);             // 레벨에 비례    
            Attack += 0;                            // 레벨이 늘어도 공격력은 그대로! 내 이름은 골렘, 돌벽이죠.
            Defence += (int)(Level * 20);           // 레벨에 비례
            Exp += (int)(10 + Level);               // 레벨이 늘어도 경험치를 많이 주지는 않는다.
            Gold += (int)(45 + Level * 6);          // 기본 많은 골드, 레벨에 비례해 추가
        }
        // 잘 안 부서지지만, 마법 생물이다 보니 무력화시키면 돈을 짭잘하게 벌 수 있다.
    }
}
