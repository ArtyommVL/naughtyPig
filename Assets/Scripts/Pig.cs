using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {
    // Event
    public static event System.Action OnDie;
    
    // Variables
    [SerializeField] private GameSettings gameSettings = default;
    [SerializeField] private Transform pigTransform = default;
    [SerializeField] private List<GameObject> pigsImages = default;

    private int _pigHp;
    private float _pigSpeed;
    private Vector3 _startPosition;
    private Vector3 _pigPosition;
    
    private bool _isRight;
    private bool _isLeft;
    private bool _isUp;
    private bool _isDown;
    
    // Properties
    public int PigHP => _pigHp;
    public Vector3 PigPosition => _pigPosition;

    // Lifecycles
    private void Awake() {
        _pigHp = gameSettings.PigHealth;
        _pigSpeed = gameSettings.PigSpeed;
        var pigPosition = pigTransform.position;
        _startPosition = pigPosition;
        _pigPosition = pigPosition;
        ActivateImage(0);
    }

    private void Update() {
        PigDeath();
        
        if (_isRight) {
            PigMoving(Vector3.right);
        }
        if (_isLeft) {
            PigMoving(Vector3.left);
        }
        if (_isUp) {
            PigMoving(Vector3.up);
        }
        if (_isDown) {
            PigMoving(Vector3.down);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<AbstractEnemy>(out var abstractEnemy)) {
            if (abstractEnemy.CanDamage()) {
                _pigHp -= gameSettings.EnemyDamage;
            }
        }
    }

    private void OnEnable() {
        UIScreenStart.OnStart += UIScreenStart_OnStart; 
        
        UIScreenHud.OnRightButtonDown += UIScreenHud_OnRightButtonDown;
        UIScreenHud.OnLeftButtonDown += UIScreenHud_OnLeftButtonDown;
        UIScreenHud.OnUpButtonDown += UIScreenHud_OnUpButtonDown;
        UIScreenHud.OnDownButtonDown += UIScreenHud_OnDownButtonDown;

        UIScreenHud.OnRightButtonUp += UIScreenHud_OnRightButtonUp;
        UIScreenHud.OnLeftButtonUp += UIScreenHud_OnLeftButtonUp;
        UIScreenHud.OnUpButtonUp += UIScreenHud_OnUpButtonUp;
        UIScreenHud.OnDownButtonUp += UIScreenHud_OnDownButtonUp;

        UIScreenWin.OnWin += UIScreenWin_OnWin;
    }
    
    private void OnDisable() {
        UIScreenStart.OnStart -= UIScreenStart_OnStart; 

        UIScreenHud.OnRightButtonDown -= UIScreenHud_OnRightButtonDown;
        UIScreenHud.OnLeftButtonDown -= UIScreenHud_OnLeftButtonDown;
        UIScreenHud.OnUpButtonDown -= UIScreenHud_OnUpButtonDown;
        UIScreenHud.OnDownButtonDown -= UIScreenHud_OnDownButtonDown;
        
        UIScreenHud.OnRightButtonUp -= UIScreenHud_OnRightButtonUp;
        UIScreenHud.OnLeftButtonUp -= UIScreenHud_OnLeftButtonUp;
        UIScreenHud.OnUpButtonUp -= UIScreenHud_OnUpButtonUp;
        UIScreenHud.OnDownButtonUp -= UIScreenHud_OnDownButtonUp;
        
        UIScreenWin.OnWin += UIScreenWin_OnWin;
    }
    
    // Public
    public void PigDamage() {
        _pigHp -= gameSettings.BombDamage;
    }

    // Private
    private void UIScreenStart_OnStart() {
        _pigHp = gameSettings.PigHealth;
        _pigSpeed = gameSettings.PigSpeed;
        pigTransform.position = _startPosition;
    }
    
    private void UIScreenHud_OnRightButtonDown() {
        _isRight = true;
        ActivateImage(0);
    }

    private void UIScreenHud_OnLeftButtonDown() {
        _isLeft = true;
        ActivateImage(1);
    }

    private void UIScreenHud_OnUpButtonDown() {
        _isUp = true;
        ActivateImage(2);
    }

    private void UIScreenHud_OnDownButtonDown() {
        _isDown = true;
        ActivateImage(3);
    }
    
    private void UIScreenHud_OnRightButtonUp() {
        _isRight = false;
    }
    
    private void UIScreenHud_OnLeftButtonUp() {
        _isLeft = false;
    }
    
    private void UIScreenHud_OnUpButtonUp() {
        _isUp = false;
    }
    
    private void UIScreenHud_OnDownButtonUp() {
        _isDown = false;
    }

    private void UIScreenWin_OnWin() {
        pigTransform.position = _startPosition;
        _isRight = false;
        _isLeft = false;
        _isUp = false;
        _isDown = false;
        _pigHp = gameSettings.PigHealth;
    }

    // Handlers
    private void PigDeath() {
        if (_pigHp <= 0) {
            OnDie?.Invoke();
            pigTransform.position = _startPosition;
            _isRight = false;
            _isLeft = false;
            _isUp = false;
            _isDown = false;
            _pigHp = gameSettings.PigHealth;
            UIScreenManager.Instance.ShowScreen<UIScreenGameOver>();
        }
    }
    
    private void PigMoving(Vector3 direction) {
        pigTransform.position += direction*_pigSpeed;
        pigTransform.rotation = Quaternion.identity;
        _pigPosition = pigTransform.position;
    }

    private void ActivateImage(int pigIndex) {
        for (int i = 0; i < pigsImages.Count; i++) {
            pigsImages[i].gameObject.SetActive(false);
        }
        pigsImages[pigIndex].gameObject.SetActive(true);
    }
}
