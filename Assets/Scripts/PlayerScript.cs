using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 1000f;
    public Transform playerBody; // �÷��̾� ��ü�� Transform
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
        // ī�޶��� ȸ��
        HandleCameraRotation();

        // �÷��̾��� �̵�
        HandleMovement();
    }

    void HandleCameraRotation()
    {
        // ���콺 �Է��� �޾� X, Y ������ ȸ�� ���� ���
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // �¿� ȸ��
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // ���� ȸ��

        // ���� ȸ���� ��� �� ����
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // ���� ȸ�� ���� ����

        // �¿� ȸ���� ���
        yRotation += mouseX;  // �¿� ȸ���� ���� ����

        // ī�޶��� ���� ȸ�� ����
        playerBody.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    void HandleMovement()
    {
        // �Է°��� ���� �̵� ���� ���
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S

        // �̵� ���� ���� ���
        moveDirection = transform.forward * moveZ + transform.right * moveX;

        // �̵� �������� �̵�
        playerBody.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
