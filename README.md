# 프로젝트 소개
C#으로 만드는 간단한 텍스트 RPG입니다.

# 개발 기간
2025.10.14 ~ 2025.10.

# 개발 인원
**6명**
>팀장: 이준혁
<br>팀원: 오민근, 조아라, 임성규, 박범근, 김태환
    
# 역할 분담
- 이준혁
    - 몬스터 클래스 구현
    - 효과 클래스 구현
    - 전투 밸런스 작업

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
# 실행 화면



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

2. 던전
- 던전은 1층부터 15층까지로, 플레이어가 도달한 층까지의 목록을 보여주고, 입장할 수 있습니다.
- 던전의 클리어 여부에 따라서 화면을 출력하고, 보상을 지급합니다.

>[DungeonEntranceScene.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Scenes/DungeonEntranceScene.cs)
<br>[DungeonResultScene.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Scenes/DungeonResultScene.cs)

3. 스테이지 구성
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

4. 인벤토리
- 인벤토리는 장비 아이템, 소비 아이템들의 정보를 가지고 있습니다.
- 정렬, 아이템 탈/장착, 소비 아이템의 사용에 관한 기능을 수행합니다.

>[Inventory.cs](https://github.com/bbbp98/Team-Project-Text-RPG-group5/blob/main/TextRPG_group5/TextRPG_group5/Inventory.cs)

5. 상점
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