using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class TurretStatsWindow : MonoSingleton<TurretStatsWindow>
{
    public static TurretStatsWindow _Instance { get; private set; }

    public RectTransform rectTransform;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text rateText;
    [SerializeField] private TMP_Text powerText;
    [SerializeField] private TMP_Text rangeText;

    private bool tweenComplete;

    public void ShowWindow(string name, string rate, string power, string range, Vector2 position)
    {
        nameText.text = $"{name}";
        rateText.text = $"Speed: {rate}/s";
        powerText.text = $"Power: {power}";
        rangeText.text = $"Range: {range}";
        rectTransform.position = position;
        Sequence openSequence = DOTween.Sequence();
        if (!tweenComplete)
        {
            openSequence.Join(DOVirtual.DelayedCall(0.5f, () => tweenComplete = false));
        }
        openSequence.Append(rectTransform.DOSizeDelta(new Vector2(80, 50), 0.5f));
        openSequence.Join(rectTransform.gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.5f)).OnComplete(() => { tweenComplete = true; });
    }

    public void HideWindow()
    {
        Sequence closeSequence = DOTween.Sequence();
        if (!tweenComplete)
        {
            closeSequence.Join(DOVirtual.DelayedCall(0.5f, () => tweenComplete = false));
        }
        closeSequence.Append(rectTransform.DOSizeDelta(new Vector2(80, 0), 0.5f));
        closeSequence.Join(rectTransform.gameObject.GetComponent<CanvasGroup>().DOFade(0, 0.5f)).OnComplete(() => { tweenComplete = true; });
    }
}
