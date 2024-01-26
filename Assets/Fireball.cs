
using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    private void Start()
    {
            Destroy(gameObject,1f);
       
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        for (int i = 0; i < speed; i++)
        {
            rb.AddForce(diference);
        }
    }
 
    private void FixedUpdate()
    {
            rb.transform.localScale  +=new Vector3(0.007f, 0.007f, 0);
        rb.transform.Rotate(new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), 0));
    }



}
