using System;

namespace GymManager
{
    public static class MainApp
    {        
        public static void Applicazione()
        {
            // Inizializza le variabili
            int scelta_generale = -1,   // lavoro: opzione del menu generale
                scelta_atleta = -1,     // lavoro: opzione del menu atleta
                scelta_scheda = -1,     // lavoro: opzione del menu scheda
                id = 0;                 // lavoro: id dell'atleta      

            // instanzia le classi Interakt con il metodo Singleton
            InteraktAtleta interaktAtleta = InteraktAtleta.Instance();
            InteraktSchedaPalestra interaktSchedaPalestra = InteraktSchedaPalestra.Instance();
            InteraktSchedaNuoto interaktSchedaNuoto = InteraktSchedaNuoto.Instance();

            // Crea il database se non esiste e apre la connessione
            GestioneDB.creaDB();

            // Trasferisce i dati dal db alle strutture dati
            interaktAtleta.popolaLista();
            interaktSchedaPalestra.popolaLista();
            interaktSchedaNuoto.popolaLista();           

            do
            {
                do
                {
                    Console.WriteLine("--- GESTORE SCHEDE ---" + Environment.NewLine);

                    // Mostra il menu generale
                    scelta_generale = Menu.menuGenerale();
                } while (scelta_generale < 1 || scelta_generale > 3);

                switch (scelta_generale)
                {
                    case 1:
                        // Mostra l'elenco di tutti gli atleti iscritti
                        interaktAtleta.mostraTutti();

                        // Sceglie e visualizza l'atleta in base all'id prescelto
                        id = interaktAtleta.mostraUno(0);

                        // Controllo del valore id
                        if (id > 0)
                        {
                            do
                            {                                
                                interaktAtleta.mostraUno(id);
                                // Mostra le opzioni relative all'atleta prescelto
                                scelta_atleta = Menu.menuAtleta();   

                            } while (scelta_atleta < 0 || scelta_atleta > 6);

                            switch (scelta_atleta)
                            {
                                case 1: // Modifica le info dell'atleta prescelto  
                                    interaktAtleta.modificaAtleta(id);                                    
                                    break;
                                case 2: // Elimina l'atleta prescelto 
                                    interaktAtleta.eliminaAtleta(id);                                        
                                    break;
                                case 3: // Mostra la scheda dell'atleta prescelto 
                                    do
                                    {       
                                        interaktSchedaPalestra.visualizzaScheda(id);

                                        // Mostra le opzioni relative alla scheda palestra dell'atleta prescelto
                                        scelta_scheda = Menu.menuScheda();

                                    } while (scelta_scheda < 0 || scelta_scheda > 4);
                                    
                                    switch (scelta_scheda)
                                    {
                                        case 1: // Crea una nuova scheda palestra
                                            interaktSchedaPalestra.creaScheda(id);
                                            break;
                                        case 2: // Modifica la scheda palestra
                                            interaktSchedaPalestra.modificaScheda(id);
                                            break;
                                        case 3: // Elimina la scheda palestra
                                            interaktSchedaPalestra.eliminaScheda(id);
                                            break;
                                        default: // Torna al menu principale
                                            scelta_generale = 0;
                                            break;
                                    }                                    
                                    break;                                
                                case 4: // Mostra la scheda nuoto dell'atleta
                                    interaktSchedaNuoto.visualizzaScheda(id);                                    

                                    // Mostra le opzioni relative alla scheda nuoto dell'atleta prescelto                                    
                                    scelta_scheda = Menu.menuScheda();
                                    switch (scelta_scheda)
                                    {
                                        case 1: // Crea una nuova scheda palestra
                                            interaktSchedaNuoto.creaScheda(id);
                                            break;
                                        case 2: // Modifica la scheda palestra
                                            interaktSchedaNuoto.modificaScheda(id);
                                            break;
                                        case 3: // Elimina la scheda palestra
                                            interaktSchedaNuoto.eliminaScheda(id);
                                            break;
                                        default: // Torna al menu principale
                                            scelta_generale = 0;                                               
                                            break;
                                    }                                    
                                    break;
                                case 5: // Calcola la durata di entrambe le schede
                                    InteraktSchede.tempoEsecuzione(interaktSchedaNuoto.ritornaScheda(id),
                                                                   interaktSchedaPalestra.ritornaScheda(id));
                                    break;                               
                                default: // Torna al menu principale                               
                                    scelta_generale = 0;                                    
                                    break;
                            }
                        }                                   
                        else // Torna al menu principale                  
                            scelta_generale = 0;                                                
                        break;
                    case 2: // Aggiunge un nuovo atleta                        
                        interaktAtleta.aggiungiAtleta();
                        scelta_generale = 0;
                        break;
                    default: // Termina l'esecuzione                      
                        Console.WriteLine("Buona giornata..." + Environment.NewLine);
                        Console.WriteLine("Premere un tasto per terminare...");
                        Console.ReadKey();
                        break;
                }

                // Pulisce lo schermo
                Console.Clear();

            } while (scelta_generale == 0 || scelta_generale != 3);
        }
    }
}