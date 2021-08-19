using UnityEngine;

public class UIScreenWin : AbstractScreen {
    // Event
    public static event System.Action OnWin;
    
    // Variables
    [SerializeField] private UIButton winButton = default;
    
    // Lifecycles
    private void OnEnable() {
        winButton.OnClick += OnWin_Click;
    }

    private void OnDisable() {
        winButton.OnClick -= OnWin_Click;
    }

    // Private
    private void OnWin_Click() {
        UIScreenManager.Instance.ShowScreen<UIScreenStart>();
        OnWin?.Invoke();
    }
}
