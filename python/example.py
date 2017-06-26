import hashlib
import hmac
import json

import requests

API_URL = 'https://api.changelly.com'
API_KEY = 'place_your_api_key_here'
API_SECRET = 'place_your_api_secret_here'

message = {
    'jsonrpc': '2.0',
    'id': 1,
    'method': 'getCurrencies',
    'params': []
}

serialized_data = json.dumps(message)

sign = hmac.new(API_SECRET.encode('utf-8'), serialized_data.encode('utf-8'), hashlib.sha512).hexdigest()

headers = {'api-key': API_KEY, 'sign': sign, 'Content-type': 'application/json'}
response = requests.post(API_URL, headers=headers, data=serialized_data)

print(response.json())

