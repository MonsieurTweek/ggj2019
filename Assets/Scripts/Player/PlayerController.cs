using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    private float _currentDashTime;
    private float _maxDashTime = 2f;

    [SerializeField]
    private bool _isDead = false;

    private CharacterController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDead == true)
        {
            return;
        }
        Move();
        DoAction();
       
    }

    protected void Move() {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Quaternion rotationVector = new Quaternion(0, Input.GetAxis("RotationHorizontal"), 0, 0);

        _controller.Move(moveVector * _speed * Time.deltaTime);

        Vector3 direction = Vector3.right * Input.GetAxisRaw("RotationHorizontal") + Vector3.forward * Input.GetAxisRaw("RotationVertical");

        if(direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

    protected void DoAction() {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("DO ACTION !");
        }
    }

    //protected void Dash() {
    //    if(Input.GetButtonDown("Fire2"))
    //    {
    //        _currentDashTime = 0.0f;
    //    }
    //    if(_currentDashTime < _maxDashTime)
    //    {
    //        // replace me with the direction you want to dash in
    //        // to dash forward, this should be a vector in the direction the player is looking
    //        // if you have access to your player in a variable, this would be something like:
    //        // var directionYouWantToDash = player.transform.forward;
    //        var directionYouWantToDash = new Vector3(0, 0, 0);
    //        // this will make your velocity exactly equal to dash speed, regardless of direction
    //        moveDir = directionYouWantToDash.normalized * dashSpeed;
    //        _currentDashTime += dashStoppingSpeed;
    //    } else
    //    {
    //        Debug.Log("Nope");
    //    }
    //    controller.Move(moveDir * Time.deltaTime);
    //}

    protected void Kill() {
        _isDead = true;
    }

    // DEPRECATED
    protected void MoveFourAxis() {
        Vector3 moveVector = Vector3.zero;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        if(Mathf.Abs(inputX) > Mathf.Abs(inputY))
        {
            inputY = 0;
        } else
        {
            inputX = 0;
        }

        moveVector = new Vector3(inputX, 0, inputY);

        _controller.Move(moveVector * _speed * Time.deltaTime);

        float directionX = Input.GetAxisRaw("RotationHorizontal");
        float directionY = Input.GetAxisRaw("RotationVertical");

        if(Mathf.Abs(directionX) > Mathf.Abs(directionY))
        {
            directionY = 0;
        } else
        {
            directionX = 0;
        }

        Vector3 direction = Vector3.right * directionX + Vector3.forward * directionY;

        if(direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

}
