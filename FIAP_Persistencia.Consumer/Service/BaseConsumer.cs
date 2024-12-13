using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Consumer.Service
{
    public abstract class BaseConsumer<T>
    {
        private readonly string _queueName;
        private readonly IServiceProvider _serviceProvider;

        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;

        protected BaseConsumer(string queueName, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var rabbitConfig = configuration.GetSection("RabbitMQ");
            _queueName = queueName;
            _hostName = rabbitConfig["HostName"];
            _userName = rabbitConfig["UserName"];
            _password = rabbitConfig["Password"];
            _serviceProvider = serviceProvider;
        }

        public async void Start()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName, // Nome do contêiner RabbitMQ
                UserName = _userName,  // Usuário padrão do RabbitMQ
                Password = _password   // Senha padrão do RabbitMQ
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var mensagemJson = Encoding.UTF8.GetString(body);

                Console.WriteLine($"[{_queueName}] Mensagem recebida: {mensagemJson}");

                var mensagem = JsonSerializer.Deserialize<T>(mensagemJson);

                using var scope = _serviceProvider.CreateScope();
                await ProcessMessage(mensagem, scope);
            };

            await channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer);

            Console.WriteLine($"[{_queueName}] Consumidor aguardando mensagens na fila...");
            Console.ReadLine();
        }

        // Método abstrato para ser implementado por cada Consumer específico
        protected abstract Task ProcessMessage(T mensagem, IServiceScope scope);
    }
}
