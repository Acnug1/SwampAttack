using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private int _shootsCount;

    public override void Shoot(Transform shootPoint)
    {
        StartCoroutine(ShootingBursts(shootPoint));
    }

    private IEnumerator ShootingBursts(Transform shootPoint)
    {
        var waitForSeconds = new WaitForSeconds(0.05f);
        for (int i = 0; i < _shootsCount; i++)
        {
            Instantiate(Bullet, shootPoint.position, Quaternion.identity);
            ChangeAmmunition();
            yield return waitForSeconds;
        }
    }
}
