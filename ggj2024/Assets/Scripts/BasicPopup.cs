using UnityEngine;
using UnityEngine.Events;

public class BasicPopup : MonoBehaviourWithContext
{
    public UnityEvent OnCloseButtonClickEvent { get; private set; }

    public bool IsShowing { get; private set; }

    private void Awake()
    {
        OnCloseButtonClickEvent = new UnityEvent();
        InitializePopup();
    }

    protected virtual void InitializePopup()
    {

    }

    public virtual void Show()
    {
        IsShowing = true;
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        IsShowing = false;
    }

    public void OnCloseButtonClick()
    {
        OnCloseButtonClickEvent.Invoke();
    }
}
