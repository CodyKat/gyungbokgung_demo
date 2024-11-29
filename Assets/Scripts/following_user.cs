using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUserRight : MonoBehaviour
{
    private Transform xrCamera;  // ����� ī�޶�(HMD)�� Transform
    private float stopRadius = 4f;
    private float slowRadius = 10f;
    private float petyPos;
    public float longTermfollowSpeed = 3f;  // Cube�� �̵��ϴ� �ӵ�

    void Start()
    {
        // XR Rig�� ī�޶� �ڵ����� ã��
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
                // Cube�� �ε巴�� ��ǥ ��ġ�� �̵�
                transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            }
        }
    }
}
