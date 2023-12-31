using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IngredientsShuffle { get => ingredientsShuffle; set => ingredientsShuffle = value; }

    [SerializeField] OrderManager orderManager;
    [SerializeField] PlatesManager platesManager;

    [SerializeField] TMP_Text txtScore;
    [SerializeField] TMP_Text txtTimer;
    [SerializeField] TMP_Text txtCountdown;
    [SerializeField] TMP_Text txtFinalScore;

    [SerializeField] PlayableDirector timeline;

    [SerializeField] List<GameObject> audioPlayers;
    [SerializeField] List<Toggle> toggles;

    int score;
    int timer;

    public UnityEvent WhenStart;
    public UnityEvent WhenGameOver;
    public UnityEvent WhenRestart;

    bool ingredientsOrder = false;
    bool ingredientsShuffle = false;

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

    public void CheckRecipe()
    {
        // Get the ingredients on the plates
        List<IngredientsSO> ingredientsPlates = GetPlatesIngredients();

            // Check how many ingredients match the current recipe
            int ingredientsChecks = CountMatchingIngredients(ingredientsPlates);

            if (ingredientsChecks == 3)
            {
                CorrectRecipe(audioPlayers[1].GetComponent<SfxPlayer>(), 10);
            }
            else
            {
                IncorretRecipe(audioPlayers[1].GetComponent<SfxPlayer>(), 5);
            }

        // Generate a new order
        orderManager.NewOrder();
        UpdateScore();
    }

    // Play positive sound effect and increase score
    private void CorrectRecipe(SfxPlayer player, int points)
    {
        player.PositiveSfx();
        score += points;
    }

    // Play negative sound effect and decrease score
    private void IncorretRecipe(SfxPlayer player, int points)
    {
        player.NegativeSfx();
        score -= points;
    }

    // Retrieve the ingredients on the plates
    private List<IngredientsSO> GetPlatesIngredients()
    {
        List<IngredientsSO> ingredientsPlates = new List<IngredientsSO>();

        foreach (GameObject plate in platesManager.Plates)
        {
            ingredientsPlates.Add(plate.GetComponent<PlateData>().Ingredient);
        }

        return ingredientsPlates;
    }

    // Count how many ingredients in the current recipe match the ingredients on the plates
    private int CountMatchingIngredients(List<IngredientsSO> ingredientsPlates)
    {
        int ingredientsChecks = 0;
        currentRecipe = orderManager.CurrentRecipe;

        if (!ingredientsOrder)
        {
            foreach (IngredientsSO ingredient in currentRecipe.ingredients)
            {
                if (ingredientsPlates.Contains(ingredient))
                {
                    ingredientsPlates.Remove(ingredient);
                    ingredientsChecks++;
                }
            }
        }
        else
        {
            for (int i = 0; i < currentRecipe.ingredients.Length; i++)
            {
                if (ingredientsPlates[i] == currentRecipe.ingredients[i])
                {
                    ingredientsChecks++;
                }
            }
        }
        

        

        return ingredientsChecks;
    }

    // Restart the game
    public void RestartGame()
    {
        WhenRestart.Invoke();
        timeline.Play();
        StartCoroutine(StartGameCountdown());
        ClearTxt();
        orderManager.ClearOrder();
    }

    // Update the score text
    private void UpdateScore()
    {
        txtScore.text = $"Score: {score}";
    }

    // Initialize game variables and start the timer
    private void StartGame()
    {
        score = 0;
        timer = 120;
        UpdateScore();
        StartCoroutine(StartTimer());
        orderManager.NewOrder();
        StartCoroutine(StartFade(audioPlayers[0].GetComponent<AudioSource>(), 10f, 0.4f));
        audioPlayers[0].GetComponent<AudioSource>().Play();
    }

    private void GameOver()
    {
        // Execute game over actions
        WhenGameOver.Invoke();
        txtFinalScore.text = $"Score: {score}";
        StartCoroutine(platesManager.ClearPlates());
    }

    // Clear the score and timer text
    private void ClearTxt()
    {
        txtScore.text = "";
        txtTimer.text = "";
    }

    public void ChangeSettings()
    {
        ingredientsOrder = toggles[0].isOn;
        toggles[1].interactable = toggles[0].isOn;
        if (toggles[0].isOn)
        {
            ingredientsShuffle = toggles[1].isOn;
        }
        else
        {
            ingredientsShuffle = false;
            toggles[1].isOn = false;
        }

    }

    public void CallStartGame()
    {
        StartCoroutine(StartGameCountdown());
    }

    // Show the countdown before starting the game
    private IEnumerator StartGameCountdown()
    {
        ClearTxt();
        orderManager.ClearOrder();
        timeline.Play();
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

    // Start the game timer
    private IEnumerator StartTimer()
    {
        while (timer > 0)
        {
            int min = timer / 60;
            int sec = timer % 60;
            txtTimer.text = string.Format("{0:00}:{1:00}", min, sec);
            if (timer == 10)
            {
                StartCoroutine(StartFade(audioPlayers[0].GetComponent<AudioSource>(), 10f, 0f));
            }
            yield return new WaitForSeconds(1f);
            timer--;
        }
        GameOver();
    }

    // Fade the audio source volume over time
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