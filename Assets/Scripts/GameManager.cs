using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentCoin;
    public int currentHealth;
    public Text coinText;

    public Text healthText;

    public GameObject thePlayer;
    public GameObject mainMenu;
    public GameObject deathMenu;
    public GameObject endMenu;
    public GameObject pauseMenu;
    
    public GameObject mainCamera;

    private bool menuActive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && menuActive == false)
        {
            PauseGame();
        }
    }

    public void AddCoin(int coinToAdd)
    {
        currentCoin += coinToAdd;
        coinText.text = "Coins : " + currentCoin;

    }

    public void SetHealth(int actualHealth)
    {
        healthText.text = "Health : " + actualHealth;
        currentHealth = actualHealth;
    }

    public void GameStart()
    {
        mainMenu.SetActive(false);
        healthText.enabled = true;
        coinText.enabled = true;
        thePlayer.SetActive(true);

        mainCamera.GetComponent<CameraController>().activeMove = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        menuActive = false;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void DeathPlayer()
    {
        deathMenu.SetActive(true);
        menuActive = true;
        healthText.enabled = false;
        
        thePlayer.SetActive(false);

        mainCamera.GetComponent<CameraController>().activeMove = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LevelEnd()
    {
        endMenu.SetActive(true);
        menuActive = true;
        healthText.enabled = false;
        
        thePlayer.SetActive(false);

        mainCamera.GetComponent<CameraController>().activeMove = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera.GetComponent<CameraController>().activeMove = false;
        menuActive = true;
    }
    
    public void ContinueGame()
    {
        mainCamera.GetComponent<CameraController>().activeMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        menuActive = false;
        
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        AddCoin(data.coin);
        this.GetComponent<HealthManager>().currentHealth = data.health;
        SetHealth(data.health);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        thePlayer.transform.position = position;

        GameStart();
    }
}
