using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class WalkSound : MonoBehaviour
{
    public AudioClip soundClip;  // 재생할 소리 클립
    private AudioSource audioSource;  // 오디오 소스 컴포넌트
    private InputDevice leftController;  // 왼쪽 컨트롤러 InputDevice
    private bool isLeftThumbstickMoving = false;  // 스틱이 움직이는지 체크

    void Start()
    {
        // AudioSource 컴포넌트를 추가하거나 기존의 것을 참조합니다.
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 소리 재생을 시작하지 않도록 초기화
        audioSource.Stop();  // 초기화 시 소리 중지

        // 왼쪽 컨트롤러 InputDevice 얻기
        GetLeftController();
    }

    void Update()
    {
        if (leftController.isValid)
        {
            // 왼쪽 컨트롤러 스틱 입력 값 확인
            Vector2 thumbstickInput;
            if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out thumbstickInput))
            {
                // 스틱이 움직일 때 소리 재생
                if (Mathf.Abs(thumbstickInput.x) > 0.1f || Mathf.Abs(thumbstickInput.y) > 0.1f)
                {
                    if (!isLeftThumbstickMoving)
                    {
                        isLeftThumbstickMoving = true;

                        // 소리가 이미 재생 중이지 않으면 소리 시작
                        if (!audioSource.isPlaying)
                        {
                            audioSource.loop = true;  // 반복 재생 설정
                            audioSource.clip = soundClip;
                            audioSource.Play();  // 소리 재생
                        }
                    }
                }
                else
                {
                    // 스틱이 멈추면 소리 중지
                    if (isLeftThumbstickMoving)
                    {
                        isLeftThumbstickMoving = false;

                        // 소리 중지 및 반복 설정 해제
                        audioSource.Stop();  // 소리 중지
                        audioSource.loop = false;  // 반복 재생 해제
                    }
                }
            }
        }
        else
        {
            // Debug.LogWarning("Left controller is not valid!");  // 디버깅 메시지 추가
            GetLeftController();  // 매 프레임마다 왼쪽 컨트롤러를 갱신
        }
    }

    // 왼쪽 컨트롤러를 찾는 메서드
    private void GetLeftController()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, inputDevices);

        if (inputDevices.Count > 0)
        {
            leftController = inputDevices[0];
            Debug.Log("Left Controller found: " + leftController.name);  // 디버깅 메시지
        }
        else
        {
            // Debug.LogError("No Left Controller found.");  // 오류 메시지
            // 모든 연결된 XR 장치 출력
            List<InputDevice> allDevices = new List<InputDevice>();
            InputDevices.GetDevices(allDevices);
            foreach (var device in allDevices)
            {
                Debug.Log("Device found: " + device.name);  // 모든 장치 리스트 출력
            }
        }
    }
}
