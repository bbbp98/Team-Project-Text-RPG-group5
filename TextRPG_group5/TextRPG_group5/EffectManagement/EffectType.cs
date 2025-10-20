using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.EffectManagement
{
    public enum EffectType  // 효과 타입
    {
        // 긍정적 효과
        AtkUp, DefUp, CriticalUp, EvasionUp,            // 버프 계열(미구현)
        Shield,                                         // 보호막 계열(미구현)

        // 부정적 효과
        AtkDown, DefDown, CriticalDown, EvasionDown,    // 디버프 계열(미구현)
        Burn, Freeze, Poison,                           // 지속 피해 계열
        Stun                                            // 상태이상 계열
    }

    /*
    효과를 메서드 안에 넣는 사용 예시
    public void PoisonSkill(Character caster, Character target) // 플레이어가 몬스터에게 독 스킬을 사용
    {
        // 스킬이 구현하는 다른 메서드
        var poison = new Poison(caster, 3); // 3턴 지속되는 독 효과 생성
        target.ApplyEffect(poison); // 몬스터에게 독 효과 적용
    }
    */

    //----------------------------------------↓ 버프 계열 ↓---------------------------------------- 
    internal class AtkUp : Effect // 공격력을 일정 수치만큼 변화시키는 버프
    {
        public int Value;
        public AtkUp(Character caster, int duration, int value) : base(caster, duration)
        {
            Type = EffectType.AtkUp;
            this.Value = value; // value만큼 공격력 증가
        }

        public override int GetAttackModifier()
        {
            return this.Value;
        }
    }

    internal class DefUp : Effect // 방어력을 일정 수치만큼 증가시키는 버프
    {
        public int Value;
        public DefUp(Character caster, int duration, int value) : base(caster, duration)
        {
            Type = EffectType.DefUp;
            this.Value = value; // value만큼 방어력 증가
        }

        public override int GetDefenceModifier()
        {
            return this.Value;
        }
    }

    internal class CriticalUp : Effect // 크리티컬 확률을 일정 수치만큼 변화시키는 버프
    {
        public double Value;
        public CriticalUp(Character caster, int duration, double value) : base(caster, duration)
        {
            Type = EffectType.CriticalUp;
            this.Value = value; // value만큼 크리티컬 확률 증가
        }

        public override double GetCriticalModifier()
        {
            return this.Value;
        }
    }

    internal class EvasionUp : Effect // 회피 확률을 일정 수치만큼 변화시키는 버프
    {
        public double Value;
        public EvasionUp(Character caster, int duration, double value) : base(caster, duration)
        {
            Type= EffectType.EvasionUp;
            this.Value = value; // value만큼 회피 확률 증가
        }

        public override double GetEvasionModifier()
        {
            return this.Value;
        }
    }


    //----------------------------------------↓ 지속 피해 계열 ↓---------------------------------------- 
    internal class Burn : Effect // 시전자의 시전 당시 마나(NowMp)에 비례하여 지속적인 피해를 입히는 효과
    {
        public double burnRate = 0.1; // 화상 피해 비율
        public int Value;

        public Burn(Character caster, int duration) : base(caster, duration)
        {
            Type = EffectType.Burn;
            this.Value = (int)(7 + caster.NowMp * burnRate);    // 매 턴 7 + 시전자의 시전 당시 마나의 10% 만큼 피해
        }
        public override void OnTurnStart(Character target)
        {
            target.NowHp -= this.Value;
            if (target.NowHp < 0)
                target.NowHp = 0;
            Console.WriteLine($"{target.Name}은(는) 타들어가는 고통을 느낍니다. {Value}의 피해!");
            Thread.Sleep(1000);
        }
    }

    internal class Freeze : Effect // 시전자의 최대 마나(MaxMp)에 비례하여 지속적인 피해를 주고, 일정 확률로 행동 불능 상태에 빠뜨린다.
    {
        private double FreezeRate = 0.1;    // 빙결 피해 비율
        private double freezeChance;        // 행동 불능 확률
        public int Value;

        public Freeze(Character caster, int duration, double chance) : base(caster, duration)
        {
            Type = EffectType.Freeze;
            this.Value = (int)(5 + caster.MaxMp * FreezeRate);    // 매 턴 5 + 시전자의 최대 마나의 10% 만큼 피해
            this.freezeChance = chance; // 예: 0.3은 30% 확률로 행동 불능
        }
        public bool TryFreeze()
        {
            Random random = new Random();
            return random.NextDouble() < freezeChance;
        }
        public override void OnTurnStart(Character target)
        {
            if (TryFreeze())
            {
                Console.WriteLine($"{target.Name}의 온몸이 얼어붙습니다.");
                target.ApplyEffect(new Stun(this.Caster, 1)); // 1턴 동안 행동 불능
            }

            target.NowHp -= this.Value;
            if (target.NowHp < 0)
                target.NowHp = 0;
            Console.WriteLine($"{target.Name}은(는) 온몸을 감싸는 추위를 느낍니다. {Value}의 피해.");
            Thread.Sleep(1000);
        }
    }

    internal class Poison : Effect
    {
        // 시전자의 레벨에 비례하여 지속적인 피해를 입히는 효과
        public double poisonRate = 0.05; // 독 피해 비율
        public int Value;
        public Poison(Character caster, int duration) : base(caster, duration)
        {
            Type = EffectType.Poison;
            this.Value = (int)(3 + caster.Attack * poisonRate);    // 매 턴 3 + 시전자의 공격력 5% 만큼 피해
        }

        public override void OnTurnStart(Character target)
        {
            target.NowHp -= this.Value;
            if (target.NowHp < 0)
                target.NowHp = 0;
            Console.WriteLine($"{target.Name}은(는) 영 속이 좋지 않습니다. {Value}의 피해.");
            Thread.Sleep(1000);
        }
    }

    internal class Stun : Effect // 행동불능 상태(IsStun)를 부여하는 효과
    {
        public Stun(Character caster, int duration) : base(caster, duration)
        {
            Type = EffectType.Stun;
        }
        // stun 효과는 OnTurnStart에서 처리할 필요 없이, Character 클래스의 IsStun 프로퍼티에서 체크(전투 중 체크 바람)
    }
}
