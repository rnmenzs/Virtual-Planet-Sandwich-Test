using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateData : MonoBehaviour
{
    [SerializeField]
    private IngredientsSO ingredient;
    
    public IngredientsSO Ingredient 
    { 
        get { return ingredient; } 
        set { ingredient = value; } 
    }

    [SerializeField] Image ingredientIcon;

    public void AddIngredient()
    {
        Debug.Log(ingredient.ingredientName);
    }
}
