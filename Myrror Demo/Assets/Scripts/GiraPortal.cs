using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraPortal : MonoBehaviour {

    public void Girar()
    {
        transform.Rotate(new Vector3(0, 0, 180), Space.Self);
    }
}
