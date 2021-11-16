using UnityEngine;
using UnityEngine.SceneManagement;
using Supyrb;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject[] prefabsToSpawn;

    private List<GameObject> objectsToDestroy = new List<GameObject>();
    private GameStartSignal gameStartSignal;
    private GameOverSignal gameOverSignal;

    private void Awake()
    {
        Signals.Get(out gameStartSignal);
        Signals.Get(out gameOverSignal);

        gameOverSignal.AddListener(DestroyGameObjects);
    }
    public void StartGame()
    {
        foreach (var go in prefabsToSpawn)
            objectsToDestroy.Add(Instantiate(go));

        gameStartSignal?.Dispatch();
    }
    public void RestartGame()
    {
        Signals.Clear();
        SceneManager.LoadScene(0);
    }
    private void DestroyGameObjects()
    {
        foreach (var go in objectsToDestroy)
            Destroy(go);
    }
}
