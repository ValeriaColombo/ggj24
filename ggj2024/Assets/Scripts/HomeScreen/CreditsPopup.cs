using UnityEngine;

public class CreditsPopup : BasicPopup
{
    [SerializeField] private GameObject es;
    [SerializeField] private GameObject en;

    public override void Show()
    {
        base.Show();
        en.SetActive(MyLocalization.CurrentLang == "EN");
        es.SetActive(MyLocalization.CurrentLang == "ES");
    }
}
