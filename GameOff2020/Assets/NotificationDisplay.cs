using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class NotificationDisplay : MonoBehaviour
{
    [SerializeField]
    private NotificationEvent notification;
    [SerializeField]
    private Toggle completeCheckBox;
    public Text Description;
    private Animator _animator;
    private bool canCreateNotification = true;
    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
        completeCheckBox = GetComponentInChildren<Toggle>(true);
    }

    // Update is called once per frame
    void Update()
    {
        var currentNotification = NotificationManager.Notifications.FirstOrDefault();
        if (currentNotification != null)
        {
            if(notification == null && canCreateNotification)
            {
                StartCoroutine(AddNotification(currentNotification));

            }
            else if(notification != null && notification.EventName != currentNotification.EventName && canCreateNotification)
            {
                StartCoroutine(CompleteNotification());
            }
        
        }
        else if(currentNotification == null)
        {
            Description.text = string.Empty;
            if (_animator.isActiveAndEnabled)
            {
                StartCoroutine(CompleteNotification());
               // _animator.enabled = false;
            }
        }
    }

    IEnumerator CompleteNotification()
    {
        canCreateNotification = false;
        completeCheckBox.isOn = true;
        Description.text = string.Empty;
        _animator.SetInteger("NotificationState",1);
        yield return new WaitForSeconds(1f);
        notification = null;
        _animator.SetInteger("NotificationState", -1);
        canCreateNotification = true;
    }

    IEnumerator AddNotification(NotificationEvent newNotification)
    {
        canCreateNotification = false;

        if (!_animator.isActiveAndEnabled)
        {
            _animator.enabled = true;
        }
        notification = newNotification;
        _animator.SetInteger("NotificationState", 0);
        completeCheckBox.isOn = false;
        yield return new WaitForSeconds(1f);
        _animator.SetInteger("NotificationState", -1);
        Description.text = notification.Description;
        canCreateNotification = true;
    }
}
