using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : UIBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler {
    // Events
    public event System.Action OnUp;
    public event System.Action OnDown;
    public event System.Action OnClick;
    
    // Variables
    [SerializeField] private Button button = default;

    private Button _cachedButton;
    
    // Lifecycles
    protected override void Awake() {
        if (button) {
            _cachedButton = button;
        }else {
            _cachedButton = GetComponent<Button>();
        }
    }

    
    protected override void OnEnable() {
    }
    
    // Public
    public void OnPointerUp(PointerEventData eventData) {
        OnUp?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData) {
        OnDown?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnClick?.Invoke();
    }
}
