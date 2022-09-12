using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    private Renderer objectRenderer;
    private Vector2 pos, sz;
    public float rot;
    public string maskName;
    private int mask;
    private IHitboxResponder _responder = null;
    public bool isSphere;
    public bool isProjectile;
    private State _state;

    // Start is called before the first frame update
    void Start()
    {

        // objectRenderer = gameObject.GetComponent<Renderer>();
        // sz = objectRenderer.bounds.extents;
        sz = Vector2.one;
        mask = LayerMask.NameToLayer(maskName);
        Debug.Log(mask);
        Debug.Log(sz);

    }

    // Update is called once per frame
    public void hitboxUpdate()
    {

        Debug.Log(_state);  
        if (isProjectile) { pos = transform.position; }
        if (_state == State.Closed) { return; }
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos, sz/2, rot, 1<<mask);

        Debug.Log(pos);

        for (int i = 0; i < colliders.Length; i++) {

            Collider2D iCollider = colliders[i];
            if(_state!=State.Colliding){_responder?.CollisionedWith(iCollider);}
            Debug.Log(colliders.Length);
            Debug.Log(colliders[0]);
            Debug.Log("se detecto golpe");

        }
        _state = (colliders.Length > 0)? State.Colliding : State.Open;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Gizmos.DrawCube(Vector3.zero, new Vector3(sz.x * 2, sz.y * 2)); // Because size is halfExtents
    }

    public void openCollissionCheck()
    {
        _state = State.Open;
    }

    public void closeCollissionCheck()
    {
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
