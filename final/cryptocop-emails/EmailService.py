import pika, requests, json
from pyparsing import empty
from luhn_validator import validate

from dotenv import load_dotenv
import os
load_dotenv()
host = os.getenv('HOST') or 'rabbitmq'
exchange = os.getenv('EXCHANGE') or 'order-exchange'
queue = os.getenv('QUEUE') or 'email-queue'
routing_key = os.getenv('ROUTING_KEY') or 'create-order'


mailgun_sandbox = "https://api.mailgun.net/v3/sandbox2fa604222f7c4ddea98f4269f0e435e3.mailgun.org/messages"
mailgun_api = "5020f0d0f059324afa7211d149494db3-d117dd33-a913e1a2"
mailgun_from = "Gisli <postmaster@sandbox2fa604222f7c4ddea98f4269f0e435e3.mailgun.org>"
email_to = "gisligudm18@ru.is"
subject = "Your order has been created"

def setup():
    connection = pika.BlockingConnection(pika.ConnectionParameters(host))
    channel = connection.channel()
    channel.exchange_declare(exchange=exchange, exchange_type='topic', durable=True)
    channel.queue_declare(queue, durable=False)
    channel.queue_bind(exchange=exchange, queue=queue, routing_key=routing_key)
    return channel, connection

def send_email(ch, method, properties, data):
    body = json.loads(data)
    
    items: str = '<h2>Products</h2>'
    for x in body['orderItems']:
        items += 'Product: {}, Quantity: {}, Unit Price: {},Total price {}'.format(x['productIdentifier'], x['quantity'], x['unitPrice'], x['totalPrice'])
    html_body = 'Thank you {} for ordering at CryptoCop{} {}, {}, {}The total is {}$ {}'.format(body['fullName'],body['streetName'],body['houseNumber'],body['zipCode'],body['country'],body['totalPrice'], items)
    print(html_body)
    
    return requests.post(
        mailgun_sandbox,
        auth=("api", mailgun_api),
        data={"from": mailgun_from,
              "to": email_to,
              "subject": subject,
              "text": html_body})

print("[*] Waiting for messages. To exit press CTRL+C")
channel, connection = setup()

channel.basic_consume(
    queue=queue,
    auto_ack=True,
    on_message_callback=send_email)

channel.start_consuming()
connection.close()