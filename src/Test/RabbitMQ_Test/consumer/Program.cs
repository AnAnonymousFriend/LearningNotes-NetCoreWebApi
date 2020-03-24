using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.1.实例化连接工厂
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            //创建通道
            var channel = connection.CreateModel();

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

                //接收到消息事件
            consumer.Received += (ch, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);

                Console.WriteLine($"收到消息： {message}");

                Console.WriteLine($"收到该消息[{ea.DeliveryTag}] 延迟10s发送回执");
                Thread.Sleep(10000);
                //确认该消息已被消费
                channel.BasicAck(ea.DeliveryTag, false);
                Console.WriteLine($"已发送回执[{ea.DeliveryTag}]");
            };
            //启动消费者 设置为手动应答消息
            channel.BasicConsume("hello", false, consumer);
            Console.WriteLine("消费者已启动");
            Console.ReadKey();
            channel.Dispose();
            connection.Close();

        }
    }
}
