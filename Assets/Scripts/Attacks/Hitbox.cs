using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    private Renderer objectRenderer;
    private Vector2 pos;
    public float rot;
    public string[] maskNames;
    private int mask;
    private IHitboxResponder _responder = null;
    public bool isSphere;
    public bool isProjectile;
    public float radius;
    public Vector2 sz;
    public Color colorOpen;
    public Color colorClosed;
    public Color colorColliding;
    public Vector2 offset;
    private Color currColor;
    private State _state;

    // Start is called before the first frame update
    void Start()
    {

        // objectRenderer = gameObject.GetComponent<Renderer>();
        // sz = objectRenderer.bounds.extents;
        mask = LayerMask.GetMask(maskNames);
        Debug.Log(mask);
        Debug.Log(sz);

    }

    // Update is called once per frame
    public void hitboxUpdate()
    {

        //Debug.Log(_state);
        //Debug.Log(transform.localScale);
        pos = transform.position + new Vector3((transform.localScale.x>0)? offset.x : -offset.x, offset.y, 0);
        if (_state == State.Closed) { return; }
        Collider2D[] colliders = (isSphere)? Physics2D.OverlapCircleAll(pos, radius, mask) : Physics2D.OverlapBoxAll(pos, sz/2, rot, mask);

        //Debug.Log(pos);

        for (int i = 0; i < colliders.Length; i++) {

            Collider2D iCollider = colliders[i];
            if(_state!=State.Colliding){
                _responder?.CollisionedWith(iCollider);
                Debug.Log(colliders.Length);
                Debug.Log(colliders[0]);
                Debug.Log("se detecto golpe");
            }

        }
        _state = (colliders.Length > 0)? State.Colliding : State.Open;
        currColor = (_state == State.Colliding)? colorColliding : colorOpen;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = currColor;
        Gizmos.matrix = Matrix4x4.TRS(pos, transform.rotation, transform.localScale);
        if (!isSphere)
        {
            Gizmos.DrawCube(Vector3.zero, new Vector3(sz.x * 2, sz.y * 2, 0)); // Because size is halfExtents
        }
        else
        {
            Gizmos.DrawSphere(Vector3.zero, radius);
        }
    }

    public void openCollissionCheck()
    {
        currColor = colorOpen;
        _state = State.Open;
    }

    public void closeCollissionCheck()
    {
        currColor = colorClosed;
        _state = State.Closed;
    }

    public void setResponder(IHitboxResponder responder)
    {
        _responder = responder;
    }

    public enum State { 
        
        Closed,
        Open,
        Colliding
    
    }

}
