using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {
    public string nextLevel;
    GameObject cam;
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cam.GetComponent<GiroCamara>().HRotation();
        Grupo06.Telemetria.Instance.TrackEvent(
            Grupo06.Telemetria.Instance.LevelEnd().
                                        Nivel(gameObject.scene.buildIndex)
        );
        ChangeScene();
        
    }
    public void ChangeScene()
    {
        GameManager.instance.ChangeScene(nextLevel);
    }
}
