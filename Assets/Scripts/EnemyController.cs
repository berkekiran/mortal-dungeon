using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyHealth = 2;
    public float speed = 2.0f;
    public bool destroyed = false;
    public GameObject player;
    public Animation EnemyAnimation;
    public AudioSource Walk;
    public bool canAttack = false;
    
    void Start()
    {
        player = GameObject.Find("Player");
        Walk = GameObject.Find("Enemies").GetComponent<AudioSource>();
    }

    void Update()
    {
        if(canAttack && PlayerController.isGameStart && !PlayerController.youDiedScreen)
            StartCoroutine(Attack());

    }

    void FixedUpdate(){
        if(!destroyed && player != null && PlayerController.isGameStart && !PlayerController.youDiedScreen){
            if(Vector2.Distance(transform.position, player.transform.position) > 1 && Vector2.Distance(transform.position, player.transform.position) < 5){
                Vector2 direction = (player.transform.position - transform.position).normalized;
                this.GetComponent<Rigidbody2D>().velocity = new Vector2 (direction.x * speed, direction.y * speed);
                if(!EnemyAnimation.isPlaying)
                    EnemyAnimation.Play("Anim_CharacterWalk");
                if(!Walk.isPlaying)
                    Walk.Play();
            } else {
                EnemyAnimation.Stop("Anim_CharacterWalk");
            }
        } else {
            Walk.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Cursor"){
            StartCoroutine(SwordAttack());
            EnemyAnimation.Play("Anim_Damage");
            if(enemyHealth > 0)
                enemyHealth--;
            else if(enemyHealth == 0)
                StartCoroutine(DestroyEnemy());
            this.GetComponentInChildren<ParticleSystem>().Play();
        }
        if(other.tag == "Player" && PlayerController.isGameStart && !PlayerController.youDiedScreen)
            canAttack = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player" && PlayerController.isGameStart && !PlayerController.youDiedScreen)
            canAttack = false;
    }

    IEnumerator SwordAttack()
    {
        yield return new WaitForSeconds(0.25f);
        GameObject.Find("Player").GetComponent<PlayerController>().SwordAttack.Play();
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.25f);
        GameObject.Find("Player").GetComponent<PlayerController>().PlayerAnim.Play("Anim_Damage");
        if(!GameObject.Find("Player").GetComponent<PlayerController>().EnemyAttack.isPlaying)
            GameObject.Find("Player").GetComponent<PlayerController>().EnemyAttack.Play();
        if(!EnemyAnimation.IsPlaying("Anim_Attack")){
            if(GameObject.Find("Player").GetComponent<PlayerController>().PlayerAnim.IsPlaying("Anim_Damage")){
                GameObject.Find("Player").GetComponentInChildren<ParticleSystem>().Play();
                PlayerController.playerHealth--;
            }
            EnemyAnimation.Play("Anim_Attack");    
        }
    }

    IEnumerator DestroyEnemy()
    {
        EnemyAnimation.Play("Anim_Damage");
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);       
        PlayerController.score+=10;
    }

}
