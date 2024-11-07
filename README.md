# StandardClassPractice_Week3
 스탠다드반 꾸준 실습 3주차

### Q1

#### 분석문제

1. 입문 주차와 비교해서 입력 받는 방식의 차이와 공통점을 비교해보세요.
   * 입문주차에서는 SendMessage 방식 사용, On ~ 방식으로 입력함수이름이 강제됨
   * 숙련주차에서는 유니티 이벤트 방식 사용, Player Input 컴포넌트에 함수를 등록해서 입력을 받음
   * 숙련주차에서 입력을 받는 클래스와 실제 움직임을 구현하는 클래스가 나뉘지 않음

2. CharacterManager와 Player의 역할에 대해 고민해보세요.
   * CharacterManager
     * 싱글톤 패턴을 사용하여 현재 플레이어가 조종하는 캐릭터의 정보에 전역 접근 지점을 마련함
   * Player
     * 실제 플레이어의 정보를 담고 있음
     * 단, 플레이어의 모든 정보가 아닌, 플레이어가 사용하는 클래스 정보를 가지고 있는 형태
     * 이 클래스를 통해 플레이어에 사용되는 각각의 클래스에 접근하게됨
       
3. 핵심 로직을 분석해보세요 (Move, CameraLook, IsGrounded)
   * Move
     * OnMoveInput 함수에서 WASD가 눌리고 있으면 curMovementInput 에 해당되는 값 저장, 눌리지 않으면 Vector2.zero 값 저장
     * curMovementInput 값의 y값을 사용하여 Vector3의 z값으로 , x 값을 사용하여 Vector3의 x값으로 저장
     * Vector3 의 y값은 현재 velocity 값 사용 -> velocity : 초당 이 값만큼 오브젝트가 이동
     * 현재 velocity 를 만들어준 Vector3값으로 바꿔주어 이동

   * CamraLook
     * OnLookInput을 통해 마우스의 위치정보를 받음, 이때 위치정보는 Delta값으로 정해져있음으로, 얼마나 이동했는지를 받게됨
     * 마우스가 이동한 값 X 마우스 민감도 를 하여 캐릭터의 회전, 위아래로 고개를 돌릴 수치를 정함, 마우스 델타의 y는 위아래, x는 좌우
     * 캐릭터 회전은 별다를 처리 없이 그대로 적용(몸 전체를 돌림), 단 위아래 회전의 경우 제한값이 필요함(고개를 360도 돌릴수 없음)
     * 사람의 목 당담 : cameraContainer 이 오브젝트를 X축을 중심으로 회전시켜 고개를 꺽는 행동을 구현
     * cameraContainer 의 rotation 값중, x값에에 mouseDelta.y 값 적용, 이때 Mathf.Clamp를 사용하여 최대, 최소값을 적용
     * mouseDelta.x 값은 이 스크립트가 달릴 오브젝트(플레이어 캐릭터)에 바로 적용하여 캐릭터 회전 구현
  
   * IsGrounded
     * Raycast 를 통해 플레이어가 땅에 붙어있는지 판단함
     * 이 스크립트가 부착된 컴포넌트의 transform 을 기준으로 전방, 후방 , 좌, 우 0.2 만큼 떨어진 지점, 위쪽 0.01 지점에서 Ray 4개 발사
     * 4개의 레이중 하나라도 groundLayer 에 선택된 레이어 오브젝트에 닿으면 true 반환(땅에 붙어있음)
     * 레이를 모두 검사했는데 검출된 레이가 없을경우 false 반환(땅에 붙어있지 않음)
     * 검사가 필요할때만 이 함수를 호출(점프시 등)

4. Move와 CameraLook 함수를 각각 FixedUpdate, LateUpdate에서 호출하는 이유에 대해 생각해보세요.
   * Move를 FixedUpdate에 구현하는 이유
     * Update의 경우, 기기의 성능에따라 Update가 호출되는 횟수가 달라짐
     * 이 경우 기기의 성능에따라 캐릭터의 이동 속도가 달라지게 될 수 있음 -> 고성능의 기기가 저성능 기기보다 더 많이 이동함
     * FixedUpdate는 고정된 횟수만큼만 호출됨 (기본 50 프레임 고정)
     * 이동 호출이 고정되었기때문에 기기 성능이 달라도 같은 거리를 이동하게됨
   * CameraLook 을 LateUpdate에 호출하는 이유
     * Update 종료후 호출됨
     * 캐릭터가 행동한 뒤, 카메라가 이동
     * 카메라는 언제나 캐릭터 위치에 기반해서 움직임 -> 캐릭터의 움직임이 종료된 후, 그 위치를 기반으로 카메라를 움직여야함
    
### Q2

#### 분석문제

1. 별도의 UI 스크립트를 만드는 이유에 대해 객체지향적 관점에서 생각해보세요.
   * 단일책임 원칙을 기반으로, UI 는 UI 관련 스크립트가 책임을 져야하기 때문

2. 인터페이스의 특징에 대해 정리해보고 구현된 로직을 분석해보세요.
   * 인터페이스의 특징
     * 특정 기능의 이름을 미리 정의한 뒤, 상속받은 클래스가 필수적으로 구현하게만든다.
     * 같은 시점에 동작하지만 다른 구현내용을 사용해야할 때 사용한다.
     * 다른 클래스여도 같은 기능을 할 필요가 있을때, 인터페이스를 사용하여 묶어줄 수 있다.
   * 구현된 로직 IDamageable
     * 현재는 캠프파이어로부터 플레이어가 데미지를 받는 내용만 구현되고있다.
     * NPC 등을 구현할 때, 플레이어가 NPC로부터 공격을 받거나, NPC가 플레이어로부터 공격을 받도록 만들 수 있다.
     * 캠프파이어는 IDamageable이 붙어있는 대상이면 조건 충족시 데미지를 준다, 따라서 NPC도 IDamageable을 상속받으면 캠프파이어로부터 데미지를 받는다.

3. 핵심 로직을 분석해보세요. (UI 스크립트 구조, CampFire, DamageIndicator)
   * UI 스크립트 구조
     * UI 스크립트는 각각의 Condition 클래스를 가지고있는구조이다.
     * Condition 클래스는 자신의 최대값, 현재값 등을 가지고있으며, 이를 UI에 표기하는 역할을 한다.
     * Condition 은 현재 캐릭터의 스텟 정보를 가짐과 동시에 스텟변화를 위한 메서드를 가지며, UI에 현재 스텟을 표기하는 역할을한다. -> 단일책임 원칙에 위배되는것은 아닌가?
       
   * CampFire
     * 캠프파이어 클래스는 지정한 범위 내에있으면 일정시간마다 데미지를 주는 역할만을 수행한다.
     * damageRate 시간마다 한번씩, things 리스트에 있는 IDamageable 을 상속받은 객체에 TakeDamage()를 호출한다.
     * things 리스트는 객체가 트리거 콜라이더에 진입할 때 해당 객체의 IDamageable 여부를 확인하며, IDamageable이 있으면 리스트에 추가한다.
     * 객체가 트리거 콜라이더에서 나갈 때, IDamageable 여부를 확인하며 IDamageable이 있으면 리스트에서 같은 객체를 찾아서 제거한다.
    
   * DamageIndicator
     * 플레이어의 onTakeDamage 이벤트를 구독하고있는 클래스이다.
     * PlayerCondition 에서 onTakeDamage 이벤트가 발생하면 Flash 함수를 호출한다.
     * Flash 함수는 현재 자신의 코루틴이 있을경우 코루틴을 중지시키며, 자신이 가진 Image 컴포넌트를 활성화 시킨다.
     * Flash 함수에서 FadeAway 코루틴을 작동시키고, 자신 클래스 변수에 저장시킨다.
     * FadeAway 코루틴은 시작시 이미지의 알파값을 특정 값으로 변경하며, 반복문을통해서 알파값을 감소시키다 알파가 0 이하가되면 종료된다.
     * 코루틴이 종료되면서 이미지 컴포넌트를 비활성화 시킨다.
    
  ### Q3

  #### 분석문제

  1. Interaction 기능의 구조와 핵심 로직을 분석해보세요.
     * 기능구조
       * Update - checkTime 마다 Ray 발사, Physics.Raycast 를통해 IInteractable 획득 시도
       * SetPromptText - IInteractable을통해 얻은 string값 화면에 표기, 화면에 아이템 설명이 활성화될지 여부 결정
       * OnInteractInput - InputAsset 을 통해 상호작용에 지정된 입력이 들어오면 해당 IInteractable 의 상호작용 함수 호출
     * 핵심 로직
       * 지정한 시간마다 카메라 전방으로 Ray를 발사한다.
       * Ray 에 지정한 레이어의 오브젝트가 검출될경우, 해당 오브젝트의 IInteractable 을 가져온다
       * IInteractable 인터페이스를 통해서 검출된 오브젝트가 가진 GetInteractPrompt 함수를 가져와서, 반환받은 string 값을 화면에 표기한다.
       * IInteractable 오브젝트를 가지고있을때 상호작용에 지정한 버튼을 입력하면 해당 상호작용 오브젝트의 상호작용 메서드를 호출한다.
  2. Inventory 기능의 구조와 핵심 로직을 분석해보세요.
     * 기능구조
       * Start - 아이템 슬롯을 생성, 인덱스 부여, AddItem 이벤트에 구독
       * AddItem - Player 획득할 아이템을 인벤토리에 추가
         * GetItemStack(ItemData data) - 아이템이 중첩 가능할경우 여유가 있는 같은 아이템을 찾아서 해당 슬롯의 카운트 증가
         * GetEmptySlot() - 중첩 불가능하거나, 중첩할 슬롯이 없을경우 비어있는 슬롯 찾음
         * 아이템을 획득하지 못한경우 ThrowItem(Itemdata) 호출
       * ThrowItem - 매개변수의 아이템을 Player의 dropPosition 위치에 생성
       * SelectItem - ItemSlot 클래스에서 호출, 해당 슬롯의 버튼이 눌리면 선택된 슬롯의 아이템 정보 표기
       * ClearSelectedItemWindow - 아이템 정보창 초기화, RemoveSelectedItem 에서 호출
       * OnUseButton - 아이템의 타입이 소모품일경우, 아이템의 consumables에 접근하여 종류에 맞는 효과 발생
         * Health 일경우 - 체력 회복
         * Hunger 일 경우 - 포만도 회복
       * OnDropButton - ThrowItem(selectedItem.item) 과 RemoveSelectedItem() 함수 호출, 아이템을 생성한 후, 인벤토리에서 해당 아이템을 지움
       * RemoveSelectedItem - 호출될경우 현재 선택한 아이템 슬롯의 중첩값(quantity) 를 1 줄임
         * quantity 값이 0 이하가 되면 해당 슬롯을 초기화
       * Toggle - PlayerContoller 으로부터 인벤토리 입력 이벤트를 받아서 활성화, 비활성화
           
     * 핵심 로직
       * 스크립트가 활성화될 때, 아이템 슬롯들을 가져와서 인덱스 부여
       * IInteractable 의 OnInteract 에서 AddItem 이벤트가 발생할경우 인벤토리에 아이템 추가
         * 아이템 추가시 아이템 슬롯의 중첩 가능여부, 비어있는 아이템창 여부를 확인
         * 여유가 있을경우 해당 슬롯에 아이템 추가
         * 여유가 없을경우 아이템 프리팹을 지정위치에 생성
       * 아이템 정보 표기 UI 업데이트
       * 아이템 사용, 장착, 장착해제, 버리기 기능 구현
         * 소모품일 경우 사용버튼 활성화. 사용시 사용 효과 직렬화 클래스를 모두 확인후, 효과 적용
         * 장비일경우 장착, 장착해제 버튼 활성화
           * 장착중일경우 장착해제버튼 활성화. 장착중인 도구 프리팹을 삭제함
           * 장착중이 아닐경우 장착버튼 활성화. 지정한 위치에 선택한 아이템의 도구 프리팹을 생성
         * 버리기 버튼은 상시 활성화
           * 장비중일경우, 장착 해제후 버리기

# 4주차

### Q2

요구사항 1 - NPC의 기능 분석

* SetState
  - 매개변수를 통해 AIState를 받아서 현재 NPC의 상태를 바꾸는 역할을 합니다.
  - 현재 NPC의 상태에 따라 NPC가 움직이는 속도가 달라집니다.
 
* PassiveUpdate
  - 현재 상태에 따라서 NPC의 실제 행동을 결정합니다.
  - 배회, 대기 상태에서만 이 메서드가 호출됩니다.
  - 배회 상태에서 목적지까지의 거리가 0.1 미만이 될 경우 대기 상태로 변경합니다.
  - 대기 상태로 변경할 때, 최소대기시간 - 최대 대기시간 사이의 시간이 지난 후 WanderToNewLocation 메서드를 호출합니다.
  - 플레이어와의 거리가 detectDistance 보다 작아질경우 공격상태로 변경하는 역할을 수행합니다.
    
* AttackingUpdate
  - 현재 상태가 공격상태일떄 이 메서드가 호출됩니다.
  - 플레이어와의 거리가 attackDistance 보다 크고, IsPlayerInFieldOfView() 메서드의 반환값이 false 일경우
    - 플레이어를 추적합니다.
    - 플레이어에게 도착하는 경로를 계산합니다.
    - 플레이어에게 도달할 수 있는 경로가 있으면 플레이어를 목적지로 설정하여 Npc 오브젝트가 이동합니다.
    - 플레이어에게 도달할 수 있는 경로가 없을경우 Fleeing 상태로 변경합니다.

  - 거리가 attackDistance 보다 짧고, IsPlayerInFieldOfView() 의 메서드가 true일경우 플레이어를 공격합니다.
    - 현재시간에서 마지막 공격 시간을 뺀 값이 attackRate 보다 클경우 공격을 시작합니다.
    - 공격을 시작하면 현재 시간을 마지막 공격 시간 변수인 lastAttackTime 에 저장합니다.
    - 플레이어가 가진 IDamagable 인터페이스의 TakeDamage 를 호출하여 damage 변수만큼의 데미지를 줍니다.
    - 애니메이터의 Attack 트리거를 호출하여 공격 모션을 작동합니다.

* FleeingUpdate
  - 현재 지정된 목적지까지 남은거리가 0.1 미만일경우 목적지를 재설정합니다.
  - 그 외의 경우 배회상태로 변경하기 때문에, 현재 이 메서드는 사실상 배회상태로 변경하는 역할만을 수행합니다

* WanderToNewLocation
  - 배회, 대기 상태에서 목적지에 도착하면 일정 시간 뒤 호출됩니다.
  - 현재 상태가 대기상태가 아닐경우 메서드를 실행하지 않습니다.
  - Npc의 상태를 배회상태로 변경합니다.
  - Npc의 목적지를 정합니다. 이때, 목적지를 정하기위해 GetWanderLocation 메서드를 호출합니다.
 
* IsPlayerInFieldOfView
  - 플레이어가 Npc의 시야각 내에 있는지를 판정합니다.
  - 플레이어까지의 벡터를 얻은 뒤, 그 벡터와 자신의 정면을 향한 벡터 사이의 각도를 얻습니다.
  - 이 각도가 fieldOfView 변수의 절반보다 작은지 비교합니다. 작으면 true, 크면 false를 반환합니다.

* GetWanderLocation
  - 호출되면 다음 목적지 위치의 Vector3값을 반환합니다.
  - 현재 위치에서 최소 배회거리 ~ 최대 배회거리 사이의 값만큼 떨어진 위치에서 무작위 이동가능 위치를 얻습니다.
  - 현재 위치와 얻은 위치 사이의 거리가 detectDistance 보다 작을경우, 위치를 다시 정합니다.
  - 30회동안 위치를 얻지 못할경우 마지막으로 얻은 위치를 목적지로 결정합니다.

* TakePhysicalDamage
  - IDamagable 인터페이스를 상속받아서 구현이 강제된 메서드입니다.
  - 스크립트가 부착된 오브젝트가 데미지를 받는것을 구현합니다.
  - 호출되면 이 스크립트의 health 값을 매개변수 flaot 값 만큼 뺍니다.
  - health 값이 0 이하가 되면 Die 메서드를 호출합니다.
  - DamageFlash 코루틴을 호출합니다.

* Die
  - NPC 의 health 값이 0 이하가되면 호출됩니다.
  - dropOnDeath 배열의 내용이 있으면 해당 배열의 ItemData.dropPrefab 을 지정한 위치에 생성합니다.
  - 이 스크립트가 붙은 오브젝트를 파괴합니다.

* DamageFlash
  - 코루틴 함수입니다.
  - 공격을 받으면 오브젝트가 가진 모든 Mesh의 색을 붉은색으로 변경합니다.
  - 0.1 초 뒤에, 모든 Mesh의 색을 하얀색으로 다시 변경합니다.

### Q3

요구사항 1 - Lerp, SLerp, 근사값

* 보간
  - 데이터의 중간값을 찾는 프로세스
* Lerp
  - Mathf.Lerp(float, float, float)
  - 두 값 사이의 값을 찾아서 반환
  - 첫번째 매개변수와, 두번째 매개변수 사이의 값을 반환하며, 첫번째 매개변수에서 시작해 세번째 매개변수 만큼 떨어진 값을 반환합니다.
* Slerp
  - Vector3.Slerp , Quaternion.Slerp
  - 두 점 사이의 값을 찾아서 반환
  - Vector 의 경우, 첫번째 Vector 매개변수에서 두번째 매개변수 로 향하는 벡터중, float 매개변수 만큼 떨어진 거리의 값을 반환
  - Quaternion 의 경우, 첫번째 매개변수에서 두번째 매개변수 향하는 각도중, float 매개변수 만큼 떨어진 거리의 값을 반환
  - 이때 세번째 매개변수 float 값은 백분율 값으로, 1을 100%로 하는 값
* Mathf.Approximately 를 사용하는 이유
  - float 값은 정확한 소수점 값을 반환하지 않음
  - 계산된 두 값이 보기에는 같아도, == 를 사용하여 비교시 약간의 오차로 인해서 두 값이 다르다는 결과를 나타낼 수 있음
  - float 값의 오차를 무시하고 같음을 알기 위해서 사용하게됨
  - 정의: Abs(b - a) < Max(1E-06f * Max(Abs(a), Abs(b)), Epsilon * 8f);
  
