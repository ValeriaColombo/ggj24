using System;
using UnityEngine.SceneManagement;

public class GameScreenPresenter : ClassWithContext
{
    protected GameScreenView view;
    protected bool isPaused;

    public GameScreenPresenter(GameScreenView view)
    {
        this.view = view;
        isPaused = false;
    }

    public virtual void Pause()
    {
        isPaused = true;
        MySoundManager.PauseAll();
    }

    public virtual void Resume()
    {
        isPaused = false;
        MySoundManager.ResumeAll();
    }

    public virtual void ExitGame()
    {
        SceneManager.LoadScene("Home");
    }

    public virtual void PlayAgain()
    {
        throw new NotImplementedException();
    }
}
