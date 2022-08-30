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

    // Start is called before the first frame update
    void Start()
    {

        objectRenderer = gameObject.GetComponent<Renderer>();
        sz = objectRenderer.bounds.extents;
        mask = LayerMask.NameToLayer(maskName);
        Debug.Log(mask);
        Debug.Log(sz);

    }

    // Update is called once per frame
    void Update()
    {

        pos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos, sz/2, rot, 1<<mask);

        if (colliders.Length > 0) {

            Debug.Log(colliders.Length);
            Debug.Log(colliders[0]);
            Debug.Log("se detecto golpe");

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Gizmos.DrawCube(Vector3.zero, new Vector3(sz.x * 2, sz.y * 2)); // Because size is halfExtents
    }


}
