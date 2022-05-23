using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerBehevior : EnemyBehevior
{
    [SerializeField] private float trainerSpeed;
    // Update is called once per frame
    void Update()
    {
        EnemyMoving();
    }

    public override void EnemyMoving()
    {
        float speed = trainerSpeed;
        enemyRB.AddForce(transform.TransformVector(Vector3.forward) * speed * Time.deltaTime, ForceMode.Impulse); // making objects move in right direction
        MovementRestrictions();
    }
}
