using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableObjectsManager : MonoBehaviour
{
    [SerializeField] private string useableItemTag = "UseableItem";
    [SerializeField] private Material useableItemMaterial;
    [SerializeField] private Material defaultItemMaterial;
    [SerializeField] private float useableRange;
    Rigidbody player;
    private Transform _currentItem;

    void Update()
    {
        if (_currentItem != null)
        {
            var itemRenderer = _currentItem.GetComponent<Renderer>();
            itemRenderer.material = defaultItemMaterial;
            _currentItem = null;
        }
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        player = GameObject.Find("Player_Astronaut").GetComponent<Rigidbody>();
        if (Physics.Raycast(ray, out hit))
        {
            var item = hit.transform;
            if (item.CompareTag(useableItemTag))
            {
                float dist = Vector3.Distance(player.transform.position, item.position);
                if (dist < useableRange)
                {
                    var itemRenderer = item.GetComponent<Renderer>();
                    if (itemRenderer != null)
                    {
                        itemRenderer.material = useableItemMaterial;
                    }
                    _currentItem = item;
                }
            }
        }
    }
}
