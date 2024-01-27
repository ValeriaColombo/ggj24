using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AvatarSelectionBtn : MonoBehaviour
{
    [SerializeField] private string CharacterId;
    public UnityEvent<string, string> OnPartSelected;

    public void OnPartBtnClick(string part)
    {
        OnPartSelected.Invoke(CharacterId, part);
    } 
}
