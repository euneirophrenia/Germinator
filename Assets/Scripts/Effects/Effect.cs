public interface Effect //non tutti useranno tutto
{
    string effectScriptName { get; } //il nome dello script corrispondente

    float effectiveness { get; set; } //con  significato diverso a seconda dell'effetto concreto, puo' essere percentuale di slow dichiarata o altro
 	int ticks { get; set; }
    float cooldown { get; set; }
    
}
