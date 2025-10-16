using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scenes
{
    internal class ActionResultScene : Scene
    {
        Character attacker;
        Character defender;

        int attBeforeHp;
        int defBeforeHp;

        public ActionResultScene(Character attacker, Character defender, int attBeforeHp, int defBeforeHp)
        {
            this.attacker = attacker;
            this.defender = defender;

            this.attBeforeHp = attBeforeHp;
            this.defBeforeHp = defBeforeHp;

        }
        
        public override void Show()
        {
            Console.Clear();

            Console.WriteLine("Battle!!");
            Console.WriteLine();

            Console.WriteLine($"{attacker.Name} 의 공격!");
            Console.WriteLine($"Lv.{defender.Level} {defender.Name} 을(를) 공격하였습니다. [데미지: {attacker.Attack}]");
            Console.WriteLine();

            Console.WriteLine($"Lv.{defender.Level} {defender.Name}");
            Console.WriteLine($"HP : {defBeforeHp} -> {defender.NowHp}");
            Console.WriteLine();

            Console.WriteLine("0. 다음");
        }

        public override void HandleInput(byte input)
        {
            throw new NotImplementedException();
        }
    }
}
