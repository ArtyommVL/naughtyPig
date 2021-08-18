using System.Collections.Generic;
using UnityEngine;

public class UIScreenManager : MonoBehaviour {
    // Variables
    [SerializeField] private RectTransform canvasParent = default;
    [SerializeField] private List<AbstractScreen> screens = default;

    private AbstractScreen _enableScreen;
    private readonly List<AbstractScreen> _disableScreens = new List<AbstractScreen>(1000);
    
    // Singleton
    private static UIScreenManager _instance;

    public static UIScreenManager Instance => _instance;
    
    // Lifecycles
    private void Awake() {
        _instance = this;
        _enableScreen = null;
        InstantiateScreens();
        ShowScreen<UIScreenStart>();
    }
    
    // Handlers
    private void InstantiateScreens() {
        for (int i = 0; i < screens.Count; i++) {
            _disableScreens.Add(Instantiate(screens[i],canvasParent));
            _disableScreens[i].gameObject.SetActive(false);
        }
    }

    public void ShowScreen<T>() where T: AbstractScreen {
        if (_enableScreen != null) {
            _enableScreen.Hide();
            _enableScreen.gameObject.SetActive(false);
            _disableScreens.Add(_enableScreen);
            _enableScreen = null;
        }

        for (int i = 0; i < _disableScreens.Count; i++) {
            if (_disableScreens[i] is T) {
                var currentScreen = _disableScreens[i];
                _enableScreen = currentScreen;
                _disableScreens.Remove(currentScreen);
                _enableScreen.Show();
                _enableScreen.gameObject.SetActive(true);
            }
        }
    }
}
