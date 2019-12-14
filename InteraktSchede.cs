using System;

namespace GymManager
{
    static class InteraktSchede
    {     
        // --- TEMPO TOTALE DI ESECUZIONE DI ENTRAMBE LE SCHEDE ---
        public static void tempoEsecuzione(Scheda s1, // input: oggetto Scheda
                                           Scheda s2) // input: oggetto Scheda
        {
            int tempo_totale, // lavoro: tempo totale di allenamento
                tempoS1 = 0,  // lavoro: tempo di allenamento scheda 1
                tempoS2 = 0;  // lavoro: tempo di allenamento scheda 2

            try
            {
                tempoS1 = s1.Durata;                               
            }
            catch (NullReferenceException) // oggetto inesistente
            {
                tempoS1 = 0;
            }

            try
            {
                tempoS2 = s2.Durata;                               
            }
            catch (NullReferenceException) // oggetto inesistente
            {
                tempoS2 = 0;
            }

            tempo_totale = tempoS1 + tempoS2;

            Console.WriteLine(Environment.NewLine + "Il tempo totale di esecuzione di entrambe le schede e': " +
                                  tempo_totale + " minuti" + Environment.NewLine);        

        Console.WriteLine("Premere un tasto per tornare al menu principale...");
            Console.ReadKey();
        }
    }
}
