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

        AddItem(new InventoryItem(Item.BrokenCommunicationsDevice));

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
                var slots = GetComponentsInChildren<InventorySlotScript>().ToList().Select(x => x.GetComponent<RawImage>());
                foreach (var slot in slots)
                {
                    if (!hotbarLocations.Any(x => x.hotbarLocation == slot.gameObject.name))
                    {
                        slot.color = Color.white;
                        slot.texture = item.sprite;
                        hotbarLocations.Add((slot.gameObject.name, item.refId));
                        break;
                    }
                }

            }

        }

        if (NotificationManager.Notifications.Any(a => a.EventName == EventName.GatherBrokenCommsResources) && itemsInInventory.Any(x => x.name == "Iron") && itemsInInventory.Any(x => x.name == "Silicon") && itemsInInventory.Any(x => x.name == "BrokenCommunicationsDevice"))
        {
            NotificationManager.CompleteNotification(EventName.GatherBrokenCommsResources);
            NotificationManager.AddNotification(new NotificationEvent(EventName.CraftComms, "Craft Communications Device using 3D Printer"));

        }

        if (NotificationManager.Notifications.Any(a => a.EventName == EventName.MineOxygen) && item.name == "Oxygen")
        {
            NotificationManager.CompleteNotification(EventName.MineOxygen);
            NotificationManager.AddNotification(new NotificationEvent(EventName.ForthMissionControlMessage, "New Message - Press M to view"));
            MessageManager.AddMessage(new Message()
            {
                Title = "Mission Control - Rescue Plan Stage 3",
                Body = "You're doing great.  If you're suit is running low on power it can be recharged in the hab. Rescue mission has been launched but it's still a bit off. Hang in there.",
                EventName = EventName.ForthMissionControlMessage
            });
            
        }

    }


    public void RemoveItem(IInventoryItem item)
    {
        itemsInInventory.Remove(itemsInInventory.Single(a => a.name == item.name));

    }

    private void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) // forward
        {
            if (SelectedSlot == null || SelectedSlot >= 8)
            {
                SelectHotbarSlot(0);
            }
            else
            {
                SelectHotbarSlot(SelectedSlot.Value + 1);
            }
        }
        if (scroll < 0f) // backwards
        {
            if (SelectedSlot == null || SelectedSlot <= 0)
            {
                SelectHotbarSlot(8);
            }
            else
            {
                SelectHotbarSlot(SelectedSlot.Value - 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.G) && Application.isEditor)
        {
            AddItem(new InventoryItem(Item.Printer));
            AddItem(new InventoryItem(Item.CommunicationsDevice));
            AddItem(new InventoryItem(Item.Drill));
            AddItem(new InventoryItem(Item.Locker));
            AddItem(new InventoryItem(Item.RockGrinder));
            AddItem(new InventoryItem(Item.Solar));
            AddItem(new InventoryItem(Item.Oxygen));
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
                if (NotificationManager.Notifications.Any(a => a.EventName == EventName.SelectHotbarSlot))
                {
                    NotificationManager.CompleteNotification(EventName.SelectHotbarSlot);
                    NotificationManager.AddNotification(new NotificationEvent(EventName.Drop, "Drop Selected Item With V"));
                }

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
                    UseItem();
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

            var slots = GetComponentsInChildren<InventorySlotScript>();
            var firstSlotImage = slots.First(q => q.gameObject.name == hotbarLocations.First(a => a.itemGuid == item.refId).hotbarLocation).GetComponent<RawImage>();
            firstSlotImage.texture = null;
            firstSlotImage.color = Color.clear;

            hotbarLocations.Remove(hotbarLocations.First(a => a.itemGuid == item.refId));
            itemsInInventory.Remove(itemsInInventory.First(a => a.refId == item.refId));


            if (player == null)
                player = GameObject.Find("Player_Astronaut").GetComponentInChildren<Animator>().gameObject;

            var forward = player.transform.position + player.transform.forward;
            Instantiate(asset, new Vector3(forward.x, player.transform.position.y + 1.5f, forward.z), player.transform.rotation);

            if (NotificationManager.Notifications.Any(a => a.EventName == EventName.Drop))
            {
                NotificationManager.CompleteNotification(EventName.Drop);
                NotificationManager.AddNotification(new NotificationEvent(EventName.GrindRock, "Pick Up Rock and Take To Grinder In Hab To Grind"));
            }


        }
    }

    void UseItem()
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
                var audio = itemToPlace.GetComponentsInChildren<AudioSource>();

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

                for (int i = 0; i < audio.Length; i++)
                {
                    audio[i].Stop();
                }

                if (NotificationManager.Notifications.Any(a => a.EventName == EventName.PlaceComms3) && item.name == "CommunicationsDevice")
                {
                    NotificationManager.CompleteNotification(EventName.PlaceComms3);
                    NotificationManager.AddNotification(new NotificationEvent(EventName.PlaceComms4, "Right Click To Place"));

                }
            }
            else if (item.Consumable)
            {
                var slots = GetComponentsInChildren<InventorySlotScript>();

                var firstSlotImage = slots.First(q => q.gameObject.name == hotbarLocations.First(a => a.itemGuid == item.refId).hotbarLocation).GetComponent<RawImage>();
                firstSlotImage.texture = null;
                firstSlotImage.color = Color.clear;
                hotbarLocations.Remove(hotbarLocations.First(a => a.itemGuid == item.refId));
                itemsInInventory.Remove(itemsInInventory.First(a => a.refId == item.refId));
                var playerManager = FindObjectOfType<PlayerManager>();

                if (item.name == "Oxygen")
                {
                    playerManager.AddOxygen(15);
                }

                if (item.name == "Rations")
                {
                    playerManager.AddFood(30);
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
            var slots = GetComponentsInChildren<InventorySlotScript>();
            var firstSlotImage = slots.First(q => q.gameObject.name == hotbarLocations.First(a => a.itemGuid == item.refId).hotbarLocation).GetComponent<RawImage>();
            firstSlotImage.texture = null;
            firstSlotImage.color = Color.clear;
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

            if (NotificationManager.Notifications.Any(a => a.EventName == EventName.PlaceComms4) && item.name == "CommunicationsDevice")
            {
                NotificationManager.CompleteNotification(EventName.PlaceComms4);

            }

            if (NotificationManager.Notifications.Any(a => a.EventName == EventName.CraftDrill) && item.name == "Drill")
            {
                NotificationManager.CompleteNotification(EventName.CraftDrill);
                NotificationManager.AddNotification(new NotificationEvent(EventName.ThirdMissionControlMessage, "New Message - Press M to view"));
                MessageManager.AddMessage(new Message()
                {
                    Title = "Mission Control - Rescue Plan Stage 2",
                    Body = "Excellent job with the drill. Make sure you have enough solar panels for your equipment and they're clean. Maybe try placing a drill on the ice patches. Mined oxygen in your hotbar can be used to extend your suit oxygen with right click.",
                    EventName = EventName.ThirdMissionControlMessage
                });

            }
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

        var slots = GetComponentsInChildren<InventorySlotScript>();

        foreach (var slot in slots)
        {
            var img = slot.GetComponent<RawImage>();
            if (img.texture == null)
            {
                img.color = Color.clear;
            }
            slot.transform.localScale = new Vector3(1, 1, 1);
            if (slot.transform.childCount > 0)
            {
                var child = slot.transform.GetChild(0).gameObject;
                child.gameObject.SetActive(false);
                child.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        SelectedSlot = slotToSelect;

        var slotImage = GetComponentsInChildren<InventorySlotScript>().FirstOrDefault(a => a.name == "Slot (" + slotToSelect + ")");

        if (slotImage != null)
        {
            var img = slotImage.GetComponent<RawImage>();
            if (img.texture != null)
            {
                img.color = Color.white;
            }
            slotImage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            if (slotImage.transform.childCount > 0)
            {
                var child = slotImage.transform.GetChild(0).gameObject;
                child.gameObject.SetActive(true);
                child.gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
        }

        if (hotbarLocations.Any(a => a.hotbarLocation.Contains(SelectedSlot.Value.ToString())))
        {
            var slotItem = hotbarLocations.FirstOrDefault(a => a.hotbarLocation.Contains(SelectedSlot.Value.ToString()));

            var item = itemsInInventory.First(q => q.refId == slotItem.itemGuid);

            if (NotificationManager.Notifications.Any(a => a.EventName == EventName.PlaceComms1) && item.name == "CommunicationsDevice")
            {
                NotificationManager.CompleteNotification(EventName.PlaceComms1);
                NotificationManager.AddNotification(new NotificationEvent(EventName.PlaceComms2, "Go Outside"));

            }
        }
    }

    public void DropItemBySlot((string hotbarLocation, Guid itemGuid) slotItem, bool instantiate = true)
    {
        var item = itemsInInventory.First(q => q.refId == slotItem.itemGuid);
        var asset = Resources.Load("Prefabs/" + item.name) as GameObject;

        var slots = GetComponentsInChildren<InventorySlotScript>();
        var firstSlotImage = slots.First(q => q.gameObject.name == hotbarLocations.First(a => a.itemGuid == item.refId).hotbarLocation).GetComponent<RawImage>();
        firstSlotImage.texture = null;
        firstSlotImage.color = Color.clear;
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
