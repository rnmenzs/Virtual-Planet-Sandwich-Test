using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonData: MonoBehaviour
{
    [SerializeField]
    IngredientsSO ingredient;

    [SerializeField] Image ingredientIcon;
    [SerializeField] TMP_Text ingredientName;


    private void Start()
    {
        ingredientIcon.sprite = ingredient.ingredientIcon;
        ingredientName.text = ingredient.ingredientName;
    }

}
