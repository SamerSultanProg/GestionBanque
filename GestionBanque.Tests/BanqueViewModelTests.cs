using System.Collections.Generic;
using System.Linq;
using GestionBanque.Models;
using GestionBanque.Models.DataService;
using GestionBanque.ViewModels;
using GestionBanque.ViewModels.Interfaces;
using Moq;

namespace GestionBanque.Tests
{
    public class BanqueViewModelTests
    {
        private BanqueViewModel CreerVm(
            out Mock<IInteractionUtilisateur> interactionMock,
            out Mock<IDataService<Client>> dsClientsMock,
            out Mock<IDataService<Compte>> dsComptesMock,
            IList<Client>? clients = null)
        {
            interactionMock = new Mock<IInteractionUtilisateur>();
            dsClientsMock = new Mock<IDataService<Client>>();
            dsComptesMock = new Mock<IDataService<Compte>>();

            clients ??= new List<Client>
            {
                new Client(1, "Amar", "Quentin", "quentin@gmail.com")
            };

            dsClientsMock.Setup(ds => ds.GetAll()).Returns(clients);

            return new BanqueViewModel(
                interactionMock.Object,
                dsClientsMock.Object,
                dsComptesMock.Object);
        }

        [Fact]
        public void Constructeur_ChargeLesClients()
        {
            // Act
            var vm = CreerVm(
                out var interactionMock,
                out var dsClientsMock,
                out var dsComptesMock);

            // Assert
            dsClientsMock.Verify(ds => ds.GetAll(), Times.Once);
            Assert.NotEmpty(vm.Clients);
        }

        [Fact]
        public void ClientSelectionne_Set_MetAJourNomPrenomCourriel()
        {
            // Arrange
            var client = new Client(1, "Amar", "Quentin", "quentin@gmail.com");
            var vm = CreerVm(
                out var interactionMock,
                out var dsClientsMock,
                out var dsComptesMock,
                new List<Client> { client });

            // Act
            vm.ClientSelectionne = client;

            // Assert
            Assert.Equal(client.Nom, vm.Nom);
            Assert.Equal(client.Prenom, vm.Prenom);
            Assert.Equal(client.Courriel, vm.Courriel);
        }

        [Fact]
        public void Modifier_AvecCourrielInvalide_RestoreAnciennesValeursEtAfficheErreur()
        {
            // Arrange
            var client = new Client(1, "Amar", "Quentin", "quentin@gmail.com");
            var vm = CreerVm(
                out var interactionMock,
                out var dsClientsMock,
                out var dsComptesMock,
                new List<Client> { client });

            vm.ClientSelectionne = client;

            string vieuxNom = client.Nom;
            string vieuxPrenom = client.Prenom;
            string vieuxCourriel = client.Courriel;

            vm.Nom = "NomModifie";
            vm.Prenom = vieuxPrenom;
            vm.Courriel = "courrielInvalide"; // va faire planter le setter

            // Act
            vm.Modifier(null);

            // Assert
            Assert.Equal(vieuxNom, client.Nom);
            Assert.Equal(vieuxPrenom, client.Prenom);
            Assert.Equal(vieuxCourriel, client.Courriel);
            interactionMock.Verify(i => i.AfficherMessageErreur(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Deposer_Valide_AugmenteBalanceEtAppelleUpdate()
        {
            // Arrange
            var compte = new Compte(1, "1234", 100.0, 1);
            var client = new Client(1, "Amar", "Quentin", "quentin@gmail.com");
            client.Comptes.Add(compte);

            var vm = CreerVm(
                out var interactionMock,
                out var dsClientsMock,
                out var dsComptesMock,
                new List<Client> { client });

            vm.ClientSelectionne = client;
            vm.CompteSelectionne = compte;
            vm.MontantTransaction = 50.0;

            // Act
            vm.Deposer(null);

            // Assert
            dsComptesMock.Verify(ds => ds.Update(compte), Times.Once);
            Assert.Equal(150.0, compte.Balance);
            Assert.Equal(0.0, vm.MontantTransaction);
        }

        [Fact]
        public void Retirer_Valide_DiminueBalanceEtAppelleUpdate()
        {
            // Arrange
            var compte = new Compte(1, "1234", 100.0, 1);
            var client = new Client(1, "Amar", "Quentin", "quentin@gmail.com");
            client.Comptes.Add(compte);

            var vm = CreerVm(
                out var interactionMock,
                out var dsClientsMock,
                out var dsComptesMock,
                new List<Client> { client });

            vm.ClientSelectionne = client;
            vm.CompteSelectionne = compte;
            vm.MontantTransaction = 40.0;

            // Act
            vm.Retirer(null);

            // Assert
            dsComptesMock.Verify(ds => ds.Update(compte), Times.Once);
            Assert.Equal(60.0, compte.Balance);
            Assert.Equal(0.0, vm.MontantTransaction);
        }
    }
}
