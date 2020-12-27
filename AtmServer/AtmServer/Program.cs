using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AtmServer
{
    class Program
    {
        static Dictionary<string, string> cards = new Dictionary<string, string>(4);
        static void Main()
        {
            Card[] cards = new Card[4] {
                new Card("4444_4444_4444_4444", "4444", 10000),
                new Card("3333_3333_3333_3333", "3333", 5000),
                new Card("2222_2222_2222_2222", "2222", 20000),
                new Card("1111_1111_1111_1111", "1111", 2000)
            };

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Any, 904));

            while (true)
            {
                server.Listen(5);
                Socket client = server.Accept();

                string command = ReadFromSocket(client);
                string[] commandSplit = command.Split(' ');

                Card card = new Card();

                if(FindCardWithNumber(cards, commandSplit[2], ref card))
                {
                    if(card.Pin == commandSplit[1])
                    {
                        int commandCode = int.Parse(commandSplit[0]);
                        int money = 0;

                        switch (commandCode)
                        {
                            case 0:
                                SendAnswerToClientAndFinishConnection(client, "1");
                                break;
                            case 1:
                                SendAnswerToClientAndFinishConnection(client, $"1 {card.Money}");
                                break;
                            case 2:
                                money = int.Parse(commandSplit[3]);
                                if (CheckIsEnoughMoney(card, money))
                                {
                                    card.Money -= int.Parse(commandSplit[3]);
                                    SendAnswerToClientAndFinishConnection(client, $"1");
                                }
                                else
                                {
                                    SendAnswerToClientAndFinishConnection(client, $"-1");
                                }
                                break;
                            case 3:
                                money = int.Parse(commandSplit[3]);
                                if (CheckIsEnoughMoney(card, money))
                                {
                                    Card card1 = new Card();
                                    if (FindCardWithNumber(cards, commandSplit[4], ref card1))
                                    {
                                        card1.Money += money;
                                    }
                                    else
                                    {
                                        SendAnswerToClientAndFinishConnection(client, $"-1");
                                        return;
                                    }
                                    card.Money -= money;
                                    SendAnswerToClientAndFinishConnection(client, $"1");
                                }
                                else
                                {
                                    SendAnswerToClientAndFinishConnection(client, $"-1");
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        SendAnswerToClientAndFinishConnection(client, "0");
                    }   
                }
                else
                {
                    SendAnswerToClientAndFinishConnection(client, "0");
                }
            }


        }
        private static string ReadFromSocket(Socket socket)
        {
            byte[] buffer;
            buffer = new byte[1024];
            socket.Receive(buffer);
            return Encoding.UTF8.GetString(buffer);
        }
        private static void SendAnswerToClientAndFinishConnection(Socket client, string command)
        {
            WriteToSocket(client, command);
            client.Close();
        }
        private static void WriteToSocket(Socket socket, string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            socket.Send(buffer);
        }
        private static bool FindCardWithNumber(Card[] cards, string number, ref Card card)
        {
            for(int i = 0; i < cards.Length; i++)
            {
                if(cards[i].Number == number)
                {
                    card = cards[i];
                    return true;
                }
            }
            return false;
        }
        private static bool CheckIsEnoughMoney(Card card, int money)
        {
            int moneyDec = money;
            if (card.Money >= moneyDec)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    class Card
    {
        public string Pin { get; }
        public string Number { get; }
        public int Money { get; set; }

        public Card(string number,  string pin,  int money)
        {
            Pin = pin;
            Number = number;
            Money = money;
        }
        public Card()
        {

        }
    }
}
