using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG_group5.EffectManagement
{
    internal abstract class Effect // 효과 클래스
    {
        public EffectType Type { get; protected set; }  // 효과 타입
        public int Duration { get; set; }               // 효과 지속 시간 (턴 단위)
        public int Value { get; protected set; }        // 효과 수치 (공격력 +10% 등)
        protected Character Caster;                     // 효과를 건 시전자 (현재 코드 상으로는 무조건 유저)

        public Effect(Character caster, int duration)
        {
            this.Caster = caster;
            Duration = duration;
        }

        public virtual void OnTurnStart(Character target) { }
        // 턴 시작 시 발동하는 효과 (독, 화상 등). 아무 효과가 없으면 빈 메서드

        public virtual int GetAttackModifier() { return 0; }
        public virtual int GetDefenceModifier() { return 0; }
        public virtual double GetCriticalModifier() { return 0; }
        public virtual double GetEvasionModifier() { return 0; }
        // 스탯에 적용되는 효과들. 기본값은 0
    }
}
