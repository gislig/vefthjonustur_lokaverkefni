import pika
import requests
import json
from dotenv import load_dotenv
import os
load_dotenv()
host = os.getenv('HOST')
exchange = os.getenv('EXCHANGE')
queue = os.getenv('QUEUE')
routing_key = os.getenv('ROUTING_KEY')

print('Waiting for order....')
connection = pika.BlockingConnection(pika.ConnectionParameters(host))
channel = connection.channel()

channel.exchange_declare(exchange=exchange, exchange_type='topic', durable=True)
channel.queue_declare(queue, durable=False)
channel.queue_bind(exchange=exchange, queue=queue, routing_key=routing_key)

def send_email(ch, method, properties, data):
    body = json.loads(data)
    items: str = '<h2>Products</h2>'
    for x in body['OrderItems']:
        items += '<h3>Product: {}, Quantity: {}, Unit Price: {}, Total price {}'.format(x['ProductIdentifier'], x['Quantity'], x['UnitPrice'], x['TotalPrice'])
    html_body = '<h1>Thank you {} for ordering at CryptoCop</h1><h2>{} {}, {}, {}</h2><h1>The total is {}$</h1><h2></h2> {}'.format(body['FullName'],body['StreetName'],body['HouseNumber'],body['ZipCode'],body['Country'],body['TotalPrice'], items) #TODO set orderItems
    print(html_body)
    
    
    to = "sindrismai@gmail.com"
    html_text = ""
    return requests.post(
        "https://api.mailgun.net/v3/sandboxbdc7d731519049848df07dfd89f9a938.mailgun.org",
        auth=("api", "fdf57c09b0b32fd604613389e770dd3e-b0ed5083-e10e5d78"),
        data={"from": "Sindri <postmaster@sandboxbdc7d731519049848df07dfd89f9a938.mailgun.org>",
              "to": [to, "Receiver"],
              "subject": "Hello",
              "text": html_text})


channel.basic_consume(
    queue=queue,
    auto_ack=True,
    on_message_callback=send_email)

channel.start_consuming()
connection.close()

# i = json.dumps(x)

# body = json.loads(i)

# try: 
#     html_body = '<h1>Thank you {} for ordering at CryptoCop</h1><h2>{} {}, {}, {}</h2><h1>The total is {}$</h1><h2></h2>'.format(body['FullName'],body['StreetName'],body['HouseNumber'],body['ZipCode'],body['Country'],body['TotalPrice']) #TODO set orderItems
#     name = body['FullName']     
#     address = body['StreetName'], ' ' , body['HouseNumber']
#     zip_code = body['ZipCode']
#     country = body['Country']
#     date = body['OrderDate']
#     total_price = body['TotalPrice']
#     order_items = body['OrderItems']

# except:
#     print("failed")