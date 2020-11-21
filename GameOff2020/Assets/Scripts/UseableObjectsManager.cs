using UnityEngine;
using UnityEngine.UI;

public class UseableObjectsManager : MonoBehaviour
{
    [SerializeField] private string useableItemTag = "UseableItem";
    Collider[] hitColliders;
    [SerializeField] private GameObject player;
    public Text InstructionText;
    void Update()
    {
        if (player != null)
        {
            Vector3 newPosition = player.transform.position;
            transform.position = newPosition;


            hitColliders = Physics.OverlapSphere(transform.position, 3f);
            string text = string.Empty;
            foreach (Collider col in hitColliders)
            {
                if (col.gameObject.CompareTag(useableItemTag))
                {
                    //solar panel
                    if (col.GetComponent<SolarPanelDust>() != null)
                    {
                        var solarPanel = col.GetComponent<SolarPanelDust>();
                       
                        text = "'E' To Remove Dust";
                        if (Input.GetKey(KeyCode.E))
                        {
                            solarPanel.RemoveDust();
                        }
                    }
                    if (col.GetComponent<Storage>() != null)
                    {

                        var storage = col.GetComponent<Storage>();

                        text = storage.IsOpen ? text = "'E' To Close Storage" : "'E' To Open Storage";
                        if (Input.GetKeyUp(KeyCode.E))
                        {
                            storage.ToggleInventory();
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
            InstructionText.text = text;
        }
    }
}
