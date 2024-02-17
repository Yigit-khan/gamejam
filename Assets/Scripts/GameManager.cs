using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject player;

    private bool isGamePaused = false;
    private bool playerIsDead = false;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if(playerIsDead)
        {
            GameOver();
        }
    }

    public void PlayerIsDead()
    {
        playerIsDead = true;
    }

    public void PauseGame()
    {
        if (!isGamePaused && !playerIsDead)
        {
            player.GetComponent<PlayerMovement>().GameIsPaused();
            isGamePaused = !isGamePaused;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else if (isGamePaused && !playerIsDead)
        {
            player.GetComponent<PlayerMovement>().GameIsPaused();
            isGamePaused = !isGamePaused;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
