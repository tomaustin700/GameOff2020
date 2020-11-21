using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections.ObjectModel;
using UnityEngine.UI;
using UnityEditor;
using System;

public class InventoryManager : MonoBehaviour
{

    public int maxSize = 8;

    public List<InventoryItem> itemsInInventory;
    private GameObject hotbar;
    private int? selectedSlot;
    private GameObject player;
    public List<(string hotbarLocation, Guid itemGuid)> hotbarLocations;
    private bool isPlaceing;
    private GameObject itemToPlace;
    private void Awake()
    {
        if (tag != "UseableItem")
            hotbar = GameObject.FindGameObjectWithTag("Hotbar");
    }
    void Start()
    {
        itemsInInventory = new List<InventoryItem>();
        if (hotbar != null)
            hotbarLocations = new List<(string hotbarLocation, Guid itemGuid)>();
    }
    public bool HasInventorySpace()
    {
        return itemsInInventory.Count < maxSize;
    }
    public void AddItem(InventoryItem item)
    {
        if (HasInventorySpace())
        {
            itemsInInventory.Add(item);
            if (hotbar != null)
            {
                var slots = GetComponentsInChildren<RawImage>();
                foreach (var slot in slots)
                {
                    if (!hotbarLocations.Any(x => x.hotbarLocation == slot.gameObject.name))
                    {
                        slot.texture = item.sprite;
                        hotbarLocations.Add((slot.gameObject.name, item.refId));
                        break;
                    }
                }

            }

        }

    }


    public void RemoveItem(IInventoryItem item)
    {
        itemsInInventory.Remove(itemsInInventory.Single(a => a.name == item.name));

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddItem(new InventoryItem(Item.Drill));
        }

        if (hotbar != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectHotbarSlot(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectHotbarSlot(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SelectHotbarSlot(3);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SelectHotbarSlot(4);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SelectHotbarSlot(5);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SelectHotbarSlot(6);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SelectHotbarSlot(7);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                SelectHotbarSlot(8);

            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                DropItem();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                if (!isPlaceing)
                    StartPlace();
                else
                    Place();
            }
        }
    }

    void DropItem()
    {
        if (selectedSlot != null && hotbarLocations.Any(a => a.hotbarLocation.Contains(selectedSlot.Value.ToString())))
        {
            var slotItem = hotbarLocations.First(a => a.hotbarLocation.Contains(selectedSlot.Value.ToString()));

    


        }
    }

    void StartPlace()
    {
        if (selectedSlot != null && hotbarLocations.Any(a => a.hotbarLocation.Contains(selectedSlot.Value.ToString())))
        {
            var slotItem = hotbarLocations.First(a => a.hotbarLocation.Contains(selectedSlot.Value.ToString()));

            var item = itemsInInventory.First(q => q.refId == slotItem.itemGuid);

            if (item.Placeable)
            {
                isPlaceing = true;

                var asset = Resources.Load("Prefabs/" + item.name) as GameObject;

                if (player == null)
                    player = GameObject.Find("Player_Astronaut").GetComponentInChildren<Animator>().gameObject;

                var forward = player.transform.position + player.transform.forward * 2;

                itemToPlace = Instantiate(asset, new Vector3(forward.x, player.transform.position.y + 1.5f, forward.z), player.transform.rotation);
                itemToPlace.GetComponentInChildren<Rigidbody>().isKinematic = true;
                itemToPlace.transform.parent = player.transform;
                var components = itemToPlace.GetComponentsInChildren<MeshRenderer>();

                for (int i = 0; i < components.Length; i++)
                {
                    // e.g. color red
                    components[i].gameObject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

                }
            }


        }
    }

    void Place()
    {
        if (isPlaceing)
        {
            var slotItem = hotbarLocations.First(a => a.hotbarLocation.Contains(selectedSlot.Value.ToString()));

            var item = itemsInInventory.First(q => q.refId == slotItem.itemGuid);
            GetComponentsInChildren<RawImage>().First(q => q.gameObject.name == hotbarLocations.First(a => a.itemGuid == item.refId).hotbarLocation).texture = null;
            hotbarLocations.Remove(hotbarLocations.First(a => a.itemGuid == item.refId));
            itemsInInventory.Remove(itemsInInventory.First(a => a.refId == item.refId));
            itemToPlace.transform.parent = null;
            var rigidBody = itemToPlace.GetComponentInChildren<Rigidbody>();
            rigidBody.isKinematic = false;
            rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

            isPlaceing = false;
        }
    }

    void SelectHotbarSlot(int slotToSelect)
    {
        if (isPlaceing)
        {
            Destroy(itemToPlace);
            isPlaceing = false;
        }

        var slots = GetComponentsInChildren<RawImage>();

        foreach (var slot in slots)
        {
            slot.color = new Color(125f / 255f, 125f / 255f, 120f / 255f, 255f / 255f);
        }

        selectedSlot = slotToSelect;

        var slotImage = GetComponentsInChildren<RawImage>().FirstOrDefault(a => a.name == "Slot (" + slotToSelect + ")");

        if (slotImage != null)
            slotImage.color = Color.red;
    }

    public void DropItemBySlot((string hotbarLocation, Guid itemGuid) slotItem, bool instantiate = true)
    {
        var item = itemsInInventory.First(q => q.refId == slotItem.itemGuid);
        var asset = Resources.Load("Prefabs/" + item.name) as GameObject;

        GetComponentsInChildren<RawImage>().First(q => q.gameObject.name == hotbarLocations.First(a => a.itemGuid == item.refId).hotbarLocation).texture = null;
        hotbarLocations.Remove(hotbarLocations.First(a => a.itemGuid == item.refId));
        itemsInInventory.Remove(itemsInInventory.First(a => a.refId == item.refId));


        if (player == null)
            player = GameObject.Find("Player_Astronaut").GetComponentInChildren<Animator>().gameObject;

        var forward = player.transform.position + player.transform.forward;
        if (instantiate)
        {
            Instantiate(asset, new Vector3(forward.x, player.transform.position.y + 1.5f, forward.z), player.transform.rotation);
        }
    }
    }
