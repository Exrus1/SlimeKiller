
using UnityEngine;


public class Platform : MonoBehaviour
{
    private Vector2 startPosition;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speedBack;
    void Start()
    {
        startPosition = transform.position;
    }
    private void FixedUpdate()
    {
        if (rb.transform.position.y < startPosition.y) 
        {
            rb.gravityScale = -0.01f;
        }
        else if (rb.transform.position.y > startPosition.y)
        {
            rb.gravityScale = 0.01f;
        }
     
    }


}
