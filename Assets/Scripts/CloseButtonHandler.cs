using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloseButtonHandler : MonoBehaviour
{
    public PanelHandler popupWindow;
    public AudioClip soundClip;  // 재생할 소리 클립
    private AudioSource audioSource;  // 오디오 소스 컴포넌트

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
    }


    public void OnButtonClick(){
        var seq = DOTween.Sequence();
        audioSource.clip = soundClip;
        audioSource.Play();  // 소리 재생
        
        //seq.Append(transform.DOScale(0.95f, 0.1f));
        //seq.Append(transform.DOScale(1.05f, 0.1f));
        //seq.Append(transform.DOScale(0.9f, 0.1f));

        seq.Play().OnComplete(() => {
            popupWindow.Hide();
        });
    }
}
