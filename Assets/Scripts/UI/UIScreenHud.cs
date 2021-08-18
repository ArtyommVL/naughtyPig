using UnityEngine;

public class UIScreenHud : AbstractScreen {
    // Events
    public static event System.Action OnRightButtonDown;
    public static event System.Action OnLeftButtonDown;
    public static event System.Action OnUpButtonDown;
    public static event System.Action OnDownButtonDown;

    public static event System.Action OnRightButtonUp;
    public static event System.Action OnLeftButtonUp;
    public static event System.Action OnUpButtonUp;
    public static event System.Action OnDownButtonUp;
    public static event System.Action OnBombButtonClick;

    // Variables
    [SerializeField] private UIButton rightButton = default;
    [SerializeField] private UIButton leftButton = default;
    [SerializeField] private UIButton upButton = default;
    [SerializeField] private UIButton downButton = default;
    [SerializeField] private UIButton bombButton = default;

    // Lifecycles
    private void OnEnable() {
        rightButton.OnDown += RightButton_OnDown;
        leftButton.OnDown += LeftButton_OnDown;
        upButton.OnDown += UpButton_OnDown;
        downButton.OnDown += DownButton_OnDown;

        rightButton.OnUp += RightButton_OnUp;
        leftButton.OnUp += LeftButton_OnUp;
        upButton.OnUp += UpButton_OnUp;
        downButton.OnUp += DownButton_OnUp;
        
        bombButton.OnClick += BombButton_OnClick;
    }

    private void OnDisable() {
        rightButton.OnDown -= RightButton_OnDown;
        leftButton.OnDown -= LeftButton_OnDown;
        upButton.OnDown -= UpButton_OnDown;
        downButton.OnDown -= DownButton_OnDown;
        
        rightButton.OnUp -= RightButton_OnUp;
        leftButton.OnUp -= LeftButton_OnUp;
        upButton.OnUp -= UpButton_OnUp;
        downButton.OnUp -= DownButton_OnUp;
        
        bombButton.OnClick -= BombButton_OnClick;
    }
    
    // Private
    private void RightButton_OnDown() {
        OnRightButtonDown?.Invoke();
    }

    private void LeftButton_OnDown() {
        OnLeftButtonDown?.Invoke();
    }

    private void UpButton_OnDown() {
        OnUpButtonDown?.Invoke();
    }

    private void DownButton_OnDown() {
        OnDownButtonDown?.Invoke();
    }
    
    private void RightButton_OnUp() {
        OnRightButtonUp?.Invoke();
    }
    
    private void LeftButton_OnUp() {
        OnLeftButtonUp?.Invoke();
    }
    
    private void UpButton_OnUp() {
        OnUpButtonUp?.Invoke();
    }
    
    private void DownButton_OnUp() {
        OnDownButtonUp?.Invoke();
    }
    
    private void BombButton_OnClick() {
        OnBombButtonClick?.Invoke();
    }
}
