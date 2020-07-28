using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private bool touchStart = false;
    private Vector2 pointA;
    public Transform cursor;
    public float speed = 2.5f;
    Vector3 velocity = Vector3.zero; 
    public List<Sprite> sprites;
    public bool isGameStart = false;
    public int currentSprite = 0;
    public GameObject StartButton;
    public GameObject NextCharacterButton;
    public GameObject PrevCharacterButton;
    public GameObject Logo;
    public GameObject StartText;
    public GameObject PlayerSprite;
    public Animation CharacterWalk;

    void Start()
    {
        currentSprite = 0;
        isGameStart = false;
        PlayerSprite.GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
        StartButton.SetActive(true);
        NextCharacterButton.SetActive(true);
        PrevCharacterButton.SetActive(true);
        Logo.SetActive(true);
        StartText.SetActive(true);
    }

    void Update()
    {
        if(isGameStart){
            if(Input.GetMouseButtonDown(0)){
                    pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
                    cursor.position = pointA;
                }

                if(Input.GetMouseButton(0)){
                    touchStart = true; 
                    cursor.GetComponent<SpriteRenderer>().enabled = true;
                } else {
                    touchStart = false;
                    cursor.GetComponent<SpriteRenderer>().enabled = false;
                    CharacterWalk.Stop();
            }
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
    }

    public void NextCharacter(){
        if(currentSprite < 8){
            currentSprite++;
            if(currentSprite == 8) currentSprite = 0;
            PlayerSprite.GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
        }
    }

    public void PrevCharacter(){
        if(currentSprite >= 0){
            currentSprite--;
            if(currentSprite == -1) currentSprite = 7;
            PlayerSprite.GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
        }
    }

    void GetDirection(){
        
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        cursor.up = direction;

    }

    void moveCharacter(Vector2 direction){

        transform.Translate(direction * speed * Time.deltaTime);
        CharacterWalk.Play();

    }
}
