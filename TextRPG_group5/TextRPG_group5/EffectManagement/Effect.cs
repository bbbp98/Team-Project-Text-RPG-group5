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
        
        protected Character Caster;                     // 효과를 건 시전자

        public Effect(Character caster, int duration)
        {
            this.Caster = caster;
            Duration = duration;
        }

        public virtual void OnTurnStart(Character target) { }
        // 턴 시작 시 발동하는 효과 (독, 화상 등). 아무 효과가 없으면 빈 메서드

        public virtual int GetAttackModifier() { return 0; }
        public virtual int GetDefenceModifier() { return 0; }
        public virtual double GetCriticalModifier() { return 0.0; }
        public virtual double GetEvasionModifier() { return 0.0; }
        // 각 스탯에 대한 보정 값을 반환하는 메서드들.
        // 기본적으로는 0을 반환. 버프/디버프 효과에서 이 메서드를 재정의(override)하여 사용.

    }
}
