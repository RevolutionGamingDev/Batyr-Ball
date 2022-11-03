using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Controll : MonoBehaviour
{
    public bool destroyable;
    public float jumpPower;
    public ParticleSystem jumpParticles;
    public ParticleSystem trailParticles;
    public ParticleSystem destroyParticles;
    public GameObject jumpBttn;
    public Animator anim;
    public Rotate rotate;

    [Header("power ups")] public float shieldDuration;
    //public GameObject shieldAura;
    [Header("RunTime")] public bool isAirJump;
    //public bool isRotationChanged;
    //public bool jumped;
    public static Controll S;
    //public AirJumpTrigger touchingAirJump;
    private Rigidbody2D _rigid;
    public bool _onGround = false;
    public bool immortalMode = true;
    public bool _grounded;
    //private bool _isPlaying;
    //private bool _isAlive = true;
    private Vector3 _camOffset;


    private bool isMouseDown;
    private bool isMouseUp;

    private int deathCount = 0;

    private void Awake()
    {
        S = this;
        _rigid = GetComponent<Rigidbody2D>();
        _rigid.gravityScale = 0;
        jumpBttn.SetActive(true);
        InterstitialAd.S.LoadAd();
        deathCount = PlayerPrefs.GetInt("deathCount");
    }

  

    public void OnPlay()
    {
        _rigid.gravityScale = 5f;
        //_isPlaying = true;
    }
    //private bool GetInput()
    //{
    //    return (Input.GetButton("Jump") || JumpButton.isPressed);
    //}

    public bool GetInputDown()
    {
        return (Input.GetButtonDown("Jump") || isMouseDown);
    }

    private bool GetInputUp()
    {
        return (Input.GetButtonUp("Jump") || isMouseUp);        
    }
    
    public void MakeJump(float mult)
    {
        _rigid.velocity = Vector3.zero;
        _rigid.AddForce(Vector3.up * (jumpPower * mult), ForceMode2D.Impulse);
    }
    //public void Jump()
    //{
    //    //if (!_isPlaying || !_isAlive) return;
        
    //    //bool triggerSpots = isAirJump || isRotationChanged;
    //    //jumped = (GetInput() && _onGround && !_grounded) || (triggerSpots && GetInputDown());
    //    //if(jumped)
    //    //{
    //    //    if (isAirJump && touchingAirJump)
    //    //    {
    //    //        touchingAirJump.Disable();
    //    //    }

    //    //    if (isRotationChanged)
    //    //    {
    //    //        SpikeGen.S.ChangeRotation();
    //    //        bool toLeft = trailParticles.transform.localScale.z > 0;
    //    //        trailParticles.transform.localScale = new Vector3(1, 1, toLeft ? -1 : 1);
    //    //        _camOffset = MultiTargetCam.S.offset;
    //    //        MultiTargetCam.S.offset = new Vector3(-1 * _camOffset.x, _camOffset.y, -1);
    //    //    }
            
    //    //    return;
    //    //}


    //    //if (GetInputUp())
    //    //{
    //    //    _grounded = false;
    //    //}
    //}



    //public void ChangePlanet()
    //{
    //    _rigid.gravityScale = -_rigid.gravityScale;
    //    _rigid.velocity = Vector3.zero;
    //    _camOffset = MultiTargetCam.S.offset;
    //    MultiTargetCam.S.offset = new Vector3(_camOffset.x, -_camOffset.y, -1);

    //}

    //private Coroutine shUpCoroutine;
    //public void ActivateShield()
    //{
    //    if (!destroyable)
    //    {
    //        anim.SetBool("shieldDown", false);
    //        StopCoroutine(shUpCoroutine);
    //    }
    //    shUpCoroutine = StartCoroutine(ShieldUp());
    //}
    //private IEnumerator ShieldUp()
    //{
    //    destroyable = false;
    //    shieldAura.SetActive(true);
    //    yield return new WaitForSeconds(shieldDuration - 3);
    //    anim.SetBool("shieldDown", true);
        
        
    //    yield return new WaitForSeconds(3);
    //    anim.SetBool("shieldDown", false);
    //    destroyable = true;
    //    shieldAura.SetActive(false);
    //}


    public void Destroy()
    {
        if (!destroyable) return;

        //_isAlive = false;
        destroyParticles.Play();
        //_rigid.gravityScale = 0;
        jumpBttn.SetActive(false);
        _rigid.velocity = Vector3.zero;
        //vfx.SetActive(false);
        rotate.speed = 0;
        anim.SetBool("isDead", true);

        //StartCoroutine(GameManager.S.LeaderboardOn());
        //Update score in leaderboard data base when dead
        GameManager.S.UpdateScore();
       
    }
    
    

    private void FixedUpdate()
    {
        if (_rigid.velocity.y < -jumpPower)
            immortalMode = true;

        if (gameObject.transform.position.y >= -1.75f) _grounded = false;

        else _grounded = true;
    }

    //private bool _tmp;
    //void CheckMouseDown()
    //{
    //    if (!_tmp && JumpButton.isPressed)
    //    {
    //        isMouseDown = true;
    //    }
    //    else
    //    {
    //        isMouseDown = false;
    //    }

    //    if (_tmp && !JumpButton.isPressed)
    //    {
    //        isMouseUp = true;
    //    }
    //    else
    //    {
    //        isMouseUp = false;
    //    }
    //    _tmp = JumpButton.isPressed;
    //}

   
    //private void LateUpdate()
    //{
    //    //Jump();
    //    //CheckMouseDown();
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("ground"))
        {
            _onGround = true;
            trailParticles.Play();

            if (immortalMode)
            {
                immortalMode = false;
                jumpParticles.Play();
            }
        }

        if (other.gameObject.layer == 7) // obstacle
        {
            Destroy();
            StartCoroutine(Death());
        }
        //if (other.gameObject.tag == "ground") _grounded = true;

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("ground"))
        {
            _onGround = false;
            trailParticles.Stop();
        }

        //if (other.gameObject.tag != "ground") _grounded = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "airJump")
        {
            isAirJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "airJump")
        {
            isAirJump = false;
            //Destroy(other.gameObject);
        }
    }

    private IEnumerator Death()
    {
        deathCount++;
        PlayerPrefs.SetInt("deathCount", deathCount);

        Debug.Log(deathCount);
        yield return new WaitForSeconds(1f);
        if (deathCount >= 30)
        {
            InterstitialAd.S.ShowAd();
            deathCount = 0;
            PlayerPrefs.SetInt("deathCount", deathCount);
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
