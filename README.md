# poc-kafka-avro
A simple project with apache kafka using schema registry and Kafka Drop UI

<details>
  <summary>Kafka Drop Image</summary>

  #### Kafka Drop Running at the project
  
  Open a browser and navigate to [http://localhost:9000](http://localhost:9000).

  ![image](https://user-images.githubusercontent.com/8093695/210865873-69db227d-3cd1-481c-b9ef-8379cec55a59.png)
  
</details>

#### Requirements
---
* Java 11 or newer
* Kafka (version 0.11.0 or newer) or Azure Event Hubs

Optional, additional integration:

* Schema Registry

#### Getting Started
---
You can run the Kafka Drop via Docker.

##### Running with Docker

```yml
version: '3'
services:
  zookeeper:
    image: confluentinc/cp-zookeeper:latest
    container_name: zookeeper
    hostname: zookeeper
    networks:
      - broker-kafka
    environment:
      ZOOKEEPER_CLIENT_PORT: 2181
      ZOOKEEPER_TICK_TIME: 2000

  kafka:
    image: confluentinc/cp-kafka:latest
    hostname: kafka
    container_name: kafka
    networks:
      - broker-kafka
    depends_on:
      - zookeeper
    ports:
      - 9092:9092
    environment:
      KAFKA_BROKER_ID: 1
      KAFKA_ZOOKEEPER_CONNECT: zookeeper:2181
      KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://kafka:29092,PLAINTEXT_HOST://localhost:9092
      KAFKA_LISTENER_SECURITY_PROTOCOL_MAP: PLAINTEXT:PLAINTEXT,PLAINTEXT_HOST:PLAINTEXT
      KAFKA_INTER_BROKER_LISTENER_NAME: PLAINTEXT
      KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR: 1
      KAFKA_CONFLUENT_SCHEMA_REGISTRY_URL: http://schema-registry:8081
      CONFLUENT_METRICS_REPORTER_BOOTSTRAP_SERVERS: kafka:29092
      CONFLUENT_SUPPORT_CUSTOMER_ID: 'anonymous'

  schema-registry:
    image: confluentinc/cp-schema-registry:latest
    hostname: schema-registry
    container_name: schema-registry
    networks:
      - broker-kafka
    depends_on:
      - kafka
    ports:
      - "8081:8081"
    environment:
      SCHEMA_REGISTRY_HOST_NAME: schema-registry
      SCHEMA_REGISTRY_KAFKASTORE_BOOTSTRAP_SERVERS: 'kafka:29092'
      SCHEMA_REGISTRY_LISTENERS: http://0.0.0.0:8081

  kafdrop:
    image: obsidiandynamics/kafdrop:latest
    hostname: kafdrop
    container_name: kafdrop
    networks:
      - broker-kafka
    depends_on:
      - kafka
      - schema-registry
    ports:
      - 19000:9000
    environment:
      KAFKA_BROKERCONNECT: kafka:29092
      SCHEMAREGISTRY_CONNECT: http://schema-registry:8081

networks:
  broker-kafka:
    driver: bridge

```
