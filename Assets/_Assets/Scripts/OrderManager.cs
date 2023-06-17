using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    [SerializeField] RecipeSO[] recipes;

    [SerializeField]
    private List<RecipeSO> shuffledRecipes;

    [SerializeField] RecipeSO currentRecipe;

    public RecipeSO CurrentRecipe { get { return currentRecipe; } }

    [SerializeField] TMP_Text txtNameRecipe;
    [SerializeField] Image iconRecipe;
    [SerializeField] TMP_Text txtIngredients;

    [SerializeField] Sprite empytSprite;

    //show the new recipe on order display
    public void NewOrder()
    {
        RandomRecipe();
        txtNameRecipe.text = currentRecipe.recipeName;
        iconRecipe.sprite = currentRecipe.recipeIcone;
        string ingredients = "";
        foreach (var i in currentRecipe.ingredients)
        {
            ingredients += i.name + "\n";
        }
        txtIngredients.text = ingredients;
    }

    //choose a random recipe
    void RandomRecipe()
    {
        ShuffleRecipes();
        int i = Random.Range(0, recipes.Length - 1);

        currentRecipe = shuffledRecipes[i];
    }

    public void ClearOrder()
    {
        txtNameRecipe.text = "";
        txtIngredients.text = "";
        iconRecipe.sprite = empytSprite;
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
}
