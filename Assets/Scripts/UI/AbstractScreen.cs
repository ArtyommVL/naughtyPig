using UnityEngine;

public abstract class AbstractScreen : MonoBehaviour{
    // Events
    public event System.Action<AbstractScreen> OnShow;
    public event System.Action<AbstractScreen> OnHide;
    
    // Variables
    private bool _isShow;
    
    // Properties
    public bool IsShow => _isShow;
    
    // Lifecycles
    public virtual void Show() {
        _isShow = true;
        gameObject.SetActive(true);
        OnShow?.Invoke(this);
    }

    public virtual void Hide() {
        _isShow = false;
        gameObject.SetActive(false);
        OnHide?.Invoke(this);
    }
}
