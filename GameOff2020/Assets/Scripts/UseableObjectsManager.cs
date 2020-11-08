using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableObjectsManager : MonoBehaviour
{
    [SerializeField] private string useableItemTag = "UseableItem";
    [SerializeField] private Material useableItemMaterial;
    private Material defaultItemMaterial;
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
                float dist = Vector3.Distance(player.transform.position, hit.point);
                if (dist < useableRange)
                {
                    var itemRenderer = (Renderer)item.GetComponentInChildren(typeof(Renderer));
                    if (itemRenderer != null)
                    {
                        defaultItemMaterial = itemRenderer.material;
                        itemRenderer.material = useableItemMaterial;
                    }
                    _currentItem = item;
                }
            }
        }
    }
}
