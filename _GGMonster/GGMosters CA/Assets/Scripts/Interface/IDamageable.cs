interface IDamageable
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage">damage amount</param>
    /// <param name="heal">it is not damage, is heal</param>
    void OnDamage(int damage, bool heal = false);
}
