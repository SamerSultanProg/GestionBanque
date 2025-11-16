using GestionBanque.Models;

namespace GestionBanque.Tests
{
    public class ClientTests
    {
        [Fact]
        public void Nom_AvecEspaces_EstTrimEtAffecte()
        {
            var client = new Client(1, "Amar", "Quentin", "test@example.com");

            client.Nom = "  NouveauNom  ";

            Assert.Equal("NouveauNom", client.Nom);
        }

        [Fact]
        public void Nom_Vide_LanceException()
        {
            var client = new Client(1, "Amar", "Quentin", "test@example.com");

            Assert.Throws<ArgumentException>(() => client.Nom = "   ");
        }

        [Fact]
        public void Prenom_AvecEspaces_EstTrimEtAffecte()
        {
            var client = new Client(1, "Amar", "Quentin", "test@example.com");

            client.Prenom = "  Sam  ";

            Assert.Equal("Sam", client.Prenom);
        }

        [Fact]
        public void Prenom_Vide_LanceException()
        {
            var client = new Client(1, "Amar", "Quentin", "test@example.com");

            Assert.Throws<ArgumentException>(() => client.Prenom = "");
        }

        [Fact]
        public void Courriel_Valide_EstTrimEtAffecte()
        {
            var client = new Client(1, "Amar", "Quentin", "test@example.com");

            client.Courriel = "  valide@test.com  ";

            Assert.Equal("valide@test.com", client.Courriel);
        }

        [Fact]
        public void Courriel_SansArobase_LanceException()
        {
            var client = new Client(1, "Amar", "Quentin", "test@example.com");

            Assert.Throws<ArgumentException>(() => client.Courriel = "pasunmail.com");
        }
    }
}
