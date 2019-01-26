using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float _speed = 2.8f;

    [SerializeField]
    private float _dashSpeed = 6f;
    [SerializeField]
    private float _dashCooldown = 2.0f;
    [SerializeField]
    private float _dashCurrentCooldown = 0.0f;

    [SerializeField]
    private bool _isDead = false;

    [SerializeField]
    private bool _isDashing = false;
    [SerializeField]
    private bool _canMove = false;

    private Animator _animator;

    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDead == true)
        {
            return;
        }
        if(_canMove == true)
        {
            Move();
        }
        
        Dash();
       
    }

    protected void Move() {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if((Mathf.Abs(moveVector.x) > 0 || Mathf.Abs(moveVector.z) > 0))
        {
            if(_animator.GetBool("IsMoving") == false)
            {
                _animator.SetBool("IsMoving", true);
            }
        } else
        {
            _animator.SetBool("IsMoving", false);
        }

        transform.position = transform.position + (moveVector * _speed * Time.deltaTime);

        Vector3 direction = Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.forward * Input.GetAxisRaw("Vertical");

        if(direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

    protected void DoAction(Trigger trigger) {
        if(Input.GetButtonDown("Fire1") && _isDashing == false)
        {
            _canMove = false;
            _animator.SetBool("IsDoingAction", true);
            Debug.Log("DO ACTION !");
            trigger.DoActiveTriggerFromAction();
        }
    }

    public void ActionDone()
    {
        _canMove = true;
        _animator.SetBool("IsDoingAction", false);
    }

    protected void Dash() {

        if(_dashCurrentCooldown <= Time.time && Input.GetButtonDown("Fire2") && _isDashing == false && _canMove == true)
        {
            _isDashing = true;
            _canMove = false;
            _dashCurrentCooldown = Time.time + _dashCooldown;
            _animator.SetBool("IsDashing", _isDashing);
        }
        if(_isDashing == true)
        {
            Vector3 directionYouWantToDash = transform.forward;

            Vector3 moveDir = directionYouWantToDash.normalized * _dashSpeed;

            transform.position = transform.position + (moveDir * Time.deltaTime);
        }
    }

    public void StopDash() {
        _isDashing = false;
        _canMove = true;
        _animator.SetBool("IsDashing", _isDashing);
    }

    protected void Kill() {
        _isDead = true;
        _animator.SetBool("IsDashing", false);
        _animator.SetBool("IsDead", true);
        Debug.Log("YOU DIE !");
    }

    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Threat")
        {
            Trap trap = other.GetComponent<Trap>();
            if(trap != null)
            {
                trap.ActiveTrapFromPlayer();
            }
            
            Kill();
        } else if(other.tag == "Trigger")
        {
            Trigger trigger = other.GetComponent<Trigger>();
            if(trigger.needAction == false)
            {
                trigger.DoActiveTriggerFromPlayer();
            }
        }
    }

    void OnTriggerStay(Collider other) {
        if(other.tag == "Trigger")
        {
            Trigger trigger = other.GetComponent<Trigger>();
            if(trigger.needAction == true)
            {
                DoAction(trigger);
            }
        }
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

    public void IsDead()
    {
        Debug.Log("Game Over (from player)");
        gameController.DieAndRetry();
    }

    public void Reset()
    {
        _isDead = false;
        _isDashing = false;
        _canMove = true;
        _animator.SetBool("IsMoving", false);
        _animator.SetBool("IsDead", false);
        _animator.SetBool("IsDashing", false);
        _animator.SetBool("IsFalling", false);
        _animator.SetBool("IsDoingAction", false);
        _animator.Play("Idle");
    }

}
