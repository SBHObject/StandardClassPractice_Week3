using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//입문주자와 비교할떄 입력방식의 차이
//- 입문주차에서는 SendMessage 방식 사용, On ~ 방식으로 입력함수이름이 강제됨
//- 숙련주차에서는 유니티 이벤트 방식 사용, Player Input 컴포넌트에 함수를 등록해서 입력을 받음
//- 숙련주차에서 입력을 받는 클래스와 실제 움직임을 구현하는 클래스가 나뉘지 않음

//
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpForce;
    public LayerMask groundLayer;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CamLook();
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    //핵심로직 분석 Move()
    //OnMoveInput 함수에서 WASD가 눌리고 있으면 curMovementInput 에 해당되는 값 저장, 눌리지 않으면 Vector2.zero 값 저장
    //curMovementInput 값의 y값을 사용하여 Vector3의 z값으로 , x 값을 사용하여 Vector3의 x값으로 저장
    //Vector3 의 y값은 현재 velocity 값 사용 -> velocity : 초당 이 값만큼 오브젝트가 이동 
    //현재 velocity 를 만들어준 Vector3값으로 바꿔주어 이동
    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;

        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    //핵심로직 분석 CameraLook()
    //OnLookInput을 통해 마우스의 위치정보를 받음, 이때 위치정보는 Delta값으로 정해져있음으로, 얼마나 이동했는지를 받게됨
    //마우스가 이동한 값 X 마우스 민감도 를 하여 캐릭터의 회전, 위아래로 고개를 돌릴 수치를 정함, 마우스 델타의 y는 위아래, x는 좌우
    //캐릭터 회전은 별다를 처리 없이 그대로 적용(몸 전체를 돌림), 단 위아래 회전의 경우 제한값이 필요함(고개를 360도 돌릴수 없음)
    //사람의 목 당담 : cameraContainer 이 오브젝트를 X축을 중심으로 회전시켜 고개를 꺽는 행동을 구현
    //cameraContainer 의 rotation 값중, x값에에 mouseDelta.y 값 적용, 이때 Mathf.Clamp를 사용하여 최대, 최소값을 적용
    //mouseDelta.x 값은 이 스크립트가 달릴 오브젝트(플레이어 캐릭터)에 바로 적용하여 캐릭터 회전 구현
    private void CamLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles = new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    //핵심로직분석 IsGrounded()
    //Raycast 를 통해 플레이어가 땅에 붙어있는지 판단함
    //이 스크립트가 부착된 컴포넌트의 transform 을 기준으로 전방, 후방 , 좌, 우 0.2 만큼 떨어진 지점, 위쪽 0.01 지점에서 Ray 4개 발사
    //4개의 레이중 하나라도 groundLayer 에 선택된 레이어 오브젝트에 닿으면 true 반환(땅에 붙어있음)
    //레이를 모두 검사했는데 검출된 레이가 없을경우 false 반환(땅에 붙어있지 않음)
    //검사가 필요할때만 이 함수를 호출(점프시 등)
    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayer))
            {
                return true;
            }
        }

        return false;
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}

