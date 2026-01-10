# 🚀 Full-Stack AWS ECS Deployment (React + .NET + PostgreSQL)

This repository contains a **production-ready full-stack web application** deployed on **AWS using ECS Fargate**, with **CI/CD via GitHub Actions**, **private networking**, and **path-based routing using an Application Load Balancer (ALB)**.

The project demonstrates **end-to-end DevOps and cloud architecture**, from VPC creation to automated deployments.


## 🧱 Tech Stack

### Frontend
- **React (Next.js)**
- Dockerized
- Served via **AWS Application Load Balancer**

### Backend
- **.NET 8 Web API**
- Entity Framework Core
- Dockerized
- Runs on **ECS Fargate**

### Database
- **PostgreSQL**
- Hosted on **Amazon RDS**
- Private subnet (not publicly accessible)

---

## ☁️ AWS Architecture Overview

### Networking (from scratch)
- **Custom VPC**
- **Public Subnets**
  - Application Load Balancer
  - NAT Gateway
- **Private Subnets**
  - ECS Fargate tasks
  - RDS PostgreSQL
- **Internet Gateway (IGW)**
  - Allows inbound internet traffic to ALB
- **NAT Gateway**
  - Allows private ECS tasks to pull images from ECR

---

## 🏗️ High-Level Architecture Flow

```text
┌──────────┐
│ Internet │
└────┬─────┘
     │
     ▼
┌──────────────────────────────┐
│ Application Load Balancer    │
│ (Public Subnets)             │
└────┬───────────────┬─────────┘
     │               │
     │ /             │ /api/*
     ▼               ▼
┌───────────────┐   ┌────────────────┐
│ React Frontend│   │ .NET API        │
│ (ECS Fargate) │   │ (ECS Fargate)   │
│ Private Subnet│   │ Private Subnet  │
└───────┬───────┘   └────────┬───────┘
        │                    │
        └──────────┬─────────┘
                   ▼
        ┌────────────────────────┐
        │ PostgreSQL (Amazon RDS) │
        │ Private Subnets         │
        └────────────────────────┘


---

## 🔀 Load Balancer Routing
```text
**Application Load Balancer (ALB)** with path-based routing:

| Path       | Target Group       | Service        |
|-----------|------------------|----------------|
| `/`       | `users-ui-tg`     | React Frontend |
| `/api/*`  | `users-api-tg`    | .NET Backend   |

---
```text
## 🔐 Security

### Security Groups
- **ALB Security Group**
  - Allows inbound HTTP (80)
- **ECS Services Security Group**
  - Allows traffic **only from ALB**
- **RDS Security Group**
  - Allows PostgreSQL traffic **only from ECS**

No services are publicly exposed except the ALB.

---

## 🐳 Dockerization

### Backend (.NET)
- Multi-stage Docker build
- Exposes port `7058`
- Runs EF Core migrations automatically on startup

### Frontend (React / Next.js)
- Build stage + runtime stage
- Exposes port `3000`
- Uses environment variable for API base URL

---

## 📦 Amazon ECR (Elastic Container Registry)
- Separate ECR repositories for:
  - `users-api`
  - `users-ui`
- Docker images are:
  - Built locally or via CI
  - Tagged (`latest` / commit SHA)
  - Pushed automatically by GitHub Actions

---

## 🚀 Amazon ECS (Fargate)

### ECS Cluster
- Custom cluster for the project

### Task Definitions
- Separate task definitions for:
  - Backend API
  - Frontend UI
- Includes:
  - Container image
  - Ports
  - Environment variables
  - Logging to CloudWatch

### ECS Services
- Keeps tasks running
- Integrated with ALB target groups
- Supports rolling deployments

---

## ⚙️ Environment Variables

### Backend (.NET)
```env
ASPNETCORE_URLS=http://+:7058
ConnectionStrings__DefaultConnection=postgresql://<user>:<password>@<host>:5432/<db>

---

### Frontend (React 19)
NEXT_PUBLIC_API_URL=http://<ALB-DNS>/api


# Project Overview

## 🔄 CI/CD Pipeline (GitHub Actions)

### Workflow
1. Checkout repository  
2. Build Docker images  
3. Authenticate with Amazon ECR  
4. Push images to ECR  
5. Force new ECS deployment  
6. ECS pulls latest image and redeploys automatically  

### Tools Used
- GitHub Actions  
- AWS IAM (least-privilege user)  
- AWS CLI  
- Docker  
- ECS AWS
- ECR AWS
- AWS FARGATE

---

## 🧪 Database Migrations

- EF Core migrations  
- Automatically executed at application startup:

```csharp
db.Database.Migrate();


## 📊 Observability

- Logs streamed to Amazon CloudWatch  
- ECS task & service health monitoring  
- ALB health checks configured  

---

## ✅ Key Achievements

- Infrastructure from scratch (VPC, subnets, routing)  
- Secure private networking  
- Production-grade ECS Fargate setup  
- ALB path-based routing  
- Dockerized full-stack application  
- CI/CD automation  
- Real-world AWS DevOps practices  
