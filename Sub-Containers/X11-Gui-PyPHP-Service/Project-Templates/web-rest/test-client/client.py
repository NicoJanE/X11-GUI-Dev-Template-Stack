import requests
import json

# Define the API URL (assuming it's running locally on port 8080)
url = 'http://localhost:8071/web-rest/wr-api.php'

# Example of a GET request with a query parameter
response = requests.get(url, params={'name': 'John'})
print(f"GET response: {response.json()}")

# Example of a POST request with a JSON body
data = {'name': 'Alice'}
response = requests.post(url, json=data)
print(f"POST response: {response.json()}")
