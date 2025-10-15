using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
    public abstract class Character
    {
        public string? Name { get; set; }
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public int NowHp { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public double Critical { get; set; }
        public double Evasion { get; set; }

        public bool IsDead => NowHp <= 0;

        public Character() { }

        public Character(string name, int hp, int atk, int def)
        {
            Name = name;
            MaxHp = hp;
            Attack = atk;
            Defence = def;
            Critical = 10.0;
            Evasion = 10.0;
        }

        public void TakeDamage(int dmg, double crit)
        {
            // 회피했다면
            if (TryEvade())
            {
                Console.WriteLine("회피했습니다.");
                return;
            }

            Random random = new Random();
            bool insCritical = random.NextDouble() < crit;

            if (insCritical)
                dmg = (int)(dmg * 1.5f);

            int damage = Math.Max(1, dmg - Defence);
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