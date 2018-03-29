using System;

namespace SimpleBlockchainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Simple Blockchain now starting...");

            //1.矿工挖出
            var blockchain = new Blockchain();

            //发起一笔交易 user1将100转给user2  第二笔交易 user2将50再次转给user1  区块作为一个 记账本 记录着两笔交易的信息  点对点交易特点
            //2.进行一笔交易
            blockchain.CreateTransaction(new Transaction("user1", "user2", 100));
            //3.进行第二笔交易
            blockchain.CreateTransaction(new Transaction("user2", "user1", 50));

            //
            System.Console.WriteLine("Starting the miner...");

            //矿工继续挖矿
            blockchain.MinePendingTransactions("miner");

            System.Console.WriteLine($"Balance of the miner is {blockchain.GetBalanceOfAddress("miner")}");

            //
            System.Console.WriteLine("Starting the miner...again");

            //矿工继续挖矿
            blockchain.MinePendingTransactions("miner");

            //矿工获取余额
            System.Console.WriteLine($"Balance of the miner is {blockchain.GetBalanceOfAddress("miner")}");
        }
    }
}
