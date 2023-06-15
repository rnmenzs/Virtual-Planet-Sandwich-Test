using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] OrderManager orderManager;
    [SerializeField] PlatesManager platesManager;

    [SerializeField] TMP_Text txtScore;
    [SerializeField] TMP_Text txtTimer;
    [SerializeField] TMP_Text txtCountDown;
    [SerializeField] TMP_Text txtFinalScore;

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
            score += 10;
        }
        else
        {
            score -= 5;
        }

        orderManager.NewOrder();
        UpdateScore();
    }

    public void RestartGame()
    {
        WhenRestart.Invoke();
        StartCoroutine(StartGameCountDown());
    }

    private void UpdateScore()
    {
        txtScore.text = $"Score: {score}";
    }

    private void StartGame()
    {
        score = 0 ;
        timer = 5;
        UpdateScore();
        StartCoroutine(StartTimer());
    }

    private void GameOver()
    {
        WhenGameOver.Invoke();
        txtFinalScore.text = $"Score: {score}";
        
    }

    private IEnumerator StartGameCountDown()
    {
        int startCountDown = 3;
        while (startCountDown > 0)
        {
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
            yield return new WaitForSeconds(1f);
            timer--;
        }

        GameOver();
    }
}
