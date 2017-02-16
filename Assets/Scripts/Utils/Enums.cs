using UnityEngine;

/// <summary>
/// Classe per evitare di riferire i layer per nome nel codice.
/// Ogni volta che si aggiunge un layer custom sarebbe carino estendere questa struttura con il valore corrispondente.
/// L'uso è completamente omogeneo a un <c>enum</c> e si può passare un <c>CustomLayerEnum</c> al posto di un <c>Layer</c> ove richiesto.
/// </summary>
public struct CustomLayerEnum
{

    private static readonly CustomLayerEnum enemy = new CustomLayerEnum("EnemyDefault");
    private static readonly CustomLayerEnum shielded = new CustomLayerEnum("ShieldedEnemy");
    private static readonly CustomLayerEnum projectile = new CustomLayerEnum("Projectile");
    public readonly string Value;

    private CustomLayerEnum(string v)
    {
        Value = v;
    }

    /// <summary>
    /// Il layer in cui a default giacciono gli enemy colpibili.
    /// </summary>
    public static CustomLayerEnum Enemy {  get { return enemy; } }

    /// <summary>
    /// Il layer in cui giacciono gli enemy con uno scudo attivo (incolpibili da proiettili)
    /// </summary>
    public static CustomLayerEnum Shielded { get { return shielded; } }

    /// <summary>
    /// Il layer in cui giacciono i proiettili, gli unici in grado di collidere con scudi e nemici.
    /// </summary>
    public static CustomLayerEnum Projectile { get { return projectile; } }

    public static implicit operator string(CustomLayerEnum e)
    {
        return e.Value;
    }

    public static implicit operator int(CustomLayerEnum e)
    {
        return LayerMask.NameToLayer(e.Value);
    }
}
