using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUserRight : MonoBehaviour
{
    public Transform xrCamera;  // ����� ī�޶�(HMD)�� Transform
    public Vector3 offset = new Vector3(1f, 0f, 0f);  // Cube�� ����� ������ 1m�� ��ġ
    public float followSpeed = 5f;  // Cube�� �̵��ϴ� �ӵ�

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
        if (xrCamera != null)
        {
            // ��ǥ ��ġ ��� (����� ���������� offset ����)
            Vector3 targetPosition = xrCamera.position +
                                      xrCamera.right * offset.x +
                                      xrCamera.up * offset.y +
                                      xrCamera.forward * offset.z;

            // Cube�� �ε巴�� ��ǥ ��ġ�� �̵�
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Cube�� �׻� ����ڸ� �ٶ󺸰� ���� (�ɼ�)
            transform.LookAt(xrCamera.position);
        }
    }
}
