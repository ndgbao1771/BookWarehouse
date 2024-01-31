﻿namespace BookWarehouse.Service.RabbitMQ
{
    public interface IRabbitMQBook
    {
        public void SendBookMessage<T>(T message);
    }
}