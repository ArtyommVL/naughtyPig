using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {
    // Variables
    [SerializeField] private GameSettings gameSettings = default;
    [SerializeField] private Transform pigTransform = default;
    [SerializeField] private List<GameObject> pigsImages = default;

    private int _pigHP;
    private Vector3 _pigPosition;
    private bool _isRight;
    private bool _isLeft;
    private bool _isUp;
    private bool _isDown;
    
    // Properties
    public int PigHP => _pigHP;
    public Vector3 PigPosition => _pigPosition;

    // Lifecycles
    private void Awake() {
        _pigHP = gameSettings.PigHealth;
        _pigPosition = pigTransform.position;
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
        /*if (other.gameObject.TryGetComponent<AbstractEnemy>(out var abstractEnemy)) {
            if (abstractEnemy.CanDamage()) {
                _pigHP -= gameSettings.EnemyDamage;
            }
        }*/
    }

    private void OnEnable() {
        UIScreenHud.OnRightButtonDown += GameScreen_OnRightButtonDown;
        UIScreenHud.OnLeftButtonDown += GameScreen_OnLeftButtonDown;
        UIScreenHud.OnUpButtonDown += GameScreen_OnUpButtonDown;
        UIScreenHud.OnDownButtonDown += GameScreen_OnDownButtonDown;

        UIScreenHud.OnRightButtonUp += GameScreen_OnRightButtonUp;
        UIScreenHud.OnLeftButtonUp += GameScreen_OnLeftButtonUp;
        UIScreenHud.OnUpButtonUp += GameScreen_OnUpButtonUp;
        UIScreenHud.OnDownButtonUp += GameScreen_OnDownButtonUp;
    }
    
    private void OnDisable() {
        UIScreenHud.OnRightButtonDown -= GameScreen_OnRightButtonDown;
        UIScreenHud.OnLeftButtonDown -= GameScreen_OnLeftButtonDown;
        UIScreenHud.OnUpButtonDown -= GameScreen_OnUpButtonDown;
        UIScreenHud.OnDownButtonDown -= GameScreen_OnDownButtonDown;
        
        UIScreenHud.OnRightButtonUp -= GameScreen_OnRightButtonUp;
        UIScreenHud.OnLeftButtonUp -= GameScreen_OnLeftButtonUp;
        UIScreenHud.OnUpButtonUp -= GameScreen_OnUpButtonUp;
        UIScreenHud.OnDownButtonUp -= GameScreen_OnDownButtonUp;
    }

    // Private
    private void GameScreen_OnRightButtonDown() {
        _isRight = true;
        ActivateImage(0);
    }

    private void GameScreen_OnLeftButtonDown() {
        _isLeft = true;
        ActivateImage(1);
    }

    private void GameScreen_OnUpButtonDown() {
        _isUp = true;
        ActivateImage(2);
    }

    private void GameScreen_OnDownButtonDown() {
        _isDown = true;
        ActivateImage(3);
    }
    
    private void GameScreen_OnRightButtonUp() {
        _isRight = false;
    }
    
    private void GameScreen_OnLeftButtonUp() {
        _isLeft = false;
    }
    
    private void GameScreen_OnUpButtonUp() {
        _isUp = false;
    }
    
    private void GameScreen_OnDownButtonUp() {
        _isDown = false;
    }

    // Handlers
    private void PigDeath() {
        if (_pigHP <= 0) {
            gameObject.SetActive(false);
        }
    }
    
    private void PigMoving(Vector3 direction) {
        pigTransform.position += direction*gameSettings.PigSpeed;
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
