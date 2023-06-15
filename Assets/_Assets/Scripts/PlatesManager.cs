using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatesManager : MonoBehaviour
{
    [SerializeField] GameObject[] plates;

    public GameObject[] Plates
    {
        get { return plates; }
    }

    public UnityEvent platesFull;

    int platesCount = 0;

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
        platesCount++;
        if(platesCount == plates.Length)
        {
            platesFull.Invoke();
            foreach (var plate in plates)
            {
                plate.GetComponent<PlateData>().Ingredient = null;
                plate.GetComponent<PlateData>().ClearPlate();
            }

            platesCount = 0;
        }
    }
}
