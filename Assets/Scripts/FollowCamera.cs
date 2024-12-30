using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 대상
    public Vector3 offset = new Vector3(0, 1, -1); // 카메라의 위치 오프셋
    public float smoothSpeed = 0.125f; // 카메라 이동 부드러움
    public float rotationSpeed = 100f; // 카메라 회전 속도
    private float horizontalInput; // 마우스 입력
    public float rotationX = 40f; // 카메라 X 축 회전 각도

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogError("Target이 설정되지 않았습니다!");
            return;
        }

        // 마우스 입력 받기
        //horizontalInput += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        // 카메라 회전 적용
        Quaternion rotation = Quaternion.Euler(rotationX, 0/*horizontalInput*/, 0);

        // 원하는 위치 계산
        Vector3 desiredPosition = target.position + offset;

        // 부드럽게 이동
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 카메라 위치 업데이트
        transform.position = smoothedPosition;

        // 카메라 회전 적용
        transform.rotation = rotation;

        // 대상 바라보기
        //transform.LookAt(target);
    }
}
