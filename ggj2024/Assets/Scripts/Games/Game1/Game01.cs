using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Game01 : MonoBehaviourWithContext
{
    [SerializeField] private Stars currentRatingStars;
    [SerializeField] private TMP_Text levelNmbr;

    [SerializeField] private Image characterHead;
    [SerializeField] private Image characterBody;
    [SerializeField] private Image characterLegs;

    [SerializeField] private AvatarSelectionBtn[] avatarBtns;
    [SerializeField] private GameObject clientRatingDialogueBox;
    [SerializeField] private Stars clientRating;

    [SerializeField] private GameObject clientOrder;
    [SerializeField] private GameObject[] tagChecks;
    [SerializeField] private TMP_Text tagsText;
    [SerializeField] private Image clock;
    [SerializeField] private TMP_Text clockText;

    [SerializeField] private Button btnCommitCharacter;
    [SerializeField] private Animator clientFace;

    public UnityEvent<int> OnFinishGame { get; private set; }
    private GameConfig gameConfig;

    private string[] currentLevelTags;
    private float currentLevelTimer = -1;

    private int currentLevel = 0;
    private List<int> ratings = new List<int>();
    private int currentRating = 3;

    private string head;
    private string body;
    private string legs;

    private void Start()
    {
        OnFinishGame = new UnityEvent<int>();
        gameConfig = new GameConfig();
        ratings.Add(3);

        clientRatingDialogueBox.SetActive(false);
        clientOrder.SetActive(false);
    }

    private void BodyPartSelected(string characterId, string part)
    {
        switch (part)
        {
            case "head":
                head = characterId;
                var headSprite = Resources.Load<Sprite>("characters/head_" + head);
                characterHead.sprite = headSprite;
                break;
            case "body":
                body = characterId;
                var bodySprite = Resources.Load<Sprite>("characters/body_" + body);
                characterBody.sprite = bodySprite;
                break;
            case "legs":
                legs = characterId;
                var legsSprite = Resources.Load<Sprite>("characters/legs_" + legs);
                characterLegs.sprite = legsSprite;
                break;
        }

        btnCommitCharacter.enabled = (!string.IsNullOrEmpty(head) && !string.IsNullOrEmpty(body) && !string.IsNullOrEmpty(legs));
    }

    public void LoadAllAvatars()
    {
        List<string> characters = gameConfig.Characters.Keys.ToList();

        for (int i = 0; i < avatarBtns.Length; i++)
        {
            if (i < characters.Count)
            {
                avatarBtns[i].SetCharacterId(characters[i]);
                avatarBtns[i].OnPartSelected.AddListener(BodyPartSelected);
            }
            else
                avatarBtns[i].gameObject.SetActive(false);
        }
    }

    public void StartLevel()
    {
        levelNmbr.text = "Level " + (currentLevel +1).ToString();

        currentLevelTags = gameConfig.GetRandomTags(gameConfig.LevelConfigs[currentLevel].CountTags);
        head = "";
        body = "";
        legs = "";
        btnCommitCharacter.enabled = false;

        var headSprite = Resources.Load<Sprite>("characters/head");
        characterHead.sprite = headSprite;
        var bodySprite = Resources.Load<Sprite>("characters/body");
        characterBody.sprite = bodySprite;
        var legsSprite = Resources.Load<Sprite>("characters/legs");
        characterLegs.sprite = legsSprite;

        StartCoroutine(ClientEnter());
    }

    private IEnumerator ClientEnter()
    {
        //entra cliente
        clientFace.Play("clientFace_waiting");
        yield return new WaitForSeconds(0.3f);

        clientOrder.SetActive(true);
        for (int i = 0; i < tagChecks.Length; i++)
        {
            tagChecks[i].SetActive(false);
        }

        //muestro tags
        tagsText.text = "";
        foreach (var tag in currentLevelTags)
        {
            tagsText.text += "- " + tag + "\n";
            yield return new WaitForSeconds(0.2f);
        }

        currentLevelTimer = 0;
    }

    public void CommitCharacter()
    {
        StartCoroutine(EndLevel());
    }

    private IEnumerator EndLevel()
    {
        int stars = 1;

        if (currentLevelTimer != -1)
        {
            //chequear tiempo restante
            float timeConsumedPerc = clock.fillAmount; //0: todo el tiempo / 1: nada de tiempo

            //chequear cantidad de tags cumplidos
            List<string> currentCharacterTags = new List<string>();
            currentCharacterTags.AddRange(gameConfig.Characters[head]);
            currentCharacterTags.AddRange(gameConfig.Characters[body]);
            currentCharacterTags.AddRange(gameConfig.Characters[legs]);
            var cantTagsVerified = 0;
            for (int i = 0; i < currentLevelTags.Length; i++)
            {
                var tag = currentLevelTags[i];
                if (currentCharacterTags.Contains(tag))
                {
                    tagChecks[i].SetActive(true);
                    cantTagsVerified++;
                }
                else
                    tagChecks[i].SetActive(false);

                yield return new WaitForSeconds(0.5f);
            }

            //chequear cantidad de personajes utilizados
            int cantUsedCharacters = 1;
            if (head != body && head != legs)
            {
                if (body != legs)
                    cantUsedCharacters = 3;
                else
                    cantUsedCharacters = 2;
            }
            else
            {
                if (body != legs)
                    cantUsedCharacters = 2;
            }

            //calcular estrellas
            stars = 3;
        }

        //mostrar expresion del cliente
        clientFace.Play("clientFace_" + stars + "stars");
        yield return new WaitForSeconds(0.3f);

        //mostrar estrellas
        clientRatingDialogueBox.SetActive(true);
        clientRating.UpdateValue(stars);
        yield return new WaitForSeconds(0.3f);

        //actualizar rating general
        ratings.Add(stars);
        UpdateCurrentRating();
        yield return new WaitForSeconds(0.3f);

        //el cliente se va

        currentLevel++;
        if (currentLevel < gameConfig.LevelConfigs.Count)
            StartLevel();
        else
            GameOver();
    }

    private void UpdateCurrentRating()
    {
        currentRating = 0;
        foreach (var r in ratings)
        {
            currentRating += r;
        }
        currentRating = currentRating / ratings.Count;
        currentRatingStars.UpdateValue(currentRating);

        if (currentRating == 1)
            GameOver();
    }

    private void GameOver()
    {
        currentLevelTimer = -1;
        OnFinishGame.Invoke(currentRating);
    }

    private void Update()
    {
        if (currentLevelTimer != -1)
        {
            currentLevelTimer += Time.fixedDeltaTime / 10;
            clock.fillAmount = currentLevelTimer / gameConfig.LevelConfigs[currentLevel].Time;
            clockText.text = currentLevelTimer.ToString();

            if (currentLevelTimer >= gameConfig.LevelConfigs[currentLevel].Time)
            {
                currentLevelTimer = -1;
                StartCoroutine(EndLevel());
            }
        }
    }

}
