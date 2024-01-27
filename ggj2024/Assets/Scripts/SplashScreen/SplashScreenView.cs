using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenView : MonoBehaviour
{
    [SerializeField] private List<Sprite> logos;
    [SerializeField] private Image logoView;
    [SerializeField] private float logoDurationInSeconds = 1;
    [SerializeField] private float logoFadeSpeed = 0.5f;

    void Start()
    {
        logoView.color = new Color(255, 255, 255, 255);
        StartCoroutine(ShowLogos());
    }

    private IEnumerator ShowLogos()
    {
        foreach(var logoSprite in logos)
        {
            logoView.sprite = logoSprite;

            float alpha = 0;
            while (alpha < 1)
            {
                alpha += logoFadeSpeed * Time.fixedDeltaTime;
                logoView.color = new Color(255, 255, 255, alpha);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(logoDurationInSeconds);
            while (alpha > 0)
            {
                alpha -= logoFadeSpeed * Time.fixedDeltaTime;
                logoView.color = new Color(255, 255, 255, alpha);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(1);
        }

        SceneManager.LoadScene("Home");
    }
}
