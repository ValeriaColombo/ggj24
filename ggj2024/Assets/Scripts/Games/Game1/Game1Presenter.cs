using UnityEngine;
using UnityEngine.SceneManagement;

public class Game1Presenter : GameScreenPresenter
{
    private Game1ScreenView MyView { get { return view as Game1ScreenView; } }
    private Game01 gameplay;

    public Game1Presenter(Game1ScreenView view, Game01 gameplay) : base(view)
    {
        Application.targetFrameRate = 60;

        this.gameplay = gameplay;
        this.gameplay.OnFinishGame.AddListener(OnGameOver);

        MySoundManager.PlayMusicLoop("Sound/music0" + Random.Range(1, 4));

        MyView.ShowTutorial();

        gameplay.LoadAllAvatars();
    }

    private void OnGameOver(int points)
    {
        MyView.ShowGameOverPopup(points);
    }

    public override void PlayAgain()
    {
        SceneManager.LoadScene("Game01");
    }

    public void OnTutorialClosed()
    {
        gameplay.StartLevel();
    }

    public override void Pause()
    {
        gameplay.Pause();
        base.Pause();
    }

    public override void Resume()
    {
        gameplay.Resume();
        base.Resume();
    }
}
