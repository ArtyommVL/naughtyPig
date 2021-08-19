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
        UIScreenStart.OnStart += UIScreenStart_OnStart;
        UIScreenHud.OnBombButtonClick += UIScreenHud_OnBombButtonClick;
    }

    private void OnDisable() {
        UIScreenStart.OnStart -= UIScreenStart_OnStart;
        UIScreenHud.OnBombButtonClick -= UIScreenHud_OnBombButtonClick;
    }
    
    // Private
    private void UIScreenStart_OnStart() {
        dog.gameObject.SetActive(true);
        farmer.gameObject.SetActive(true);
    }

    private void UIScreenHud_OnBombButtonClick() {
        _isShowScreen = true;
    }
}
