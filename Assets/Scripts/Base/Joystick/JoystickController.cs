using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 Direction = Vector2.zero;
    public RectTransform joystick;
    public RectTransform handle;
    public bool isDragging = false;
    float radius;
    Vector3 originPos = Vector3.zero;

    private void Awake()
    {
        originPos = joystick.position;
        radius = joystick.rect.width / 2;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        joystick.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Direction = eventData.position - (Vector2)joystick.position;
        float distance = Vector2.Distance(joystick.position, eventData.position);
        if (distance > radius)
        {
            handle.localPosition = Direction.normalized * radius;
        }
        else
        {
            handle.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        handle.localPosition = Vector2.zero;
        Direction = Vector2.zero;
        joystick.position = originPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }
}
