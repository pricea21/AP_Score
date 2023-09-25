using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enlarge : MonoBehaviour
{
    public delegate void GetLarger(Collision getBig);

    public static event GetLarger OnGetLarger;

    void OnCollisionEnter(Collision getBig)
    {
        if (getBig.collider.tag == "Sphere")
        {
            if (OnGetLarger != null)
            {
                OnGetLarger(getBig);
            }
        }
    }
}
