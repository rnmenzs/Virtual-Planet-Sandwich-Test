using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesManager : MonoBehaviour
{
    [SerializeField] GameObject[] plates;

    public void AddOnPlate(IngredientsSO ingredient)
    {
        foreach (var plate in plates)
        {
            if(plate.GetComponent<PlateData>().Ingredient == null) 
            {
                plate.GetComponent<PlateData>().Ingredient = ingredient;
                plate.GetComponent<PlateData>().AddIngredient();
                break;
            }
        }
    }
}
