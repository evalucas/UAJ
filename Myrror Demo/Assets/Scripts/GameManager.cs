using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Grupo06;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    Transform respawnPoint;
    GameObject player;
    MovementPlayer movementPlayer;
    UIManager uiManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            Telemetria.Instance.TrackEvent(Telemetria.Instance.SessionStart());
        }

        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Nivel 01");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("Nivel 02");

        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("Nivel 03");

        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("Nivel 04");
        }
#endif



    }

    //Recibe a un GameObject y lo establece como el "player"
    public void SetPlayer(GameObject thisPlayer)
    {
        player = thisPlayer;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void PlayerDeath()
    {
        //Debug.Log("PlayerDeath Start");
        movementPlayer = player.GetComponent<MovementPlayer>();
        movementPlayer.FlipOnDeath();
        movementPlayer.Respawn(respawnPoint);
        Telemetria.Instance.TrackEvent(
            Telemetria.Instance.Death().
                                X(transform.position.x).
                                Y(transform.position.y)
        );
        //Debug.Log("PlayerDeath End");
    }

    public void SetSpawn(Transform newSpawn)
    {
        respawnPoint = newSpawn;
        //Debug.Log("Spawn set");
    }

    public void ChangeScene(string scene)
    {
        FMODUnity.StudioEventEmitter audio = GetComponent<FMODUnity.StudioEventEmitter>();
        if (audio != null)
        {
            if (audio.IsPlaying())
            {
                
                if (scene == "Créditos" || scene == "Controles") audio.Stop();
            }
            else audio.Play();
        }

        if(scene == "Nivel 01")
        {
            Invoke("endTest", 900); //15 minutos
        }

        SceneManager.LoadScene(scene);
    }

    public void endTest()
    {
        ChangeScene("Finale");
    }

    public void GetUIManager(UIManager thisUIManager)
    {
        uiManager = this.GetComponent<UIManager>();
    }

    public void ExitGame()
    {
        Debug.Log("exiting...");
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        Telemetria.Instance.TrackEvent(Telemetria.Instance.SessionEnd());
    }
}
