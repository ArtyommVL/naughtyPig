using UnityEngine;

public class EnemyManager : MonoBehaviour {
    // Variables
    [SerializeField] private Transform dog = default;
    [SerializeField] private Transform farmer = default;

    private Vector3 _startPositionDog;
    private Vector3 _startPositionFarmer;
    
    // Lifecycle
    private void Awake() {
        _startPositionDog = dog.transform.position;
        Debug.Log(_startPositionDog);
        _startPositionFarmer = farmer.transform.position;
    }

    private void OnEnable() {
        UIScreenStart.OnStart += UIScreenStart_OnStart;
    }

    private void OnDisable() {
        UIScreenStart.OnStart -= UIScreenStart_OnStart;
    }
    
    // Private
    private void UIScreenStart_OnStart() {
        dog.gameObject.SetActive(true);
        farmer.gameObject.SetActive(true);
    }
}
