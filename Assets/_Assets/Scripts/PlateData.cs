using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateData : MonoBehaviour
{
    [SerializeField]
    private IngredientsSO ingredient;
    [SerializeField] Image ingredientIcon;
    [SerializeField] Sprite empytSprite;


    public IngredientsSO Ingredient 
    { 
        get { return ingredient; } 
        set { ingredient = value; } 
    }

    public void AddIngredient()
    {
        ingredientIcon.sprite = this.ingredient.ingredientIcon;
    }

    public void ClearPlate()
    {
        ingredientIcon.sprite = empytSprite;
    }
}
