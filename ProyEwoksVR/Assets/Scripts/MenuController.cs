using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuCanvas;  // Canvas del menú principal
    public GameObject pauseMenuCanvas; // Canvas del menú de pausa
    public GameObject guidePanel;      // Panel de la guía
    public GameObject creditsPanel;    // Panel de los créditos
    public GameObject player;          // Referencia al jugador
    public Collider restrictedArea;    // Área restringida para el menú inicial

    private bool isPaused = false;     // Indica si el juego está pausado
    private bool isGameStarted = false; // Indica si el juego ha comenzado
    private PlayerInputActions inputActions; // Referencia a las acciones de entrada

    private BalloonThrower balloonThrower; // Referencia al script BalloonThrower para gestionar la pausa

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        balloonThrower = player.GetComponent<BalloonThrower>(); // Obtener el BalloonThrower

        // Vincular la acción de pausa con el método TogglePauseMenu
        inputActions.UI.Menu.performed += _ => TogglePauseMenu(); 
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Disable();
    }

    private void Start()
    {
        ShowMainMenu();
        LockPlayer(true); // Restringe al jugador al inicio
        Time.timeScale = 0f; // Pausa el juego al inicio
    }

    public void StartGame()
    {
        isGameStarted = true;
        mainMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        guidePanel.SetActive(false);
        creditsPanel.SetActive(false);

        LockPlayer(false); // Permite movimiento del jugador
        Time.timeScale = 1f; // Reanuda el tiempo
    }

    public void ShowGuide()
    {
        guidePanel.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void BackToMainMenu()
    {
        guidePanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        pauseMenuCanvas.SetActive(false);
        LockPlayer(false); // Permite movimiento del jugador
        isPaused = false;
        Time.timeScale = 1f; // Reanuda el tiempo
    }

    public void PauseGame()
    {
        if (!isGameStarted) return; // Solo permite pausar si el juego ha iniciado

        pauseMenuCanvas.SetActive(true);
        LockPlayer(true); // Restringe movimiento del jugador
        isPaused = true;
        Time.timeScale = 0f; // Pausa el tiempo
    }

    public void ReloadGame()
    {
        Time.timeScale = 1f; // Asegura que el tiempo esté normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia la escena actual
    }

    public void TogglePauseMenu()
    {
        if (!isGameStarted) return; // Solo permite abrir el menú de pausa si el juego ha iniciado

        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
        guidePanel.SetActive(false);
        creditsPanel.SetActive(false);

        isGameStarted = false; // Resetea el estado del juego
        Time.timeScale = 0f; // Pausa el tiempo
    }

    private void LockPlayer(bool lockMovement)
    {
        if (player != null)
        {
            var controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = !lockMovement; // Activa/desactiva el movimiento
            }

            if (restrictedArea != null)
            {
                restrictedArea.enabled = lockMovement; // Activa/desactiva el área restringida
            }
        }
    }
}
