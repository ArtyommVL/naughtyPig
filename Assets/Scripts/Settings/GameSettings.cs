using UnityEngine;

[CreateAssetMenu(menuName = "NaughtyPig/GameSettings",fileName = "GameSettings",order = 1)]
public class GameSettings : ScriptableObject {
    // Variables
    [Header("Pig")]
    [SerializeField] private int pigHealth = default;
    [SerializeField] private float pigSpeed = default;
    [Header("Enemy")] 
    [SerializeField] private int enemyHealth = default;
    [SerializeField] private int enemyDamage = default;
    [SerializeField] private float startChasingRadius = default;
    [SerializeField] private float dirtyTime = default;
    [Header("Bomb")] 
    [SerializeField] private int bombDamage = default;
    [SerializeField] private float pushTime = default;
    [SerializeField] private float attackTime = default;
    
    // Properties
    public int PigHealth => pigHealth;
    public float PigSpeed => pigSpeed;
    public int EnemyHealth => enemyHealth;
    public int EnemyDamage => enemyDamage;
    public float StartChasingRadius => startChasingRadius;
    public float DirtyTime => dirtyTime;
    public int BombDamage => bombDamage;
    public float PushTime => pushTime;
    public float AttackTime => attackTime;
}
