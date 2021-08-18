using UnityEngine;

public class UIScreenStart : AbstractScreen {
    // Events
    public static event System.Action OnStart; 
    
    // Variables
    [SerializeField] private UIButton startButton = default;
    
    // Lifecycles
    private void OnEnable() {
        startButton.OnClick += OnStart_Click;
    }

    private void OnDisable() {
        startButton.OnClick -= OnStart_Click;
    }

    // Private
    private void OnStart_Click() {
        UIScreenManager.Instance.ShowScreen<UIScreenHud>();
        OnStart?.Invoke();
    }
}
