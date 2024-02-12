using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Player _player;

    [SerializeField]
    private float _speed = 3f;

    private Animator _animator;

    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _animator = GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-6.7f, 6.80f), 6, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            _speed = 0;
            _animator.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }

        if (other.transform.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _speed = 0;
            _animator.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
    }





}
