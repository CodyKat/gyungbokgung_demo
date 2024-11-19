using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 1000f;
    public Transform playerBody; // 플레이어 본체의 Transform
    public float moveSpeed = 100f;
    private Vector3 moveDirection;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Start()
    {
        playerBody = transform;
    }
    void Update()
    {
        // 카메라의 회전
        HandleCameraRotation();

        // 플레이어의 이동
        HandleMovement();
    }

    void HandleCameraRotation()
    {
        // 마우스 입력을 받아 X, Y 방향의 회전 값을 계산
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // 좌우 회전
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // 상하 회전

        // 상하 회전값 계산 및 제한
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // 상하 회전 각도 제한

        // 좌우 회전값 계산
        yRotation += mouseX;  // 좌우 회전은 제한 없음

        // 카메라의 상하 회전 적용
        playerBody.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    void HandleMovement()
    {
        // 입력값에 따른 이동 벡터 계산
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S

        // 이동 방향 벡터 계산
        moveDirection = transform.forward * moveZ + transform.right * moveX;

        // 이동 방향으로 이동
        playerBody.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
