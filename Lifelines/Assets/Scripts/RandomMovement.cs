using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 
public class RandomMovement : MonoBehaviour 
{
    [SerializeField] private float idleResetTime = 3;
    public float walkingSpeed = 2, walkingRange;
    public bool isWalking;
    public Animator navMeshAnimator;
    public NavMeshAgent agent;
    [SerializeField] private EnemyState currentState;
    private GameTimer idleTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Awake()
    {
        idleTimer = new GameTimer(idleResetTime);
    }
    void Update()
    {
        if(agent.hasPath) navMeshAnimator.SetFloat("Velocity", agent.velocity.magnitude);
        EnemyBehaviour();
    }
    private void EnemyBehaviour()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleBehaviour();
                break;
            case EnemyState.Roaming:
                RoamingBehaviour();
                break;
            default:
                break;
        }
    }
    public enum EnemyState
    {
        Idle,
        Roaming
    }
    private Vector3 RandomPosition()
    {
        Vector3 randomPos = UnityEngine.Random.insideUnitSphere * walkingRange;

        Vector3 newRandomPosition = transform.position + randomPos;

        return newRandomPosition;
    }
    public void MoveToLocation(Vector3 point)
    {
        agent.speed = walkingSpeed;
        agent.SetDestination(point);
    }
    private void RoamingBehaviour()
    {
        if (agent.hasPath == false)
        {
            agent.SetDestination(RandomPosition());
            return;
        }
    }
    private void IdleBehaviour()
    {
        if (idleTimer.Tick() == true)
        {
            SetCurrentState(EnemyState.Roaming);
        }
    }
    public class GameTimer
    {
        private float currentTime;
        private float resetTime;

        public GameTimer(float resetTime)
        {
            this.resetTime = resetTime;
            currentTime = resetTime;
        }

        public bool Tick()
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                return false;
            }
            else
            {
                currentTime = resetTime;
                return true;
            }
        }
    }
    private void SetCurrentState(EnemyState newState)
    {
        print($"Switching from {currentState} To {newState}");

        currentState = newState;

        switch (currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Roaming:
                break;
            default:
                break;
        }
    }
}
