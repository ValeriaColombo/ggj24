using UnityEngine;

public class GameScreenView : MonoBehaviour
{
    protected GameScreenPresenter presenter;

    [SerializeField] private PausePopUp pausePopUp;
    [SerializeField] private GameOverPopup gameOverPopUp;

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        pausePopUp.Hide();
        gameOverPopUp.Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePopUp.IsShowing)
                pausePopUp.OnCloseButtonClick();
            else if (gameOverPopUp.IsShowing)
                gameOverPopUp.OnCloseButtonClick();
            else
                OnPauseBtnClick();
        }
    }

    public void ShowGameOverPopup(int points)
    {
        gameOverPopUp.Show(points);
        gameOverPopUp.OnPlayAgainButtonClickEvent.AddListener(OnPlayAgain);
        gameOverPopUp.OnCloseButtonClickEvent.AddListener(OnBackToHome);
    }

    public void OnPauseBtnClick()
    {
        presenter.Pause();

        pausePopUp.Show();
        pausePopUp.OnCloseButtonClickEvent.AddListener(OnResume);
        pausePopUp.OnBackToHomeClickEvent.AddListener(OnBackToHome);
    }

    private void OnResume()
    {
        presenter.Resume();

        pausePopUp.OnCloseButtonClickEvent.RemoveAllListeners();
        pausePopUp.OnBackToHomeClickEvent.RemoveAllListeners();
        pausePopUp.Hide();
    }

    private void OnPlayAgain()
    {
        presenter.PlayAgain();
    }

    private void OnBackToHome()
    {
        presenter.ExitGame();
    }
}
