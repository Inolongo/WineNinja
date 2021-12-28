using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    private const float SpawnRate = 3f;

    [SerializeField] private GameObject endGameObj;
    [SerializeField] private TextMeshProUGUI endGameScore;
    [SerializeField] private Button restartButton;

    public event Action GameEnded;

    private int _score;
    public TextMeshProUGUI scoreText;
    private Coroutine _spawnRoutine;


    private void Start()
    {
        restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        scoreText.text = "Score " + _score;
    }

    public void StartGame()
    {
        _spawnRoutine = StartCoroutine(SpawnTarget());
        _score = 0;
        endGameObj.SetActive(false);
    }

    private IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnRate);

            var typeToSpawn = GetRandTargetTypeToSpawn();

            var target = SpawnManager.Instance.TrySpawnTarget(typeToSpawn);

            if (target == null || target.TargetType == TargetType.Bomb) continue;
            
            target.TargetFallen += OnTargetFall;
        }
    }

    private TargetType GetRandTargetTypeToSpawn()
    {
        var index = Random.Range(1, 100);

        if (index < 10)
        {
            return TargetType.Byhlo;
        }

        if (index < 40)
        {
            return TargetType.Bomb;
        }

        if (index < 65)
        {
            return TargetType.Pizza;
        }
        else
        {
            return TargetType.Cookie;
        }
    }


    private void OnTargetFall(Target target)
    {
        target.TargetFallen -= OnTargetFall;

        if(target.TargetType != TargetType.Bomb && target.transform.position.y < 10)
        {
            //GameOverMethod();
        }
    }

    // private void GameOverMethod()
    // {
    //     StopCoroutine(_spawnRoutine);
    //     endGameObj.SetActive(true);
    //     GameEnded?.Invoke();
    //     endGameScore.text = "Score " + _score;
    //     _score = 0;
    // }

    private void RestartGame()
    {
        StartGame();
    }

    private void OnRestartButtonClick()
    {
        RestartGame();
    }
}