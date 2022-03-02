using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadMusic : MonoBehaviour {

    public static DontDestroyOnLoadMusic instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this.gameObject);
        }
    }
}
