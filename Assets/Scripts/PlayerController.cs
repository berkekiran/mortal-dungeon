using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool touchStart = false;
    private Vector2 pointA;
    public Transform cursor;
    public float speed = 2.5f;
    Vector3 velocity = Vector3.zero; 
    public List<Sprite> sprites;
    public static bool isGameStart = false;
    public int currentSprite = 0;
    public GameObject StartButton;
    public GameObject NextCharacterButton;
    public GameObject PrevCharacterButton;
    public GameObject Logo;
    public GameObject StartText;
    public GameObject PlayerSprite;
    public Animation PlayerAnim;
    public AudioSource Walk;
    public AudioSource SwordSwing;
    public AudioSource SwordAttack;
    public AudioSource EnemyAttack;
    public AudioSource LevelCompleted;
    public AudioSource YouDied;
    public AudioSource Button;
    public AudioSource Soundtrack;
    public GameObject Score;
    public GameObject Health;
    public GameObject LCText;
    public GameObject LCScoreT;
    public GameObject LCScore;
    public GameObject YDText;
    public GameObject YDT;
    public GameObject Background;
    public GameObject RestartButton;
    public static int playerHealth = 10;
    public static int score = 0;
    public static bool youDiedScreen = false;

    void Start()
    {
        Time.timeScale = 0;
        playerHealth = 10;
        score = 0;
        currentSprite = 0;
        isGameStart = false;
        youDiedScreen = false;
        PlayerSprite.GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
        StartButton.SetActive(true);
        NextCharacterButton.SetActive(true);
        PrevCharacterButton.SetActive(true);
        Logo.SetActive(true);
        StartText.SetActive(true);
        Score.SetActive(false);
        Health.SetActive(false);
        LCText.SetActive(false);
        LCScoreT.SetActive(false);
        LCScore.SetActive(false);        
        YDText.SetActive(false);
        YDT.SetActive(false);
        RestartButton.SetActive(false);
        Background.SetActive(false);
        Score.GetComponent<Text>().text = score.ToString();
        Health.GetComponent<Text>().text = playerHealth.ToString();
    }

    void Update()
    {
        if(isGameStart && !youDiedScreen){

            Score.GetComponent<Text>().text = score.ToString();
            LCScore.GetComponent<Text>().text = score.ToString();
            Health.GetComponent<Text>().text = playerHealth.ToString();

            if(Input.GetMouseButtonDown(0)){
                    pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
                    cursor.position = pointA;
                    SwordSwing.Play();
                }

                if(Input.GetMouseButton(0)){
                    touchStart = true; 
                    cursor.GetComponent<SpriteRenderer>().enabled = true;
                } else {
                    touchStart = false;
                    cursor.GetComponent<SpriteRenderer>().enabled = false;
                    cursor.position = new Vector2(-23, 9);
                    PlayerAnim.Stop("Anim_CharacterWalk");
                    if(!SwordSwing.isPlaying)
                        SwordSwing.Stop();
                    Walk.Stop();
            }

        }

        if(playerHealth <= 0)
            youDiedScreen = true;

        if(youDiedScreen){
            Score.SetActive(false);
            Health.SetActive(false);
            Logo.SetActive(true);
            YDText.SetActive(true);
            YDT.SetActive(true);
            Time.timeScale = 0;
            Walk.Stop();
            if(!YouDied.isPlaying)
                YouDied.Play();
            Background.SetActive(true);
            Soundtrack.Stop();
            RestartButton.SetActive(true);
        }
    }

    private void FixedUpdate(){
        if(isGameStart){

            if(touchStart){
                Vector2 direction = cursor.up;

                if(direction.x < 0)
                    transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
                else
                    transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

                GetDirection();
                moveCharacter(direction);
            }
        }
    }

    public void StartGame(){
        StartButton.SetActive(false);
        NextCharacterButton.SetActive(false);
        PrevCharacterButton.SetActive(false);
        Logo.SetActive(false);
        StartText.SetActive(false);
        isGameStart = true;
        Button.Play();
        Score.SetActive(true);
        Health.SetActive(true);
        LCText.SetActive(false);
        LCScoreT.SetActive(false);
        LCScore.SetActive(false);
        RestartButton.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame(){
        SceneManager.LoadScene("Game");
    }

    public void NextCharacter(){
        if(currentSprite < 8){
            currentSprite++;
            if(currentSprite == 8) currentSprite = 0;
            PlayerSprite.GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
        }
        Button.Play();
    }

    public void PrevCharacter(){
        if(currentSprite >= 0){
            currentSprite--;
            if(currentSprite == -1) currentSprite = 7;
            PlayerSprite.GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
        }
        Button.Play();
    }

    void GetDirection(){
        
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        cursor.up = direction;

    }

    void moveCharacter(Vector2 direction){

        transform.Translate(direction * speed * Time.deltaTime);
        if(!Walk.isPlaying)
            Walk.Play();
        if(!PlayerAnim.IsPlaying("Anim_CharacterWalk"))
            PlayerAnim.Play("Anim_CharacterWalk");

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "LevelCompleted"){
            isGameStart = false;
            Score.SetActive(false);
            Health.SetActive(false);
            Logo.SetActive(true);
            LCText.SetActive(true);
            LCScoreT.SetActive(true);
            LCScore.SetActive(true);
            Walk.Stop();
            Time.timeScale = 0;
            LevelCompleted.Play();
            Background.SetActive(true);
            RestartButton.SetActive(true);
            Soundtrack.Stop();
        }

    }
}
