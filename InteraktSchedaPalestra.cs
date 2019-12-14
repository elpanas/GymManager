using System;
using System.Collections.Generic;

namespace GymManager
{
    // contiene i nomi dei gruppi muscolari
    public enum Muscoli
    {
        Deltoidi,
        Tricipiti,
        Bicipiti,
        Dorsali,
        Pettorali,
        Addominali,
        Gambe
    }

    class InteraktSchedaPalestra : IScheda
    {
        private static InteraktSchedaPalestra _instance = null;

        // costruttore protetto
        protected InteraktSchedaPalestra()
        {
            // niente
        }

        // crea una singola istanza
        public static InteraktSchedaPalestra Instance()
        {
            if (_instance == null)
                _instance = new InteraktSchedaPalestra();

            return _instance;
        }

        // attributo contenente le schede
        private List<SchedaPalestra> LPalestra = new List<SchedaPalestra>();

        public List<SchedaPalestra> SPalestra
        {
            get { return LPalestra; }
        }

        // --- CREA UNA NUOVA SCHEDA --- 
        public void creaScheda(int id) // input: id atleta
        {
            // dichiarazione variabili                  
            bool errore;                                    // lavoro: flag in caso di eccezione            
            SchedaPalestra palestra = new SchedaPalestra(); // lavoro: oggetto contenente le info della scheda nuoto

            Console.WriteLine("*** Crea una nuova scheda palestra ***"
                              + Environment.NewLine
                              + Environment.NewLine);
            do
            {
                errore = false;

                // downcasting
                palestra = (SchedaPalestra)inputDati();
                palestra.Idatleta = id;

                // inserisce i dati nel database
                if (GestioneDB.eseguiQuery("INSERT INTO Palestra (Durata," +
                                                                 "Istruttore," +
                                                                 "SerieDeltoidi," +
                                                                 "SerieTricipiti," +
                                                                 "SerieBicipiti," +
                                                                 "SerieDorsali," +
                                                                 "SeriePettorali, " +
                                                                 "SerieAddominali, " +
                                                                 "SerieGambe," +
                                                                 "RipDeltoidi," +
                                                                 "RipTricipiti," +
                                                                 "RipBicipiti," +
                                                                 "RipDorsali," +
                                                                 "RipPettorali, " +
                                                                 "RipAddominali, " +
                                                                 "RipGambe," +
                                                                 "IdAtleta) VALUES (" +
                                                                 palestra.Durata + ", '" +
                                                                 palestra.Istruttore + "', " +
                                                                 palestra.Deltoidi.serie + ", " +
                                                                 palestra.Tricipiti.serie + ", " +
                                                                 palestra.Bicipiti.serie + ", " +
                                                                 palestra.Dorsali.serie + ", " +
                                                                 palestra.Pettorali.serie + ", " +
                                                                 palestra.Addominali.serie + ", " +
                                                                 palestra.Gambe.serie + ", " +
                                                                 palestra.Deltoidi.ripetizioni + ", " +
                                                                 palestra.Tricipiti.ripetizioni + ", " +
                                                                 palestra.Bicipiti.ripetizioni + ", " +
                                                                 palestra.Dorsali.ripetizioni + ", " +
                                                                 palestra.Pettorali.ripetizioni + ", " +
                                                                 palestra.Addominali.ripetizioni + ", " +
                                                                 palestra.Gambe.ripetizioni + ", " +
                                                                 id + ")"))
                {
                    // aggiunge l'oggetto alla lista
                    LPalestra.Add(palestra);
                    popolaLista();
                    Console.WriteLine("Inserimento effettuato" + Environment.NewLine);                    
                }
                else
                {
                    errore = true;
                    Console.WriteLine("Errore di inserimento" + Environment.NewLine);                    
                }

                Console.WriteLine("Premere un tasto per continuare...");
                Console.ReadKey();
            } while (errore);             
        }

        // --- MODIFICA UNA SCHEDA ---
        public void modificaScheda(int id) // input: id atleta
        {
            bool errore,                                    // lavoro: flag in caso di eccezione                 
                 trovato = false;                           // lavoro: flag in caso di record presente
            SchedaPalestra palestra = new SchedaPalestra(); // lavoro: oggetto scheda

            foreach (SchedaPalestra p in LPalestra)
            {
                if (p.Idatleta == id)
                {
                    trovato = true;

                    Console.WriteLine("*** Aggiorna la scheda palestra ***" + Environment.NewLine + Environment.NewLine);
                    do
                    {
                        errore = false;

                        // downcasting
                        palestra = (SchedaPalestra)inputDati();

                        // aggiorna id dati nel db
                        if (GestioneDB.eseguiQuery("UPDATE Palestra SET " +
                                                   "Istruttore = '" + p.Istruttore +
                                                   "',SerieDeltoidi = " + p.Deltoidi.serie +
                                                   ",SerieTricipiti = " + p.Tricipiti.serie +
                                                   ",SerieBicipiti = " + p.Bicipiti.serie +
                                                   ",SerieDorsali = " + p.Dorsali.serie +
                                                   ",SeriePettorali = " + p.Pettorali.serie +
                                                   ",SerieAddominali = " + p.Addominali.serie +
                                                   ",SerieGambe = " + p.Gambe.serie +
                                                   ",RipDeltoidi = " + p.Deltoidi.ripetizioni +
                                                   ",RipTricipiti = " + p.Tricipiti.ripetizioni +
                                                   ",RipBicipiti = " + p.Bicipiti.ripetizioni +
                                                   ",RipDorsali = " + p.Dorsali.ripetizioni +
                                                   ",RipPettorali = " + p.Pettorali.ripetizioni +
                                                   ",RipAddominali = " + p.Addominali.ripetizioni +
                                                   ",RipGambe = " + p.Gambe.ripetizioni +                                                   
                                                   " WHERE IdAtleta = " + p.Idatleta +
                                                   " AND DataP = (SELECT DataP" +
                                                                " FROM Palestra" +
                                                                " WHERE IdAtleta = " + p.Idatleta +
                                                                " ORDER BY DataP DESC" +
                                                                " LIMIT 1)"))
                        {
                            // aggiorna l'oggetto
                            p.Idatleta = id;
                            p.Istruttore = palestra.Istruttore;
                            p.Durata = palestra.Durata;
                            p.Istruttore = palestra.Istruttore;
                            p.Deltoidi = palestra.Deltoidi;
                            p.Tricipiti = palestra.Tricipiti;
                            p.Bicipiti = palestra.Bicipiti;
                            p.Dorsali = palestra.Dorsali;
                            p.Pettorali = palestra.Pettorali;
                            p.Addominali = palestra.Addominali;
                            p.Gambe = palestra.Gambe;                                 

                            Console.WriteLine("Aggiornamento effettuato" + Environment.NewLine);                            
                        }
                        else
                        {
                            errore = true;
                            Console.WriteLine("Errore di aggiornamento" + Environment.NewLine);                            
                        }                       
                    } while (errore);
                }
            }

            if (!trovato)
                Console.WriteLine("Scheda non presente" + Environment.NewLine);

            Console.WriteLine("Premere un tasto per continuare...");
            Console.ReadKey();
        }

        // --- CANCELLA L'ULTIMA SCHEDA ---
        public void eliminaScheda(int id) // input: id dell'atleta
        {
            int indice = -1;      // lavoro: flag per scheda presente
            bool errore;          // lavoro: flag per errore
            string elimina = "N"; // lavoro: conferma eliminazione

            foreach (SchedaPalestra p in LPalestra)
            {
                if (p.Idatleta == id)
                {
                    do
                    {
                        errore = false;
                        try
                        {
                            Console.WriteLine("Eliminare la scheda? S/N");
                            elimina = Console.ReadLine();

                            if (!(elimina is string))
                                throw new Exception("Inserire una lettera, 'S' o 'N'");

                            if (elimina == "S" || elimina == "s")
                            {                                
                                // elimina il record dal database
                                if (GestioneDB.eseguiQuery("DELETE FROM Palestra WHERE" +
                                                           " IdAtleta = " + p.Idatleta +
                                                           " AND DataP = (SELECT DataP" +
                                                                         " FROM Palestra" +
                                                                         " WHERE IdAtleta = " + p.Idatleta +
                                                                         " ORDER BY DataP DESC" +
                                                                         " LIMIT 1)"))
                                {
                                    // salva l'indice dell'elemento da rimuovere
                                    indice = LPalestra.IndexOf(p);                                    
                                    Console.WriteLine(Environment.NewLine + "Scheda eliminata con successo" + Environment.NewLine);
                                }
                                else
                                {
                                    errore = true;
                                    Console.WriteLine("Errore in fase di eliminazione" + Environment.NewLine);                                    
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            errore = true;
                            Console.WriteLine("Errore di input: " + e + Environment.NewLine);
                        }                                            
                    } while (errore);                            
                }   
            }

            if (indice > -1)
                LPalestra.RemoveAt(indice); // elimina la scheda dalla lista
            else if (indice < 0 || (elimina == "S" || elimina == "s"))               
                Console.WriteLine("Scheda non presente" + Environment.NewLine);            

            Console.WriteLine(Environment.NewLine + "Premere un tasto per continuare...");
            Console.ReadKey();
        }

        // --- MOSTRA L'ULTIMA SCHEDA DELL'ATLETA RICHIESTO
        public void visualizzaScheda(int id)     // input: id atleta                                            
        {
            bool trovato = false;   // lavoro: flag per in caso di scheda presente                    

            foreach (SchedaPalestra p in LPalestra)
            { 
                if (p.Idatleta == id && !trovato)
                {
                    // mostra a video la scheda                    
                    Console.WriteLine(p.mostraScheda());
                    trovato = true;
                }
            }

            if (!trovato)
            {
                Console.Clear();
                Console.WriteLine("Non esiste una scheda palestra per questo atleta" + Environment.NewLine);
            }
        }

        // --- RESTITUISCE L'ULTIMA SCHEDA ---
        public Scheda ritornaScheda(int id)      // input: id atleta                                            
        {
            bool trovato = false;                // lavoro: flag per in caso di scheda presente
            SchedaPalestra ultima_scheda = null; // lavoro: oggetto contenente le info della scheda

            foreach (SchedaPalestra p in LPalestra)
            {
                if (p.Idatleta == id && !trovato)
                {
                    ultima_scheda = p;
                    trovato = true;
                }
            }

            return ultima_scheda;
        }

        // --- PRENDE DATI IN INPUT ---
        public Scheda inputDati()
        {
            // Dichiarazione variabili
            int i = 0;                                      // lavoro: contatore
            bool errore = false;                            // lavoro: flag in caso di eccezione
            SchedaPalestra palestra = new SchedaPalestra(); // lavoro: oggetto contenente le info della scheda
            Esercizio ex = new Esercizio();                 // lavoro: struttura di appoggio

            do
            {
                try
                {
                    // prende in input i valori da inserire nell'oggetto
                    Console.WriteLine("Inserire il tempo di esecuzione della scheda in minuti: ");
                    palestra.Durata = int.Parse(Console.ReadLine());

                    Console.WriteLine(Environment.NewLine + "Inserire nome dell'istruttore: ");
                    palestra.Istruttore = Console.ReadLine();

                    foreach (string muscolo in Enum.GetNames(typeof(Muscoli)))
                    {                        
                        // Prende in input serie e vasche di ogni stile
                        Console.WriteLine(Environment.NewLine + "Inserire nr. di serie di " + muscolo + ":");
                        ex.serie = int.Parse(Console.ReadLine());

                        Console.WriteLine(Environment.NewLine + "Inserire nr. di ripetizioni di " + muscolo + ":");
                        ex.ripetizioni = int.Parse(Console.ReadLine());

                        if (ex.serie < 0 || ex.ripetizioni < 0)
                            throw new Exception("Il numero delle serie e delle ripetizioni deve essere positivo");
                        else
                        {   // segue l'ordine degli elementi del tipo enum
                            switch (i)
                            {
                                case 0:
                                    palestra.Deltoidi = ex;
                                    break;
                                case 1:
                                    palestra.Tricipiti = ex;
                                    break;
                                case 2:
                                    palestra.Bicipiti = ex;
                                    break;
                                case 3:
                                    palestra.Dorsali = ex;
                                    break;
                                case 4:
                                    palestra.Pettorali = ex;
                                    break;
                                case 5:
                                    palestra.Addominali = ex;
                                    break;
                                case 6:
                                    palestra.Gambe = ex;
                                    break;
                            }

                            i++;
                        }
                    }
                }
                catch (FormatException e) // formato errato
                {
                    Console.WriteLine("Errore di input: " + e + Environment.NewLine);                    
                    errore = true;
                }
                catch (Exception e) // input fuori dal dominio accettato
                {
                    Console.WriteLine("Errore di input: " + e + Environment.NewLine);                    
                    errore = true;
                }

                if (errore)
                {
                    Console.WriteLine("Premere un tasto per continuare...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (errore);

            return palestra;
        }

        // --- TRASFERISCE I DATI DAL DATABASE ALLA LISTA
        public void popolaLista()
        {
            SchedaPalestra palestra;         // lavoro: oggetto scheda palestra
            List<string[]> risultati = null; // lavoro: memorizza i record del database            

            risultati = GestioneDB.eseguiSelect("SELECT * " +                                                               
                                                "FROM Atleti as A LEFT JOIN Palestra as P " +
                                                "WHERE A.ID = P.IdAtleta " +
                                                "ORDER BY P.DataP DESC");            

            if(risultati.Count > 0)
            {
                LPalestra.Clear(); // inizializza la lista

                foreach (string[] record in risultati)
                {
                    int Id = int.Parse(record[0]),      // lavoro: id atleta               
                        Durata = int.Parse(record[5]);  // lavoro: durata della scheda
                    string Istruttore = record[6],      // lavoro: nome dell'istruttore  
                           Data = record[22];           // lavoro: data della scheda

                    palestra = new SchedaPalestra(Id, Durata, Istruttore, Data);                    

                    // inserisce i dati nell'oggetto palestra
                    palestra.inserisciDeltoidi(int.Parse(record[7]), int.Parse(record[14]));
                    palestra.inserisciTricipiti (int.Parse(record[8]), int.Parse(record[15]));
                    palestra.inserisciBicipiti(int.Parse(record[9]), int.Parse(record[16]));
                    palestra.inserisciDorsali(int.Parse(record[10]), int.Parse(record[17]));
                    palestra.inserisciPettorali(int.Parse(record[11]), int.Parse(record[18]));
                    palestra.inserisciAddominali (int.Parse(record[12]), int.Parse(record[19]));
                    palestra.inserisciGambe(int.Parse(record[13]), int.Parse(record[20]));                    

                    // inserisce l'oggetto nella lista schede
                    LPalestra.Add(palestra);                    
                }
            }            
        }
    }
}
