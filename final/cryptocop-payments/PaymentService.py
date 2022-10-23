from multiprocessing import connection
import json, pika
from luhn_validator import validate

host = "localhost"
exchange = "order-exchange"
queue = "payment-queue"
routing_key = "create-order"

def setup():
    connection = pika.BlockingConnection(pika.ConnectionParameters(host))
    channel = connection.channel()
    channel.exchange_declare(exchange=exchange, exchange_type='topic', durable=True)
    channel.queue_declare(queue, durable=False)
    channel.queue_bind(exchange=exchange, queue=queue, routing_key=routing_key)
    return channel, connection

channel, connection = setup()

def output(ch, method, properties, data):
    load_data: str = json.loads(data)
    card_number = load_data["creditCard"]
    result = validate(card_number)

    if result:
        print("Payment is successful, correct card number")
    else:
        print("Payment is not successful, incorrect card number")


channel.basic_consume(
    queue=queue,
    auto_ack=True,
    on_message_callback=output)

channel.start_consuming()
connection.close()