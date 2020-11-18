using UnityEngine;

public class UseableObjectsManager : MonoBehaviour
{
    [SerializeField] private string useableItemTag = "UseableItem";
    Collider[] hitColliders;
    [SerializeField] private GameObject player;
    void Update()
    {
        Vector3 newPosition = player.transform.position;
        transform.position = newPosition;


        hitColliders = Physics.OverlapSphere(transform.position, 3f);
        foreach (Collider col in hitColliders)
        {
            if (col.gameObject.CompareTag(useableItemTag))
            {
                //solar panel
                if (col.GetComponent<SolarPanelDust>() != null)
                {
                    var solarPanel = col.GetComponent<SolarPanelDust>();
                    solarPanel.isUseable = true;
                }
            }
        }
    }
}
