using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f; // �̵� �ӵ�
    public float rotationSpeed = 700f; // ȸ�� �ӵ�
    public float jumpHeight = 1f; // ���� ����
    public Transform cameraTransform; // ī�޶��� ��ġ

    private CharacterController controller; // ĳ���� ��Ʈ�ѷ� ������Ʈ
    private Vector3 velocity; // ĳ������ �ӵ�
    public bool isGrounded; // ���� �ִ��� üũ
    private float gravity = -9.8f; // �߷�

    //private float lookSpeedX = 2f; // ���콺 ȸ�� �ӵ� x
    //private float lookSpeedY = 2f; // ���콺 ȸ�� �ӵ� y
    //private float currentRotationX = 0f; // ī�޶� ȸ�� x

    private Animator animator;
    public bool isAttack = false;
    [SerializeField] Weapon weapon;

    [SerializeField] SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        spawnManager = GameObject.Find("SpawnManager").gameObject.GetComponent<SpawnManager>();
        weapon = GameObject.Find("SM_Wep_Sword_03").gameObject.GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    spawnManager.currentAnimals -= 1;
        //}

        // ���콺 ȸ�� ó��
        //CameraRotation();

        // ���� �ִ��� Ȯ��
        isGrounded = true;
        //isGrounded = controller.isGrounded;

        // �̵� ó��
        if (!isAttack)
        {
            MoveCharacter();
        }

        // ���� ó��
        if (isGrounded && velocity.y < 0 && !isAttack)
        {
            velocity.y = -2f; // ���� ���� �� �ӵ� �ʱ�ȭ
        }

        // ����
        if (isGrounded && Input.GetButtonDown("Jump") && !isAttack)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // ���� ���� ���
        }

        // �߷� ����
        velocity.y += gravity * Time.deltaTime;

        // ĳ���� �̵� ����
        if (!isAttack)
        {
            controller.Move(velocity * Time.deltaTime);
        }

        // ����
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isAttack)
        {
            isAttack = true;
            weapon.isDestory = true;
            animator.SetTrigger("attack1");
            float sec = 2.5f;
            StartCoroutine(Waiting(sec));
        }
        if (Input.GetKeyDown(KeyCode.S) && !isAttack)
        {
            isAttack = true;
            weapon.isDestory = true;
            animator.SetTrigger("attack2");
            float sec = 2.5f;
            StartCoroutine(Waiting(sec));
        }
        if (Input.GetKeyDown(KeyCode.D) && !isAttack)
        {
            isAttack = true;
            weapon.isDestory = true;
            animator.SetTrigger("attack3");
            float sec = 2.1f;
            StartCoroutine(Waiting(sec));
        }
        if (Input.GetKeyDown(KeyCode.Q) && !isAttack)
        {
            isAttack = true;
            weapon.isDestory = true;
            animator.SetTrigger("attackB1");
            float sec = 3f;
            StartCoroutine(Waiting(sec));
        }
        if (Input.GetKeyDown(KeyCode.W) && !isAttack)
        {
            isAttack = true;
            weapon.isDestory = true;
            animator.SetTrigger("attackB2");
            float sec = 2.5f;
            StartCoroutine(Waiting(sec));
        }
        if (Input.GetKeyDown(KeyCode.E) && !isAttack)
        {
            isAttack = true;
            weapon.isDestory = true;
            animator.SetTrigger("attackB3");
            float sec = 2.4f;
            StartCoroutine(Waiting(sec));
        }
    }

    IEnumerator Waiting(float sec)
    {
        yield return new WaitForSeconds(sec);
        isAttack = false;
    }

    void MoveCharacter()
    {
        // �̵� �Է�
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ī�޶� �������� �̵� ���� ���
        Vector3 direction = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        direction.y = 0; // y�� ȸ���� �����ݴϴ�.
        
        // �޸���
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
            // �̵� ó��
            moveSpeed = 2f;
            controller.Move(direction * moveSpeed * Time.deltaTime);
        } else
        {
            animator.SetFloat("moveV", vertical);
            animator.SetFloat("moveH", horizontal);
            // �̵� ó��
            moveSpeed = 1f;
            controller.Move(direction * moveSpeed * Time.deltaTime);
        }
                
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

    void Running()

    {
        animator.SetFloat("moveV", 2);
        animator.SetFloat("moveH", 2);
    }

    //void CameraRotation()
    //{
    //    // ���콺 �Է� ��
    //    float mouseX = Input.GetAxis("Mouse X");
    //    float mouseY = Input.GetAxis("Mouse Y");

    //    // x�� ȸ�� (�¿�). ī�޶�� ĳ���� ��� �¿�� ȸ��
    //    transform.Rotate(Vector3.up * mouseX * lookSpeedX);
    //    cameraTransform.Rotate(Vector3.up * mouseX * lookSpeedX); // ī�޶� �¿� ȸ��

    //    // y�� ȸ�� (����)
    //    currentRotationX -= mouseY * lookSpeedY;
    //    currentRotationX = Mathf.Clamp(currentRotationX, -80f, 80); // ȸ�� ���� ����
    //    cameraTransform.localRotation = Quaternion.Euler(currentRotationX, 0f, 0f);
    //}
}
