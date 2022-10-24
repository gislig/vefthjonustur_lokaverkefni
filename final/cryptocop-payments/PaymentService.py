from multiprocessing import connection
import json, pika
from luhn_validator import validate
from dotenv import load_dotenv
import os
load_dotenv()
host = os.getenv('HOST') or 'rabbitmq'
exchange = os.getenv('EXCHANGE') or 'order-exchange'
queue = os.getenv('QUEUE') or 'payment-queue'
routing_key = os.getenv('ROUTING_KEY') or 'create-order'


connection = pika.BlockingConnection(pika.ConnectionParameters(host=host))
channel = connection.channel()
channel.exchange_declare(exchange=exchange, exchange_type='topic', durable=True)
channel.queue_declare(queue=queue)
channel.queue_bind(exchange=exchange, queue=queue, routing_key=routing_key)

print("[*] Waiting for messages. To exit press CTRL+C")

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