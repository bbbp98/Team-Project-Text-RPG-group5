using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
    /// <summary>
    /// 1회성 소모품의 추상클래스
    /// </summary>
    public abstract class UsableItem : Item
    {
        // 추상클래스 선언
        public abstract void UseItem();
    }
}
