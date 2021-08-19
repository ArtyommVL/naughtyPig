using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    // Variables
    [SerializeField] private Dog dog = default;
    [SerializeField] private Farmer farmer = default;

    private bool _isShowScreen;
    
    // Lifecycles
    private void Awake() {
        _isShowScreen = true;
    }

    private void Update() {
        if (_isShowScreen) {
            if (!dog.CanDamage() && !farmer.CanDamage()) {
                _isShowScreen = false;
                UIScreenManager.Instance.ShowScreen<UIScreenWin>();
            }
        }
    }

    private void OnEnable() {
        UIScreenHud.OnBombButtonClick += UIScreenHud_OnBombButtonClick;
        UIScreenGameOver.OnRetry += UIScreenGameOver_OnRetry;
        UIScreenWin.OnWin += UIScreenWin_OnWin;
    }

    private void OnDisable() {
        UIScreenHud.OnBombButtonClick -= UIScreenHud_OnBombButtonClick;
        UIScreenGameOver.OnRetry -= UIScreenGameOver_OnRetry;
        UIScreenWin.OnWin -= UIScreenWin_OnWin;
    }
    
    // Private
    private void UIScreenHud_OnBombButtonClick() {
        _isShowScreen = true;
    }

    private void UIScreenGameOver_OnRetry() {
        ActivateObjects();
    }

    private void UIScreenWin_OnWin() {
        ActivateObjects();
    }
    
    // Handlers
    private void ActivateObjects(){
        dog.gameObject.SetActive(true);
        farmer.gameObject.SetActive(true);
    }
}
