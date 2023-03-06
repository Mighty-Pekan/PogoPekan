using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerEnterHandler
{
    public Image Image;
    public Sprite spriteRed;
    public Sprite spriteWhite;

    public void OnPointerExit(PointerEventData eventData)
    {
        Image.sprite = spriteWhite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Image.sprite = spriteRed;
    }

    public void OnSelect(BaseEventData eventData)
    {
        Image.sprite = spriteRed;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Image.sprite = spriteWhite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Image.sprite = spriteRed;
    }
}
