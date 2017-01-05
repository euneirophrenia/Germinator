/* Per creare un nuovo effetto occorre:
 * 1)Ereditare da Effect
 * 2)Definire un costruttore che accetti tutti i parametri necessari all'effetto (vedi esempi)
 * [3] Specificare scriptName con il nome della classe Unity che attua l'effetto sul gameobject
 * Il passo 3 non è indispensabile se lo script rispetta la convenzione di nomi
 * <NomeEffetto>+"Script"
 * 
 * Usare direttamente new <TipoEffetto>(params), genera warning, ora che sono ScriptableObjects,
 * Unity vorrebbe che fossero creati con ScriptableObject.CreateInstance<Tipo>().
 * Se si vuole procedere così, occorre prevedere un metodo "init(params)" per passarci i parametri e settarli correttamente,
 * essenzialmente chiamandolo dopo, al posto del costruttore.
 * 
 * Per effetti i cui script NON ereditano da TimeBasedAbility (come Damage), basta definire l'efficacia e lo script.
 * Per effetti come lo slow, che hanno una durata, anche cooldown e ticks, e l'effetto durerà cooldown*ticks secondi.
 * Per effetti come un cleanse, che sono 1-shot e non hanno un valore, basta definire lo scriptName.
 * */

using System;
using UnityEngine;

[Serializable]
public abstract class Effect : ScriptableObject //non tutti useranno tutto
{
	[SerializeField]
	protected float effect =0f; 

	[SerializeField]
	protected int ticks = 0;

	[SerializeField]
	protected float cooldown = 0f;

	[SerializeField]
	protected string scriptName; //il nome dello script corrispondente

	protected Effect()
	{
		this.scriptName=this.GetType().Name+"Script";
	}

	public string effectScriptName { 
		get { 
			return scriptName; 
		} 
	} 

    public virtual float Effectiveness { 
		get {
			return effect;
		} 
		set {
			effect=value;
		}
	}//con  significato diverso a seconda dell'effetto concreto, puo' essere percentuale di slow dichiarata o altro

 	public virtual int Ticks {
		get {
			return ticks;
		}
		set{
			ticks=value;
		}
	}
    public virtual float Cooldown {
		get {
			return cooldown;	
		} 
		set{
			cooldown=value;
		}
	}
    
}
