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
            //1.1.实例化连接工厂
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            //创建通道
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
