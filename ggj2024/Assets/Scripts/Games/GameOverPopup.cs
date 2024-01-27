using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameOverPopup : BasicPopup
{
    public UnityEvent OnPlayAgainButtonClickEvent { get; private set; }

    [SerializeField] private TMP_Text pointsTxt;

    public virtual void Show(int points)
    {
        pointsTxt.text = MyLocalization.GetLocalizedText("x_points").Replace("{0}", points.ToString());
        base.Show();
    }

    protected override void InitializePopup()
    {
        OnPlayAgainButtonClickEvent = new UnityEvent();
    }

    public void OnPlayAgainButtonClick()
    {
        OnPlayAgainButtonClickEvent.Invoke();
    }
}
