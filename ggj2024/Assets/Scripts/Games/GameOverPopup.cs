using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameOverPopup : BasicPopup
{
    public UnityEvent OnPlayAgainButtonClickEvent { get; private set; }

    [SerializeField] private Stars stars;

    public virtual void Show(int points)
    {
        MySoundManager.PlaySfxSound(SoundByStars(points));
        stars.UpdateValue(points);
        base.Show();
    }

    private string SoundByStars(int points)
    {
        switch (points)
        {
            case 1:
                return "TODOOO";
            case 2:
                return "TODOOO";
            case 3:
                return "TODOOO";
            case 4:
                return "TODOOO";
            default: //case 5:
                return "TODOOO";
        }
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
