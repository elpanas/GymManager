using System;

namespace GymManager
{
    class SchedaPalestra : Scheda
    {
        // attributi
        private Esercizio _deltoidi,
                          _tricipiti,
                          _bicipiti,
                          _dorsali,
                          _pettorali,
                          _addominali,
                          _gambe;  

        // costruttore
        public SchedaPalestra()            
        {
            // niente
        }
        
        // costruttore
        public SchedaPalestra(int id,
                              int durata,                              
                              string istruttore,
                              string data)
            : base(id, durata, istruttore, data)
        {
            // niente
        }

        // Proprietà
        public Esercizio Deltoidi
        {
            get { return _deltoidi; }
            set { _deltoidi = value; }
        }

        public Esercizio Tricipiti
        {
            get { return _tricipiti; }
            set { _tricipiti = value; }
        }

        public Esercizio Bicipiti
        {
            get { return _bicipiti; }
            set { _bicipiti = value; }
        }

        public Esercizio Dorsali
        {
            get { return _dorsali; }
            set { _dorsali = value; }
        }

        public Esercizio Pettorali
        {
            get { return _pettorali; }
            set { _pettorali = value; }
        }

        public Esercizio Addominali
        {
            get { return _addominali; }
            set { _addominali = value; }
        }

        public Esercizio Gambe
        {
            get { return _gambe; }
            set { _gambe = value; }
        }

        // Metodi
        public void inserisciDeltoidi(int s,
                                      int r)
        {
            _deltoidi.serie = s;
            _deltoidi.ripetizioni = r;
        }

        public void inserisciTricipiti(int s,
                                       int r)
        {
            _tricipiti.serie = s;
            _tricipiti.ripetizioni = r;
        }

        public void inserisciBicipiti(int s,
                                      int r)
        {
            _bicipiti.serie = s;
            _bicipiti.ripetizioni = r;
        }

        public void inserisciDorsali(int s,
                                     int r)
        {
            _dorsali.serie = s;
            _dorsali.ripetizioni = r;
        }

        public void inserisciPettorali(int s,
                                       int r)
        {
            _pettorali.serie = s;
            _pettorali.ripetizioni = r;
        }

        public void inserisciAddominali(int s,
                                        int r)
        {
            _addominali.serie = s;
            _addominali.ripetizioni = r;
        }

        public void inserisciGambe(int s,
                                   int r)
        {
            _gambe.serie = s;
            _gambe.ripetizioni = r;
        }
        
        // metodo ereditato da Scheda
        public override string mostraScheda()
        {            
            string result = null; // output: contiene la versione testuale dela scheda

            result = "Scheda Palestra" + Environment.NewLine + Environment.NewLine +
                     "Istruttore: " + Istruttore + "\tData: " + Data + "\tDurata: " + Durata + " min" + Environment.NewLine + Environment.NewLine +
                     "Deltoidi: " + Deltoidi.serie + " x " + Deltoidi.ripetizioni + Environment.NewLine +
                     "Tricipiti: " + Tricipiti.serie + " x " + Tricipiti.ripetizioni + Environment.NewLine +
                     "Bicipiti: " + Bicipiti.serie + " x " + Bicipiti.ripetizioni + Environment.NewLine +
                     "Dorsali: " + Dorsali.serie + " x " + Dorsali.ripetizioni + Environment.NewLine +
                     "Pettorali: " + Pettorali.serie + " x " + Pettorali.ripetizioni + Environment.NewLine +
                     "Addominali: " + Addominali.serie + " x " + Addominali.ripetizioni + Environment.NewLine +
                     "Gambe: " + Gambe.serie + " x " + Gambe.ripetizioni + Environment.NewLine +
                     "----------------------------------";
            
            return result;
        }       
    }
}
