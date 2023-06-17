using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] OrderManager orderManager;
    [SerializeField] PlatesManager platesManager;

    [SerializeField] TMP_Text txtScore;
    [SerializeField] TMP_Text txtTimer;
    [SerializeField] TMP_Text txtCountdown;
    [SerializeField] TMP_Text txtFinalScore;

    [SerializeField] PlayableDirector timeline;

    [SerializeField] List<GameObject> audioPlayers;

    int score;
    int timer;

    public UnityEvent WhenStart;
    public UnityEvent WhenGameOver;
    public UnityEvent WhenRestart;




    RecipeSO currentRecipe;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(StartGameCountDown());
    }

    public void CheckRecipe()
    {
        int ingredientsChecks = 0;
        List<IngredientsSO> ingredientsPlates = new List<IngredientsSO>();

        currentRecipe = orderManager.CurrentRecipe;
        
        foreach(GameObject plate in platesManager.Plates)
        {
            ingredientsPlates.Add(plate.GetComponent<PlateData>().Ingredient);
        }
        
        foreach(IngredientsSO ingredient in currentRecipe.ingredients)
        {
            for(int i = 0; i < ingredientsPlates.Count; i++)
            {
                if (ingredient.ingredientName == ingredientsPlates[i].ingredientName)
                {
                    ingredientsPlates.Remove(ingredient);
                    ingredientsChecks++;
                    break;
                }
            }
            
        }

        if(ingredientsChecks == 3)
        {
            audioPlayers[1].GetComponent<SfxPlayer>().PositiveSfx();
            score += 10;
        }
        else
        {
            audioPlayers[1].GetComponent<SfxPlayer>().NegativeSfx();
            score -= 5;
        }

        orderManager.NewOrder();
        UpdateScore();
    }

    public void RestartGame()
    {
        WhenRestart.Invoke(); 
        timeline.Play();
        StartCoroutine(StartGameCountDown());
        ClearTxt();
        orderManager.ClearOrder();
    }

    private void UpdateScore()
    {
        txtScore.text = $"Score: {score}";
    }

    private void StartGame()
    {
        score = 0;
        timer = 30;
        UpdateScore();
        StartCoroutine(StartTimer());
        orderManager.NewOrder();
        StartCoroutine(StartFade(audioPlayers[0].GetComponent<AudioSource>(), 10f, 0.4f));
        audioPlayers[0].GetComponent<AudioSource>().Play();
    }

    private void GameOver()
    {
        WhenGameOver.Invoke();
        txtFinalScore.text = $"Score: {score}";
        
    }

    private void ClearTxt()
    {
        txtScore.text = "";
        txtTimer.text = "";
    }

    private IEnumerator StartGameCountDown()
    {
        audioPlayers[1].GetComponent<SfxPlayer>().CountdownSfx();
        int startCountDown = 3;
        while (startCountDown > 0)
        {
            txtCountdown.text = startCountDown.ToString();
            yield return new WaitForSeconds(1f);
            startCountDown--;
        }
        StartGame();
        WhenStart.Invoke();
    }

    private IEnumerator StartTimer()
    {
        while (timer > 0)
        {
            int min = timer / 60;
            int sec = timer % 60;
            txtTimer.text = string.Format("{0:00}:{1:00}", min, sec);
            if(timer == 10)
            {
                StartCoroutine(StartFade(audioPlayers[0].GetComponent<AudioSource>(), 10f, 0f));
            }
            yield return new WaitForSeconds(1f);
            timer--;
        }


        GameOver();
    }
    private static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
