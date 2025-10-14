using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scenes
{
     internal class Monster: Character
     {
          public Monster(string name, int level)
          {
               Name = name;
               Level = level;
               MaxHp = 100;
               NowHp = MaxHp;
          }
     }

     internal class StageManager
     {
          public List<Monster> CreateMonsters(int stage)
          {
               List<Monster> monsters = new List<Monster>();

               // 이런 방식으로 생각 중입니다.
               if (stage <= 2)
               {
                    // 슬라임1, 고블린1

                    // 슬라임
                    monsters.Add(new Monster("Slime", stage));
                    // 고블린
                    monsters.Add(new Monster("Goblin", stage + 1));
               }
               else if (stage <= 4)
               {
                    // 슬라임2, 고블린2

                    // 슬라임
                    monsters.Add(new Monster("Slime", stage));
                    monsters.Add(new Monster("Slime", stage));
                    // 고블린
                    monsters.Add(new Monster("Goblin", stage + 1));
                    monsters.Add(new Monster("Goblin", stage + 1));
               }

               return monsters;
          }
     }
}
