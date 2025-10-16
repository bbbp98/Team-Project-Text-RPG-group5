using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scenes
{
     internal class InventoryScene : Scene
     {
          private Player player;

          public InventoryScene(Player player)
          {
               this.player = player;
          }

          public override void HandleInput(byte input)
          {
               throw new NotImplementedException();
          }

          public override void Show()
          {
               throw new NotImplementedException();
          }
     }
}
