using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUserRight : MonoBehaviour
{
    public Transform xrCamera;  // 사용자 카메라(HMD)의 Transform
    public Vector3 offset = new Vector3(1f, 0f, 0f);  // Cube가 사용자 오른쪽 1m에 위치
    public float followSpeed = 5f;  // Cube가 이동하는 속도

    void Start()
    {
        // XR Rig의 카메라를 자동으로 찾기
        if (xrCamera == null)
        {
            xrCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        if (xrCamera != null)
        {
            // 목표 위치 계산 (사용자 오른쪽으로 offset 적용)
            Vector3 targetPosition = xrCamera.position +
                                      xrCamera.right * offset.x +
                                      xrCamera.up * offset.y +
                                      xrCamera.forward * offset.z;

            // Cube를 부드럽게 목표 위치로 이동
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Cube가 항상 사용자를 바라보게 설정 (옵션)
            transform.LookAt(xrCamera.position);
        }
    }
}
