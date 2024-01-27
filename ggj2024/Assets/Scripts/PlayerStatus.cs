using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int SoftCurrency { get; private set; }
    public int HardCurrency { get; private set; }
    public int XP { get; private set; }

    private const string Key_SoftCurrency = "soft_currency";
    private const string Key_HardCurrency = "hard_currency";
    private const string Key_XP = "xp";

    private void Start()
    {
        if (PlayerPrefs.HasKey(Key_SoftCurrency))
            SoftCurrency = PlayerPrefs.GetInt(Key_SoftCurrency);
        else
            SoftCurrency = 0;
        
        if (PlayerPrefs.HasKey(Key_HardCurrency))
            HardCurrency = PlayerPrefs.GetInt(Key_HardCurrency);
        else
            HardCurrency = 0;

        if (PlayerPrefs.HasKey(Key_XP))
            XP = PlayerPrefs.GetInt(Key_XP);
        else
            XP = 0;
    }

    private void PersistSoftCurrency()
    {
        PlayerPrefs.SetInt(Key_SoftCurrency, SoftCurrency);
        PlayerPrefs.Save();
    }

    private void PersistHardCurrency()
    {
        PlayerPrefs.SetInt(Key_HardCurrency, HardCurrency);
        PlayerPrefs.Save();
    }

    private void PersistXP()
    {
        PlayerPrefs.SetInt(Key_XP, XP);
        PlayerPrefs.Save();
    }

    public void AddSoftCurrency(int amountToAdd)
    {
        SoftCurrency += amountToAdd;
        PersistSoftCurrency();
    }

    public bool SpendSoftCurrency(int amountToSpend)
    {
        if (SoftCurrency >= amountToSpend)
        {
            SoftCurrency -= amountToSpend;
            PersistSoftCurrency();
            return true;
        }
        else
        {
            Debug.Log("Not enough soft currency to spend");
            return false;
        }
    }

    public void AddHardCurrency(int amountToAdd)
    {
        HardCurrency += amountToAdd;
        PersistHardCurrency();
    }

    public bool SpendHardCurrency(int amountToSpend)
    {
        if (HardCurrency >= amountToSpend)
        {
            HardCurrency -= amountToSpend;
            PersistHardCurrency();
            return true;
        }
        else
        {
            Debug.Log("Not enough hard currency to spend");
            return false;
        }
    }

    public void AddXP(int amountToAdd)
    {
        XP += amountToAdd;
        PersistXP();
    }
}
