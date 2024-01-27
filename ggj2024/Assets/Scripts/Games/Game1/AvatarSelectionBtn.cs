using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AvatarSelectionBtn : MonoBehaviour
{
    [SerializeField] private Image avatarImg;

    private string characterId;
    public UnityEvent<string, string> OnPartSelected;

    public void OnPartBtnClick(string part)
    {
        OnPartSelected.Invoke(characterId, part);
    }

    public void SetCharacterId(string id)
    {
        characterId = id;
        var avatarSprite = Resources.Load<Sprite>("characters/avatar_full_" + id);

        avatarImg.sprite = avatarSprite;
    }
}
