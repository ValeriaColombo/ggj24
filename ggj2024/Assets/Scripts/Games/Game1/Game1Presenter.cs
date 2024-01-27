using System;
using UnityEngine.SceneManagement;

public class Game1Presenter : GameScreenPresenter
{
    private Game1ScreenView MyView { get { return view as Game1ScreenView; } }
    private Game01 gameplay;

    public Game1Presenter(Game1ScreenView view, Game01 gameplay) : base(view)
    {
        this.gameplay = gameplay;
        this.gameplay.OnFinishGame.AddListener(OnGameOver);

        //MySoundManager.PlayMusicLoop("Sound/music01");

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
}
