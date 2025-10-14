using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scene
{
     internal abstract class Scene
     {
          public abstract void Show();
          public abstract void HandleInput(byte input);
     }
}
