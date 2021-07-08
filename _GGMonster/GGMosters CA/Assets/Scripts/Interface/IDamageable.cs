interface IDamageable
{
    /// <summary>
    /// Sets Damage
    /// </summary>
    /// <param name="damage">Damage. if Tickdamage: damage / count</param>
    /// <param name="heal">set true if heal</param>
    /// <param name="tickDamage">set true if tickDamage</param>
    /// <param name="count">damage continue count</param>
    void OnDamage(int damage, bool heal = false, bool tickDamage = false, int count = 0, int skillEnum = -1, bool doNotEndTurn = false);
}
