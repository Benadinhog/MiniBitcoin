using System;
using System.Globalization;
using MiniBitcoin.Console.Models;

namespace MiniBitcoin.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            
            Wallet wallet = new Wallet(10000.00m);
            decimal currentBtcPrice = 350000.00m; 

            bool running = true;
            while (running)
            {
                System.Console.Clear();
                System.Console.WriteLine("========================================");
                System.Console.WriteLine("          MINI BITCOIN WALLET           ");
                System.Console.WriteLine("========================================");
                System.Console.WriteLine($"Cotação Atual BTC: R$ {currentBtcPrice:N2}");
                System.Console.WriteLine($"Saldo BRL:         R$ {wallet.BalanceBrl:N2}");
                System.Console.WriteLine($"Saldo BTC:         {wallet.BalanceBtc:F8} BTC");
                System.Console.WriteLine($"Patrimônio Total:  R$ {wallet.GetTotalPortfolioValue(currentBtcPrice):N2}");
                System.Console.WriteLine("----------------------------------------");
                System.Console.WriteLine("1. Atualizar Cotação do Bitcoin");
                System.Console.WriteLine("2. Comprar Bitcoin (usando BRL)");
                System.Console.WriteLine("3. Vender Bitcoin (receber BRL)");
                System.Console.WriteLine("4. Exibir Extrato de Transações");
                System.Console.WriteLine("5. Sair");
                System.Console.WriteLine("========================================");
                System.Console.Write("Escolha uma opção: ");

                string option = System.Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            System.Console.Write("Digite a nova cotação do BTC em BRL: ");
                            if (decimal.TryParse(System.Console.ReadLine(), out decimal newPrice) && newPrice > 0)
                            {
                                currentBtcPrice = newPrice;
                                System.Console.WriteLine("Cotação atualizada com sucesso!");
                            }
                            else
                            {
                                System.Console.WriteLine("Preço inválido.");
                            }
                            break;

                        case "2":
                            System.Console.Write("Digite o valor em BRL que deseja investir: ");
                            if (decimal.TryParse(System.Console.ReadLine(), out decimal brlToSpend))
                            {
                                wallet.BuyBitcoin(brlToSpend, currentBtcPrice);
                                System.Console.WriteLine("Compra realizada com sucesso!");
                            }
                            else
                            {
                                System.Console.WriteLine("Valor inválido.");
                            }
                            break;

                        case "3":
                            System.Console.Write("Digite a quantidade de BTC que deseja vender: ");
                            if (decimal.TryParse(System.Console.ReadLine(), out decimal btcToSell))
                            {
                                wallet.SellBitcoin(btcToSell, currentBtcPrice);
                                System.Console.WriteLine("Venda realizada com sucesso!");
                            }
                            else
                            {
                                System.Console.WriteLine("Quantidade inválida.");
                            }
                            break;

                        case "4":
                            System.Console.Clear();
                            System.Console.WriteLine("=== EXTRATO DE TRANSAÇÕES ===");
                            if (wallet.Transactions.Count == 0)
                            {
                                System.Console.WriteLine("Nenhuma transação realizada ainda.");
                            }
                            else
                            {
                                foreach (var tx in wallet.Transactions)
                                {
                                    string typeStr = tx.Type == TransactionType.Buy ? "COMPRA" : "VENDA";
                                    System.Console.WriteLine($"[{tx.Timestamp:dd/MM/yyyy HH:mm:ss}] {typeStr}: {tx.AmountBtc:F8} BTC por R$ {tx.AmountBrl:N2} (Preço unitário: R$ {tx.PriceAtTransaction:N2})");
                                }
                            }
                            break;

                        case "5":
                            running = false;
                            System.Console.WriteLine("Saindo... Até logo!");
                            continue;

                        default:
                            System.Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"Erro: {ex.Message}");
                    System.Console.ResetColor();
                }

                System.Console.WriteLine("\nPressione qualquer tecla para continuar...");
                System.Console.ReadKey();
            }
        }
    }
}