using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public InputAction sceneReset;
    public InputAction menuInteract;
    private GameObject menu;
    private bool menuActive = true;
    private WaitForSeconds delay;
    private void Start()
    {
        sceneReset.Enable();
        menuInteract.Enable();
        menu = GameObject.FindGameObjectWithTag("Menu");
        delay = new WaitForSeconds(0.5f);
    }

    void Update()
    {

        if (sceneReset.IsPressed())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (menuInteract.IsPressed())
        {
            menuActive = !menuActive;
            menu.SetActive(menuActive);
            menuInteract.Disable();
            StartCoroutine(MenuDelay());
        }
    }

    IEnumerator MenuDelay(){
        yield return delay;
        menuInteract.Enable();
    }

}
