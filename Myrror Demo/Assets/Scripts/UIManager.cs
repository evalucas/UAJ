using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    bool highlightedText = false, menuPausa = false;
    public bool isThisAMenu;
    public GameObject menu;
    public Text reanudar, menuInicial;

    private void Start()
    {
        GameManager.instance.GetUIManager(this);
    }

    private void Update()
    {
        if (!isThisAMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeMenuState();
        }
    }

    public void HighlightTextColor(Text text)
    {
        if(!highlightedText && text.GetComponentInParent<Button>().interactable != false)
        {
            text.color = Color.black;
            highlightedText = true;
        }
    }

    public void ReturnTextToNormal(Text text)
    {
        if (highlightedText && text.GetComponentInParent<Button>().interactable != false)
        {
            text.color = Color.white;
            highlightedText = false;
        }
    }

    public void ChangeScene(string scene)
    {
        GameManager.instance.ChangeScene(scene);
    }

    public void DisableButton(Button thisButton)
    {
        thisButton.interactable = false;
    }

    public void ExitGame()
    {
        GameManager.instance.ExitGame();
    }

    public void ChangeMenuState()
    {
        if (!menuPausa)
        {
            reanudar.color = Color.white;
            menuInicial.color = Color.white;
            highlightedText = false;
            menu.SetActive(true);
            menuPausa = true;
            pauseGame();
        }

        else
        {
            menu.SetActive(false);
            menuPausa = false;
            pauseGame();
        }
    }

    public void pauseGame()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}
