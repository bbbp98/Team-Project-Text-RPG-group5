using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
     internal abstract class Character
     {
          public string Name { get; set; }  
          public int Level { get; set; }  
          public int MaxHp { get; set; }  
          public int NowHp { get; set; }  
          public int Attack { get; set; }  
          public int Defence { get; set; }

          public bool IsDead => NowHp <= 0;

          public Character(string name, int hp, int atk, int def)
          {
               Name = name;
               MaxHp = hp;
               Attack = atk;
               Defence = def;
          }

          public void TakeDamage(int dmg)
          {
               int damage = Math.Max(1, dmg - Defence);
               NowHp -= damage;
               if (NowHp < 0)
                    NowHp = 0;
          }

          public virtual void ShowStatus()
          {
               Console.WriteLine("ShowStat");
          }
     }
}
