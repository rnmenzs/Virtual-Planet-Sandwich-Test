using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] OrderManager orderManager;
    [SerializeField] PlatesManager platesManager;

    int ingredientsChecks = 0;

    public int score = 0;
    

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

        Debug.Log(score);
    }
}
