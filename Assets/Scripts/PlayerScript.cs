using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 70f;
    public Transform playerBody; // �÷��̾� ��ü�� Transform
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

        // player�� �¿� ȸ��
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);

        Camera camera = this.GetComponentInChildren<Camera>();

        camera.transform.localEulerAngles = new Vector3(xRotation, 0, 0);
    }

    void HandleMovement()
    {
        // �Է°��� ���� �̵� ���� ���
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");   // W/S

        Vector3 m_Movement = new Vector3(horizontal, 0, vertical).normalized;
        this.transform.Translate(m_Movement * Time.deltaTime * moveSpeed);
    }
}
