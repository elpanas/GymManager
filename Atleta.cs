using System;

namespace GymManager
{
    class Atleta
    {
        // attributi
        private int _id = 0;
        private string _nome;
        private string _cognome;
        private int _eta;

        // costruttore
        public Atleta()
        {
            // niente
        }

        // proprietà
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Cognome
        {
            get { return _cognome; }
            set { _cognome = value; }
        }

        public int Eta
        {
            get { return _eta; }
            set { _eta = value; }
        }

        // metodi
        public string mostraAtleta()
        {
            string result;

            result = Id + ".\t" +
                     Nome + "\t" +
                     Cognome + "\t" +
                     Eta + Environment.NewLine;          

            return result;
        }
    }
}
