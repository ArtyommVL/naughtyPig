using UnityEngine;

public class BombGenerator : MonoBehaviour {
    // Event
    public static event System.Action OnPop;
    
    // Variables
    [SerializeField] private Transform pig = default;
    
    // Lifecycles
    private void OnEnable() {
        UIScreenHud.OnBombButtonClick += UIScreenHud_OnBombButtonClick;
    }

    private void OnDisable() {
        UIScreenHud.OnBombButtonClick -= UIScreenHud_OnBombButtonClick;
    }

    // Private 
    private void UIScreenHud_OnBombButtonClick() {
        PopBomb();
    }
    
    // Handlers
    private void PopBomb() {
        GlobalPool.Instance.Pop<Bomb>().transform.position = pig.transform.position;
        OnPop?.Invoke();
    }
}
