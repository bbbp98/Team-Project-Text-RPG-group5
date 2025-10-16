using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Managers
{
     internal class StageManager
     {
          static private StageManager? _instance;

          // singleton
          public static StageManager Instance
          {
               get
               {
                    if (_instance == null)
                         _instance = new StageManager();
                    return _instance;
               }
          }

          private Player? player;

          public void SetPlayer(Player player)
          {
               this.player = player;
          }

          public List<Monster> CreateMonsters(int stage)
          {
               List<Monster> monsters = new List<Monster>();

               // 이런 방식으로 생각 중입니다. or switch(더 촘촘하게 하려면)
               if (stage <= 2)
               {
                    // 슬라임1, 고블린1
                    monsters.Add(new Slime(stage));
                    monsters.Add(new Goblin(stage));
               }
               else if (stage <= 4)
               {
                    // 고블린1, 홉 고블린1, 오크1
                    monsters.Add(new Goblin(stage));
                    monsters.Add(new HobGoblin(stage));
                    monsters.Add(new Orc(stage));
               }
               else if (stage <= 6)
               {
                    monsters.Add(new Golem(stage));
                    monsters.Add(new Skeleton(stage));
                    monsters.Add(new ShadowAssassin(stage));
               }
               else if (stage <= 8)
               {
                    double rand = new Random().NextDouble();
                    if (rand < 0.05)
                         monsters.Add(new PresentBox(stage));
                    monsters.Add(new Golem(stage));
                    monsters.Add(new Jester(stage));
                    monsters.Add(new Jester(stage));
               }
               else if (stage == 9)
               {
                    monsters.Add(new Dople(player!));
                    monsters.Add(new Slime(stage));
                    monsters.Add(new Slime(stage));
               }
               else if (stage == 10)
               {
                    // boss
                    monsters.Add(new Dragon(stage));
               }

               return monsters;
          }
     }
}
