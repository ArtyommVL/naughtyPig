using UnityEngine;

public class UIScreenGameOver : AbstractScreen{
    // Event
    public static event System.Action OnRetry;
    
    // Variables
    [SerializeField] private UIButton retryButton = default;
    
    // Lifecycles
    private void OnEnable() {
        retryButton.OnClick += OnRetry_Click;
    }

    private void OnDisable() {
        retryButton.OnClick -= OnRetry_Click;
    }

    // Private
    private void OnRetry_Click() {
        UIScreenManager.Instance.ShowScreen<UIScreenStart>();
        OnRetry?.Invoke();
    }
}
