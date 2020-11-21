using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Recipe", menuName = "Custom/Recipe")]
public class Recipe : ScriptableObject
{
    public string RecipeName;
    public Texture Image;
    public string Description;
    public List<Item> Requirements = new List<Item>();
    public Item ReturnItem;
}
