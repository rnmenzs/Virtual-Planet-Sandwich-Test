using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "SO/Recipe")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public Sprite recipeIcone;
    public IngredientsSO[] ingredients;
}
