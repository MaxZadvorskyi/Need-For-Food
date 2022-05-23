using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehevior : MonoBehaviour
{
    public Rigidbody enemyRB;
    
    [SerializeField] private float upperLowerBorder = 8f;
    [SerializeField] private float sidesBorder = 13.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMoving();
    }

    public void MovementRestrictions() // ABSTRACTION
    {
        // destroying object when it goes out of bounds
        if (transform.position.z > upperLowerBorder || transform.position.z < -upperLowerBorder || transform.position.x > sidesBorder || transform.position.x < -sidesBorder)
        {
            Destroy(gameObject);
        }
    }

    public virtual void EnemyMoving() // ABSTRACTION
    {
        float speed = 0;
        enemyRB.AddForce(transform.TransformVector(Vector3.forward) * speed * Time.deltaTime, ForceMode.Impulse); // making objects move in right direction
        MovementRestrictions();
    }
}
