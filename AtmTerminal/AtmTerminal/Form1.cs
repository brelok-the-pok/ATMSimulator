using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtmTerminal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string pin;
        private string cardNumber;
        private bool pinAccepted = false;
        private int falsePinCount = 0;

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if(cardBox.SelectedIndex > -1)
                {
                    cardBox.Enabled = false;
                    helpLabel.Visible = false;

                    cardNumber = cardBox.Text.Replace(' ', '_' );

                    if (GetPinFromUser())
                    {
                        SendPinToServer();
                    }
                }
                else
                {
                    checkBox1.Checked = false;
                    helpLabel.Visible = true;
                }
            }
            else
            {
                cardBox.Enabled = true;
                pin = "";
                cardNumber = "";
                pinAccepted = false;
                falsePinCount = 0;

                AccountButton.Enabled = false;
                GetMoneyButton.Enabled = false;
                TransferButton.Enabled = false;
            }
        }
        private bool GetPinFromUser()
        {
            string pin = Microsoft.VisualBasic.Interaction.InputBox("Введите ПИН");

            if(pin.Length != 4)
            {
                MessageBox.Show("Неверная длина ПИНа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                this.pin = pin;
                return true;
            }
        }

        private void SendPinToServer()
        {
            string command = MakeCommand(0, pin, cardNumber);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect("127.0.0.1", 904);

            WriteToSocket(server, command);

            if(ReadFromSocket(server) == "1")
            {
                MessageBox.Show("ПИН подтверждён", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pinAccepted = true;
                falsePinCount = 0;

                AccountButton.Enabled = true;
                GetMoneyButton.Enabled = true;
                TransferButton.Enabled = true;
            }
            else
            {
                falsePinCount++;

                if(falsePinCount == 3)
                {
                    MessageBox.Show($"Неверный ПИН\n Карта изымается", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                else
                {
                    checkBox1.Checked = false;
                    MessageBox.Show($"Неверный ПИН\nоОталось {3 - falsePinCount} попыток", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string ReadFromSocket(Socket socket)
        {
            byte[] buffer;
            buffer = new byte[1024];
            socket.Receive(buffer);
            string buff = Encoding.UTF8.GetString(buffer);

            string answer = "";
            for (int i = 0; buff[i] != '\0'; i++)
            {
                answer += buff[i];
            }

            return answer;
        }
        private void WriteToSocket(Socket socket, string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            socket.Send(buffer);
        }

        private string MakeCommand(int commandCode, string PIN, string cardNumber, string sum = "0", string seconCardNumber = "")
        {
            string command = $"{commandCode} {PIN} {cardNumber} ";
            if(commandCode > 1)
            {
                command += $"{sum} ";
            }
            if(commandCode > 2)
            {
                command += $"{seconCardNumber}";
            }
            return command + ' ';
        }

        private void AccountButton_Click(object sender, EventArgs e)
        {
            if (pinAccepted)
            {
                string command = MakeCommand(1, pin, cardNumber);

                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect("127.0.0.1", 904);

                WriteToSocket(server, command);
                string resault = ReadFromSocket(server);
                if (resault[0] == '1')
                {
                    MessageBox.Show($"Операция проведена успешно\nОстаток: {resault.Split()[1]}", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    falsePinCount++;

                    if (falsePinCount == 3)
                    {
                        MessageBox.Show($"Неверный ПИН\n Карта изымается", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show($"Неверный ПИН\nоОталось {3 - falsePinCount} попыток", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите карту и введите ПИН", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GetMoneyButton_Click(object sender, EventArgs e)
        {
            if (pinAccepted)
            {
                string money = Microsoft.VisualBasic.Interaction.InputBox("Введите сумму для вывода");

                if (pin.Length != 4)
                {
                    MessageBox.Show("Ошибка при вводе суммы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string command = MakeCommand(2, pin, cardNumber, money);

                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect("127.0.0.1", 904);

                WriteToSocket(server, command);
                string resault = ReadFromSocket(server);
                if (resault[0] == '1')
                {
                    MessageBox.Show($"Операция проведена успешно", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if(resault[0] == '0')
                {
                    falsePinCount++;

                    if (falsePinCount == 3)
                    {
                        MessageBox.Show($"Неверный ПИН\n Карта изымается", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show($"Неверный ПИН\nоОталось {3 - falsePinCount} попыток", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Недостаточно денег на счёте", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите карту и введите ПИН", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TransferButton_Click(object sender, EventArgs e)
        {
            if (pinAccepted)
            {
                string money = Microsoft.VisualBasic.Interaction.InputBox("Введите сумму для вывода");

                if (pin.Length != 4)
                {
                    MessageBox.Show("Ошибка при вводе суммы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Form2 form2 = new Form2(cardNumber.Replace('_', ' '));
                form2.ShowDialog();
                string card2 = form2.card2.Replace(' ', '_');
                form2.Close();

                string command = MakeCommand(3, pin, cardNumber, money, card2);

                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect("127.0.0.1", 904);

                WriteToSocket(server, command);
                string resault = ReadFromSocket(server);
                if (resault[0] == '1')
                {
                    MessageBox.Show($"Операция проведена успешно", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (resault[0] == '0')
                {
                    falsePinCount++;

                    if (falsePinCount == 3)
                    {
                        MessageBox.Show($"Неверный ПИН\n Карта изымается", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show($"Неверный ПИН\nоОталось {3 - falsePinCount} попыток", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Недостаточно денег на счёте", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите карту и введите ПИН", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cardBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cardBox.SelectedIndex == -1)
            {
                AccountButton.Enabled = false;
                GetMoneyButton.Enabled = false;
                TransferButton.Enabled = false;

                checkBox1.Enabled = false;
            }
            else
            {
                checkBox1.Enabled = true;
            }
        }
    }
}
