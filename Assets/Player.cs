using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites_idle = new Sprite[5];
    [SerializeField] private Sprite[] sprites_walk = new Sprite[6];
    [SerializeField] private Sprite[] sprites_jump = new Sprite[2];
    [SerializeField] private SpriteRenderer sprite_player;
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera Main_camera;
    [SerializeField] private Slider hpBar;
    [SerializeField] private TMP_Text hpBar_text;
    [SerializeField] private GameObject Shoot_sound;

    private Vector3 Change_position;
    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));
    private int Health = 100;
    private bool isGround=false;
    private KeyCode key_in_Corrutine;
    [SerializeField] private GameObject fireball_prefab;
    public static Vector3 player_pos;
    public float period = 0.0001f;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGround=true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "slime")
        {
            Health -=(int) (collision.gameObject.transform.localScale.y*100);
            Debug.Log("גאמגא");
            if (Health<=0)
            {
                SceneManager.LoadScene(0);
            }
        }
        if (collision.gameObject.layer==7)
        {
            Health += 10;
            Destroy(collision.gameObject);
            if (Health>100) { Health = 100; }
        }
    }
    void Start()
    {
        StartCoroutine(Animator(KeyCode.None)); 
    }
   
        IEnumerator Animator(KeyCode keycode)
    {
        key_in_Corrutine= keycode;
        while (keycode== KeyCode.None)
        {
            for (int i = 0; i < sprites_idle.Length; i++)
            {
                sprite_player.sprite = sprites_idle[i];
                yield return new WaitForSeconds(0.4f); 
            }
        }
        while (keycode== KeyCode.Space|| isGround==false)
        {
            for (int i = 0; i < sprites_jump.Length; i++)
            {
                sprite_player.sprite = sprites_jump[i];
                yield return new WaitForSeconds(0.2f);
            }
        }
        while ((keycode == KeyCode.A || keycode == KeyCode.D) && isGround)
        {
            for (int i = 0; i < sprites_walk.Length; i++)
            {
                sprite_player.sprite = sprites_walk[i];

                yield return new WaitForSeconds(0.2f);
            }
        }
     }
    private void Camera_move()
    {
        Main_camera.transform.position = (Vector3)transform.position-new Vector3(0,0,10);
    }
    private void Player_control(KeyCode keycode)
    {
        switch (keycode)
        {
            case KeyCode.Space: rb.velocity=Vector2.up*jumpForce;
                
               
                isGround = false;
                break;
            case KeyCode.D: Change_position= new Vector3(moveSpeed,0)*Time.deltaTime;
                transform.position += Change_position;
                rb.transform.rotation= Quaternion.Euler(0,0,0); 
              
                break;
            case KeyCode.A:
                Change_position= -new Vector3(moveSpeed, 0) * Time.deltaTime;
                rb.transform.rotation = Quaternion.Euler(0, -180, 0);
                transform.position += Change_position;
                break;     
        }
        if (key_in_Corrutine!=keycode)
        {
            StopAllCoroutines();
            StartCoroutine(Animator(keycode));
        }
    }
    private void Shoot() 
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(fireball_prefab, transform.position, transform.rotation);
            if (Shoot_sound.activeSelf == false)
            {
                Shoot_sound.SetActive(true);
            }
        }
        else
        {
            Shoot_sound.SetActive(false);
        }
    }
    private void Hp_bar_animator()
    {
        hpBar.value = Health;
        hpBar_text.text = Health.ToString();
    }
    private void FixedUpdate()
    {
        player_pos = transform.position;
        if (Input.anyKey)
        {

            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                    Player_control(keyCode);
                }
            }
        }
        else if (key_in_Corrutine != KeyCode.None)
        {
            StopAllCoroutines();
            StartCoroutine(Animator(KeyCode.None));
        }
        Camera_move();
        int rand = Random.Range(0, 3);
        if (rand!=0)
        {
            Shoot();
        }
        Hp_bar_animator();
      
    }
}