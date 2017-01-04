/* Per creare un nuovo effetto occorre:
 * 1)Ereditare da Effect
 * 2)Definire un costruttore che accetti tutti i parametri necessari all'effetto (vedi esempi)
 * 3)Specificare scriptName con il nome della classe Unity che attua l'effetto sul gameobject
 * 
 * Per effetti i cui script NON ereditano da TimeBasedAbility (come Damage), basta definire l'efficacia e lo script.
 * Per effetti come lo slow, che hanno una durata, anche cooldown e ticks, e l'effetto durerà cooldown*ticks secondi.
 * Per effetti come un cleanse, che sono 1-shot e non hanno un valore, basta definire lo scriptName.
 * */


public abstract class Effect //non tutti useranno tutto
{
	protected float effect =0f; 
	protected int tick = 0;
	protected float cooldown = 0f;
	protected string scriptName;

	public string effectScriptName { 
		get { 
			return scriptName;
		} 
	} //il nome dello script corrispondente

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
			return tick;
		}
		set{
			tick=value;
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
