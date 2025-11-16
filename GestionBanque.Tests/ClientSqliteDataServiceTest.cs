using GestionBanque.Models;
using GestionBanque.Models.DataService;

namespace GestionBanque.Tests
{
    [Collection("Dataservice")]
    public class ClientSqliteDataServiceTest
    {
        private const string CheminBd = "test.bd";

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Get_ShouldBeValid()
        {
            // Préparation
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            Client clientAttendu = new Client(1, "Amar", "Quentin", "quentin@gmail.com");
            clientAttendu.Comptes.Add(new Compte(1, "9864", 831.76, 1));
            clientAttendu.Comptes.Add(new Compte(2, "2370", 493.04, 1));

            // Exécution
            Client? clientActuel = ds.Get(1);

            // Affirmation
            Assert.Equal(clientAttendu, clientActuel);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void GetAll_ShouldReturnAllClientsWithAccounts()
        {
            // Préparation
            var ds = new ClientSqliteDataService(CheminBd);

            // Exécution
            var clients = ds.GetAll().ToList();

            // Affirmation
            Assert.Equal(3, clients.Count);

            var client1 = clients.Single(c => c.Id == 1);

            var clientAttendu = new Client(1, "Amar", "Quentin", "quentin@gmail.com");
            clientAttendu.Comptes.Add(new Compte(1, "9864", 831.76, 1));
            clientAttendu.Comptes.Add(new Compte(2, "2370", 493.04, 1));

            Assert.Equal(clientAttendu, client1);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void RecupererComptes_ShouldLoadAccountsForClient()
        {
            // Préparation
            var ds = new ClientSqliteDataService(CheminBd);
            var client = new Client(1, "Amar", "Quentin", "quentin@gmail.com");

            // Exécution
            ds.RecupererComptes(client);

            // Affirmation
            Assert.Equal(2, client.Comptes.Count);
            Assert.Contains(client.Comptes, c => c.NoCompte == "9864" && c.Balance == 831.76);
            Assert.Contains(client.Comptes, c => c.NoCompte == "2370" && c.Balance == 493.04);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Update_ShouldUpdateClientInDatabase()
        {
            // Préparation
            var ds = new ClientSqliteDataService(CheminBd);
            var client = ds.Get(1)!;

            string nouveauNom = "NouveauNom";
            string nouveauPrenom = "NouveauPrenom";
            string nouveauCourriel = "nouveau@test.com";

            client.Nom = nouveauNom;
            client.Prenom = nouveauPrenom;
            client.Courriel = nouveauCourriel;

            // Exécution
            bool resultat = ds.Update(client);

            // Affirmation
            Assert.True(resultat);

            var clientModifie = ds.Get(1)!;
            Assert.Equal(nouveauNom, clientModifie.Nom);
            Assert.Equal(nouveauPrenom, clientModifie.Prenom);
            Assert.Equal(nouveauCourriel, clientModifie.Courriel);
        }
    }
}
