using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehevior : MonoBehaviour
{
    Rigidbody enemyRB;
    public float speed;
    float upperLowerBorder = 12;
    float sidesBorder = 23;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRB.AddForce(transform.TransformVector(Vector3.forward) * speed * Time.deltaTime, ForceMode.Impulse); // making objects move in right direction

        // destroying object when it goes out of bounds
        if (transform.position.z > upperLowerBorder || transform.position.z < -upperLowerBorder || transform.position.x > sidesBorder || transform.position.x < -sidesBorder)
        {
            Destroy(gameObject);
        }
    }
}
