using System;
using System.Collections.Generic;

namespace GymManager
{
    public enum Stili
    {
        Crawl,
        Dorso,
        Rana,
        Delfino
    }

    class InteraktSchedaNuoto : IScheda
    {
        private static InteraktSchedaNuoto _instance = null;

        // costruttore protetto
        protected InteraktSchedaNuoto()
        {
            // niente
        }

        // crea una singola istanza della classe
        public static InteraktSchedaNuoto Instance()
        {
            if (_instance == null)
                _instance = new InteraktSchedaNuoto();

            return _instance;
        }

        private List<SchedaNuoto> LNuoto = new List<SchedaNuoto>();

        public List<SchedaNuoto> SNuoto
        {
            get { return LNuoto; }
        }

        // --- CREA UNA SCHEDA ---
        public void creaScheda(int id) // input: id atleta
        {
            bool errore = false;                    // lavoro: flag in caso di eccezione 
            SchedaNuoto nuoto = new SchedaNuoto();  // lavoro: oggetto contenente le info della scheda

            Console.WriteLine("*** Crea una nuova scheda nuoto ***"
                              + Environment.NewLine
                              + Environment.NewLine);

            do
            {   
                // downcasting dell'oggetto Scheda
                nuoto = (SchedaNuoto)inputDati();
                nuoto.Idatleta = id;

                // inserisce i dati nel database
                if (GestioneDB.eseguiQuery("INSERT INTO Nuoto (Durata," +
                                                           "Istruttore," +
                                                           "SerieCrawl," +
                                                           "SerieDorso," +
                                                           "SerieRana," +
                                                           "SerieDelfino," +
                                                           "RipCrawl," +
                                                           "RipDorso," +
                                                           "RipRana," +
                                                           "RipDelfino," +
                                                           "IdAtleta) VALUES (" +
                                                            nuoto.Durata + ", '" +
                                                            nuoto.Istruttore + "', " +
                                                            nuoto.Crawl.serie + ", " +
                                                            nuoto.Dorso.serie + ", " +
                                                            nuoto.Rana.serie + ", " +
                                                            nuoto.Delfino.serie + ", " +
                                                            nuoto.Crawl.ripetizioni + ", " +
                                                            nuoto.Dorso.ripetizioni + ", " +
                                                            nuoto.Rana.ripetizioni + ", " +
                                                            nuoto.Delfino.ripetizioni + ", " +
                                                            id + ")"))
                {
                    // aggiunge l'oggetto alla lista
                    LNuoto.Add(nuoto);
                    popolaLista();
                    Console.WriteLine("Inserimento effettuato" + Environment.NewLine);
                }
                else
                {
                    errore = true;
                    Console.WriteLine(Environment.NewLine + "Errore di inserimento" + Environment.NewLine);
                }

                Console.WriteLine(Environment.NewLine + "Premere un tasto per continuare...");
                Console.ReadKey();
            } while (errore);
        }

        // --- MODIFICA L'ULTIMA SCHEDA ---
        public void modificaScheda(int id) // input: id atleta
        {
            bool errore,                            // lavoro: flag in caso di eccezione  
                 trovato = false;                   // lavoro: flag per oggetto trovato 
            SchedaNuoto nuoto = new SchedaNuoto();  // lavoro: oggetto contenente le info della scheda

            Console.WriteLine("*** Aggiorna la scheda nuoto ***" + Environment.NewLine + Environment.NewLine);

            foreach (SchedaNuoto n in LNuoto)
            {
                if (n.Idatleta == id)
                {
                    trovato = true;
                
                    do
                    {
                        errore = false;

                        // downcasting dell'oggetto
                        nuoto = (SchedaNuoto)inputDati();

                        // aggiorna id dati nel db
                        if (GestioneDB.eseguiQuery("UPDATE Nuoto SET " +
                                                    "SerieCrawl = " + n.Crawl.serie +
                                                    ",SerieDorso = " + n.Dorso.serie +
                                                    ",SerieRana = " + n.Rana.serie +
                                                    ",SerieDelfino = " + n.Delfino.serie +
                                                    ",RipCrawl = " + n.Crawl.ripetizioni +
                                                    ",RipDorso = " + n.Dorso.ripetizioni +
                                                    ",RipRana = " + n.Rana.ripetizioni +
                                                    ",RipDelfino = " + n.Delfino.ripetizioni +
                                                    " WHERE IdAtleta = " + n.Idatleta +
                                                    " AND DataN = (SELECT DataN" +
                                                                 " FROM Nuoto" +
                                                                 " WHERE IdAtleta = " + n.Idatleta +
                                                                 " ORDER BY DataN DESC" +
                                                                 " LIMIT 1)"))
                        {
                            n.Crawl = nuoto.Crawl;
                            n.Dorso = nuoto.Dorso;
                            n.Rana = nuoto.Rana;
                            n.Delfino = nuoto.Delfino;
                            Console.WriteLine("Aggiornamento effettuato" + Environment.NewLine);                                                    
                        }
                        else
                        {
                            errore = true;
                            Console.WriteLine(Environment.NewLine + "Errore di aggiornamento" + Environment.NewLine);                            
                        }                                               
                    } while (errore);
                }
            }

            if (!trovato)
                Console.WriteLine(Environment.NewLine + "Scheda non presente" + Environment.NewLine);

            Console.WriteLine(Environment.NewLine + "Premere un tasto per continuare...");
            Console.ReadKey();
        }               

        // --- CANCELLA L'ULTIMA SCHEDA ---
        public void eliminaScheda(int id) // input: id atleta
        {            
            bool errore;          // lavoro: flag per errore
            string elimina = "N"; // lavoro: conferma eliminazione
            int indice = -1;      // lavoro: flag per scheda presente

            foreach (SchedaNuoto n in LNuoto)
            {
                if (n.Idatleta == id)
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
                                if (GestioneDB.eseguiQuery("DELETE FROM Nuoto WHERE" +
                                                           " IdAtleta = " + n.Idatleta +
                                                           " AND DataN = (SELECT DataN" +
                                                                         " FROM Nuoto" +
                                                                         " WHERE IdAtleta = " + n.Idatleta +
                                                                         " ORDER BY DataN DESC" +
                                                                         " LIMIT 1)"))
                                {
                                    // salva l'indice dell'elemento da rimuovere
                                    indice = LNuoto.IndexOf(n);
                                    Console.WriteLine(Environment.NewLine + "Scheda eliminata con successo" + Environment.NewLine);
                                }
                                else
                                {
                                    errore = true;
                                    Console.WriteLine(Environment.NewLine + "Errore in fase di eliminazione" + Environment.NewLine);
                                }
                            }
                        }
                        catch (Exception e) // eccezione
                        {
                            errore = true;
                            Console.WriteLine(Environment.NewLine + "Errore di input: " + e);
                        }
                    } while (errore);    
                }
            }

            if (indice > -1)
                LNuoto.RemoveAt(indice); // elimina la scheda dalla lista
            else if (indice < 0 || (elimina == "S" || elimina == "s"))
                Console.WriteLine("Scheda non presente" + Environment.NewLine);

            Console.WriteLine(Environment.NewLine + "Premere un tasto per continuare...");
            Console.ReadKey();
        }

        // --- MOSTRA L'ULTIMA SCHEDA ---
        public void visualizzaScheda(int id)    // input: id atleta                                            
        {
            bool trovato = false; // lavoro: flag per in caso di scheda presente

            foreach (SchedaNuoto n in LNuoto)
            {
                if (n.Idatleta == id && !trovato)
                {
                    // mostra a video la scheda
                    Console.WriteLine(n.mostraScheda());
                    trovato = true;
                }
            }

            if (!trovato)
            {
                Console.Clear();
                Console.WriteLine("Non esiste una scheda nuoto per questo atleta" + Environment.NewLine);
            }
        }

        // --- RESTITUISCE L'ULTIMA SCHEDA ---
        public Scheda ritornaScheda(int id)     // input: id atleta                                            
        {
            bool trovato = false;               // lavoro: flag per in caso di scheda presente
            SchedaNuoto ultima_scheda = null;   // lavoro: oggetto contenente le info della scheda

            foreach (SchedaNuoto n in LNuoto)
            {
                if (n.Idatleta == id && !trovato)
                {
                    ultima_scheda = n;
                    trovato = true;
                }
            }

            return ultima_scheda;
        }

        // --- PRENDE DATI IN INPUT ---
        public Scheda inputDati()
        {
            // Dichiarazione variabili
            int i = 0;                              // lavoro: contatore
            bool errore = false;                    // lavoro: flag in caso di eccezione
            SchedaNuoto nuoto = new SchedaNuoto();  // lavoro: oggetto contenente le info della scheda nuoto
            Esercizio ex = new Esercizio();         // lavoro: variabile strutturata Esercizio

            do
            {
                try
                {   // prende in input i valori da inserire nell'oggetto
                    Console.WriteLine("Inserire il tempo di esecuzione della scheda in minuti: ");
                    nuoto.Durata = int.Parse(Console.ReadLine());

                    Console.WriteLine(Environment.NewLine + "Inserire nome dell'istruttore: ");
                    nuoto.Istruttore = Console.ReadLine();

                    foreach (string stile in Enum.GetNames(typeof(Stili)))
                    {
                        // Prende in input serie e vasche di ogni stile
                        Console.WriteLine(Environment.NewLine + "Inserire nr. di serie " + stile + ":");
                        ex.serie = int.Parse(Console.ReadLine());

                        Console.WriteLine(Environment.NewLine + "Inserire nr. di vasche " + stile + ":");
                        ex.ripetizioni = int.Parse(Console.ReadLine());
                        
                        if (ex.serie < 0 || ex.ripetizioni < 0)
                            throw new Exception("Il numero delle serie e delle vasche deve essere positivo");
                        else
                        {   // segue l'ordine degli elementi del tipo enum
                            switch (i)
                            {
                                case 0:
                                    nuoto.Crawl = ex;
                                    break;
                                case 1:
                                    nuoto.Dorso = ex;
                                    break;
                                case 2:
                                    nuoto.Rana = ex;
                                    break;
                                case 3:
                                    nuoto.Delfino = ex;
                                    break;
                            }

                            i++;
                        }
                    }
                }
                catch (FormatException e) // formato errato
                {
                    Console.WriteLine(Environment.NewLine + "Errore di input: " + e + Environment.NewLine);                    
                    errore = true;
                }
                catch (Exception e) // input fuori dal dominio accettato
                {
                    Console.WriteLine(Environment.NewLine + "Errore di input: " + e + Environment.NewLine);                    
                    errore = true;
                }

                if (errore)
                {
                    Console.WriteLine(Environment.NewLine + "Premere un tasto per continuare...");
                    Console.ReadKey();                    
                }
            } while (errore);

            return nuoto;
        }

        // --- TRASFERISCE I DATI DAL DATABASE ALLA LISTA ---
        public void popolaLista()
        {
            SchedaNuoto nuoto = null;        // lavoro: oggetto scheda nuoto
            List<string[]> risultati = null; // lavoro: memorizza i record del database

            risultati = GestioneDB.eseguiSelect("SELECT * " +                                                
                                                "FROM Atleti as A LEFT JOIN Nuoto as N " +
                                                "WHERE A.ID = N.IdAtleta " +
                                                "ORDER BY N.DataN DESC");

            if (risultati.Count > 0)
            {
                LNuoto.Clear(); // inizializza la scheda nuoto

                foreach (string[] record in risultati)
                {
                    int Id = int.Parse(record[0]),      // lavoro: id atleta
                        Durata = int.Parse(record[5]);  // lavoro: durata della scheda
                    string Istruttore = record[6],      // lavoro: nome dell'istruttore
                           Data = record[16];           // lavoro: data della scheda

                    nuoto = new SchedaNuoto(Id, Durata, Istruttore, Data);

                    // inserisce i dati nell'oggetto nuoto
                    nuoto.inserisciCrawl(int.Parse(record[7]), int.Parse(record[11]));
                    nuoto.inserisciDorso(int.Parse(record[8]), int.Parse(record[12]));
                    nuoto.inserisciRana(int.Parse(record[9]), int.Parse(record[13]));
                    nuoto.inserisciDelfino(int.Parse(record[10]), int.Parse(record[14]));                                      

                    // inserisce l'oggetto nella lista schede
                    LNuoto.Add(nuoto);                    
                }
            }
        }
    }
}
