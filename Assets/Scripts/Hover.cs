using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    [SerializeField] bool isHover;
    [SerializeField] float scaleSpeed = 24;
    [SerializeField]Vector3 scaleFactor = new Vector3(1.3f, 1.3f, 1.3f);
    Vector3 initialScale;
    void Start()
    {
        initialScale = transform.localScale;
    }

    public void OnEnable() {
        isHover = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        isHover = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isHover == true)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scaleFactor, scaleSpeed * Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, scaleSpeed * Time.deltaTime);
        }
    }
}
