using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderDisplay : MonoBehaviour
{
    [SerializeField] RecipeSO[] recipes;

    [SerializeField] RecipeSO currentRecipe;

    [SerializeField] TMP_Text nameRecipe;
    [SerializeField] Image iconRecipe;
    [SerializeField] TMP_Text ingredients;

    private void Start()
    {
        RandomRecipe();
        NewOrder();
    }

    private void NewOrder()
    {
        nameRecipe.text = currentRecipe.recipeName;
        iconRecipe.sprite = currentRecipe.recipeIcone;
        string ingredients = "";
        foreach (var i in currentRecipe.ingredients)
        {
            ingredients += i.name + "\n";
        }
        this.ingredients.text = ingredients;
    }

    void RandomRecipe()
    {
        int index = Random.Range(0, recipes.Length);

        currentRecipe = recipes[index];
    }
}
