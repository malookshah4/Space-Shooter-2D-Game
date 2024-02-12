using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private Image _livesImage;

    [SerializeField]
    private Sprite[] _livesSprits;

    [SerializeField]
    private TextMeshProUGUI _gameOverText;

    [SerializeField]
    private TextMeshProUGUI _restartText;

    private GameManager _gameManager;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }


    public void UpdateLives(int currentLive)
    {
        _livesImage.sprite = _livesSprits[currentLive];
        if (currentLive == 0)
        {
            gameOverSeqance();
        }
    }

    void gameOverSeqance()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(FlickerGameOverRoutine());
    }

    IEnumerator FlickerGameOverRoutine()
    {
        while (true)
        {
         _gameOverText.text = "Game Over";
        yield return new WaitForSeconds(0.5f);
        _gameOverText.text = "";
        yield return new WaitForSeconds(0.5f);   
        }
    }


    public void Fire()
    {
        if (_player != null)
        {
            _player.FireLaser();
        }
    }
}
