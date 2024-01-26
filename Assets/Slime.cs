using System.Collections;

using UnityEngine;

public class Slime : MonoBehaviour
{ [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float Move_speed;
    [SerializeField] private int Move_force;
    [SerializeField] private GameObject smoke_prefab;
    [SerializeField] private GameObject Death_sound_prefab;
    [SerializeField] private GameObject Heath_prefab;

    private int health;
    IEnumerator Move()
    {
        while (true)
        {
                rb.AddForce((Player.player_pos-transform.position)*Move_force);
            yield return new WaitForSeconds(Move_speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        health--;
        if (health<=0)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartCoroutine(Move());
        health = Random.Range(50,200);
        transform.localScale = new Vector2((float)health/1000, (float)health / 1000);
    }
    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        Manager.Counter_kill++;
        Instantiate(smoke_prefab, transform.position, transform.rotation);
        if (Random.Range(0,5)==1)
        {
          GameObject Hearth=  Instantiate(Heath_prefab, transform.position, transform.rotation);
            Destroy(Hearth, 5f);
        }
        GameObject death_sound=Instantiate(Death_sound_prefab);
        Destroy(death_sound, 1f);

    }

}
