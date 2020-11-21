using System.Linq;
using UnityEngine;

public class UseableObjectsManager : MonoBehaviour
{
    [SerializeField] private string useableItemTag = "UseableItem";
    [SerializeField] private Material highlightMaterial;
    Collider[] hitColliders;
    [SerializeField] private GameObject player;
    private GameObject currentItem;
    private Material currentItemDefaultMaterial;

    void Update()
    {
        if (player != null)
        {
            Vector3 newPosition = player.transform.position;
            transform.position = newPosition;


            hitColliders = Physics.OverlapSphere(transform.position, 3f);
            foreach (Collider col in hitColliders)
            {
                if (col.gameObject.CompareTag(useableItemTag))
                {
                    //highlight
                    if (currentItem == null)
                    {
                        currentItemDefaultMaterial = col.gameObject.GetComponent<Renderer>().material;
                        col.gameObject.transform.gameObject.GetComponent<Renderer>().material = highlightMaterial;
                        currentItem = col.gameObject.transform.gameObject;
                    }



                    //solar panel
                    if (col.GetComponent<SolarPanelDust>() != null)
                    {
                        var solarPanel = col.GetComponent<SolarPanelDust>();
                        if (Input.GetKey(KeyCode.E))
                        {
                            solarPanel.RemoveDust();
                        }
                    }
                    if (col.GetComponent<Storage>() != null)
                    {
                        var storage = col.GetComponent<Storage>();
                        if (Input.GetKeyUp(KeyCode.E))
                        {
                            storage.ToggleInventory();
                        }
                    }
                    if (col.GetComponentInParent<RockInteraction>() != null)
                    {
                        var rock = col.GetComponentInParent<RockInteraction>();

                        if (Input.GetKey(KeyCode.E))
                        {
                            rock.PickUpRock();
                        }
                    }

                }
            }
            if (currentItem != null && !hitColliders.Contains(currentItem.GetComponent<Collider>()))
            {
                currentItem.GetComponent<Renderer>().material = currentItemDefaultMaterial;
                currentItem = null;
                currentItemDefaultMaterial = null;
            }

        }
    }
}
