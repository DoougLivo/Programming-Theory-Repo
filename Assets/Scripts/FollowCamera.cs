using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // ī�޶� ���� ���
    public Vector3 offset = new Vector3(0, 1, -1); // ī�޶��� ��ġ ������
    public float smoothSpeed = 0.125f; // ī�޶� �̵� �ε巯��
    public float rotationSpeed = 100f; // ī�޶� ȸ�� �ӵ�
    private float horizontalInput; // ���콺 �Է�
    public float rotationX = 40f; // ī�޶� X �� ȸ�� ����

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogError("Target�� �������� �ʾҽ��ϴ�!");
            return;
        }

        // ���콺 �Է� �ޱ�
        //horizontalInput += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        // ī�޶� ȸ�� ����
        Quaternion rotation = Quaternion.Euler(rotationX, 0/*horizontalInput*/, 0);

        // ���ϴ� ��ġ ���
        Vector3 desiredPosition = target.position + offset;

        // �ε巴�� �̵�
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // ī�޶� ��ġ ������Ʈ
        transform.position = smoothedPosition;

        // ī�޶� ȸ�� ����
        transform.rotation = rotation;

        // ��� �ٶ󺸱�
        //transform.LookAt(target);
    }
}
