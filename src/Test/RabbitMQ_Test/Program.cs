using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ_Test
{
    class Program
    {
        // 定义生产者
        static void Main(string[] args)
        {
            // 连接RabbitMQ
            //ConnectionFactory factory = new ConnectionFactory
            //{
            //    UserName = "admin",
            //    Password = "admin",
            //    HostName = "http://127.0.0.1:15672/"
            //};

            //// 创建连接


            var factory = new ConnectionFactory()
            {
                HostName = "192.168.1.121",
                Port = 15672,
                UserName = "admin",
                Password = "admin"
            };
            IConnection conn = factory.CreateConnection();

            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            //声明一个队列
            channel.QueueDeclare("hello", false, false, false, null);

            Console.WriteLine("\nRabbitMQ连接成功，请输入消息，输入exit退出！");

            string input;
            do
            {
                input = Console.ReadLine();

                var sendBytes = Encoding.UTF8.GetBytes(input);
                //发布消息
                channel.BasicPublish("", "hello", null, sendBytes);

            } while (input.Trim().ToLower() != "exit");
            channel.Close();
            connection.Close();

        }
    }
}
