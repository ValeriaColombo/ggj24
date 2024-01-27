using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenPresenter : ClassWithContext
{
    private HomeScreenView view;

    public HomeScreenPresenter(HomeScreenView view)
    {
        this.view = view;
        MySoundManager.PlayMusicLoop("Sound/music0" + Random.Range(1,4));
    }

    public void GoToPlayGame(string gameId)
    {
        switch (gameId)
        {
            case "game1":
                SceneManager.LoadScene("Game01");
                break;
        }
    }
}
