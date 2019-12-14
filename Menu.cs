using System;

namespace GymManager
{
    public static class Menu
    {
        public static int menuGenerale()
        {
            int scelta = 0; // output: input dell'utente

            try
            {
                Console.WriteLine("---------------------------" + Environment.NewLine +
                                  "1. Visualizza Info Atleta" + Environment.NewLine +
                                  "2. Aggiungi Atleta" + Environment.NewLine +
                                  "3. Termina app" + Environment.NewLine + Environment.NewLine +
                                  "Inserire il numero dell'opzione desiderata:");

                scelta = int.Parse(Console.ReadLine());

                if (scelta < 1 || scelta > 3)
                    throw (new Exception("Il valore inserito, " + scelta +
                                         " deve essere un numero intero compreso tra 1 e 3" +
                                         Environment.NewLine));
            }
            catch (FormatException e) // formato errato
            {
                Console.WriteLine(Environment.NewLine + "Errore di input: " + e);
            }
            catch (Exception e) // eccezione
            {
                Console.WriteLine(Environment.NewLine + "Errore di input: " + e);
            }

            if (scelta < 1 || scelta > 3)
            {
                Console.WriteLine(Environment.NewLine + "Premere un tasto per continuare... ");
                Console.ReadKey();
            }

            Console.Clear();

            return scelta;
        }

        public static int menuAtleta()
        {
            int scelta = 0; // output: input dell'utente            

            try
            {
                scelta = -1;

                Console.WriteLine("---------------------------" + Environment.NewLine +
                                  "1. Modifica atleta" + Environment.NewLine +
                                  "2. Elimina atleta" + Environment.NewLine +
                                  "3. Visualizza ultima scheda Palestra" + Environment.NewLine +
                                  "4. Visualizza ultima Scheda Nuoto" + Environment.NewLine +
                                  "5. Durata totale delle schede" + Environment.NewLine +
                                  "6. Torna al menu principale" + Environment.NewLine + Environment.NewLine +
                                  "Inserire il numero dell'opzione desiderata:");

                scelta = int.Parse(Console.ReadLine());

                if (scelta < 1 || scelta > 6)
                    throw (new Exception("Il valore inserito, " + scelta +
                                         " deve essere un numero intero compreso tra 1 e 6" +
                                         Environment.NewLine));
            }
            catch (FormatException e) // formato errato
            {
                Console.WriteLine(Environment.NewLine + "Errore di input: " + e);                
            }
            catch (Exception e) // eccezione
            {
                Console.WriteLine(Environment.NewLine + "Errore di input: " + e);                
            }

            if (scelta < 0 || scelta > 6)
            {                
                Console.WriteLine(Environment.NewLine + "Premere un tasto per continuare... ");
                Console.ReadKey();
            }

            Console.Clear();

            return scelta;
        }

        public static int menuScheda()
        {
            int scelta = -1; // output: input dell'utente

            try
                {   
                    Console.WriteLine("---------------------------" + Environment.NewLine +
                                      "1. Aggiungi scheda" + Environment.NewLine +
                                      "2. Modifica scheda" + Environment.NewLine +
                                      "3. Elimina scheda" + Environment.NewLine +
                                      "4. Torna al menu principale" + Environment.NewLine + Environment.NewLine +
                                      "Inserire il numero dell'opzione desiderata:");

                    scelta = int.Parse(Console.ReadLine());

                    if (scelta < 1 || scelta > 4)
                        throw (new Exception("Il valore inserito, " + scelta +
                                             " deve essere un numero intero compreso tra 1 e 4" +
                                             Environment.NewLine));
                }
                catch (FormatException e) // formato errato
                {
                    Console.WriteLine(Environment.NewLine + "Errore di input: " + e);                    
                }
                catch (Exception e) // formato errato
                {
                    Console.WriteLine(Environment.NewLine + "Errore di input: " + e);                    
                }

                if (scelta < 0 || scelta > 4)
                {
                    Console.WriteLine(Environment.NewLine + "Premere un tasto per continuare... ");
                    Console.ReadKey();                    
                }           

            Console.Clear();

            return scelta;
        }
    }
}
