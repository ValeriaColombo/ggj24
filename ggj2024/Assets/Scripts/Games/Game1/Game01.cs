
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
    [SerializeField] private Animator client;

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
        isPaused = true;

        OnFinishGame = new UnityEvent<int>();
        gameConfig = new GameConfig();
        ratings.Add(3);

        clientRatingDialogueBox.SetActive(false);
        clientOrder.SetActive(false);
        clock.fillAmount = 0;
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
                avatarBtns[i].OnPartSelected.AddListener(BodyPartSelected);
            }
            else
                avatarBtns[i].gameObject.SetActive(false);
        }
    }

    public void StartLevel()
    {
        levelNmbr.text = MyLocalization.GetLocalizedText("nivel") + " " + (currentLevel +1).ToString();

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

        isPaused = false;
        StartCoroutine(ClientEnter());
    }

    private IEnumerator ClientEnter()
    {
        //entra cliente
        client.Play("client_enter");
        clientFace.Play("clientFace_waiting");
        yield return new WaitForSeconds(1f);

        clientOrder.SetActive(true);
        for (int i = 0; i < tagChecks.Length; i++)
        {
            tagChecks[i].SetActive(false);
        }

        //muestro tags
        tagsText.text = "";
        foreach (var tag in currentLevelTags)
        {
            tagsText.text += "- " + MyLocalization.GetLocalizedText("tag_" + tag) + "\n";
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
            currentLevelTimer = -1;

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

            stars = StarsByTags(currentLevelTags.Length, cantTagsVerified);
            Debug.LogFormat("Lavel {3}; Quiero {0} tags, consegui {1} --> {2} estrellas", currentLevelTags.Length, cantTagsVerified, stars, currentLevel +1);

            if (timeConsumedPerc > 0.5f)
            {
                if (timeConsumedPerc > 0.75f)
                {
                    stars -= 2;
                    Debug.LogFormat("Lavel {0}; Casi que se queda sin tiempo, descuenta 2 estrellas, le quedan {1}", currentLevel + 1, stars);
                }
                else
                {
                    stars -= 1;
                    Debug.LogFormat("Lavel {0}; Casi que se queda sin tiempo, descuenta 2 estrella, le quedan {1}", currentLevel + 1, stars);
                }
            }

            //chequear cantidad de personajes utilizados
            int cantUsedCharacters = 1;
            if (head != body && head != legs)
            {
                if (body != legs)
                    cantUsedCharacters = 3;
                else
                {
                    cantUsedCharacters = 2;
                }
            }
            else
            {
                if (body != legs)
                    cantUsedCharacters = 2;
            }

            if (cantUsedCharacters == 2)
            {
                stars -= 1;
                Debug.LogFormat("Level {0}; uso solo 2 personajes --> resto 1 estrella, quedan {1}", currentLevel + 1, stars);
            }
            else if (cantUsedCharacters == 1)
            {
                Debug.LogFormat("Level {0}; uso un solo personaje --> 1 estrella", currentLevel + 1);
                stars = 1;
            }
        }

        stars = System.Math.Max(1, stars); //verifico que no me hayan quedado estrellas negativas

        //mostrar expresion del cliente
        MySoundManager.PlaySfxSound(GetClientSoundByStars(stars));
        clientFace.Play("clientFace_" + stars + "stars");
        yield return new WaitForSeconds(1.3f);

        //mostrar estrellas
        clientRatingDialogueBox.SetActive(true);
        clientRating.UpdateValue(stars);
        MySoundManager.PlaySfxSound("Sound/rate");
        yield return new WaitForSeconds(0.8f);

        //actualizar rating general
        ratings.Add(stars);
        UpdateCurrentRating();
        yield return new WaitForSeconds(1f);

        //el cliente se va
        clientOrder.SetActive(false);
        clientRatingDialogueBox.SetActive(false);
        client.Play("client_exit");
        yield return new WaitForSeconds(1f);

        currentLevel++;
        if (currentLevel < gameConfig.LevelConfigs.Count)
            StartLevel();
        else
            GameOver();
    }

    private string GetClientSoundByStars(int stars)
    {
        switch (stars)
        {
            case 1:
                return "Sound/client_1star_" + Random.Range(0, 5);
            case 2:
                return "Sound/client_2star_" + Random.Range(0, 3);
            case 3:
                return "Sound/client_3star_" + Random.Range(0, 3);
            case 4:
                return "Sound/client_4star_" + Random.Range(0, 3);
            default: //case 5:
                return "Sound/client_5star_" + Random.Range(0, 6);
        }
    }

    private int StarsByTags(int desiredTags, int cantTagsVerified)
    {
        switch (desiredTags)
        {
            case 1:
                switch (cantTagsVerified)
                {
                    case 1:
                        return 5;
                    default: //case 0
                        return 1;
                }
            case 2:
                switch (cantTagsVerified)
                {
                    case 2:
                        return 5;
                    case 1:
                        return 3;
                    default: //case 0
                        return 1;
                }
            case 3:
                switch (cantTagsVerified)
                {
                    case 3:
                        return 5;
                    case 2:
                        return 4;
                    case 1:
                        return 3;
                    default: //case 0
                        return 1;
                }
            case 4:
                switch (cantTagsVerified)
                {
                    case 4:
                        return 5;
                    case 3:
                        return 4;
                    case 2:
                        return 3;
                    case 1:
                        return 2;
                    default: //case 0
                        return 1;
                }
            default: //case 5
                switch (cantTagsVerified)
                {
                    case 5:
                        return 5;
                    case 4:
                        return 4;
                    case 3:
                        return 3;
                    case 2:
                        return 2;
                    case 1:
                        return 2;
                    default: //case 0
                        return 1;
                }
        }
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
        if (!isPaused)
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

    private bool isPaused;
    public virtual void Pause()
    {
        isPaused = true;
    }

    public virtual void Resume()
    {
        isPaused = false;
    }

}
