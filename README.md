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
       * 
     * 핵심 로직
  2. Inventory 기능의 구조와 핵심 로직을 분석해보세요.
     * 기능구조
       *
     * 핵심 로직
       *
       
   
