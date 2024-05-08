using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class InformationManager : MonoSingleton<InformationManager>
{
    public delegate void CloseAlert();
    public event CloseAlert AlertClosed;

    public static InformationManager _Instance { get; private set; }
    public CanvasGroup canvasGroup;
    public TMP_Text alertMessage;

    public void ConfirmAlert()
    {
        CloseAlertMessage();
        if (AlertClosed != null)
        {
            AlertClosed.Invoke();
        }
        AlertClosed = null;
    }

    public void CloseAlertMessage()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(canvasGroup.DOFade(0f, 1f));
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void OpenAlert(string message)
    {
        alertMessage.text = message;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(canvasGroup.DOFade(1f, 1f));
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(canvasGroup.gameObject.GetComponent<RectTransform>());
    }
}
