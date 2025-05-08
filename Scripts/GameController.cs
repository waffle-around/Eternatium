using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    //public GameObject cam3;
    //public bool isIso = false;
    //public bool notIso = true;

    public GameObject settingsPanel;
    MouseLook mouseLook;
    GameObject player;
    GameObject anim;

    private void Start()
    {
        EnableC2();

        settingsPanel.SetActive(false);

        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        if (foundPlayer != null)
        {
            player = foundPlayer;
        }

        GameObject cameras = GameObject.Find("Cameras");
        if (cameras != null)
        {
            mouseLook = cameras.GetComponentInChildren<MouseLook>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnableC2();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EnableC1();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.activeSelf)
            {
                settingsPanel.SetActive(false);
                UnpauseGame();
            }
            else
            {
                settingsPanel.SetActive(true);
                PauseGame();
            }
        }
    }

    public void EnableC1()
    {
        //isIso = false;
        //notIso = true;
        cam1.SetActive(true);
        cam2.SetActive(false);
        //cam3.SetActive(false);
    }

    public void EnableC2()
    {
        //isIso = false;
        //notIso = true;
        cam2.SetActive(true);
        cam1.SetActive(false);
        //cam3.SetActive(false);
    }

    public void EnableC3()
    {
        //notIso = false;
        //isIso = true;
        //cam3.SetActive(true);
        cam1.SetActive(false);
        cam2.SetActive(false);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;

       foreach (MouseLook ml in FindObjectsOfType<MouseLook>())
        {
            ml.enabled = false;
        }

        if (player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.enabled = false;
            }
        }
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1;

        foreach (MouseLook ml in FindObjectsOfType<MouseLook>())
        {
            ml.enabled = true;
        }

        if (player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.enabled = true;
            }
        }

    }

}
