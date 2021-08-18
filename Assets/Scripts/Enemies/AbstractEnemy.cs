using System;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class AbstractEnemy : MonoBehaviour {
    // Variables
    [SerializeField] private GameSettings gameSettings = default;
    [SerializeField] private List<GameObject> waypoints = default;
    [SerializeField] private List<Sprite> enemySpritesPatrol = default;
    [SerializeField] private List<Sprite> enemySpritesChase = default;
    [SerializeField] private List<Sprite> enemySpritesDirty = default;

    private int _waypointIndex = 0;
    private int _enemyHealthPoint;
    private float _wayPointAngle;
    private float _pigAngle;
    private float _timer;
    private SpriteRenderer _enemyRenderer;
    private Pig _pig;
    private AILerp _aiLerp;
    private AIDestinationSetter _aiDestinationSetter;

    private bool _isPatrol;
    private bool _isChase;
    private bool _isDirty;
    private bool _isAlive;

    private readonly Collider2D[] _tempChasePoints = new Collider2D[3];

    protected bool IsAlive => _isAlive;

    // Lifecycles
    protected virtual void Awake() {
        _enemyRenderer = gameObject.GetComponent<SpriteRenderer>();
        _aiLerp = GetComponent<AILerp>();
        _aiDestinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        _enemyHealthPoint = gameSettings.EnemyHealth;
        _timer = 0;
        _isPatrol = true;
        _isAlive = true;
        _pig = default;
        Array.Clear(_tempChasePoints, 0, _tempChasePoints.Length);
    }

    protected virtual void Update(){
        SetSpritesEnemy();
        if (!_isDirty) {
            if (_isPatrol) {
                EnemyPatrol();
                _wayPointAngle = CalculateRotation(waypoints[_waypointIndex].transform);
                Array.Clear(_tempChasePoints, 0, _tempChasePoints.Length);
                _pig = null;
                
                int count = Physics2D.OverlapCircleNonAlloc(transform.position, gameSettings.StartChasingRadius,
                        _tempChasePoints);
                if (count > 0) {
                    for (int i = 0; i < count; i++) {
                        if (_tempChasePoints[i].TryGetComponent(out _pig)) { 
                            _isPatrol = false;
                            _isChase = true;
                            return;
                        }
                    }
                }
            }else if (_isChase && _pig != default) {
                EnemyChase();
                _pigAngle = CalculateRotation(_pig.transform); 
            }
        }else {
            CustomTimer();
        }
    }

    protected virtual void EnemyChase() {
        if (Vector2.Distance(transform.position, _pig.transform.position) < gameSettings.StartChasingRadius) { 
            _aiDestinationSetter.target = _pig.transform;
        }else { 
            _aiDestinationSetter.target = waypoints[_waypointIndex].transform; 
            _isChase = false; 
            _isPatrol = true;
        }
    }

    protected virtual void EnemyPatrol() {
        _aiDestinationSetter.target = waypoints[_waypointIndex].transform;

        if (transform.position == waypoints[_waypointIndex].transform.position) {
            _waypointIndex += 1;
        }

        if (_waypointIndex == waypoints.Count) {
            _waypointIndex = 0;
        }
    }

    protected virtual float CalculateRotation(Transform target) {
        var enemyDirection = target.position - transform.position;
        float angle = Mathf.Round(
            (Mathf.RoundToInt(Mathf.Atan2(enemyDirection.y, enemyDirection.x)) * Mathf.Rad2Deg / 10) * 10);
        return angle;
    }

    protected virtual void SetSpritesEnemy() {
        if (!_isDirty) {
            if (_isPatrol) {
                if (_wayPointAngle < 0) {
                    _enemyRenderer.sprite = enemySpritesPatrol[0];
                }

                if (_wayPointAngle >= 0 && _wayPointAngle < 45) {
                    _enemyRenderer.sprite = enemySpritesPatrol[1];
                }

                if (_wayPointAngle >= 45 && _wayPointAngle < 135) {
                    _enemyRenderer.sprite = enemySpritesPatrol[2];
                }

                if (_wayPointAngle >= 135 && _wayPointAngle <= 180) {
                    _enemyRenderer.sprite = enemySpritesPatrol[3];
                }
            }

            if (_isChase) {
                if (_pigAngle < 0) {
                    _enemyRenderer.sprite = enemySpritesChase[0];
                }

                if (_pigAngle >= 0 && _wayPointAngle < 45) {
                    _enemyRenderer.sprite = enemySpritesChase[1];
                }

                if (_pigAngle >= 45 && _pigAngle < 135) {
                    _enemyRenderer.sprite = enemySpritesChase[2];
                }

                if (_pigAngle >= 135 && _pigAngle <= 180) {
                    _enemyRenderer.sprite = enemySpritesChase[3];
                }
            }
        }
        else {
            if (_wayPointAngle < 0) {
                _enemyRenderer.sprite = enemySpritesDirty[0];
            }

            if (_wayPointAngle >= 0 && _wayPointAngle < 45) {
                _enemyRenderer.sprite = enemySpritesDirty[1];
            }

            if (_wayPointAngle >= 45 && _wayPointAngle < 135) {
                _enemyRenderer.sprite = enemySpritesDirty[2];
            }

            if (_wayPointAngle >= 135 && _wayPointAngle <= 180) {
                _enemyRenderer.sprite = enemySpritesDirty[3];
            }
        }
    }

    protected virtual void CustomTimer() {
        _timer += Time.deltaTime;
        if (_timer >= gameSettings.DirtyTime) {
            EnemyDeath();
            _isDirty = false;
            _timer = 0;
        }
    }

    protected virtual void EnemyDeath() {
        if (_enemyHealthPoint <= 0) {
            gameObject.SetActive(false);
        }
    }

    public virtual void EnemyDamage() {
        _enemyHealthPoint -= gameSettings.BombDamage;
        _aiLerp.canMove = false;
        _isDirty = true;
    }

    public abstract bool CanDamage();    
}
