using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 70f;
    public Transform playerBody; // 플레이어 본체의 Transform
    public float moveSpeed = 20f;
    private Vector3 moveDirection;
    public float horizonal_rotate_speed = 1f;

    private float horizontal_angle = 0.0f;

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

        if (Input.GetKey(KeyCode.K))
        {
            horizontal_angle -= horizonal_rotate_speed;
        }
        if (Input.GetKey(KeyCode.L))
        {
            horizontal_angle += horizonal_rotate_speed;
        }
        playerBody.rotation = Quaternion.Euler(0f, horizontal_angle, 0f);
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
