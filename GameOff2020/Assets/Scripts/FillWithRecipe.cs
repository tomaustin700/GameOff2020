using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillWithRecipe : MonoBehaviour
{
    public Recipe Recipe;
    public Text TextObject;

    private void Awake()
    {
        TextObject.text = Recipe.RecipeName;
        GetComponent<RawImage>().texture = Recipe.Image;
    }
    public void SelectRecipe()
    {
        var ob = GameObject.FindGameObjectWithTag("CraftList");
        if(ob != null)
        {
            ob.GetComponent<Craft>().UpdateRecipe(Recipe);
        }
    }
}
