using BookWarehouse.DTO.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWarehouse.Service.MassTransit
{
    public class SendObjConsumer : IConsumer<Book>
    {
        private readonly ILogger<SendObjConsumer> _logger;

        public SendObjConsumer(ILogger<SendObjConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Book> context)
        {
            _logger.LogInformation($"Receive book data: {context.Message.Name}");
            return Task.CompletedTask;
        }
    }
}
