using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameOverPopup : BasicPopup
{
    public UnityEvent OnPlayAgainButtonClickEvent { get; private set; }

    [SerializeField] private Stars stars;
    [SerializeField] private TMP_Text finaleDesc;

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
                return "Sound/gameover_bad";
            case 2:
                return "Sound/gameover_medium";
            case 3:
                return "Sound/gameover_medium";
            case 4:
                return "Sound/gameover_good_" + Random.Range(0,2);
            default: //case 5:
                return "Sound/gameover_good_" + Random.Range(0, 2);
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
