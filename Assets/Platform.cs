
using UnityEngine;


public class Platform : MonoBehaviour
{
    private Vector2 startPosition;
    [SerializeField] private Rigidbody2D rb;
    private Transform _transform;
    [SerializeField] private float speedBack;
    void Start()
    {
        startPosition = transform.position;
        _transform=rb.transform;
    }
    private void FixedUpdate()
    {
        if (_transform.position.y < startPosition.y) 
        {
            rb.gravityScale = -0.01f;
        }
        else if (_transform.position.y > startPosition.y)
        {
            rb.gravityScale = 0.01f;
        }
     
    }


}
