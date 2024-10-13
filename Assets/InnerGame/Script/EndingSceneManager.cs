using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingSceneManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] endingSceneBackgroundSprites;
    [SerializeField]
    private PlayerDiagnostics[] playerDiagnostics;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private Button quitButton;

    [SerializeField]
    private GameStatistics statistics;

    private void Awake()
    {
        if (endingSceneBackgroundSprites == null)
        {
            throw new NullReferenceException(nameof(endingSceneBackgroundSprites));
        }
        if (retryButton == null)
        {
            throw new NullReferenceException(nameof(retryButton));
        }
        if (quitButton == null)
        {
            throw new NullReferenceException(nameof(quitButton));
        }

        retryButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameTest");
        });
        quitButton.onClick.AddListener(() =>
        {
            // TODO: load main scene
        });
    }

    private void Start()
    {
        statistics = GameStatistics.Instance;
        int playerIndex = statistics.Player1JarAttackCount > statistics.Player2JarAttackCount ? 0 : 1;
        image.sprite = endingSceneBackgroundSprites[playerIndex];
        for (int i = 0; i < playerDiagnostics.Length; i++)
        {
            playerDiagnostics[i].gameObject.SetActive(i == playerIndex);
        }
    }
}
