using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
    /// <summary>
    /// 직업 간 차별성을 두기위해 직업별 장착가능한 무기나, 방어구를 구분하기 위한 열거형
    /// </summary>
    public enum Class
    {
        Warrior = 1,
        Magician
    }

    /// <summary>
    /// 무기나 방어구의 능력치 초기화 및 동작 추상클래스화
    /// </summary>
    public abstract class EquipItem : Item
    {
        public abstract void YouEquipItem();
    }
}
