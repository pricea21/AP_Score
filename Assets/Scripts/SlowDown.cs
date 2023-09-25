using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    public delegate void GetSlow(Collider slowMo);

    public static event GetSlow OnGoSlow;

    void OnTriggerEnter(Collider slowMo)
    {
        if (slowMo.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            if (OnGoSlow != null)
            {
                OnGoSlow(slowMo);
            }
        }
    }
}
