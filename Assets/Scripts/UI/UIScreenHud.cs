using UnityEngine;

public class UIScreenHud : AbstractScreen {
    // Events
    public static event System.Action OnRightButtonDown;
    public static event System.Action OnLeftButtonDown;
    public static event System.Action OnUpButtonDown;
    public static event System.Action OnDownButtonDown;
    public static event System.Action OnBombButtonDown;
    
    public static event System.Action OnRightButtonUp;
    public static event System.Action OnLeftButtonUp;
    public static event System.Action OnUpButtonUp;
    public static event System.Action OnDownButtonUp;
    public static event System.Action OnBombButtonUp;

    // Variables
    [SerializeField] private UIButton rightButton = default;
    [SerializeField] private UIButton leftButton = default;
    [SerializeField] private UIButton upButton = default;
    [SerializeField] private UIButton downButton = default;
    [SerializeField] private UIButton bombButton = default;

    // Lifecycles
    private void OnEnable() {
        rightButton.OnDown += RightButton_OnButtonDown;
        leftButton.OnDown += LeftButton_OnButtonDown;
        upButton.OnDown += UpButton_OnButtonDown;
        downButton.OnDown += DownButton_OnButtonDown;
        bombButton.OnDown += BombButton_OnButtonDown;
        
        rightButton.OnUp += RightButton_OnButtonUp;
        leftButton.OnUp += LeftButton_OnButtonUp;
        upButton.OnUp += UpButton_OnButtonUp;
        downButton.OnUp += DownButton_OnButtonUp;
        bombButton.OnUp += BombButton_OnButtonUp;
    }

    private void OnDisable() {
        rightButton.OnUp -= RightButton_OnButtonUp;
        leftButton.OnUp -= LeftButton_OnButtonUp;
        upButton.OnUp -= UpButton_OnButtonUp;
        downButton.OnUp -= DownButton_OnButtonUp;
        bombButton.OnUp -= BombButton_OnButtonUp;
    }
    
    // Private
    private void RightButton_OnButtonDown() {
        OnRightButtonDown?.Invoke();
    }

    private void LeftButton_OnButtonDown() {
        OnLeftButtonDown?.Invoke();
    }

    private void UpButton_OnButtonDown() {
        OnUpButtonDown?.Invoke();
    }

    private void DownButton_OnButtonDown() {
        OnDownButtonDown?.Invoke();
    }

    private void BombButton_OnButtonDown() {
        OnBombButtonDown?.Invoke();
    }
    
    private void RightButton_OnButtonUp() {
        OnRightButtonUp?.Invoke();
    }
    
    private void LeftButton_OnButtonUp() {
        OnLeftButtonUp?.Invoke();
    }
    
    private void UpButton_OnButtonUp() {
        OnUpButtonUp?.Invoke();
    }
    
    private void DownButton_OnButtonUp() {
        OnDownButtonUp?.Invoke();
    }
    
    private void BombButton_OnButtonUp() {
        OnBombButtonUp?.Invoke();
    }
}
