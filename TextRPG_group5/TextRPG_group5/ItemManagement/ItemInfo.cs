using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.ItemManage
{


    public static class ItemInfo
    {

        // 무기, 방어구의 전체아이템을 딕셔너리에 보존
        private static Dictionary<string, EquipItem> EquipMentDB = new Dictionary<string, EquipItem>()
        {
            // 전사 무기
            {"목검", new Weapon("목검", " ", 5, 1, 0.05d, 100, 1, false)},
            {"강철검", new Weapon("강철검", " ", 10, 2, 0.1d, 300, 1, false)},
            {"BF대검", new Weapon("BF대검", " ", 30, 5, 0.25d, 1500, 1, false)},
            {"워해머", new Weapon("워해머", " ", 20, 3, 0.15d, 700, 1, false)},

            // 법사 무기
            {"나무스태프", new Weapon("나무스태프", " ", 1, 5, 0.05d, 100, 2, false)},
            {"철제스태프", new Weapon("철제스태프", " ", 3, 10, 0.15d, 300, 2, false)},
            {"아크스태프", new Weapon("아크스태프", "", 5, 30, 0.35d, 1500, 2, false)},
            {"스페셜완드", new Weapon("스페셜완드", "", 4, 20, 0.25d, 700, 2, false)},

            // 궁수 무기
            {"나무 활", new Weapon("나무 활", " ", 5, 1, 0.2d, 100, 2, false)},
            {"철제 활", new Weapon("철제 활", " ", 10, 2, 0.3d, 300, 2, false)},
            {"에이전트 보우", new Weapon("에이전트 보우", "", 30, 5, 0.5d, 1500, 2, false)},
            {"석궁", new Weapon("석궁", "", 20, 3, 0.4d, 700, 2, false)},

            // 도적 무기
            {"녹슨 단검", new Weapon("녹슨 단검", " ", 5, 1, 0.05d, 100, 2, false)},
            {"강철 단검", new Weapon("강철 단검", " ", 10, 2, 0.15d, 300, 2, false)},
            {"카람빗", new Weapon("카람빗", "", 30, 5, 0.35d, 1500, 2, false)},
            {"쿠나이", new Weapon("쿠나이", "", 20, 3, 0.25d, 700, 2, false)},

            //--------------------------------------------------------------------------//

            // 전사 방어구
            {"가죽갑옷", new Armor("가죽갑옷", " ", 15, 0.1d, 300, 1, false)},
            {"강철갑옷", new Armor("강철갑옷", " ", 30, 0.1d, 700, 1, false)},
            {"체인메일", new Armor("체인메일", " ", 45, 0.1d, 1800, 1, false)},

            // 법사 방어구
            {"수련생의 로브", new Armor("수련생의 로브", " ", 10, 0.1d, 200, 2, false)},
            {"숙련자의 로브", new Armor("숙련자의 로브", " ", 20, 0.15d, 400, 2, false)},
            {"달인의 로브", new Armor("달인의 로브", " ", 30, 0.3d, 2000, 2, false)},

            // 궁수 방어구
            {"가죽 보호구", new Armor("가죽 보호구", " ", 10, 0.2d, 200, 2, false)},
            {"철제 보호구", new Armor("철제 보호구", " ", 20, 0.25d, 400, 2, false)},
            {"특제 보호구", new Armor("특제 보호구", " ", 30, 0.35d, 2000, 2, false)},

            // 도적 방어구
            {"암행복", new Armor("암행복", " ", 10, 0.3d, 200, 2, false)},
            {"잠행복", new Armor("잠행복", " ", 20, 0.4d, 400, 2, false)},
            {"위장복", new Armor("위장복", " ", 30, 0.5d, 2000, 2, false)},

            // 테스트용 장비 (전직업 공용)
            {"테스트용무기", new Weapon("테스트용무기", " ", 999, 999, 1.0d, 0, 5, false)},
            {"테스트용 방어구", new Armor("테스트용 방어구", " ", 3000, 1.0d, 0, 5, false)},
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
        public static ItemManagement GetItem(string name)
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
