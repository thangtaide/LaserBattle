using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChoseButton : MonoBehaviour, IPointerClickHandler, ISubmitHandler, ISelectHandler
{
    [SerializeField] private TMP_Text button;

    [SerializeField] private ChoseButtonEvent _onSelectEvent;
    [SerializeField] private ChoseButtonEvent _onSubmitEvent;
    [SerializeField] private ChoseButtonEvent _onClickEvent;

    public ChoseButtonEvent OnSelectEvent { get => _onSelectEvent; set => _onSelectEvent = value; }
    public ChoseButtonEvent OnSubmitEvent { get => _onSubmitEvent; set => _onSubmitEvent = value; }
    public ChoseButtonEvent OnClickEvent { get => _onClickEvent; set => _onClickEvent = value; }
    public string ButtonName { get => button.text; set => button.text = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClickEvent.Invoke(this);
    }

    public void OnSelect(BaseEventData eventData)
    {
        _onSelectEvent.Invoke(this);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        _onSubmitEvent.Invoke(this);
    }
    public void ObtainSelectionFucus()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        _onSelectEvent.Invoke(this);
    }
}

[System.Serializable] public class ChoseButtonEvent: UnityEvent<ChoseButton>
{

}
