# 🔔 Notifications API - Fase 3 (MVP AWS)

## 📌 Visão Geral

A **Notifications API** é um microsserviço responsável pelo envio e gerenciamento de notificações dentro do ecossistema da Fase 3.

Construída sob uma arquitetura **serverless e orientada a eventos na AWS**, esta API permite a comunicação assíncrona entre serviços, garantindo que usuários sejam notificados sobre eventos relevantes como transações, atualizações e ações do sistema.

---

## 🎯 Objetivo

* Centralizar o envio de notificações do sistema
* Desacoplar comunicação entre microsserviços
* Suportar múltiplos canais de notificação (extensível)
* Garantir escalabilidade e resiliência

---

## 🏗️ Arquitetura

A aplicação segue **Arquitetura Hexagonal (Ports & Adapters)** combinada com **event-driven architecture**, ideal para sistemas distribuídos.

### 🔹 Camadas

* **Domain**

  * Entidades de notificação
  * Regras de negócio
  * Contratos (interfaces)

* **Application**

  * Casos de uso (SendNotification, ProcessEvent, etc.)
  * Orquestração de eventos

* **Infrastructure**

  * Integração com serviços AWS (mensageria, envio)
  * Implementações concretas

* **API / EntryPoint**

  * AWS Lambda handlers
  * Recebimento de eventos ou requisições HTTP

---

## ☁️ Infraestrutura AWS

A Notifications API utiliza serviços gerenciados para processamento e entrega de mensagens:

* **AWS Lambda**

  * Processamento das notificações

* **Amazon API Gateway**

  * Entrada HTTP (quando aplicável)

* **Amazon SNS / SQS** *(quando aplicável)*

  * Comunicação assíncrona entre serviços

* **AWS CloudWatch**

  * Logs e monitoramento

* **AWS IAM**

  * Controle de permissões

💡 A AWS oferece serviços nativos para gerenciamento de notificações com suporte a múltiplos canais como e-mail, chat e push, permitindo distribuição eficiente de eventos do sistema ([Amazon Web Services, Inc.][1])

---

## 🔗 Funcionalidades

* 🔔 Envio de notificações
* 📩 Processamento de eventos
* 📡 Integração com outros microsserviços
* 📊 Possível rastreamento de status (entregue, falha, pendente)
* 📬 Suporte a múltiplos canais (extensível)

---

## 🔄 Fluxo de Funcionamento

1. Um evento ocorre (ex: pagamento aprovado)
2. O serviço de origem publica o evento
3. A Notifications API consome esse evento
4. A notificação é processada
5. O usuário recebe a mensagem (email, push, etc.)

➡️ Esse modelo reduz acoplamento e melhora escalabilidade do sistema distribuído

---

## 🔐 Segurança

* Validação de eventos recebidos
* Controle via IAM
* Isolamento entre serviços
* Possível uso de filas para garantir entrega (retry)

---

## 🚀 Stack Tecnológica

* **.NET 8**
* **C#**
* **AWS Lambda**
* **API Gateway**
* **SNS / SQS**
* **CloudWatch**
* **xUnit + Moq**

---

## ⚙️ Execução do Projeto

### 🔧 Pré-requisitos

* .NET 8 SDK
* AWS CLI configurado
* Conta AWS ativa
* Amazon Lambda Tools

---

### ▶️ Execução local

```bash
dotnet restore
dotnet build
dotnet run
```

---

### ☁️ Deploy na AWS

```bash
dotnet lambda deploy-serverless
```

Ou via infraestrutura como código:

```bash
terraform init
terraform apply
```

---

## 📦 Estrutura do Projeto

```bash
src/
 ├── Domain/
 ├── Application/
 ├── Infrastructure/
 ├── API/
 └── Shared/
```

---

## 🔄 Integração com o Ecossistema

A Notifications API consome eventos de:

* 👤 Users API → criação/atualização de usuário
* 💳 Payments API → status de pagamento
* 🎮 Games API → ações relacionadas a jogos
