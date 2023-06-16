using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    [SerializeField] RecipeSO[] recipes;

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
        int index = Random.Range(0, recipes.Length);

        currentRecipe = recipes[index];
    }

    public void ClearOrder()
    {
        txtNameRecipe.text = "";
        txtIngredients.text = "";
        iconRecipe.sprite = empytSprite;
    }
}
