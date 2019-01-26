using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.8f;

    [SerializeField]
    private float _dashSpeed = 10f;
    private float _currentDashTime = 0.0f;
    [SerializeField]
    private float _maxDashTime = 2f;
    [SerializeField]
    private float _dashStoppingSpeed = 0.5f;

    [SerializeField]
    private bool _isDead = false;

    [SerializeField]
    private bool _isDashing = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDead == true)
        {
            return;
        }
        if(_isDashing == false)
        {
            Move();
        }
        
        DoAction();
        Dash();
       
    }


    protected void Move() {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.position = transform.position + (moveVector * _speed * Time.deltaTime);

        Vector3 direction = Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.forward * Input.GetAxisRaw("Vertical");

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

    protected void Dash() {
        if(Input.GetButtonDown("Fire2") && _isDashing == false)
        {
            _currentDashTime = 0.0f;
            _isDashing = true;
        }
        if(_isDashing == true && _currentDashTime < _maxDashTime)
        {
            Vector3 directionYouWantToDash = transform.forward;

            Vector3 moveDir = directionYouWantToDash.normalized * _dashSpeed * _speed;
            _currentDashTime += _dashStoppingSpeed;

            transform.position = transform.position + (moveDir * Time.deltaTime);
        } else
        {
            _isDashing = false;
        }
        
    }

    protected void Kill() {
        _isDead = true;
    }

    // DEPRECATED
    protected void MoveFree() {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.position = transform.position + (moveVector * _speed * Time.deltaTime);

        Vector3 direction = Vector3.right * Input.GetAxisRaw("RotationHorizontal") + Vector3.forward * Input.GetAxisRaw("RotationVertical");

        if(direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
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

        transform.position = transform.position + (moveVector * _speed * Time.deltaTime);

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
