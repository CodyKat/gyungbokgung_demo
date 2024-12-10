using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloseButtonHandler : MonoBehaviour
{
    public PanelHandler popupWindow;

    public void OnButtonClick(){
        var seq = DOTween.Sequence();

        //seq.Append(transform.DOScale(0.95f, 0.1f));
        //seq.Append(transform.DOScale(1.05f, 0.1f));
        //seq.Append(transform.DOScale(0.9f, 0.1f));

        seq.Play().OnComplete(() => {
            popupWindow.Hide();
        });
    }
}
