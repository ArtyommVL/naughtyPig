using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    // Event
    public static event System.Action<Bomb> OnDestroy;
    
    // Variables
    [SerializeField] private GameSettings gameSettings = default;
    [SerializeField] private List<GameObject> bombsSprites = default;

    private Collider2D _bombCollider;
    private float _timer = 0;
    private float _lifecycleTimer = 0;
    private bool _isTimer;
    
    // Lifecycles
    private void Awake() {
        _bombCollider = gameObject.GetComponent<Collider2D>();
        _bombCollider.enabled = false;
    }

    private void Update() {
        if (_isTimer) {
            _lifecycleTimer += Time.deltaTime;
            if (_lifecycleTimer >= gameSettings.AttackTime) {
                _bombCollider.enabled = true;
                bombsSprites[0].SetActive(false);
                bombsSprites[1].SetActive(true);
                _timer += Time.deltaTime;
                if (_timer >= gameSettings.PushTime) {
                    bombsSprites[0].SetActive(true);
                    bombsSprites[1].SetActive(false);
                    _lifecycleTimer = 0;
                    _timer = 0;
                    GlobalPool.Instance.Push(this);
                    _isTimer = false;
                }
            }
        } 
    }

    private void OnEnable() { 
        UIScreenGameOver.OnRetry += UIScreenGameOver_OnRetry;
        BombGenerator.OnPop += BombGenerator_OnPop;
    }

    private void OnDisable() {
        UIScreenGameOver.OnRetry -= UIScreenGameOver_OnRetry;
        BombGenerator.OnPop -= BombGenerator_OnPop;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.TryGetComponent<AbstractEnemy>(out var abstractEnemy)) {
            if (abstractEnemy.CanDamage()) {
                abstractEnemy.EnemyDamage();
            }
        }
        
        if (other.gameObject.TryGetComponent<Pig>(out var pig)) {
            pig.PigDamage();
        }
    }
    
    // Private
    private void UIScreenGameOver_OnRetry() {
        _isTimer = false; 
        _timer = 0;
        _lifecycleTimer = 0;
        GlobalPool.Instance.Push(this);
    }
    
    private void BombGenerator_OnPop() {
        _isTimer = true;
    }
}
