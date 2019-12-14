using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace GymManager
{
    public static class GestioneDB
    {
        // Metodo che crea il database
        public static void creaDB()
        {       
            SQLiteConnection gymdbConnection = null;

            try
            {
                if (!System.IO.File.Exists("gymdb.sqlite"))
                    SQLiteConnection.CreateFile("gymdb.sqlite");

                gymdbConnection = new SQLiteConnection("Data Source=gymdb.sqlite;Version=3;");

                // Apre la connessione al database
                gymdbConnection.Open();

                string TabellaUtenti = "CREATE TABLE IF NOT EXISTS Atleti( " +
                                       "ID       INTEGER         PRIMARY KEY AUTOINCREMENT, " +
                                       "Nome     VARCHAR(50)     NOT NULL, " +
                                       "Cognome  VARCHAR(50)     NOT NULL, " +
                                       "Eta      TINYINT         NOT NULL DEFAULT 0, " +
                                       "Data     DATETIME        DEFAULT CURRENT_TIMESTAMP)";

                string TabellaPalestra = "CREATE TABLE IF NOT EXISTS Palestra( " +
                                         "ID                INTEGER      PRIMARY KEY AUTOINCREMENT, " +
                                         "Durata            TINYINT      NOT NULL DEFAULT 1, " +
                                         "Istruttore        VARCHAR(50)  NOT NULL, " +
                                         "SerieDeltoidi     TINYINT      NOT NULL, " +
                                         "SerieTricipiti    TINYINT      NOT NULL, " +
                                         "SerieBicipiti     TINYINT      NOT NULL, " +
                                         "SerieDorsali      TINYINT      NOT NULL, " +
                                         "SeriePettorali    TINYINT      NOT NULL, " +
                                         "SerieAddominali   TINYINT      NOT NULL, " +
                                         "SerieGambe        TINYINT      NOT NULL, " +
                                         "RipDeltoidi       TINYINT      NOT NULL, " +
                                         "RipTricipiti      TINYINT      NOT NULL, " +
                                         "RipBicipiti       TINYINT      NOT NULL, " +
                                         "RipDorsali        TINYINT      NOT NULL, " +
                                         "RipPettorali      TINYINT      NOT NULL, " +
                                         "RipAddominali     TINYINT      NOT NULL, " +
                                         "RipGambe          TINYINT      NOT NULL, " +
                                         "IdAtleta          INTEGER      NOT NULL, " +
                                         "DataP             DATETIME     DEFAULT CURRENT_TIMESTAMP, " +                                         
                                         "FOREIGN KEY (IdAtleta) REFERENCES Atleti(ID) ON DELETE CASCADE)";

                string TabellaNuoto = "CREATE TABLE IF NOT EXISTS Nuoto( " +
                                      "ID             INTEGER     PRIMARY KEY AUTOINCREMENT, " +
                                      "Durata         TINYINT     NOT NULL DEFAULT 1, " +
                                      "Istruttore     VARCHAR(50) NOT NULL, " +
                                      "SerieCrawl     TINYINT     NOT NULL, " +
                                      "SerieDorso     TINYINT     NOT NULL, " +
                                      "SerieRana      TINYINT     NOT NULL, " +
                                      "SerieDelfino   TINYINT     NOT NULL, " +
                                      "RipCrawl       TINYINT     NOT NULL, " +
                                      "RipDorso       TINYINT     NOT NULL, " +
                                      "RipRana        TINYINT     NOT NULL, " +
                                      "RipDelfino     TINYINT     NOT NULL, " +
                                      "IdAtleta       INTEGER     NOT NULL, " +
                                      "DataN          DATETIME    DEFAULT CURRENT_TIMESTAMP, " +
                                      "FOREIGN KEY (IdAtleta) REFERENCES Atleti(ID) ON DELETE CASCADE)";

                // Creazione delle tabelle
                SQLiteCommand creaTabellaUtenti = new SQLiteCommand(TabellaUtenti, gymdbConnection);
                creaTabellaUtenti.ExecuteNonQuery();

                SQLiteCommand creaTabellaPalestra = new SQLiteCommand(TabellaPalestra, gymdbConnection);
                creaTabellaPalestra.ExecuteNonQuery();

                SQLiteCommand creaTabellaNuoto = new SQLiteCommand(TabellaNuoto, gymdbConnection);
                creaTabellaNuoto.ExecuteNonQuery();
            }
            catch (Exception exdb)
            {
                // Stampa il testo dell'eccezione a schermo
                Console.WriteLine("Errore di accesso al db: " + exdb);
            }
            finally
            {
                if (gymdbConnection != null)
                {
                    try
                    {
                        // Chiusura del database
                        gymdbConnection.Close();
                    }
                    catch (Exception exdb)
                    {
                        // Stampa il testo dell'eccezione a schermo
                        Console.WriteLine("Errore di chiusura del db: " + exdb);
                    }
                    finally
                    {
                        // Chiude la connessione al db e dealloca le risorse
                        gymdbConnection.Dispose();
                    }
                }
            }            
        }
        
        public static bool eseguiQuery(string query) // input: testo della query
        {
            SQLiteConnection gymdbConnection = null;
            bool executed = false;

            try
            {
                gymdbConnection = new SQLiteConnection("Data Source=gymdb.sqlite;Version=3;");

                gymdbConnection.Open();

                SQLiteCommand querySemplice = new SQLiteCommand(query, gymdbConnection);
                querySemplice.ExecuteNonQuery();

                executed = true;
            }
            catch (Exception exdb)
            {
                // Stampa l'eccezione a video
                Console.WriteLine("Errore di accesso al db: " + exdb);
            }
            finally
            {            
                if (gymdbConnection != null)
                {
                    try
                    {
                        // Chiusura del database
                        gymdbConnection.Close();
                    }
                    catch (Exception exdb)
                    {
                        // Stampa il testo dell'eccezione/errore a schermo
                        Console.WriteLine("Errore di chiusura del db: " + exdb);
                    }
                    finally
                    {
                        // Chiude la connessione al db e dealloca le risorse
                        gymdbConnection.Dispose();
                    }  
                }                                  
            }

            return executed;
        }

        public static List<string[]> eseguiSelect(string query) // input: testo della query
        {
            List<string[]> risultati = new List<string[]>(); // output: dati estratti dal db
            SQLiteConnection gymdbConnection = null;         // lavoro: oggetto connessione
            SQLiteDataReader reader = null;                  // lavoro: oggetto lettore dei dati

            try
            {
                gymdbConnection = new SQLiteConnection("Data Source=gymdb.sqlite;Version=3;");

                gymdbConnection.Open();

                SQLiteCommand querySelect = new SQLiteCommand(query, gymdbConnection);
                reader = querySelect.ExecuteReader();

                while (reader.Read())
                {
                    int i = 0;

                    // creo un array temporaneo per il record
                    string[] record = new string[reader.GetValues().Count];

                    // scorro i risultati
                    foreach (string campo in reader.GetValues())
                    {                        
                        record[i] = reader[campo].ToString(); // aggiungo i campi al record
                                                
                        i++; // incremento il contatore
                    }

                    risultati.Add(record);
                }
            }
            catch (Exception exdb)
            {
                // Stampa l'eccezione a video
                Console.WriteLine("Errore di accesso al db: " + exdb);
            }
            finally
            {
                if (gymdbConnection != null)
                {
                    try
                    {
                        // Chiusura del database
                        gymdbConnection.Close();
                    }
                    catch (Exception exdb)
                    {
                        // Stampa il testo dell'eccezione/errore a schermo
                        Console.WriteLine("Errore di chiusura del db: " + exdb);
                    }
                    finally
                    {
                        // Chiude la connessione al db e dealloca le risorse
                        gymdbConnection.Dispose();
                    }
                }
            }

            return risultati;
        }
    }
}
