using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;


public class Storage : MonoBehaviour
{
    public Inventory Inventory;
    public GameObject InventoryUI;
    public GameObject InventorySlot;
    [Range(1,8)]
    public int SlotsXLength = 5;
    [Range(1,5)]
    public int SlotsYLength = 5;
    public bool IsOpen = false;
    public CurrentInventoryOwner Owner;
    public GameObject CameraObject;
    public bool Locked = false;
    public List<Item> PreExistingItems = new List<Item>();
    
    // Start is called before the first frame update
    private void Awake()
    {
        Inventory = ScriptableObject.CreateInstance<Inventory>();
        InventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
        Owner = InventoryUI.GetComponent<CurrentInventoryOwner>();
        CameraObject = Camera.main?.gameObject;
    }
    void Start()
    {
        Inventory.InventorySlots = new InventoryItem[SlotsYLength, SlotsXLength];
        
        foreach(Item item in PreExistingItems)
        {
            Inventory.AddItem(new InventoryItem(item));
        }   
    }
    private void Update()
    {
        if (CameraObject == null)
        {
            CameraObject = Camera.main?.gameObject;
        }
    }
    // Update is called once per frame
    public void ToggleInventory()
    {
        if(IsOpen)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        var volume = GameObject.FindGameObjectWithTag("PostProcessVolume")?.GetComponent<Volume>();
        if (volume != null)
        {
            for (int i = 0; i < volume.profile.components.Count; i++)
            {
                Debug.Log(volume.profile.components[i].name);
                if (volume.profile.components[i].name.Contains("Bloom"))
                {
                    Bloom bloom = (Bloom)volume.profile.components[i];
                    bloom.intensity.value = 1f;

                }
            }
        }
        InventoryUI.GetComponent<Image>().enabled = true;
        IsOpen = true;
        ReDraw();
        Owner.CurrentOwner = gameObject;
        CameraObject.GetComponent<CameraFollow>().CanAlterCursor = false;
        CameraObject.GetComponent<CameraFollow>().MoveToCursor = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
    public void CloseInventory()
    {
        var volume = GameObject.FindGameObjectWithTag("PostProcessVolume")?.GetComponent<Volume>();
        if (volume != null)
        {
            for (int i = 0; i < volume.profile.components.Count; i++)
            {
                Debug.Log(volume.profile.components[i].name);
                if (volume.profile.components[i].name.Contains("Bloom"))
                {
                    Bloom bloom = (Bloom)volume.profile.components[i];
                    bloom.intensity.value = 0.25f;

                }
            }
        }
        GameObject.FindGameObjectsWithTag("InventorySlot").ToList().ForEach(x => Destroy(x));
        InventoryUI.GetComponent<Image>().enabled = false;
        IsOpen = false;
        Owner.CurrentOwner = null;
        var cam = CameraObject.GetComponent<CameraFollow>();
        cam.CanAlterCursor = true;
        CameraObject.GetComponent<CameraFollow>().MoveToCursor = true;
    }
    public void ReDraw()
    {
        if (IsOpen)
        {
            GameObject.FindGameObjectsWithTag("InventorySlot").ToList().ForEach(x => Destroy(x));
            for (int k = 0; k < Inventory.InventorySlots.GetLength(0); k++)
            {
                for (int l = 0; l < Inventory.InventorySlots.GetLength(1); l++)
                {
                    var item = Inventory.InventorySlots[k, l];
                    var slot = Instantiate(InventorySlot, InventoryUI.transform);
                    var rect = slot.GetComponent<RectTransform>();
                    if (item != null)
                    {
                        slot.GetComponentInChildren<RawImage>().texture = item.sprite;
                        slot.name = item.refId.ToString();
                    }

                    slot.GetComponent<RectTransform>().position = new Vector3(rect.position.x + (l * 80), rect.position.y + (k * -80), rect.position.z);
                }
            }
        }
    }
}
