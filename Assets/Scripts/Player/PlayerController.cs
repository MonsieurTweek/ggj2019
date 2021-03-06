﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public PlayerAudioController playerAudioController;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _canMove = true;
        gameObject.transform.position = gameController.spawner.transform.position;
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

        // Debug
        if(Input.GetButtonDown("Fire3") == true)
        {
            //Kill();
        }
        if(Input.GetButtonDown("Fire4") == true)
        {
            //EndGame();
        }
       
    }

    protected void Move() {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if((Mathf.Abs(moveVector.x) > 0 || Mathf.Abs(moveVector.z) > 0))
        {
            if(_animator.GetBool("IsMoving") == false)
            {
                _animator.SetBool("IsMoving", true);
            }
            if(playerAudioController.audioSource.isPlaying == false)
            {
                playerAudioController.PlaySFX(PlayerAudioController.KeySFX.Walk);
            }
        } else if(_animator.GetBool("IsMoving") == true)
        {
            _animator.SetBool("IsMoving", false);
            playerAudioController.StopSFX();
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

            playerAudioController.PlaySFX(PlayerAudioController.KeySFX.Dash);
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
        if(_animator.GetBool("IsMoving") == true)
        {
            playerAudioController.PlaySFX(PlayerAudioController.KeySFX.Walk);
        }
    }

    protected void Kill() {
        _isDead = true;
        _animator.SetBool("IsDashing", false);
        _animator.SetBool("IsDead", true);

        playerAudioController.PlaySFX(PlayerAudioController.KeySFX.Death);
    }

    public void OnTriggerEnter(Collider other) {

        if(_isDead == true)
        {
            return;
        }

        if(other.tag == "Threat")
        {
            Trap trap = other.GetComponent<Trap>();
            // Cas des portes
            if(trap == null)
            {
                trap = other.GetComponentInParent<Trap>();
            }
            if(trap != null)
            {
                if(trap.isActive())
                {
                    Kill();
                }
                trap.ActiveTrapFromPlayer();
            } else
            {
                Kill();
            }

        } else if(other.tag == "Trigger")
        {
            Trigger trigger = other.GetComponent<Trigger>();
            if(trigger.needAction == false)
            {
                trigger.DoActiveTriggerFromPlayer();
            }
        } else if(other.tag == "Room")
        {

            RoomController roomController;

            GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
            foreach(GameObject room in rooms)
            {
                roomController = room.GetComponent<RoomController>();
                roomController.DisableLocalAudioSource();
            }

            roomController = other.GetComponent<RoomController>();
            roomController.EnableLocalAudioSource();    
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

        // Disable sounds
        RoomController roomController;

        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        foreach (GameObject room in rooms)
        {
            roomController = room.GetComponent<RoomController>();
            roomController.DisableLocalAudioSource();
        }
    }

    public void EndGame()
    {
        gameController.EndGame();
    }

}
