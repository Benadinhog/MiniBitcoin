using System;

namespace MiniBitcoin.Console.Models
{
    public enum TransactionType { Buy, Sell }

    public class Transaction
    {
        public DateTime Timestamp { get; }
        public TransactionType Type { get; }
        public decimal AmountBrl { get; }
        public decimal AmountBtc { get; }
        public decimal PriceAtTransaction { get; }

        public Transaction(TransactionType type, decimal amountBrl, decimal amountBtc, decimal priceAtTransaction)
        {
            Timestamp = DateTime.UtcNow;
            Type = type;
            AmountBrl = amountBrl;
            AmountBtc = amountBtc;
            PriceAtTransaction = priceAtTransaction;
        }
    }
}