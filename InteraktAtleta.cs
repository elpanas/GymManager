using System;
using System.Collections.Generic;

namespace GymManager
{
    class InteraktAtleta : IAtleta
    {
        private static InteraktAtleta _instance = null; // tiene traccia dell'istanza
        
        // costruttore protetto
        protected InteraktAtleta()
        {
            // niente
        }

        // metodo per la creazione dell'istanza
        public static InteraktAtleta Instance()
        {
            if (_instance == null)
                _instance = new InteraktAtleta();

            return _instance;
        }

        private List<Atleta> LAtleti = new List<Atleta>(); // attributo contenente gli atleti

        // proprietà
        public List<Atleta> Atleti
        {
            get { return LAtleti; }
        }

        // metodi
        // --- CREA UN ATLETA ---
        public void aggiungiAtleta()
        {   
            bool errore = false;            // lavoro: flag in caso di eccezione
            Atleta atleta = new Atleta();   // lavoro: oggetto contenente le info dell'atleta

            Console.WriteLine("*** Inserimento Nuovo Atleta ***"
                              + Environment.NewLine
                              + Environment.NewLine);
            do
            {
                // prende i dati in input da tastiera
                atleta = inputDati();

                // inserisce i dati nel database
                if (GestioneDB.eseguiQuery("INSERT INTO Atleti (Nome,Cognome,Eta) " +
                                           "VALUES('" + atleta.Nome +
                                           "','" + atleta.Cognome +
                                           "'," + atleta.Eta + ")"))
                {
                    // memorizza l'id dell'ultimo record inserito
                    atleta.Id = ultimoInserito(atleta);

                    // aggiunge l'oggetto alla lista
                    LAtleti.Add(atleta);
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

        // --- MODIFICA UN ATLETA ---
        public void modificaAtleta(int id) // input: id atleta
        {            
            bool trovato = false,         // output: flag in caso di atleta presente
                 errore = false;          // lavoro: flag in caso di eccezione 
            Atleta atleta = new Atleta(); // lavoro: oggetto d'appoggio

            Console.WriteLine("*** Modifica Atleta ***" + Environment.NewLine + Environment.NewLine);

            foreach (Atleta a in LAtleti)
            {      
                if(a.Id == id)
                {
                    trovato = true;
                    do
                    {
                        errore = false;

                        // prende i dati da tastiera
                        atleta = inputDati();

                        // aggiorna dati dell'atleta nel database
                        if (GestioneDB.eseguiQuery("UPDATE Atleti SET" +
                                                   " Nome = '" + atleta.Nome +
                                                   "', Cognome = '" + atleta.Cognome +
                                                   "', Eta = " + atleta.Eta +
                                                   " WHERE ID = " + a.Id))
                        {
                            a.Nome = atleta.Nome;
                            a.Cognome = atleta.Cognome;
                            a.Eta = atleta.Eta;
                            Console.WriteLine("Aggiornamento effettuato" + Environment.NewLine);
                        }
                                                    
                        else
                        {
                            errore = true;
                            Console.WriteLine("Errore di inserimento" + Environment.NewLine);
                        }          
                    } while (errore);                  
                }                                             
            }

            if (!trovato)
                Console.WriteLine("Non esiste un atleta con questo nome");

            Console.WriteLine("Premere un tasto per continuare...");
            Console.ReadKey();
        }

        // --- ELIMINA UN ATLETA ---
        public void eliminaAtleta(int id) // input: id atleta
        {            
            bool errore;          // lavoro: flag per errore
            string elimina = "N"; // lavoro: conferma eliminazione 
            int indice = -1;      // lavoro: posizione dell'oggetto in lista

            Console.WriteLine("*** Elimina Atleta ***" + Environment.NewLine);
            
            foreach (Atleta a in LAtleti)
            {
                if (a.Id == id)
                {            
                    do
                    {
                        errore = false;

                        try
                        {
                            Console.WriteLine("Eliminare l'atleta? S/N");
                            elimina = Console.ReadLine();

                            if (!(elimina is string))
                                throw new Exception("Inserire una lettera, 'S' o 'N'");

                            if (elimina == "S" || elimina == "s")
                            {
                                // elimina l'atleta dal database
                                if (GestioneDB.eseguiQuery("DELETE FROM Atleti WHERE ID = " + id))
                                {
                                    // salva la posizione dell'oggetto in lista
                                    indice = LAtleti.IndexOf(a);
                                    Console.WriteLine("Atleta eliminato con successo" + Environment.NewLine);
                                }                                    
                            }
                        }
                        catch (Exception e) // eccezione
                        {
                            errore = true;
                            Console.WriteLine("Errore di input: " + e);
                        }
                    } while (errore);                    
                }                    
            }

            if (indice > -1) // rimuove l'oggetto dalla lista
                LAtleti.RemoveAt(indice);
            else if (elimina == "S" || elimina == "s")
                Console.WriteLine("Atleta non presente" + Environment.NewLine);

            Console.WriteLine(Environment.NewLine + "Premere un tasto per continuare...");
            Console.ReadKey();
            Console.Clear();
        }

        // --- MOSTRA UN ELENCO DI TUTTI GLI ATLETI ---
        public int mostraTutti()
        {
            int numero_atleti = LAtleti.Count; // output: numero atleti

            Console.WriteLine("*** Elenco Atleti ***" + Environment.NewLine);

            if (numero_atleti > 0)
            {             
                Console.WriteLine("Id\t" +
                                  "Nome\t" +
                                  "Cognome \t" +
                                  "Eta" + Environment.NewLine);

                foreach (Atleta a in LAtleti)                
                    Console.WriteLine(a.mostraAtleta());                
            }            

            return numero_atleti;
        }

        // --- VISUALIZZA UN SINGOLO ATLETA ---
        public int mostraUno(int id)        // input: id atleta
        {
            int idA = 0,                    // output: id atleta
                numero_atleti;              // lavoro: totale atleti in lista
            bool trovato = false,           // lavoro: flag in caso di atleta presente 
                 errore;                    // lavoro: flag in caso di eccezione            

            numero_atleti = LAtleti.Count;

            if (numero_atleti > 0)
            {
                do
                {
                    errore = false;

                    try
                    {
                        if (id == 0)
                        {
                            Console.WriteLine("Inserire l'id dell'atleta che si desidera visualizzare");
                            idA = int.Parse(Console.ReadLine());
                        }
                        else
                            idA = id;

                        if (idA < 0)
                            throw new Exception("Inserire un numero intero positivo");

                        foreach (Atleta a in LAtleti)
                        {
                            if (a.Id == idA)
                            {
                                trovato = true;
                                Console.Clear();
                                Console.WriteLine(Environment.NewLine + a.mostraAtleta());
                            }
                        }

                        if (!trovato)
                        {
                            idA = 0;
                            Console.WriteLine("Atleta non presente" + Environment.NewLine);
                            Console.ReadKey();
                        }
                    }
                    catch (FormatException e)
                    {
                        errore = true;
                        Console.WriteLine(Environment.NewLine + "Errore di input: " + e);                                             
                    }
                    catch (Exception e)
                    {
                        errore = true;
                        Console.WriteLine(Environment.NewLine + "Errore di input: " + e);                                             
                    }             
                    
                    if (errore)
                    {
                        Console.WriteLine(Environment.NewLine + "Premere un tasto per continuare...");
                        Console.ReadKey(); 
                    }

                } while (errore || !trovato);                
            }
            else                          
                Console.WriteLine(Environment.NewLine + "Non sono presenti atleti" + Environment.NewLine);

            if (numero_atleti == 0 || !trovato)
            {
                Console.WriteLine(Environment.NewLine + "Premi un tasto per continuare...");
                Console.ReadKey();                
            }            

            return idA;
        }

        // --- ID ULTIMO UTENTE INSERITO ---
        public int ultimoInserito(Atleta a)
        {
            int id = 0;
            List<string[]> risultati;            

            risultati = GestioneDB.eseguiSelect("SELECT ID FROM Atleti" +
                                                " WHERE Nome = '" + a.Nome +
                                                "' AND Cognome = '" + a.Cognome +
                                                "' ORDER BY data DESC " +
                                                " LIMIT 1");

            if (risultati.Count > 0)            
                foreach (string[] record in risultati)                             
                    id = int.Parse(record[0]); 

            return id;
        }

        // --- PRENDE DATI IN INPUT ---
        public Atleta inputDati()
        {
            Atleta atleta = new Atleta();  // lavoro: oggetto atleta
            bool errore;                   // lavoro: flag in caso di eccezione

            do {
                errore = false;

                try
                {   // prende in input i valori da inserire nell'oggetto
                    Console.WriteLine("Inserire il Nome: ");
                    atleta.Nome = Console.ReadLine();

                    Console.WriteLine(Environment.NewLine + "Inserire il Cognome: ");
                    atleta.Cognome = Console.ReadLine();

                    Console.WriteLine(Environment.NewLine + "Inserire l'eta': ");
                    atleta.Eta = int.Parse(Console.ReadLine());

                    foreach (Atleta a2 in LAtleti)
                    {
                        if ((a2.Nome == atleta.Nome)
                           && (a2.Cognome == atleta.Cognome)
                           && (a2.Eta == atleta.Eta))
                            throw new Exception("Atleta gia' presente in memoria");
                    }
                }
                catch (FormatException e) // eccezione in caso di formato errato
                {
                    errore = true;
                    Console.WriteLine(Environment.NewLine + "Errore di input: " + e);                    
                }
                catch (Exception e) // eccezione
                {
                    errore = true;
                    Console.WriteLine(Environment.NewLine + "Errore: " + e);                    
                }
            } while (errore);

            if (errore)
            {
                Console.WriteLine(Environment.NewLine + "Premere un tasto per continuare...");
                Console.ReadLine();
            }

            return atleta;
        }

        // --- TRASFERISCE I DATI DAL DATABASE ALLA LISTA --- 
        public void popolaLista()
        {
            Atleta atleta = null;       // lavoro: oggetto atleta 
            List<string[]> risultati;   // lavoro: memorizza i record del database
            
            risultati = GestioneDB.eseguiSelect("SELECT * FROM Atleti");

            if (risultati.Count > 0)
            {
                foreach (string[] record in risultati)
                {
                    atleta = new Atleta();

                    atleta.Id = int.Parse(record[0]);
                    atleta.Nome = record[1];
                    atleta.Cognome = record[2];
                    atleta.Eta = int.Parse(record[3]);                    

                    // inserisce l'oggetto nella lista 
                    LAtleti.Add(atleta);  
                }
            }
        }
    }
}
