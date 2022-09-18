using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Player : Singleton<Player>
{
    [Header("Movement")]
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float recoilEffectAmount = 5;
    [SerializeField] private float swerveSpeed;
    //[SerializeField] private Transform sideMovementRoot;
    [SerializeField] private float sideMovementLimit;


    private float lastFrameFingerPositionX;
    private float moveFactorX;

    private Rigidbody rb;


    public override void Awake()
    {
        base.Awake();
          
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {

        if (GameManager.GameState == State.InGame)
        {
            HandleSideMovement();
            MoveForward();
        }
 
    }   
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
    }
    private void HandleSideMovement()
    {
        GetInput();

        float swerveAmount = swerveSpeed * moveFactorX * Time.deltaTime;
        var currentPos = transform.position; 
        currentPos.x += swerveAmount;
        currentPos.x = Mathf.Clamp(currentPos.x, -sideMovementLimit, sideMovementLimit);

        transform.position = currentPos;

    }
    [Button]
    public void CollideWithAnObstacle()
    {
        GameManager.GameState = State.Collision;
        rb.AddForce(Vector3.back * recoilEffectAmount, ForceMode.VelocityChange);
        StartCoroutine(ResetVelocity());
    }
    IEnumerator ResetVelocity()
    {
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector3.zero;
        GameManager.GameState = State.InGame;
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            moveFactorX = Input.mousePosition.x - lastFrameFingerPositionX;
            lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            moveFactorX = 0f;
        }
    }
}
