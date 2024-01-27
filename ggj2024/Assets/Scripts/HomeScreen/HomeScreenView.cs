using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenView : MonoBehaviourWithContext
{
    private HomeScreenPresenter presenter;

    [SerializeField] private SettingsPopUp settingsPopUp;
    [SerializeField] private CreditsPopup creditsPopUp;

    [SerializeField] private GameObject ENicon;
    [SerializeField] private GameObject ESicon;

    void Start()
    {
        presenter = new HomeScreenPresenter(this);
        settingsPopUp.Hide();
        creditsPopUp.Hide();

        ENicon.SetActive(MyLocalization.CurrentLang == "EN");
        ESicon.SetActive(MyLocalization.CurrentLang == "ES");
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

    public void OnLangButtonClick()
    {
        if (MyLocalization.CurrentLang == "EN")
            MyLocalization.SetLanguage("ES");
        else
            MyLocalization.SetLanguage("EN");

        ENicon.SetActive(MyLocalization.CurrentLang == "EN");
        ESicon.SetActive(MyLocalization.CurrentLang == "ES");
    }
}
