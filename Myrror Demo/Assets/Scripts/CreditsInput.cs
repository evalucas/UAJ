using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsInput : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) GameManager.instance.ChangeScene("Menu");
	}
}
