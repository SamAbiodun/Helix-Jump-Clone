using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelComplete;
    public static bool isGameStarted;
    public static bool mute = false;

    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject gamePlayPanel;
    public GameObject startMenuPanel;

    public static int currentLevelIndex;
    public Slider gameProgressSlider;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;

    public static int numberOfPassedRings;

    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }


    // Start is called before the first frame update
    void Start()
    {
        numberOfPassedRings = 0;
        isGameStarted = gameOver = levelComplete = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Update UI elements
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex+1).ToString();

        int progress = numberOfPassedRings * 100 / FindObjectOfType<HelixManager>().numberOfRings;
        gameProgressSlider.value = progress;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGameStarted)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            isGameStarted = true;
            gamePlayPanel.SetActive(true);
            startMenuPanel.SetActive(false);
        }


        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("Level");
            }
        }

        if (levelComplete)
        {
            levelCompletedPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex + 1);
                SceneManager.LoadScene("Level");
            }
        }
    }
}
