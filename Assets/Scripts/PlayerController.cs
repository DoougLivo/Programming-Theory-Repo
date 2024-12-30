using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.5f; // 이동 속도
    public float rotationSpeed = 700f; // 회전 속도
    public float jumpHeight = 1f; // 점프 높이
    public Transform cameraTransform; // 카메라의 위치

    private CharacterController controller; // 캐릭터 컨트롤러 컴포넌트
    private Vector3 velocity; // 캐릭터의 속도
    public bool isGrounded; // 땅에 있는지 체크
    private float gravity = -9.8f; // 중력

    private float lookSpeedX = 2f; // 마우스 회전 속도 x
    private float lookSpeedY = 2f; // 마우스 회전 속도 y
    private float currentRotationX = 0f; // 카메라 회전 x

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 회전 처리
        CameraRotation();

        // 땅에 있는지 확인
        isGrounded = true;
        //isGrounded = controller.isGrounded;

        // 이동 처리
        MoveCharacter();

        // 점프 처리
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 땅에 있을 때 속도 초기화
        }

        // 점프
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // 점프 높이 계산
        }

        // 중력 적용
        velocity.y += gravity * Time.deltaTime;

        // 캐릭터 이동 적용
        controller.Move(velocity * Time.deltaTime);
    }

    void MoveCharacter()
    {
        // 이동 입력
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 카메라 기준으로 이동 벡터 계산
        Vector3 direction = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        direction.y = 0; // y축 회전을 없애줍니다.

        // 이동 처리
        controller.Move(direction * moveSpeed * Time.deltaTime);

        // 캐릭터 회전 (부드럽게 회전)
        if (direction.magnitude >= 0.1f)
        {
            // 회전 각도 계산
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            // 회전 적용
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void CameraRotation()
    {
        // 마우스 입력 값
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // x축 회전 (좌우). 카메라와 캐릭터 모두 좌우로 회전
        transform.Rotate(Vector3.up * mouseX * lookSpeedX);
        cameraTransform.Rotate(Vector3.up * mouseX * lookSpeedX); // 카메라도 좌우 회전

        // y축 회전 (상하)
        currentRotationX -= mouseY * lookSpeedY;
        currentRotationX = Mathf.Clamp(currentRotationX, -80f, 80); // 회전 각도 제한
        cameraTransform.localRotation = Quaternion.Euler(currentRotationX, 0f, 0f);
    }
}
