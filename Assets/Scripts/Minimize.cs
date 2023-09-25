using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimize : MonoBehaviour
{
    public delegate void GetSmaller(Collision getSmall);

    public static event GetSmaller OnGetSmaller;

    void OnCollisionEnter(Collision getSmall)
    {
        if (getSmall.collider.tag == "Cylinder")
        {
            if (OnGetSmaller != null)
            {
                OnGetSmaller(getSmall);
            }
        }
    }
}
