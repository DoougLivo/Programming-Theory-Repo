using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.5f; // �̵� �ӵ�
    public float rotationSpeed = 700f; // ȸ�� �ӵ�
    public float jumpHeight = 1f; // ���� ����
    public Transform cameraTransform; // ī�޶��� ��ġ

    private CharacterController controller; // ĳ���� ��Ʈ�ѷ� ������Ʈ
    private Vector3 velocity; // ĳ������ �ӵ�
    public bool isGrounded; // ���� �ִ��� üũ
    private float gravity = -9.8f; // �߷�

    private float lookSpeedX = 2f; // ���콺 ȸ�� �ӵ� x
    private float lookSpeedY = 2f; // ���콺 ȸ�� �ӵ� y
    private float currentRotationX = 0f; // ī�޶� ȸ�� x

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
        // ���콺 ȸ�� ó��
        CameraRotation();

        // ���� �ִ��� Ȯ��
        isGrounded = true;
        //isGrounded = controller.isGrounded;

        // �̵� ó��
        MoveCharacter();

        // ���� ó��
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ���� ���� �� �ӵ� �ʱ�ȭ
        }

        // ����
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // ���� ���� ���
        }

        // �߷� ����
        velocity.y += gravity * Time.deltaTime;

        // ĳ���� �̵� ����
        controller.Move(velocity * Time.deltaTime);
    }

    void MoveCharacter()
    {
        // �̵� �Է�
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ī�޶� �������� �̵� ���� ���
        Vector3 direction = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        direction.y = 0; // y�� ȸ���� �����ݴϴ�.

        // �̵� ó��
        controller.Move(direction * moveSpeed * Time.deltaTime);

        // ĳ���� ȸ�� (�ε巴�� ȸ��)
        if (direction.magnitude >= 0.1f)
        {
            // ȸ�� ���� ���
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            // ȸ�� ����
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void CameraRotation()
    {
        // ���콺 �Է� ��
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // x�� ȸ�� (�¿�). ī�޶�� ĳ���� ��� �¿�� ȸ��
        transform.Rotate(Vector3.up * mouseX * lookSpeedX);
        cameraTransform.Rotate(Vector3.up * mouseX * lookSpeedX); // ī�޶� �¿� ȸ��

        // y�� ȸ�� (����)
        currentRotationX -= mouseY * lookSpeedY;
        currentRotationX = Mathf.Clamp(currentRotationX, -80f, 80); // ȸ�� ���� ����
        cameraTransform.localRotation = Quaternion.Euler(currentRotationX, 0f, 0f);
    }
}
