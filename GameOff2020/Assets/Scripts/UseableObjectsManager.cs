using System.Linq;
using System.Text.RegularExpressions;
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
    [SerializeField] private GameObject playerWithCam;

    public Text InstructionText;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + transform.forward, new Vector3(1f, 1f, 1));
    }
    void Update()
    {
        if (player != null && !playerWithCam.GetComponent<PlayerManager>().playerDead)
        {
            Vector3 newPosition = player.transform.position;
            transform.position = newPosition;

            hitColliders = Physics.OverlapBox(transform.position + transform.forward, new Vector3(1f, 1f, 1), transform.rotation);
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
                    else if (col.Equals(currentItem) && col.gameObject.GetComponentInChildren<Renderer>().material != highlightMaterial)
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
                    if (col.GetComponent<RockGrinder>() != null)
                    {

                        var rockGrinder = col.GetComponent<RockGrinder>();

                        text = "Select a Rock and Press 'E'";
                        if (Input.GetKeyUp(KeyCode.E))
                        {
                            rockGrinder.TradeRock();
                        }
                    }
                    if (col.GetComponentInChildren<Printer>() != null)
                    {

                        var printer = col.GetComponentInChildren<Printer>();

                        text = printer.IsOpen ? "'E' Close Crafting" : "'E' Begin Crafting";
                        if (Input.GetKeyUp(KeyCode.E))
                        {
                            printer.ToggleCrafting();
                        }
                    }
                    if (col.GetComponentInParent<RockInteraction>() != null)
                    {
                        var rock = col.GetComponentInParent<RockInteraction>();
                        var name = rock.name.Replace("(Clone)", "");

                        //if (name == "Rock1" || name == "Rock2" || name == "Rock3")
                        //{
                        //    text = "'E' To Pick Up Common Moon Rock";
                        //}
                        //else
                        //    text = "'E' To Pick Up Rare Moon Rock";


                        text = "'E' To Pick Up Moon Rock";

                        if (Input.GetKey(KeyCode.E))
                        {
                            rock.PickUpRock();
                        }
                    }

                    if (col.GetComponentInParent<ElementInteraction>() != null)
                    {
                        var element = col.GetComponentInParent<ElementInteraction>();
                        text = "'E' To Pick Up " + col.transform.parent.name.Replace("(Clone)", "");
                        if (Input.GetKey(KeyCode.E))
                        {
                            element.PickUpElement();
                        }
                    }

                    if (col.GetComponentInParent<DropUsableInteraction>() != null)
                    {
                        var prefab = col.GetComponentInParent<DropUsableInteraction>();
                        text = "'E' To Pick Up " + Regex.Replace(col.transform.parent.name.Replace("Drop(Clone)", ""), "(\\B[A-Z])", " $1");
                        if (Input.GetKey(KeyCode.E))
                        {
                            prefab.PickUpUsable();
                        }
                    }

                    if (col.GetComponentInParent<SuitPowerInteraction>() != null)
                    {
                        var prefab = col.GetComponentInParent<SuitPowerInteraction>();
                        text = "'E' To Recharge Suit";
                        if (Input.GetKey(KeyCode.E))
                        {
                            prefab.RechargeSuit();
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
