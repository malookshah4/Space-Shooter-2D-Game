using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 0.5f;
    private float _nextFire = 0.15f;
    [SerializeField]
    private int _life = 3;

    [SerializeField]
    private float _speed = 3.5f;
    private bool _isTripleShotAcitve = false;
    private bool _speedPowerActive = false;
    private bool _damagePowerActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _sheild;
    [SerializeField]
    private int _score;
    private UIManager _uIManager;
    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private AudioClip _laserClip;

    private AudioSource _audioSource;

   
   [SerializeField] private InputActionReference moveActionToUse;
   [SerializeField] private Button fireButton;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -3f, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource != null)
        {
            _audioSource.clip = _laserClip;
        }

    }

    // Update is called once per frame
    void Update()
    {
        ControlForMobile();
        // if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        // {
            
        // }


    }

    private void ControlForMobile()
    {
           if (_speedPowerActive)
        {
            _speed = 6.5f;
        }
        else
        {
            _speed = 3.5f;
        }


        Vector2 moveDirection = moveActionToUse.action.ReadValue<Vector2>();
        transform.Translate(moveDirection * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Math.Clamp(transform.position.y, -3.9f, 4.5f), 0);
    }
    void CalculateMovement()
    {

        float horizentalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // transform.Translate(Vector3.right * horizentalInput * 3.5f * Time.deltaTime);
        // transform.Translate(Vector3.up * verticalInput * 3.5f * Time.deltaTime);

        if (_speedPowerActive)
        {
            _speed = 6.5f;
        }
        else
        {
            _speed = 3.5f;
        }

        transform.Translate(new Vector3(horizentalInput, verticalInput, 0) * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Math.Clamp(transform.position.y, -3.9f, 4.5f), 0);

        if (transform.position.x > 11f)
        {
            transform.position = new Vector3(-11f, transform.position.y, 0);
        }
        else if (transform.position.x < -11f)
        {
            transform.position = new Vector3(11f, transform.position.y, 0);
        }

    }


    public void FireLaser()
    {
        _nextFire = Time.time + _fireRate;

        if (_isTripleShotAcitve)
        {
            Vector3 tripleShotPos = new Vector3(-1f, 0.74f, 0f);
            Instantiate(_tripleShotPrefab, transform.position + tripleShotPos, Quaternion.identity);
        }
        else
        {
            Vector3 lasetOffset = new Vector3(0, 0.85f, 0);
            Instantiate(_laserPrefab, transform.position + lasetOffset, Quaternion.identity);
        }

        _audioSource.Play();

    }

    public void Damage()
    {
        if (_damagePowerActive == true)
        {
            return;
        }

        _life--;

        if (_life == 2)
        {
            _engines[0].SetActive(true);
        }
        else if (_life == 1)
        {
            _engines[1].SetActive(true);
        }

        _uIManager.UpdateLives(_life);
        if (_life < 1)
        {
            _spawnManager.OnPlayerDead();
            Destroy(this.gameObject);
        }
    }

    public void DamagePowerActive()
    {
        _damagePowerActive = true;
        _sheild.SetActive(true);
        StartCoroutine(DamagePowerDeActiveRoutine());
    }

    IEnumerator DamagePowerDeActiveRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _damagePowerActive = false;
        _sheild.SetActive(false);
    }


    public void TripleShotActive()
    {
        _isTripleShotAcitve = true;
        StartCoroutine(TripleShotDeActive());

    }

    IEnumerator TripleShotDeActive()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotAcitve = false;
    }


    public void SpeedPowerupActive()
    {
        _speedPowerActive = true;
        StartCoroutine(SpeedPowerDeactiveRoutine());

    }

    IEnumerator SpeedPowerDeactiveRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speedPowerActive = false;
    }

    public void AddScore(int point)
    {
        _score += point;
        _uIManager.UpdateScore(_score);
    }

}
