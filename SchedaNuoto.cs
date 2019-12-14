using System;

namespace GymManager
{
    class SchedaNuoto : Scheda
    {
        // attributi
        private Esercizio _vascheCrawl,
                          _vascheDorso,
                          _vascheRana,
                          _vascheDelfino;

        // costruttore
        public SchedaNuoto()
        {
            // niente
        } 

        // costruttore
        public SchedaNuoto(int id,
                           int durata,                           
                           string istruttore,
                           string data)
            : base(id, durata, istruttore, data)
        {
            // niente
        }

        // Proprietà
        public Esercizio Crawl
        {
            get { return _vascheCrawl; }
            set { _vascheCrawl = value; }
        }

        public Esercizio Dorso
        {
            get { return _vascheDorso; }
            set { _vascheDorso = value; }
        }

        public Esercizio Rana
        {
            get { return _vascheRana; }
            set { _vascheRana = value; }
        }

        public Esercizio Delfino
        {
            get { return _vascheDelfino; }
            set { _vascheDelfino = value; }
        }

        // metodi        
        public void inserisciCrawl(int s,
                                   int r)
        {
            _vascheCrawl.serie = s;
            _vascheCrawl.ripetizioni = r;
        }

        public void inserisciDorso(int s,
                                   int r)
        {
            _vascheDorso.serie = s;
            _vascheDorso.ripetizioni = r;
        }

        public void inserisciRana(int s,
                                  int r)
        {
            _vascheRana.serie = s;
            _vascheRana.ripetizioni = r;
        }

        public void inserisciDelfino(int s,
                                     int r)
        {
            _vascheDelfino.serie = s;
            _vascheDelfino.ripetizioni = r;
        }        
        
        public int contaVasche()
        {
            int totale = 0; // output: numero totale delle vasche

            totale = Crawl.serie * Crawl.ripetizioni +
                     Dorso.serie * Dorso.ripetizioni +
                     Rana.serie * Rana.ripetizioni +
                     Delfino.serie * Delfino.ripetizioni;

            return totale;
        }

        // metodo ereditato da Scheda
        public override string mostraScheda()
        {
            string result = null; // output: contiene la versione testuale dela scheda

            result = "Scheda Nuoto" + Environment.NewLine + Environment.NewLine +
                     "Istruttore: " + Istruttore + "\tData: " + Data + "\tDurata: " + Durata + " min" + Environment.NewLine + Environment.NewLine +
                     "Nr. vasche a Stile Libero: " + Crawl.serie + " x " + Crawl.ripetizioni + Environment.NewLine +
                     "Nr. vasche a Dorso: " + Dorso.serie + " x " + Dorso.ripetizioni + Environment.NewLine +
                     "Nr. vasche a Rana: " + Rana.serie + " x " + Rana.ripetizioni + Environment.NewLine +
                     "Nr. vasche a Delfino: " + Delfino.serie + " x " + Delfino.ripetizioni + Environment.NewLine +
                     "Totale vasche: " + contaVasche() + Environment.NewLine +
                     "----------------------------------";

            return result;            
        }
    }
}
