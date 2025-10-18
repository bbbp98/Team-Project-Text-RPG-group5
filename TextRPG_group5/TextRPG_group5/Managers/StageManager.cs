using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Managers
{
     internal class StageManager
     {
          static private StageManager? instance;
          private readonly Dictionary<int, IStageFactory> stageFactories = new();
          // singleton
          public static StageManager Instance
          {
               get
               {
                    if (instance == null)
                         instance = new StageManager();
                    return instance;
               }
          }

          private StageManager()
          {
               RegisterStages();
          }

          private Player? player;

          public void SetPlayer(Player player)
          {
               this.player = player;
          }

          public List<Monster> CreateMonsters(int currentStage)
          {
               if (stageFactories.TryGetValue(currentStage, out IStageFactory? factory))
               {
                    return factory.Create(player!, currentStage);
               }

               return new List<Monster>();
          }

          private void RegisterStages()
          {
               stageFactories.Add(1, new Stage1Factory());
               stageFactories.Add(2, new Stage2Factory());
               stageFactories.Add(3, new Stage3Factory());
               stageFactories.Add(4, new Stage4Factory());
               stageFactories.Add(5, new Stage5Factory());
               stageFactories.Add(6, new Stage6Factory());
               stageFactories.Add(7, new Stage7Factory());
               stageFactories.Add(8, new Stage8Factory());
               stageFactories.Add(9, new Stage9Factory());
               stageFactories.Add(10, new Stage10Factory());
               stageFactories.Add(11, new Stage11Factory());
               stageFactories.Add(12, new Stage12Factory());
               stageFactories.Add(13, new Stage13Factory());
               stageFactories.Add(14, new Stage14Factory());
               stageFactories.Add(15, new Stage15Factory());
          }
     }
}
