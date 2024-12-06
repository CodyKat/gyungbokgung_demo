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

        // 왼쪽 컨트롤러 InputDevice 얻기
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, inputDevices);
        if (inputDevices.Count > 0)
        {
            leftController = inputDevices[0];
        }
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

                        // 소리가 재생 중인지 확인
                        if (!audioSource.isPlaying)
                        {
                            audioSource.PlayOneShot(soundClip);  // 소리 재생
                        }
                    }
                }
                else
                {
                    isLeftThumbstickMoving = false;
                }
            }
        }
    }
}
