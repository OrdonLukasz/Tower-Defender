using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
public class ScreenEdgesUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(canvasGroup.DOFade(0f, 0.5f));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(canvasGroup.DOFade(0.5f, 0.5f));
    }
}
