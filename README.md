# RabbitMqApp
This project create to .net 6 c# console application.\
The main reason to create this repository its a tutorial about to rabbitmq.\

## Create docker compose file
```
version: '3.9'
services:
  s_rabbitmq:
    container_name: c_rabbitmq
    image: rabbitmq:3-management
    ports:
      - 5672:5672
      - 15672:15672
    volumes:
      - ./data:/var/lib/rabbitmq
      - ./log:/var/log/rabbitmq
```
## Docker compose up
```
docker-compose up
```
## Restore and Run each product 
```
dotnet restore
dotnet run
```
