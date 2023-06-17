using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    private List<RecipeSO> shuffledRecipes;
    private IngredientsSO[] shuffledIngredients;

    public RecipeSO CurrentRecipe { get { return currentRecipe; } }
    
    [SerializeField] RecipeSO[] recipes;

    [SerializeField] RecipeSO currentRecipe;


    [SerializeField] TMP_Text txtNameRecipe;
    [SerializeField] Image iconRecipe;
    [SerializeField] TMP_Text txtIngredients;

    [SerializeField] Image[] iconIngredients;

    [SerializeField] Sprite empytSprite;

    //show the new recipe on order display
    public void NewOrder()
    {
        RandomRecipe();
        txtNameRecipe.text = currentRecipe.recipeName;
        iconRecipe.sprite = currentRecipe.recipeIcone;
        string ingredients = "";

        for (int i = 0; i < currentRecipe.ingredients.Length; i++)
        {
            if (i != 2){
                ingredients += currentRecipe.ingredients[i].name + "\n\n";
            }
            else
            {
                ingredients += currentRecipe.ingredients[i].name;
            }

            iconIngredients[i].sprite = currentRecipe.ingredients[i].ingredientIcon;
        }
        txtIngredients.text = ingredients;
    }

    //choose a random recipe
    void RandomRecipe()
    {
        ShuffleRecipes();
        
        int i = Random.Range(0, recipes.Length - 1);

        currentRecipe = shuffledRecipes[i];

        if (GameManager.Instance.IngredientsShuffle)
        {
            ShuffleIngredients();
        }
    }

    public void ClearOrder()
    {
        txtNameRecipe.text = "";
        txtIngredients.text = "";
        iconRecipe.sprite = empytSprite;
        for(int i = 0; i < iconIngredients.Length; i++)
        {
            iconIngredients[i].sprite = empytSprite;
        }
    }


    //Fisher–Yates shuffle
    private void ShuffleRecipes()
    {

        shuffledRecipes = new List<RecipeSO>(recipes);
        int i = shuffledRecipes.Count;
        while (i > 1)
        {
            i--;
            int j = Random.Range(0, i + 1);
            RecipeSO value = shuffledRecipes[j];
            shuffledRecipes[j] = shuffledRecipes[i];
            shuffledRecipes[i] = value;
        }
    }

    void ShuffleIngredients()
    {
        shuffledIngredients = currentRecipe.ingredients;
        int i = currentRecipe.ingredients.Length;
        while (i > 1)
        {
            i--;
            int j = Random.Range(0, i + 1);
            IngredientsSO value = shuffledIngredients[j];
            shuffledIngredients[j] = shuffledIngredients[i];
            shuffledIngredients[i] = value;
        }
        currentRecipe.ingredients = shuffledIngredients;
    }
}
