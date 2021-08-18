using System.Collections.Generic;
using UnityEngine;

public class GlobalPool : MonoBehaviour {
    // Variables
    [SerializeField] private int poolSize = 10;
    [SerializeField] private Transform poolParent = default;
    [SerializeField] private List<Bomb> abstractParts = default;

    private readonly List<Bomb> _enableCache = new List<Bomb>(1000);
    private readonly List<Bomb> _disableCache = new List<Bomb>(1000);
    
    // Singleton
    private static GlobalPool _instance;

    public static GlobalPool Instance => _instance;

    // Lifecycles
    private void Awake() {
        _instance = this;
        SpawnPool();
    }
    
    // Private
    private void SpawnPool() {
        for (int i = 0; i < abstractParts.Count; i++) {
            for (int j = 0; j < poolSize; j++) {
                _disableCache.Add(Instantiate(abstractParts[i], poolParent));
            }
        }

        for (int i = 0; i < _disableCache.Count; i++) {
            _disableCache[i].gameObject.SetActive(false);
        }
    }
    
    // Public
    public T Pop<T>() where T : Bomb {
        for (int i = 0; i < _disableCache.Count; i++) {
            if (_disableCache[i] is T) {
                var currentPart = _disableCache[i];
                currentPart.gameObject.SetActive(true);
                _disableCache.Remove(currentPart);
                _enableCache.Add(currentPart);
                return currentPart as T;
            }
        }
        
        for (int i = 0; i < abstractParts.Count; i++) {
            if (abstractParts[i] is T) {
                _enableCache.Add(Instantiate(abstractParts[i], poolParent));
                _enableCache[_enableCache.Count-1].gameObject.SetActive(true);
            }
        }
        return _enableCache[_enableCache.Count-1] as T ;
    }

    public void Push(Bomb abstractPart) {
        _enableCache.Remove(abstractPart);
        abstractPart.gameObject.SetActive(false);
        _disableCache.Add(abstractPart);
    }
}
