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
    public Material canPlaceMat;
    public Material cantPlaceMat;

    public List<InventoryItem> itemsInInventory;
    private GameObject hotbar;
    public int? SelectedSlot;
    private GameObject player;
    public List<(string hotbarLocation, Guid itemGuid)> hotbarLocations;
    private bool isPlaceing;
    private bool canPlace;
    private GameObject itemToPlace;
    Collider[] hitColliders;
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
        if (Input.GetKeyDown(KeyCode.G) && Application.isEditor)
        {
            AddItem(new InventoryItem(Item.Printer));
            AddItem(new InventoryItem(Item.CommunicationsDevice));
            AddItem(new InventoryItem(Item.Drill));
            AddItem(new InventoryItem(Item.Locker));
            AddItem(new InventoryItem(Item.RockGrinder));
        }
        if (Input.GetKeyDown(KeyCode.R) && Application.isEditor)
        {
            AddItem(new InventoryItem(Item.Rock1));
            AddItem(new InventoryItem(Item.Rock2));
            AddItem(new InventoryItem(Item.Rock3));
            AddItem(new InventoryItem(Item.Rock3));
            AddItem(new InventoryItem(Item.Rock4));
            AddItem(new InventoryItem(Item.Rock5));
            AddItem(new InventoryItem(Item.Rock4));
            AddItem(new InventoryItem(Item.Rock5));
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            DropAll();
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
            else if (Input.GetMouseButtonDown(1))
            {
                if (!isPlaceing)
                    StartPlace();
                else
                    Place();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isPlaceing)
            {
                CancelPlace();
            }
        }

        if (isPlaceing)
        {
            hitColliders = Physics.OverlapBox(itemToPlace.transform.position, new Vector3(1f, 1f, 1f), itemToPlace.transform.rotation);
            var renderers = itemToPlace.GetComponentsInChildren<Renderer>();
            var colliders = itemToPlace.GetComponentsInChildren<Collider>();

            if (hitColliders.Except(colliders).Any())
            {

                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].material = cantPlaceMat;
                }
                canPlace = false;
            }
            else
            {
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].material = canPlaceMat;
                }
                canPlace = true;
            }



        }
    }

    void DropAll()
    {
        SelectedSlot = 0;
        while (itemsInInventory.Any())
        {
            DropItem();
            SelectedSlot++;
        }
    }

    void DropItem()
    {
        if (SelectedSlot != null && hotbarLocations.Any(a => a.hotbarLocation.Contains(SelectedSlot.Value.ToString())))
        {
            var slotItem = hotbarLocations.First(a => a.hotbarLocation.Contains(SelectedSlot.Value.ToString()));

            var item = itemsInInventory.First(q => q.refId == slotItem.itemGuid);

            GameObject asset;

            if (!item.Placeable)
            {
                asset = Resources.Load("Prefabs/" + item.name) as GameObject;

            }
            else
            {
                asset = Resources.Load("Prefabs/" + item.DropPrefabName) as GameObject;

            }

            GetComponentsInChildren<RawImage>().First(q => q.gameObject.name == hotbarLocations.First(a => a.itemGuid == item.refId).hotbarLocation).texture = null;
            hotbarLocations.Remove(hotbarLocations.First(a => a.itemGuid == item.refId));
            itemsInInventory.Remove(itemsInInventory.First(a => a.refId == item.refId));


            if (player == null)
                player = GameObject.Find("Player_Astronaut").GetComponentInChildren<Animator>().gameObject;

            var forward = player.transform.position + player.transform.forward;
            Instantiate(asset, new Vector3(forward.x, player.transform.position.y + 1.5f, forward.z), player.transform.rotation);


        }
    }

    void StartPlace()
    {
        if (SelectedSlot != null && hotbarLocations.Any(a => a.hotbarLocation.Contains(SelectedSlot.Value.ToString())))
        {
            var slotItem = hotbarLocations.First(a => a.hotbarLocation.Contains(SelectedSlot.Value.ToString()));

            var item = itemsInInventory.First(q => q.refId == slotItem.itemGuid);

            if (item.Placeable)
            {
                isPlaceing = true;

                var asset = Resources.Load("Prefabs/" + item.name) as GameObject;

                if (player == null)
                    player = GameObject.Find("Player_Astronaut").GetComponentInChildren<Animator>().gameObject;

                var forward = player.transform.position + player.transform.forward * 3;

                itemToPlace = Instantiate(asset, new Vector3(forward.x, player.transform.position.y + 1.5f, forward.z), player.transform.rotation);
                var rigid = itemToPlace.GetComponentInChildren<Rigidbody>();
                rigid.isKinematic = true;


                itemToPlace.transform.parent = player.transform;
                var renderers = itemToPlace.GetComponentsInChildren<Renderer>();
                var paticleSystems = itemToPlace.GetComponentsInChildren<ParticleSystem>();
                var animations = itemToPlace.GetComponentsInChildren<Animation>();

                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].material = canPlaceMat;
                }

                for (int i = 0; i < paticleSystems.Length; i++)
                {
                    paticleSystems[i].Stop();
                }

                for (int i = 0; i < animations.Length; i++)
                {
                    animations[i].Stop();
                }
            }


        }
    }

    void Place()
    {
        if (isPlaceing && canPlace)
        {
            var slotItem = hotbarLocations.First(a => a.hotbarLocation.Contains(SelectedSlot.Value.ToString()));

            var item = itemsInInventory.First(q => q.refId == slotItem.itemGuid);
            GetComponentsInChildren<RawImage>().First(q => q.gameObject.name == hotbarLocations.First(a => a.itemGuid == item.refId).hotbarLocation).texture = null;
            hotbarLocations.Remove(hotbarLocations.First(a => a.itemGuid == item.refId));
            itemsInInventory.Remove(itemsInInventory.First(a => a.refId == item.refId));

            var asset = Resources.Load("Prefabs/" + item.name) as GameObject;

            var newItem = Instantiate(asset, itemToPlace.transform.position, itemToPlace.transform.rotation);

            var rigidBody = newItem.GetComponentInChildren<Rigidbody>();
            rigidBody.isKinematic = false;
            rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            rigidBody.mass = 10f;

            Destroy(itemToPlace);

            isPlaceing = false;
        }
    }

    void CancelPlace()
    {
        Destroy(itemToPlace);
        isPlaceing = false;
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

        SelectedSlot = slotToSelect;

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
