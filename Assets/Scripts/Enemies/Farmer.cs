using UnityEngine;

public class Farmer : AbstractEnemy{
    // Public
    public override bool CanDamage() {
        return IsAlive;
    }
}
