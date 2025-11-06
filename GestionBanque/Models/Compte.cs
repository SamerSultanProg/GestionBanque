using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GestionBanque.Models
{
    public class Compte : INotifyPropertyChanged
    {
        private double _balance;

        public Compte(long id, string noCompte, double balanceDepart, long clientId) 
        {
            Id = id;
            NoCompte = noCompte;
            Balance = balanceDepart;
            ClientId = clientId;
        }

        public void Retirer(double montant)
        {
            if (montant > Balance)
            {
                throw new ArgumentOutOfRangeException($"Montant non valide pour le retrait : {montant}");
            }

            Balance -= montant;
        }

        public void Deposer(double montant)
        {
            if (montant < 0)
            {
                throw new ArgumentOutOfRangeException($"Montant non valide pour le dépôt : {montant}");
            }

            Balance -= montant;
        }

        public long Id { get; set; }
        public string NoCompte { get; set; }
        public double Balance 
        {
            get { return _balance; }
            set
            { 
                _balance = value;
                OnPropertyChanged();
            }
        }

        public long ClientId { get; set; }

        public override string ToString()
        {
            return $"#{NoCompte} : {Balance:C2}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Compte compte &&
                   _balance == compte._balance &&
                   Id == compte.Id &&
                   NoCompte == compte.NoCompte &&
                   ClientId == compte.ClientId;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
