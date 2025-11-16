using GestionBanque.Models;

namespace GestionBanque.Tests
{
    public class CompteTests
    {
        [Fact]
        public void Deposer_MontantPositif_AugmenteBalance()
        {
            // Arrange
            var compte = new Compte(1, "1234", 100.0, 1);

            // Act
            compte.Deposer(50.0);

            // Assert
            Assert.Equal(150.0, compte.Balance);
        }

        [Fact]
        public void Deposer_MontantNegatif_LanceException()
        {
            var compte = new Compte(1, "1234", 100.0, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => compte.Deposer(-10.0));
        }

        [Fact]
        public void Retirer_MontantValide_DiminueBalance()
        {
            var compte = new Compte(1, "1234", 100.0, 1);

            compte.Retirer(40.0);

            Assert.Equal(60.0, compte.Balance);
        }

        [Fact]
        public void Retirer_MontantPlusGrandQueBalance_LanceException()
        {
            var compte = new Compte(1, "1234", 100.0, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => compte.Retirer(150.0));
        }

        [Fact]
        public void Retirer_MontantNegatif_LanceException()
        {
            var compte = new Compte(1, "1234", 100.0, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => compte.Retirer(-5.0));
        }
    }
}
