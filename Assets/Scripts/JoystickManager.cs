using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Player player;
    public JoystickValue value;
    RectTransform rect;
    Vector2 touch = Vector2.zero;
    public RectTransform handle;
    float rectWidth;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        rectWidth = rect.sizeDelta.x * 0.5f;
    }
    public void OnDrag(PointerEventData eventData)
    {
        touch = (eventData.position - rect.anchoredPosition) / rectWidth;
        if (touch.magnitude > 1)
            touch = touch.normalized;
        value.joyTouch = touch;
        //new Vector2(touch.x * player.Speed * Time.deltaTime, touch.y * player.Speed * Time.deltaTime) ;
        //handle.anchoredPosition = touch * rectWidth;
        handle.anchoredPosition = touch * (rectWidth/2);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        value.joyTouch = Vector2.zero;
        player.keyReset();
        handle.anchoredPosition = Vector2.zero;
    } 
}