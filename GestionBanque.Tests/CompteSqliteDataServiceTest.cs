using GestionBanque.Models;
using GestionBanque.Models.DataService;

namespace GestionBanque.Tests
{
    // Collection pour éviter l'accès concurrent à la même BD
    [Collection("Dataservice")]
    public class CompteSqliteDataServiceTest
    {
        private const string CheminBd = "test.bd";

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Get_ShouldBeValid()
        {
            // Préparation
            CompteSqliteDataService ds = new CompteSqliteDataService(CheminBd);
            Compte compteAttendu = new Compte(1, "9864", 831.76, 1);

            // Exécution
            Compte? compteActuel = ds.Get(1);

            // Affirmation
            Assert.Equal(compteAttendu, compteActuel);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Update_ShouldUpdateBalanceInDatabase()
        {
            // Préparation
            var ds = new CompteSqliteDataService(CheminBd);
            var compte = ds.Get(1)!;          // id = 1, balance initiale 831.76
            double nouvelleBalance = compte.Balance + 100.0;
            compte.Balance = nouvelleBalance;

            // Exécution
            bool resultat = ds.Update(compte);

            // Affirmation
            Assert.True(resultat);

            var compteModifie = ds.Get(1)!;
            Assert.Equal(nouvelleBalance, compteModifie.Balance);
        }
    }
}
