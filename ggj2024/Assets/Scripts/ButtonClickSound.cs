using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviourWithContext
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        int randomIndex = Random.Range(0, 6);
        MySoundManager.PlaySfxSound("Sound/click_" + randomIndex);
    }
}
