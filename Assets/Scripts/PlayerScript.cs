using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 70f;
    public Transform playerBody; // 플레이어 본체의 Transform
    public float moveSpeed = 20f;
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

        // player의 좌우 회전
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);

        Camera camera = this.GetComponentInChildren<Camera>();

        camera.transform.localEulerAngles = new Vector3(xRotation, 0, 0);
    }

    void HandleMovement()
    {
        // 입력값에 따른 이동 벡터 계산
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");   // W/S

        Vector3 m_Movement = new Vector3(horizontal, 0, vertical).normalized;
        this.transform.Translate(m_Movement * Time.deltaTime * moveSpeed);
    }
}
