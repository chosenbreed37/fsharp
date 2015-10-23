#r @"C:\Users\Eric\.dnx\packages\RabbitMQ.Client\3.5.6\lib\net40\RabbitMQ.Client.dll"

open RabbitMQ.Client
open RabbitMQ.Client.Events

let createConnection =
    let factory = new ConnectionFactory()
    factory.UserName <- "guest"
    factory.Password <- "guest"
    factory.VirtualHost <- @"/"
    factory.HostName <- "localhost"
    factory.CreateConnection()

let openChannel (connection: IConnection) =
    connection.CreateModel()

let createExchange (channel: IModel) =
    channel.ExchangeDeclare("dx", ExchangeType.Direct)
    channel

let createQueue (channel: IModel) =
    channel.QueueDeclare("qx", false, false, false, null) |> ignore
    channel

let bindQueue queue exchange routingKey (channel: IModel) =
    channel.QueueBind(queue, exchange, routingKey)
    channel

let publish exchange routingKey (message: string) (channel: IModel) =
   let bytes = System.Text.Encoding.UTF8.GetBytes(message)
   channel.BasicPublish(exchange, routingKey, null, bytes)
   channel

let getHandler (channel: IModel) =
    let consumer = new EventingBasicConsumer(channel)
    consumer.Received.Add(
        fun (e: BasicDeliverEventArgs) -> 
            printfn "%A" (System.Text.Encoding.UTF8.GetChars(e.Body))
            channel.BasicAck(e.DeliveryTag, false)
    )
    consumer :> IBasicConsumer

let subscribe queue (handler: IModel -> IBasicConsumer) (channel: IModel) =
    channel.BasicConsume(queue, false, handler channel)

let publication =
    createConnection 
    |> openChannel 
    |> createExchange 
    |> createQueue 
    |> bindQueue "qx" "dx" ""
    |> publish "dx" "" "hello, world"

let subscription =
    createConnection
    |> openChannel
    |> subscribe "qx" getHandler
