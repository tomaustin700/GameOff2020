using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
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
            List<string> combinedInventory = new List<string>();
            var combinedStorage = FindObjectsOfType<Storage>();
            var type = Enum.Parse(typeof(Item), "Drill");
            var storageStrings = new List<string>();
            combinedStorage.ToList().ForEach(x => storageStrings.AddRange(x.Inventory.GetAllItemNames()));
            combinedInventory.AddRange(storageStrings);
            combinedInventory.AddRange(inventoryCopy);
            bool passed = true;
            foreach (Item item in requirements)
            {
                if (combinedInventory.Contains(item.ToString()))
                {
                    if (inventoryCopy.Contains(item.ToString()))
                    {
                        inventoryCopy.Remove(item.ToString());
                    }
                    combinedInventory.Remove(item.ToString());
                }
                else
                {
                    passed = false;
                }
            }
            if (inventoryCopy.Count >= HotBar.maxSize)
            {
                passed = false;
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
            var combinedStorage = FindObjectsOfType<Storage>();
            var type = Enum.Parse(typeof(Item), "Drill");
            foreach (Item item in requirements)
            {
                if(HotBar.itemsInInventory.Any(x => x.name == item.ToString()))
                {
                    var firstRemove = HotBar.itemsInInventory.First(x => x.name == item.ToString());
                    var hotBarLoc = HotBar.hotbarLocations.First(x => x.itemGuid == firstRemove.refId);
                    HotBar.DropItemBySlot(hotBarLoc, false);
                }
                else
                {
                    foreach(Storage storage in combinedStorage)
                    {
                        if(storage.Inventory.Contains(item))
                        {
                            var itemInStorage = storage.Inventory.GetItemByItemType(item);
                            storage.Inventory.RemoveItemFromSlot(itemInStorage.posX, itemInStorage.posY);
                        }
                    }
                }
          
            }
            HotBar.AddItem(new InventoryItem(recipe.ReturnItem));
            UpdateRecipe(recipe);

            if (NotificationManager.Notifications.Any(a => a.EventName == EventName.CraftComms))
            {
                NotificationManager.CompleteNotification(EventName.CraftComms);
                NotificationManager.AddNotification(new NotificationEvent(EventName.PlaceComms1, "Select Communications Device"));

            }
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
            itemParts.Add($"{item}({recipe.Requirements.Count(x => x == item)})");
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
