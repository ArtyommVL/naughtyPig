using UnityEngine;

public class Dog : AbstractEnemy {
    // Public
    public override bool CanDamage() {
        return IsAlive;
    }
}
