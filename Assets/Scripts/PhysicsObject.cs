using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public Vector3 velocity;
    public float desiredx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 acceleration = -9.81f * Vector3.up;
        velocity += acceleration * Time.fixedDeltaTime;
        Vector2 movement = velocity * Time.fixedDeltaTime;
        velocity.x = desiredx;
        Movement(new Vector2(movement.x, 0), true);
        Movement(new Vector2(0, movement.y), false);
    }

    void Movement (Vector2 move, bool movex)
    {
        if (move.magnitude < 0.00001f) return;
        RaycastHit2D[] results = new RaycastHit2D[16];
        int cnt = GetComponent<Rigidbody2D>().Cast(move, results, move.magnitude + 0.01f);
        for (int i = 0; i < cnt; i++)
        {
            if (Mathf.Abs(results[i].normal.x) > 0.5 && movex)
            {
                move.x = 0;
                velocity.x = 0;
                CollideWithHorizontal(results[i].collider);
            }
            if (Mathf.Abs(results[i].normal.y) > 0.5 && !movex)
            {
                move.y = 0;
                velocity.y = 0;
                CollideWithVertical(results[i].collider);
            }
        }

        transform.position += (Vector3)move;
    }

    public virtual void CollideWithHorizontal(Collider2D other)
    {
        
    }
    public virtual void CollideWithVertical(Collider2D other)
    {
        
    }
}
