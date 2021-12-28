using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button startGame;

    [SerializeField] private Button exitGame;

    [SerializeField] private Button suckMyDickButton;

    [SerializeField] private Image frog;
    
    private void Start()
    {
        startGame.onClick.AddListener(OnGameStartButtonClick);
        exitGame.onClick.AddListener(OnExitButtonClick);
        suckMyDickButton.onClick.AddListener(OnSuckButtonClick);
    }

    private void OnSuckButtonClick()
    {
        
        frog.gameObject.SetActive(true);
        StartCoroutine(ExitGame());

    }

    private static void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void OnGameStartButtonClick()
    {
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }

    private IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
}
