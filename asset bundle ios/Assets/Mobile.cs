using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Notifications.iOS;
using UnityEngine;

public class Mobile : MonoBehaviour
{
    public iOSNotificationTrigger ios { get; set; }

    void Start()
    {
        StartCoroutine(RequestAuthorization());
    }
    IEnumerator RequestAuthorization()
    {
        Debug.Log(1);
        var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Badge;

        Debug.Log(2);
        using (var req = new AuthorizationRequest(authorizationOption, true))
        {
            Debug.Log(3);
            while (!req.IsFinished)
            {
                Debug.Log(4);
                yield return null;
            };

            Debug.Log(5);
            string res = "\n RequestAuthorization:";
            res += "\n finished: " + req.IsFinished;
            res += "\n granted :  " + req.Granted;
            res += "\n error:  " + req.Error;
            res += "\n deviceToken:  " + req.DeviceToken;
            Debug.Log(res);
        }
    }
}
