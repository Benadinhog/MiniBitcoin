using System;
using MiniBitcoin.Console.Models;
using Xunit;

namespace MiniBitcoin.Tests
{
    public class WalletTests
    {
        [Fact]
        public void Constructor_ShouldInitializeCorrectly_WhenBalancesAreValid()
        {
            var wallet = new Wallet(5000m);
            Assert.Equal(5000m, wallet.BalanceBrl);
            Assert.Equal(0m, wallet.BalanceBtc);
            Assert.Empty(wallet.Transactions);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenInitialBrlIsNegative()
        {
            Assert.Throws<ArgumentException>(() => new Wallet(-100m));
        }

        [Fact]
        public void BuyBitcoin_ShouldUpdateBalancesAndAddTransaction_WhenFundsAreAvailable()
        {
            var wallet = new Wallet(1000m);
            decimal btcPrice = 100000m;
            decimal amountToSpend = 500m;

            wallet.BuyBitcoin(amountToSpend, btcPrice);

            Assert.Equal(500m, wallet.BalanceBrl);
            Assert.Equal(0.005m, wallet.BalanceBtc);
            Assert.Single(wallet.Transactions);
        }

        [Fact]
        public void BuyBitcoin_ShouldThrowException_WhenBrlBalanceIsInsufficient()
        {
            var wallet = new Wallet(100m);
            decimal btcPrice = 100000m;

            Assert.Throws<InvalidOperationException>(() => wallet.BuyBitcoin(150m, btcPrice));
        }

        [Fact]
        public void SellBitcoin_ShouldUpdateBalancesAndAddTransaction_WhenBtcBalanceIsSufficient()
        {
            var wallet = new Wallet(1000m);
            decimal btcPrice = 100000m;
            wallet.BuyBitcoin(1000m, btcPrice);

            wallet.SellBitcoin(0.005m, btcPrice);

            Assert.Equal(500m, wallet.BalanceBrl);
            Assert.Equal(0.005m, wallet.BalanceBtc);
            Assert.Equal(2, wallet.Transactions.Count);
        }

        [Fact]
        public void SellBitcoin_ShouldThrowException_WhenBtcBalanceIsInsufficient()
        {
            var wallet = new Wallet(1000m);
            decimal btcPrice = 100000m;

            Assert.Throws<InvalidOperationException>(() => wallet.SellBitcoin(0.1m, btcPrice));
        }

        [Fact]
        public void GetTotalPortfolioValue_ShouldReturnCorrectConsolidatedValue()
        {
            var wallet = new Wallet(1000m);
            decimal btcPrice = 100000m;
            wallet.BuyBitcoin(500m, btcPrice);

            decimal updatedBtcPrice = 200000m;
            decimal totalValue = wallet.GetTotalPortfolioValue(updatedBtcPrice);

            Assert.Equal(1500m, totalValue);
        }
    }
}