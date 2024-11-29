using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUserRight : MonoBehaviour
{
    private Transform xrCamera;  // 사용자 카메라(HMD)의 Transform
    private float stopRadius = 4f;
    private float slowRadius = 10f;
    private float petyPos;
    public float longTermfollowSpeed = 3f;  // Cube가 이동하는 속도

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
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(xrCamera.position.x, 0, xrCamera.position.z));
        if (xrCamera != null)
        {
            float speed = longTermfollowSpeed;
            if (distance < slowRadius)
            {
                speed *= Mathf.Clamp01((distance - stopRadius) / (slowRadius - stopRadius));
            }
            if (distance > stopRadius)
            {
                petyPos = xrCamera.position.y - 2f;
                Vector3 targetPosition = new Vector3(xrCamera.position.x, petyPos, xrCamera.position.z);
                // Cube를 부드럽게 목표 위치로 이동
                transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            }
        }
    }
}
