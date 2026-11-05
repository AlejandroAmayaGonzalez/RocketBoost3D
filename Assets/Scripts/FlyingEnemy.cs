using UnityEngine;
using DG.Tweening;
using System;

public class FlyingEnemy : MonoBehaviour {

    [Header("Detection Settings")]
    [SerializeField] private float detectionRange = 7f;
    [SerializeField] private float loseRange = 10f;
    
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float floatingHeight = 1f;
    [SerializeField] private float floatingDuration = 1f;

    private Transform playerTransform;
    private float distanceToPlayer;

    private Vector3 startPosition;
    private Tween floatingTween;

    private enum EnemyState { Idle, Chasing, Returning }
    private EnemyState currentState = EnemyState.Idle;

    void Start(){
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;

        StartFloating();
    }

    void Update(){
        if (playerTransform == null) return;

        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (currentState == EnemyState.Idle && distanceToPlayer <= detectionRange){
            StartChasing();
        }else if (currentState == EnemyState.Chasing && distanceToPlayer > loseRange){
            ReturnToStart();
        }
    }



    private void ReturnToStart(){
        currentState = EnemyState.Returning;
        
        float distanceToStart = Vector3.Distance(transform.position, startPosition);

        float duration = Mathf.Max(0.1f, distanceToStart / moveSpeed);

        floatingTween = transform
                    .DOMove(startPosition, duration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => StartFloating());
    }

    private void StartChasing(){
        currentState = EnemyState.Chasing;
        floatingTween.Kill();

        // Move towards the player
        float duration = Mathf.Max(0.1f, distanceToPlayer / moveSpeed);

        floatingTween = transform.DOMove(playerTransform.position, duration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => { StartFloating(); });
    }

    private void StartFloating(){
        // Start floating animation using DOTween
        floatingTween = transform.DOMoveY(startPosition.y + floatingHeight, floatingDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
