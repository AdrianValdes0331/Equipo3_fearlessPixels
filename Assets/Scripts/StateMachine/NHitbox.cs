using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class NHitbox
{

    private Renderer objectRenderer;
    [HideInInspector] public Vector2 pos;
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
    [HideInInspector] public Color currColor;
    private State _state;
    [HideInInspector] public Transform transform;
    // private static Quaternion tRot;
    // private static Vector3 tScale;

    // Start is called before the first frame update
    public void Start()
    {

        // objectRenderer = gameObject.GetComponent<Renderer>();
        // sz = objectRenderer.bounds.extents;
        Debug.Log(maskNames);
        mask = LayerMask.GetMask(maskNames);
        Debug.Log(mask);
        Debug.Log(sz);
        // tRot = transform.rotation;
        // tScale = transform.localScale;
        pos = transform.position + new Vector3((transform.localScale.x>0)? offset.x : -offset.x, offset.y, 0);

    }

    // Update is called once per frame
    public void hitboxUpdate()
    {

        //Debug.Log(_state);
        //Debug.Log(transform.localScale);
        pos = transform.position + new Vector3((transform.localScale.x>0)? offset.x : -offset.x, offset.y, 0);
        if (_state == State.Closed) { return; }
        Collider2D[] colliders = (isSphere)? Physics2D.OverlapCircleAll(pos, radius, mask) : Physics2D.OverlapBoxAll(pos, sz, rot, mask);

        //Debug.Log(sz);

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

    // [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    // private static void OnDrawGizmos(PlayerController scr, GizmoType gizmoType)
    // {
    //     Gizmos.color = currColor;
    //     //Gizmos.matrix = Matrix4x4.TRS(pos, tRot, tScale);
    //     if (!isSphere)
    //     {
    //         Gizmos.DrawCube(Vector3.zero, new Vector3(sz.x * 2, sz.y * 2, 0)); // Because size is halfExtents
    //     }
    //     else
    //     {
    //         Gizmos.DrawSphere(Vector3.zero, radius);
    //     }
    // }

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
