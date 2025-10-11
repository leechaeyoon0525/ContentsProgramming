using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("캐릭터 설정")]
    public string playerName = "이채윤";
    public float moveSpeed = 5.0f;

    private Animator animator;

    void Start()
    {
        // 컴포넌트 가져오기
        animator = GetComponent<Animator>();
        
        // 캐릭터 소개
        Debug.Log("안녕하세요, " + playerName + "님!");
        Debug.Log("이동 속도: " + moveSpeed);

        if (animator != null)
            Debug.Log("Animator 컴포넌트를 찾았습니다!");
        else
            Debug.LogError("Animator 컴포넌트가 없습니다!");
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        // WASD 이동 입력
        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1); // 좌우 반전
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);  // 원래 방향
        }
        if (Input.GetKey(KeyCode.W))
            movement += Vector3.up;
        if (Input.GetKey(KeyCode.S))
            movement += Vector3.down;

        // 대각선 이동 시 속도 보정
        if (movement != Vector3.zero)
            movement.Normalize();

        // Shift 입력 감지 → 달리기
        float currentMoveSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentMoveSpeed = moveSpeed * 2f;
            Debug.Log("달리기 모드 활성화!");
        }

        // 이동 적용
        transform.Translate(movement * currentMoveSpeed * Time.deltaTime, Space.World);

        // Animator용 Speed 계산
        float currentSpeed = movement != Vector3.zero ? currentMoveSpeed : 0f;
        if (animator != null)
        {
            animator.SetFloat("Speed", currentSpeed);
            Debug.Log("Current Speed: " + currentSpeed);
        }
    }
}