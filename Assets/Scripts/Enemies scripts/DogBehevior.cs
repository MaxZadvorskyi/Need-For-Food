using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehevior : EnemyBehevior
{
    [SerializeField] private float dogSpeed;
    // Update is called once per frame
    void Update()
    {
        EnemyMoving();
    }

    public override void EnemyMoving()
    {
        float speed = dogSpeed;
        enemyRB.AddForce(transform.TransformVector(Vector3.forward) * speed * Time.deltaTime, ForceMode.Impulse); // making objects move in right direction
        MovementRestrictions();
    }
}
