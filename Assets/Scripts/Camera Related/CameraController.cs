using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void KillTween()
    {
        DOTween.KillAll();
    }

    public void ChangeCameraPosition(Vector3 rotation,Vector3 position , float orthoSize, float duration)
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(mainCamera.DOOrthoSize(orthoSize, duration));
        mySequence.Join(mainCamera.transform.DORotate(rotation, duration));
        mySequence.Join(mainCamera.transform.DOMove(position, duration));
    }
}
