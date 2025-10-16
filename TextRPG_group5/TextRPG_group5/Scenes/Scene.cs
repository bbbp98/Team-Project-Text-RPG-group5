using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scenes
{
     internal abstract class Scene
     {
          // 입력 값 처리 메서드
          public abstract void Show();

          // 화면에 보여줄 텍스트들(Console.Write관련)
          public abstract void HandleInput(byte input);
     }
}
