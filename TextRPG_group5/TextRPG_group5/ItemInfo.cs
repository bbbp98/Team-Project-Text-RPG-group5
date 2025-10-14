using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{


    public static class ItemInfo
    {

        // 무기, 방어구의 전체아이템을 딕셔너리에 보존
        private static Dictionary<string, EquipItem> EquipMentDB = new Dictionary<string, EquipItem>()
        {
            {"목검", new Weapon("목검", " ", 5, 5, 100, 1)},
            {"강철검", new Weapon("강철검", " ", 10, 10, 300, 1)},
            {"BF대검", new Weapon("BF대검", " ", 30, 25, 1500, 1)},
            {"워해머", new Weapon("워해머", " ", 20, 15, 700, 1)},
            {"나무스태프", new Weapon("나무스태프", " ", 5, 5, 100, 2)},
            {"철제스태프", new Weapon("철제스태프", " ", 10, 15, 300, 2)},
            {"아크스태프", new Weapon("아크스태프", "", 30, 35, 1500, 2)},
            {"스페셜완드", new Weapon("스페셜완드", "", 20, 25, 700, 2)},
            {"가죽갑옷", new Armor("가죽갑옷", " ", 15, 300, 1)},
            {"강철갑옷", new Armor("강철갑옷", " ", 30, 700, 1)},
            {"체인메일", new Armor("체인메일", " ", 45, 1800, 1)},
            {"수련생의 로브", new Armor("수련생의 로브", " ", 10, 200, 2)},
            {"숙련자의 로브", new Armor("숙련자의 로브", " ", 20, 400, 2)},
            {"달인의 로브", new Armor("달인의 로브", " ", 30, 2000, 1)},
        };

        // 모든 소비아이템 정보를 딕셔너리에 보존
        private static Dictionary<string, UsableItem> UsableDB = new Dictionary<string, UsableItem>()
        {
            { "HP소형포션", new Potion("HP소형포션", "", 50, 50)},
            { "HP대형포션", new Potion("HP대형포션", "", 300, 150)},
            { "MP소형포션", new Potion("MP소형포션", "", 50, 70)},
            { "MP대형포션", new Potion("MP대형포션", "", 200, 200)},
        };


        // 획득한 아이템의 이름을 매개변수로 받아 딕셔너리의 키를 대조해 그에 해당하는 아이템 오브젝트를 반환
        // 인벤토리의 아이템을 출력할 때, 아이템의 이름만 매개변수로 받는 로직을 반복문을 통해 반복해주면 출력이 용이
        public static Item GetItem(string name)
        {
            if(EquipMentDB.ContainsKey(name))
            {
                return EquipMentDB[name];
            }
            else if(UsableDB.ContainsKey(name))
            {
                return UsableDB[name];
            }
            else
            {
                return null;
            }
        }
    }
}
