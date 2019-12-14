namespace GymManager
{
    interface IScheda
    {
        void creaScheda(int id);
        void modificaScheda(int id);        
        void eliminaScheda(int id);
        void visualizzaScheda(int id);
        Scheda ritornaScheda(int id);
        Scheda inputDati();
        void popolaLista();
    }

    interface IAtleta
    {
        void aggiungiAtleta();
        void modificaAtleta(int id);
        void eliminaAtleta(int id);
        int mostraTutti();
        int mostraUno(int id);
        Atleta inputDati();
        int ultimoInserito(Atleta a);
        void popolaLista();
    }
}
