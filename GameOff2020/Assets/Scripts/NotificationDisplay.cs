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
            if (notification == null && canCreateNotification)
            {
                StartCoroutine(AddNotification(currentNotification));

            }
            else if (notification != null && notification.EventName != currentNotification.EventName && canCreateNotification)
            {
                StartCoroutine(CompleteNotification());
            }

        }
        else if (currentNotification == null)
        {
            Description.text = string.Empty;
            if (_animator.isActiveAndEnabled)
            {
                StartCoroutine(CompleteNotification());

            }
        }
    }

    IEnumerator CompleteNotification()
    {
        canCreateNotification = false;
        completeCheckBox.isOn = true;
        yield return new WaitForSeconds(0.5f);
        completeCheckBox.gameObject.SetActive(false);

        Description.text = string.Empty;
        _animator.SetInteger("NotificationState", 1);
        yield return new WaitForSeconds(0.5f);

        notification = null;
        //    _animator.SetInteger("NotificationState", -1);
        canCreateNotification = true;
        //if (!NotificationManager.Notifications.Any())
        //{
        //    yield return new WaitForSeconds(1.0f);
        //    _animator.enabled = false;
        //}    
          
    }

    IEnumerator AddNotification(NotificationEvent newNotification)
    {
        StopCoroutine(CompleteNotification());
        canCreateNotification = false;
        completeCheckBox.gameObject.SetActive(false);

        if (!_animator.isActiveAndEnabled)
        {
            _animator.enabled = true;
        }
        notification = newNotification;
        _animator.SetInteger("NotificationState", 0);
        completeCheckBox.isOn = false;

        yield return new WaitForSeconds(0.8f);
        //   _animator.SetInteger("NotificationState", -1);
        NotificationManager.Notifications.First().IsReady = true;
        completeCheckBox.gameObject.SetActive(true);
        Description.text = notification.Description;
        notification.IsReady = true;

        canCreateNotification = true;
    }
}
