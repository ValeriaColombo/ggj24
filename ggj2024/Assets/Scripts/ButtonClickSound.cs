using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviourWithContext
{
    [SerializeField] private AudioClip[] audioClips;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        MySoundManager.PlaySfxSound(audioClips[randomIndex]);
    }
}
