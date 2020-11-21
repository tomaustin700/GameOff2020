using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Craft : MonoBehaviour
{
    public Recipe recipe;
    public InventoryManager HotBar;
    public Button Button;
    public Text RecipeName;
    public Text RecipeDescription;
    public RawImage RecipeImage;
    public Text Requirements;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        if (recipe != null)
        {
            UpdateRecipe(recipe);
        }
    }
    public bool ValidRequirements()
    {
        if (recipe != null)
        {
            var requirements = recipe.Requirements;
            var inventoryCopy = HotBar.itemsInInventory.Select(x => x.name).ToList();
            bool passed = true;
            foreach (Item item in requirements)
            {
                if (inventoryCopy.Contains(item.ToString()))
                {
                    inventoryCopy.Remove(item.ToString());
                }
                else
                {
                    passed = false;
                }
            }
            return passed;
        }
        return false;
    }
    public void CraftItem()
    {
        if (ValidRequirements())
        {
            var requirements = recipe.Requirements;
            var inventoryCopy = HotBar.itemsInInventory.Select(x => x.name).ToList();
            foreach (Item item in requirements)
            {
                var firstRemove = HotBar.itemsInInventory.First(x => x.name == item.ToString());
                var hotBarLoc = HotBar.hotbarLocations.First(x => x.itemGuid == firstRemove.refId);
                HotBar.DropItemBySlot(hotBarLoc, false);
            }
            HotBar.AddItem(new InventoryItem(recipe.ReturnItem));
        }
    }

    public void UpdateRecipe(Recipe recipeToUse)
    {
        recipe = recipeToUse;
        RecipeName.text = recipe.RecipeName;
        RecipeDescription.text = recipe.Description;
        RecipeImage.texture = recipe.Image;
        var uniqueItems = recipe.Requirements.Distinct();
        string requirements = "Requires: ";
        List<string> itemParts = new List<string>();
        foreach(var item in uniqueItems)
        {
            itemParts.Add($"{item}({uniqueItems.Count(x => x == item)})");
        }
        Requirements.text = requirements += string.Join(",", itemParts);
        if (ValidRequirements())
        {
            Button.gameObject.SetActive(true);
        }
        else
        {
            Button.gameObject.SetActive(false);
        }
    }
}
