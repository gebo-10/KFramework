using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ButtonClickScale : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    public float ClickUpScale = 1f;
    public float ClickDownScale = 0.9f;

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(ClickUpScale, 0.05f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(ClickDownScale, 0.05f); 
    }
}


