using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenView : MonoBehaviour
{
    private HomeScreenPresenter presenter;

    [SerializeField] private SettingsPopUp settingsPopUp;
    [SerializeField] private CreditsPopup creditsPopUp;

    void Start()
    {
        presenter = new HomeScreenPresenter(this);
        settingsPopUp.Hide();
        creditsPopUp.Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPopUp.IsShowing)
                settingsPopUp.OnCloseButtonClick();
            else if (creditsPopUp.IsShowing)
                creditsPopUp.OnCloseButtonClick();
            else
                CloseApp();
        }
    }

    public void PlayGame(string gameId)
    {
        presenter.GoToPlayGame(gameId);
    }

    public void ShowSettingsPopup()
    {
        settingsPopUp.Show();
        settingsPopUp.OnCloseButtonClickEvent.AddListener(CloseSettings);
    }
    public void ShowCreditsPopup()
    {
        creditsPopUp.Show();
        creditsPopUp.OnCloseButtonClickEvent.AddListener(CloseCredits);
    }

    private void CloseSettings()
    {
        settingsPopUp.Hide();
    }

    private void CloseCredits()
    {
        creditsPopUp.Hide();
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
