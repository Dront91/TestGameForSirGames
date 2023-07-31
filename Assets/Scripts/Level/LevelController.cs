using UnityEngine;
using System;
using System.Collections.Generic;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Gate _gate;
    [SerializeField] private CoinCounter _coinCounter;
    [SerializeField] private LevelSettings _settings;
    [SerializeField] private SquareArea _enemySpawnArea;
    public Action OnGameStart;
    public Action OnGameEnd;
    public Action OnPlayerWin;
    public Action OnPlayerLose;
    private bool _isCountdownBegin = false;
    private float _countdownTimer = 3;
    public float CountdownTimer => _countdownTimer;
    private GameObject _player;
    private List<Destructable> _enemyList;
    private int _coinCount = 0;
    public int CoinCount => _coinCount;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _enemyList = new List<Destructable>();
        SpawnPlayer();
        SpawnEnemies();
        _isCountdownBegin = true;
    }
    private void Update()
    {
        if(_isCountdownBegin)
        {
            _countdownTimer -= Time.deltaTime;
            if(_countdownTimer <= 0 )
            {
                StartGame();
            }
        }
    }
    public void PlayerWon()
    {
        OnPlayerWin?.Invoke();
        OnGameEnd?.Invoke();
    }
    private void SpawnPlayer()
    {
        _player = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
        _player.GetComponent<Destructable>().OnDeath += OnPlayerDeath;
    }
    private void OnDestroy()
    {
        if(_player != null)
        {
            _player.GetComponent<Destructable>().OnDeath -= OnPlayerDeath;
        }
        foreach(var e in _enemyList)
        {
            e.OnDeath -= OnEnemyDie;
        }
    }

    private void OnPlayerDeath(Destructable obj)
    {
        OnGameEnd?.Invoke();
        OnPlayerLose?.Invoke();
    }

    private void SpawnEnemies()
    {
        foreach((GameObject prefab, int count) in _settings.EnumerateSquads())
        {
            for(int i = 0; i < count; i++)
            {
                var e = Instantiate(prefab, _enemySpawnArea.GetRandomPointInSquare(), Quaternion.identity);
                var dest = e.GetComponent<Destructable>();
                _enemyList.Add(dest);
                dest.OnDeath += OnEnemyDie;
            }
        }
    }

    private void OnEnemyDie(Destructable obj)
    {
        obj.OnDeath -= OnEnemyDie;
        _enemyList.Remove(obj);
        if(_enemyList.Count == 0)
        {
            _gate.OpenGate();
        }
    }

    private void StartGame()
    {
        OnGameStart?.Invoke();
    }

    internal void AddCoin(int coin)
    {
        _coinCount += coin;
        _coinCounter.UpdateCoinUI();
    }
}
