# 프로젝트 소개
C#으로 만드는 간단한 텍스트 RPG입니다.

# 개발 기간
2025.10.14 ~ 2025.10.21

# 개발 인원
**6명**
>팀장: 이준혁
<br>팀원: 오민근, 조아라, 임성규, 박범근, 김태환
    
# 역할 분담
- 이준혁
    - 몬스터 클래스 구현
    - 효과 클래스 구현
    - 전투 밸런스 작업
    - 발표 자료(PPT, 시연 영상) 제작 및 발표

- 오민근
    - 아이템 클래스 구현
    - 퀘스트 시스템 구현

- 조아라
    - 전투 시스템 구현

- 임성규
    - 플레이어 클래스 구현
    - 플레이어 생성 로직 구현

- 박범근
    - 구조 설계
    - Git 관리
    - 캐릭터 클래스 구현
    - 던전 구현
    - 인벤토리 구현
    - 상점 구현

- 김태환
    - 게임 종료 로직 구현
    - 세이브/로드 기능 구현

# 와이어프레임
<img width="6914" height="8672" alt="Image" src="https://github.com/user-attachments/assets/82d37809-5fcc-4f93-8d5a-2a5214ea17e5" />

# 구현 목록
0. Main
- 추상 클래스 Scene을 상속받는 여러 scene들에서 입력 값에 대한 동작을 수행합니다. 
- 플레이어의 정보는 각 씬 생성자의 매개변수로 전달되도록 하였습니다.
```cs
while (true)
{
     if (currentScene is NewTitleScene
          || currentScene is CreatePlayerScene)
     { currentScene!.Show(); }
     else
     {
          currentScene!.Show();
          if (isSkipInput)
          {
               isSkipInput = false;
               continue;
          }
          Console.WriteLine();
          Console.WriteLine("원하시는 행동을 입력해주세요.");
          Console.Write(">> ");
          byte input = byte.TryParse(Console.ReadLine(), out byte val) ? val : byte.MaxValue;
          Console.Clear();
          currentScene.HandleInput(input);
     }
}
```

-----------------------------------------------------------------------------

1. Character class
- 플레이어나 몬스터의 부모 클래스 입니다.
- 이름, 레벨, HP, 공격력, 방어력 같은 기본적인 스테이터스를 프로퍼티로 가집니다.
- 오브젝트가 공격 받았을 때의 데미지를 계산하는 역할을 주로 합니다.
```cs
public void TakeDamage(int dmg, double crit, bool isWithSkill = false)
{
     if (!isWithSkill)
     {
          if (TryEvade())
          {
               Console.WriteLine("회피했습니다.");
               return;
          }
     }

     Random random = new Random();
     bool isCritical = random.NextDouble() < crit;

     if (isCritical)
     {
          dmg = (int)(dmg * 1.5f);
          Console.WriteLine("치명타!");
     }

     int finalDefence = GetFinalDefence();
     int damage = Math.Max(1, dmg - finalDefence);

     NowHp -= damage;
     if (NowHp < 0)
          NowHp = 0;
}

public bool TryEvade()
{
     Random random = new Random();
     return random.NextDouble() < GetFinalEvasion();
}
```
>[Character.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Character.cs)

----------------

2. Monster class
- Character 클래스를 상속받는 하위 클래스입니다.
- Character에서 상속한 변수 외에 공격 메시지, 처시 시 경험치, 골드를 추가로 가집니다.
- 모든 몬스터들은 Monster class의 자식 클래스로, 각각 컨셉을 가지고, 그에 맞춰 다른 성장 공식을 가지고 있습니다.

```cs
internal class Monster : Character
{
    public MonsterType Type { get; set; }
    public string Msg { get; set; } // 몬스터 공격 메시지 예: 슬라임이 말랑거린다. 고블린이 날카로운 칼날을 휘두른다!
    public int Exp { get; set; } // 처치 시 얻는 경험치
    public int Gold { get; set; } // 처치 시 얻는 골드

    public int expEvent = 50; // 원활한 테스트를 위해 경험치 10배로 상승
    public Monster(string name, string msg, MonsterType type, int hp, int atk, int def, double critical, double evasion) : base(name, hp, atk, def)
    {
        Msg = msg;
        Critical = critical;
        Evasion = evasion;
    }
}

```


```cs
/* 몬스터 작업 규칙
internal class 이름 : Monster
{
    public 이름() : base("name", "msg", type, hp, atk, def, exp, gold, critical, evasion)
    {
        MaxHp += (HP 성장 공식);
        NowHp = MaxHp;
        Attack += (Atk 성장 공식);
        Defence += (방어력 성장 공식);
        Exp += (경험치 성장 공식);
        Gold += (골드 증가 공식);
        Critical += (치명타 확률 증가 공식);// 필요한 경우에만
        Evasion += (회피 확률 증가 공식);// 필요한 경우에만
    }
}
*/
```
- 깜짝상자: 낮은 확률로 던전에서 마주칠 수 있는 많은 골드를 주는 럭키 몬스터
- 도플갱어: 플레이어의 능력치를 80%로 복사하는 몬스터. 플레이어의 방어력과 공격력의 차이가 클 수록 위험한 몬스터
- 드래곤: 매우 높은 스테이터스를 가진 최종 보스 몬스터
- 그 외에도 특색있는 몬스터들을 구현하였습니다.
>[Monster.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Monster.cs)

---------------------

3. 전투 효과
- 플레이어에게 적용되는 효과(버프, 디버프, 지속 피해(DoT), 상태이상)을 담당합니다.
```cs
internal abstract class Effect // 효과 클래스
{
    public EffectType Type { get; protected set; }  // 효과 타입
    public int Duration { get; set; }               // 효과 지속 시간 (턴 단위)
    
    protected Character Caster;                     // 효과를 건 시전자

    public Effect(Character caster, int duration)
    {
        this.Caster = caster;
        Duration = duration;
    }

    public virtual void OnTurnStart(Character target) { }
    // 턴 시작 시 발동하는 효과 (독, 화상 등). 아무 효과가 없으면 빈 메서드

    public virtual int GetAttackModifier() { return 0; }
    public virtual int GetDefenceModifier() { return 0; }
    public virtual double GetCriticalModifier() { return 0.0; }
    public virtual double GetEvasionModifier() { return 0.0; }
    // 각 스탯에 대한 보정 값을 반환하는 메서드들.
    // 기본적으로는 0을 반환. 버프/디버프 효과에서 이 메서드를 재정의(override)하여 사용.

```
- 각 효과는 시전자의 정보와 지속 시간(턴)을 매개 변수로 받습니다.


```cs
public enum EffectType  // 효과 타입
{
    // 긍정적 효과
    AtkUp, DefUp, CriticalUp, EvasionUp,            // 버프 계열(미구현)
    Shield,                                         // 보호막 계열(미구현)

    // 부정적 효과
    AtkDown, DefDown, CriticalDown, EvasionDown,    // 디버프 계열(미구현)
    Burn, Freeze, Poison,                           // 지속 피해 계열
    Stun                                            // 상태이상 계열
}
```
효과 타입은 enum을 통해 구분하고 있습니다.

```cs
public bool IsStun => effects.Any(e => e.Type == EffectType.Stun);
```

- '기절'의 경우, 부모 클래스인 Character에서 체크합니다.
- 전투 시 턴이 시작되었을 때 '기절' 상태라면 그 객체는 행동할 수 없습니다.

```cs
public void ApplyEffect(Effect effect) // 효과를 적용하는 메서드
{
     effects.Add(effect);                                                            // 동일 효과도 중첩 적용 가능
     Console.WriteLine($"{Name}에게 {effect.Type} 효과가 적용됩니다!");              // 효과 적용 메시지 출력
}

public void UpdateEffect() // 턴마다 효과를 처리하고 지속시간을 관리하는 메서드
{
     if (effects.Count == 0) return;

     for (int i = effects.Count - 1; i >= 0; i--)
     {
          var effect = effects[i];
          effect.OnTurnStart(this);   // 턴마다 효과가 적용되는 효과 로직을 실행하라고 지시.
          effect.Duration--;          // 지속시간을 1 감소시키고, 0이 되면 제거.
          if (effect.Duration <= 0)
          {
               Console.WriteLine($"{Name}의 {effect.Type} 효과가 사라졌습니다.");
               effects.RemoveAt(i);
          }
     }
}
```
- Character의 ApplyEffect 메서드와 UpdateEffect 메서드를 통해
객체에게 적용되어 있는 효과를 관리합니다.
>[Effect.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/EffectManagement/Effect.cs)
<br>[EffectType.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/EffectManagement/EffectType.cs)
-----------------

4. 던전
- 던전은 1층부터 15층까지로, 플레이어가 도달한 층까지의 목록을 보여주고, 입장할 수 있습니다.
- 던전의 클리어 여부에 따라서 화면을 출력하고, 보상을 지급합니다.

>[DungeonEntranceScene.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Scenes/DungeonEntranceScene.cs)
<br>[DungeonResultScene.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Scenes/DungeonResultScene.cs)

-----------------

5. 스테이지 구성
- 각 스테이지의 몬스터 정보는 StageManager를 통해서 전달됩니다.
- StageManager에서는 몬스터를 생성해주는 StageFactory를 통해 몬스터의 List를 반환합니다.

```cs
public List<Monster> CreateMonsters(int currentStage)
{
     if (stageFactories.TryGetValue(currentStage, out IStageFactory? factory))
     {
          return factory.Create(player!, currentStage);
     }

     return new List<Monster>();
}
```

>[StageManager.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Managers/StageManager.cs)
<br>[StageFactories.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/StageFactories.cs)
<br>[IStageFactory.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/IStageFactory.cs)

----------------------------------

6. 전투
   
   <img width="719" height="227" alt="image" src="https://github.com/user-attachments/assets/a5e118b5-ac3a-4ef7-a3a6-305cb351215f" />

- Battle 클래스 (`Battle.cs`)
  - 현재 전투에 대한 전반적인 데이터와 로직을 담고 있는 클래스입니다.
  - 생성 시, 현재 플레이어(`player`), 선택된 스테이지(`currentStage`), 해당 스테이지 출현 몬스터 목록(`monsters`) 을 필요로 합니다.
    <br>
    ```cs
    internal class Battle
    {
        ...
        
        public Battle(Player player, List<Monster> monsters, byte currentStage)
        {
            Player = player;
            Monsters = monsters;
            CurrentStage = currentStage;    // 플레이어어가 선택한 스테이지
        
            // Player 선공
            isPlayerTurn = true;
        
            // 초기 상태 설정
            CurrentState = BattleState.None;
        
            // 던전 클리어 시, Player 능력치 전후 비교를 위한 복제본 생성
            PreBattlePlayer = Player.Clone();
        }
        ...
    }
        
    ```
  - 일반 공격 (`HitNormalAttack()`), 스킬 사용 (`UseSkill()`), 아이템 사용 (`UseItem()`) 등의 전투 행동별 로직이 구현되어 있습니다.
    - 전투 행동과 턴에 맞게 `Attacker` 와 `Defender` 가 설정되어 `ActionResultScene` 을 호출합니다.

- BattleScene 클래스 (`BattleScene.cs`)
  - 추상 클래스 Scene 을 상속받아, 메인 전투 씬을 구성하는 클래스입니다.
  - 호출 시, 현재 전투 정보 (`Battle` 객체) 를 필요로 합니다.
    ```cs
    internal class BattleScene : Scene
    {
        public Battle CurrentBattle { get; private set; }
    
        public Player Player { get { return CurrentBattle.Player; } }
        public List<Monster> Monsters { get { return CurrentBattle.Monsters; } }
    
        public List<UsableItem> UsableItemList { get { return Player.Inventory.GetUsableItems(); } }
    
        /* 효과가 처리되었는지 여부 확인용 */
        public bool effectsProcessed = false;
    
        public BattleScene(Battle currentBattle)
        {
            CurrentBattle = currentBattle;
        }
        ...
    }
    ```
  - `HandleInput()`
    - `Battle` 의 `CurrentState` 와 다양한 `Choice` 변수들을 활용해, 전투 상황별 플레이어의 입력을 분기하여 처리합니다.
  - `Show()`
    - 플레이어의 전투 행동 선택에 따라 부분적으로 출력이 달라지도록 구현되어 있습니다.
    - 플레이어의 턴이 끝나면, 자동으로 몬스터의 턴으로 넘어가도록 구현되어 있습니다.
  - 각각의 출력을 메서드로 분리하여, 유지보수성을 높였습니다.
- ActionResultScene 클래스 (`ActionResultScene.cs`)
  - 추상 클래스 Scene 을 상속받아, 플레이어가 선택한 전투 행동별 결과 출력 씬을 구성하는 클래스입니다.
  - 전투 상황별 호출되는 생성자를 분리하여, 현재 전투 정보 (`Battle` 객체) 를 비롯한, 각각의 필요한 데이터를 요구합니다.
    ```cs
    internal class ActionResultScene : Scene
    {
        public Character Attacker { get; private set; }
        public List<Character> Defenders { get; private set; }
    
        public int AttBeforeHp { get; private set; }
        public int AttBeforeMp { get; private set; }
        public List<int> DefBeforeHp { get; private set; }
    
        public Battle CurrentBattle { get; private set; }
    
        private Potion selectedItem;
    
        public ActionResultScene(Battle current, Character att, List<Character> defs, int attBeforeMp, List<int> defBeforeHp)
        {
            // 일반 공격, 스킬 공격용 ActionResultScene 생성자
            Attacker = att;
            Defenders = defs;
    
            AttBeforeMp = attBeforeMp;
            DefBeforeHp = defBeforeHp;
    
            CurrentBattle = current;
        }
    
        public ActionResultScene(Battle current, int playerBeforeHp, int playerBeforeMp, UsableItem selectedItem)
        {
            // 아이템 사용용 ActionResultScene 생성자
            CurrentBattle = current;
            Attacker = CurrentBattle.Player;
            AttBeforeHp = playerBeforeHp;
            AttBeforeMp = playerBeforeMp;
    
            this.selectedItem = (Potion)selectedItem;
        }
    }
    ```
  - `HandleInput()`
    - 다시 `BattleScene` 으로 돌아가기 전, 필요한 변수들을 초기화 합니다.
  - `Show()`
    - 지정된 `Attacker` 와 `Defender` 의 정보를 전투 행동에 따라 출력하도록 구형되어 있습니다. 
>[Battle.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Battle.cs)
<br>[BattleScene.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Scenes/BattleScene.cs)
<br>[ActionResultScene.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Scenes/ActionResultScene.cs)

----------------------------------
7. 아이템
- ItemManagerment 클래스
<br>모든 아이템들의 이름, 설명, 가격 등의 프로퍼티가 정의된 클래스입니다.
- EquipItem 클래스
<br>무기와 방어구를 관리하는 클래스
<br>각 장비마다 착용할 수 있는 직업들을 구분하기 위한 enum이 정의되어 있습니다.
- Armor 클래스
<br>EquipItem클래스를 상속받고, 생성자에서 아이템 이름, 방어력, 회피율, 가격, 설명, 장착여부, 착용가능 직업 을 넘겨받은 매개변수로 세팅합니다.
<br> 여기서 착용가능 직업의 데이터는 int자료형을 enum자료형으로 명시적 변환하여 저장합니다.
- Weapon클래스
<br> EquipItem클래스를 상속받고, 생성자에서 아이템 이름, 공격력, 크리티컬 확률, 가격, 설명, 장착여부, 착용가능 직업 을 넘겨받은 매개변수로 세팅합니다.
<br>여기서 착용가능 직업의 데이터는 int자료형을 enum자료형으로 명시적 변환하여 저장.
- UsableItem 클래스
<br>ItemManagement클래스를 상속받고, 포션의 종류를 구분하기 위한 enum이 선언된 클래스입니다.
<br> 포션의 종류에 따라 효과를 다르게해야하기 때문에, 가상메서드가 선언되어있음.
- Potion 클래스
<br>HP와 MP 회복 포션의 효과를 다르게 적용하기 위해, 부모클래스인 UsableItem의 enum을 이용해 구분하고, 다른 효과를 적용하기 위해 부모클래스에 선언된 가상메서드가 재정의 되어있습니다.
<br>Hp회복포션 사용시와 Mp회복포션 사용시의 메시지가 각각 다르게 표시하고, 회복효과가 플레이어의 최대 체력과 최대 마나를 초과하지 않도록 하기위한 로직이 포함되어 있습니다.
- ItemInfo 클래스
<br>무기, 방어구 등의 장비아이템과, 포션 등의 소모아이템이 각각 다른 딕셔너리에 저장되어있고, GetItem 메서드에 아이템의 이름을 매개변수로 전달하면 먼저 장비아이템 관련 딕셔너리에서 키값이 일치하는지 확인 -> 없으면 소비아이템 관련 딕셔너리에서 키값이 일치하는 항목 확인 -> 없으면 null을 반환하고 일치하는 항목이 있다면 장비아이템의 경우 장비아이템 딕셔너리의 키와 일치하는 항목만을 전달하고, 포션의 경우, UsableItem형의 항목을 Potion 형으로 다운캐스팅 후 반환합니다. 
<br>( Usable형으로 받아올 경우 Potion형만의 프로퍼티 값을 받아올 수 없기 때문 )

>[ItemManagement.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/ItemManage/ItemManagement.cs)
<br>[EquipItem.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/ItemManage/EquipItem.cs)
<br>[Armor.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/ItemManage/Armor.cs)
<br>[Weapon.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/ItemManage/Weapon.cs)
<br>[UsableItem.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/ItemManage/UsableItem.cs)
<br>[Potion.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/ItemManage/Potion.cs)
<br>[ItemInfo.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/ItemManage/ItemInfo.cs)

-------------------

8. 인벤토리
- 인벤토리는 장비 아이템, 소비 아이템들의 정보를 가지고 있습니다.
- 정렬, 아이템 탈/장착, 소비 아이템의 사용에 관한 기능을 수행합니다.

>[Inventory.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Inventory.cs)

-------------------------------------

9. 상점
- 아이템을 구매/판매할 수 있는 상점입니다.
- 아이템의 스테이터스와 같은 설명, 가격이 표시됩니다.
- 아이템의 구매/판매 기능은 ShopManager를 통해서 수행됩니다.
- 아이템 구매
    - 장비 아이템: 아이템을 최대 1개씩만 구매할 수 있습니다. 
        - 보유 중인 아이템은 가격 대신 [보유 중] 이라는 메시지가 표기됩니다.
    - 소비 아이템: 소비 아이템은 계속 구매할 수 있도록 했습니다.
        - 현재 보유 중인 소비 아이템의 개수가 함께 표시됩니다.
- 아이템 판매
    - 장비 아이템: 인벤토리의 아이템을 판매할 수 있습니다.
        - 장착 중인 아이템을 판매하려 하면 "장착 중인 아이템입니다. 판매하시겠습니까?" 라는 확인 질문을 합니다.
    - 소비 아이템: 소비 아이템은 1개 씩 판매되도록 했습니다.
        - 소비 아이템을 모두 판매하면 상점의 판매 리스트에서 사라집니다.


>[ShopScene.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Scenes/ShopScene.cs)
<br>[ShopManager.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Managers/ShopManager.cs)

10. 퀘스트
퀘스트정보가 저장되어 있는 json파일을 읽어와 역직렬화를 진행하고, 읽어온 값을 퀘스트 클래스의 각 프로퍼티에 자동으로 적용합니다. 
<br>try, catch문을 사용해 오류발생시 파일 로드 실패 메시지를 출력하고 null을 반환하다록 설정되어 있고, 파일로드 성공시에는 읽어온 퀘스트데이터를 리스트화 시킨후 퀘스트씬 클래스에 넘겨줍니다. 
<br>퀘스트 씬에서는, 불러온 퀘스트정보 중에 이미 완료하고 보상까지 받은퀘스트는 제외하고 출력하는 로직이 있어 진행전, 진행중, 보상수령가능 퀘스트만 출력된다. 
<br>여기서 플레이어는 퀘스트id를 입력하여 특정 퀘스트를 선택하고 이를 퀘스트 매니저 클래스의 AcceptQuest메서드에 전달하고 호출하면, 진행전인 퀘스트에는 수락과 거절을 묻는 메시지가 출력되며 플레이어는 왼쪽오른쪽 방향키를 이용하여 수락과 거절을 선택할 수 있고, 수락을 선택하면, 퀘스트를 수락하고, 퀘스트의 스테이터스를 변경하고 이를 json파일에 저장하며, 거절 시에는 메인 메뉴로 돌아옵니다. 
<br>진행중인 퀘스트를 선택했을 경우에 퀘스트 수락하기를 누르면 이미 진행중인 퀘스트라는 메시지를 출력하고 어떠한 변경사항도 적용되지 않고 메인메뉴로 복귀합니다. 
<br>보상수령가능한 완료퀘스트의 경우에는 수락 거절을 묻는 메시지는 생략하고, 메시지로 안내후 보상수령 메서드에 플레이어형 데이터를 전달후 경험치, 골드, 보상아이템을 플레이어의 인벤토리에 추가합니다.
<br> 퀘스트의 진행상황은 battle 클래스에서 몬스터가 사망했을때, 몬스터의 이름을 UpdateProgress메서드에 전달하면 진행중인 퀘스트의 조건에 들어있는 몬스터의 이름과 대조하여 일치하면 조건과 관련해 저장되어 있는 리스트의 Current수치를 1올려주고, 퀘스트 완료여부를 확인하여 완료되면 퀘스트 스테이터스를 완료 상태로 전환하고, 변경사항을 json파일에 저장한다.
<br>퀘스트 정보를 저장할 때에는 먼저 퀘스트정보를 한글로도 원활하게 저장할 수 있게 엔코더 옵션을 설정한후, 데이터를 직렬화시켜 기존 json파일을 덮어쓰는 형식으로 저장됩니다.

>[Quest.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/QuestManagement/Quest.cs)
<br>[QuestManager.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/QuestManagement/QuestManager.cs)
