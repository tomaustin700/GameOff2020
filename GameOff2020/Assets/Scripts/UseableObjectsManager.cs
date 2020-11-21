using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UseableObjectsManager : MonoBehaviour
{
    [SerializeField] private string useableItemTag = "UseableItem";
    [SerializeField] private Material highlightMaterial;
    Collider[] hitColliders;
    [SerializeField] private GameObject player;
    private GameObject currentItem;
    private Material currentItemDefaultMaterial;

    public Text InstructionText;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + transform.forward, new Vector3(1f, 1f, 1));
    }
    void Update()
    {
        if (player != null)
        {
            Vector3 newPosition = player.transform.position;
            transform.position = newPosition;

            hitColliders = Physics.OverlapBox(transform.position + transform.forward, new Vector3(1f,1f,1),transform.rotation);
            //hitColliders = Physics.OverlapCapsule(transform.position, transform.position + transform.forward,1.5f);
            string text = string.Empty;
            foreach (Collider col in hitColliders)
            {
                if (col.gameObject.CompareTag(useableItemTag))
                {
                    //highlight
                    if (currentItem == null)
                    {
                        currentItemDefaultMaterial = col.gameObject.GetComponentInChildren<Renderer>().material;
                        col.gameObject.transform.gameObject.GetComponentInChildren<Renderer>().material = highlightMaterial;
                        currentItem = col.gameObject.transform.gameObject;
                    }
                    else if(col.Equals(currentItem) && col.gameObject.GetComponentInChildren<Renderer>().material != highlightMaterial)
                    {
                        currentItemDefaultMaterial = col.gameObject.GetComponentInChildren<Renderer>().material;
                        col.gameObject.transform.gameObject.GetComponentInChildren<Renderer>().material = highlightMaterial;
                    }



                    //solar panel
                    if (col.GetComponent<SolarPanelDust>() != null)
                    {
                        var solarPanel = col.GetComponent<SolarPanelDust>();
                       
                        text = "'E' To Remove Dust";
                        if (Input.GetKey(KeyCode.E))
                        {
                            solarPanel.RemoveDust();
                            col.gameObject.transform.gameObject.GetComponentInChildren<Renderer>().material = highlightMaterial;
                        }
                    }
                    if (col.GetComponent<Storage>() != null)
                    {

                        var storage = col.GetComponent<Storage>();

                        text = storage.IsOpen ? "'E' To Close Storage" : "'E' To Open Storage";
                        if (Input.GetKeyUp(KeyCode.E))
                        {
                            storage.ToggleInventory();
                        }
                    }
                    if (col.GetComponent<Printer>() != null)
                    {

                        var printer = col.GetComponent<Printer>();

                        text = printer.IsOpen ? "'E' Close Crafting" : "'E' Begin Crafting";
                        if (Input.GetKeyUp(KeyCode.E))
                        {
                            printer.ToggleCrafting();
                        }
                    }
                    if (col.GetComponentInParent<RockInteraction>() != null)
                    {
                        var rock = col.GetComponentInParent<RockInteraction>();
                        text = "'E' To Pick Up Rock"; 
                        if (Input.GetKey(KeyCode.E))
                        {
                            rock.PickUpRock();
                        }
                    }

                }
            }
            if (currentItem != null && !hitColliders.Contains(currentItem.GetComponent<Collider>()))
            {
                currentItem.GetComponentInChildren<Renderer>().material = currentItemDefaultMaterial;
                var solar = currentItem.GetComponent<SolarPanelDust>();
                if (solar != null)
                {
                    solar.SetMaterial();
                }
                var storage = currentItem.GetComponent<Storage>();
                if (storage != null)
                {
                    storage.CloseInventory();
                }
                var printer = currentItem.GetComponent<Printer>();
                if (printer != null)
                {
                    printer.CloseCrafting();
                }
                currentItem = null;
                currentItemDefaultMaterial = null;
            }
            InstructionText.text = text;
        }
    }
}
