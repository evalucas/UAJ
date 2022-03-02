using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    bool used = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!used)
        {
            GameManager.instance.SetSpawn(this.gameObject.transform);
            used = true;
        }
    }
}
