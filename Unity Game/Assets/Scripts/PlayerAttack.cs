using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float range = 5f;

    public Weapon weapon;

    private Vector2 playerPos;
    private Vector2 startPos;
    private Vector2 endPos;


    // Start is called before the first frame update
    void Start()
    {
        Transform transform = GetComponent<Transform>();
    }

    void Update()
    {
        playerPos = new Vector2(transform.position.x, transform.position.y);
    }

    private void OnFire(InputValue inputValue)
    {
        startPos = playerPos;

        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(mousePos3D.x, mousePos3D.y);

        Vector2 direction = mousePos - startPos;

        endPos = startPos + (direction.normalized * range);

        weapon.Attack(endPos);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(startPos, endPos);
    }
}