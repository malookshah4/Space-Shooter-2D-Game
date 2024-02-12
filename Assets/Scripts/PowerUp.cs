using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip _clip;


    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_clip,transform.position);
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedPowerupActive();
                        break;

                    case 2:
                        player.DamagePowerActive();
                        break;

                    default:
                        Debug.Log("Default Value passed");
                        break;
                }

            }

            Destroy(this.gameObject);
        }
    }

}
