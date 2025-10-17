using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.EffectManagement;

namespace TextRPG_group5
{
     internal abstract class Character
     {
          public string? Name { get; set; }
          public int Level { get; set; }
          public virtual int MaxHp { get; set; }
          public virtual int NowHp { get; set; }
          public virtual int Attack { get; set; }
          public virtual int Defence { get; set; }
          public virtual double Critical { get; set; }
          public virtual double Evasion { get; set; }
          public virtual int MaxMp { get; set; }
          public virtual int NowMp { get; set; }

          public List<Effect> effects { get; private set; }

          public bool IsDead => NowHp <= 0;

          public Character() { }

          public Character(string name, int hp, int atk, int def)
          {
               Name = name;
               MaxHp = hp;
               NowHp = MaxHp;
               Attack = atk;
               Defence = def;
               Critical = 0.1;
               Evasion = 0.1;
               effects = new List<Effect>();
          }

          // dmg: 플레이어, 몬스터의 공격력
          // crit: 
          public void TakeDamage(int dmg, double crit)
          {
               // 회피했다면
               if (TryEvade())
               {
                    Console.WriteLine("회피했습니다.");
                    return;
               }

               Random random = new Random();
               bool isCritical = random.NextDouble() < crit;

               // 크리티컬이면 
               if (isCritical)
                    dmg = (int)(dmg * 1.5f);

               int damage = Math.Max(1, dmg - Defence); // dmg - Defence가 음수라면 최소 데미지 1
               NowHp -= damage;
               if (NowHp < 0)
                    NowHp = 0;
          }

          public bool TryEvade()
          {
               Random random = new Random();
               return random.NextDouble() < Evasion;
          }

          public virtual void ShowStatus()
          {
               Console.WriteLine($"Lv.{Level} {Name} HP: {NowHp}/{MaxHp}");
          }

          public bool IsStun => effects.Any(e => e.Type == EffectType.Stun);

          public void ApplyEffect(Effect effect) // 효과를 적용하는 메서드
          {
               effects.Add(effect);                                                            // 동일 효과도 중첩 적용 가능
               Console.WriteLine($"{Name}에게 {effect.Type} 효과가 적용됩니다!");              // 효과 적용 메시지 출력
          }

          public void UpdateEffect() // 턴마다 효과를 처리하고 지속시간을 관리하는 메서드
          {
               if (effects.Count == 0) return;

               for (int i = effects.Count - 1; i >= 0; i--)
               {
                    var effect = effects[i];
                    effect.OnTurnStart(this);   // 턴마다 효과가 적용되는 효과 로직을 실행하라고 지시.
                    effect.Duration--;          // 지속시간을 1 감소시키고, 0이 되면 제거.
                    if (effect.Duration <= 0)
                    {
                         Console.WriteLine($"{Name}의 {effect.Type} 효과가 사라졌습니다.");
                         effects.RemoveAt(i);
                    }
               }
          }

          // 모든 버프/디버프가 적용된 최종 스탯을 계산하는 메서드들
          public int GetFinalAttack()
          {
               int modifier = effects.Sum(e => e.GetAttackModifier());
               return Attack + modifier;
               //return Attack; // 아직 GetAttackModifier가 없으므로 임시로 기본값 반환
          }

          public int GetFinalDefence()
          {
               int modifier = effects.Sum(e => e.GetDefenceModifier());
               return Defence + modifier;
               //return Defence; // 아직 GetDefenceModifier가 없으므로 임시로 기본값 반환
          }

          public double GetFinalCritical()
          {
               double modifier = effects.Sum(e => e.GetCriticalModifier());
               return Critical + modifier;
               //return Critical; // 아직 GetCriticalModifier가 없으므로 임시로 기본값 반환
          }

          public double GetFinalEvasion()
          {
               double modifier = effects.Sum(e => e.GetEvasionModifier());
               return Evasion + modifier;
               //return Evasion; // 아직 GetEvasionModifier가 없으므로 임시로 기본값 반환
          }
     }
}
