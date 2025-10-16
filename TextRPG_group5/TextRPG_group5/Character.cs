using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

          public bool IsDead => NowHp <= 0;

          public Character() { }

          public Character(string name, int hp, int atk, int def)
          {
               Name = name;
               MaxHp = hp;
               Attack = atk;
               Defence = def;
               Critical = 0.1;
               Evasion = 0.1;
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
               Console.WriteLine($"Lv.{Level} {Name} HP: {NowHp}");
          }
     }
}
