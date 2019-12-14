namespace GymManager
{
    abstract class Scheda
    {
        // attributi
        private int _idatleta,
                    _durata;                    
        private string _istruttore,
                       _data;

        // costruttore
        public Scheda()
        {
            // niente
        }

        // costruttore
        public Scheda(int idatleta,
                      int durata, 
                      string istruttore,
                      string data)
        {
            _idatleta = idatleta;
            _durata = durata;
            _istruttore = istruttore;
            _data = data;
        }     

        // proprietà
        public int Idatleta
        {
            get { return _idatleta; }
            set { _idatleta = value; }
        }

        public int Durata
        {
            get { return _durata; }
            set { _durata = value; }
        }  

        public string Istruttore
        {
            get { return _istruttore; }
            set { _istruttore = value; }
        }

        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        // metodo astratto
        public abstract string mostraScheda();               
    }
}
