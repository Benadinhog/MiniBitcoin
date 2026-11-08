using System;
using System.Collections.Generic;

namespace MiniBitcoin.Console.Models
{
    public class Wallet
    {
        public decimal BalanceBrl { get; private set; }
        public decimal BalanceBtc { get; private set; }
        private readonly List<Transaction> _transactions;
        public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        public Wallet(decimal initialBrl)
        {
            if (initialBrl < 0)
                throw new ArgumentException("O saldo inicial em BRL não pode ser negativo.");

            BalanceBrl = initialBrl;
            BalanceBtc = 0;
            _transactions = new List<Transaction>();
        }

        public void BuyBitcoin(decimal amountBrl, decimal currentBtcPrice)
        {
            if (amountBrl <= 0)
                throw new ArgumentException("O valor de compra deve ser maior que zero.");
            if (currentBtcPrice <= 0)
                throw new ArgumentException("A cotação do Bitcoin deve ser maior que zero.");
            if (BalanceBrl < amountBrl)
                throw new InvalidOperationException("Saldo insuficiente em BRL.");

            decimal btcAcquired = amountBrl / currentBtcPrice;
            BalanceBrl -= amountBrl;
            BalanceBtc += btcAcquired;

            _transactions.Add(new Transaction(TransactionType.Buy, amountBrl, btcAcquired, currentBtcPrice));
        }

        public void SellBitcoin(decimal amountBtc, decimal currentBtcPrice)
        {
            if (amountBtc <= 0)
                throw new ArgumentException("A quantidade de BTC para venda deve ser maior que zero.");
            if (currentBtcPrice <= 0)
                throw new ArgumentException("A cotação do Bitcoin deve ser maior que zero.");
            if (BalanceBtc < amountBtc)
                throw new InvalidOperationException("Saldo insuficiente em Bitcoin.");

            decimal brlReceived = amountBtc * currentBtcPrice;
            BalanceBtc -= amountBtc;
            BalanceBrl += brlReceived;

            _transactions.Add(new Transaction(TransactionType.Sell, brlReceived, amountBtc, currentBtcPrice));
        }

        public decimal GetTotalPortfolioValue(decimal currentBtcPrice)
        {
            if (currentBtcPrice < 0)
                throw new ArgumentException("A cotação do Bitcoin não pode ser negativa.");

            return BalanceBrl + (BalanceBtc * currentBtcPrice);
        }
    }
}