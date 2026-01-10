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

```

##  🔀 Load Balancer Routing
```text
**Application Load Balancer (ALB)** with path-based routing:

| Path       | Target Group       | Service        |
|-----------|------------------|----------------|
| `/`       | `users-ui-tg`     | React Frontend |
| `/api/*`  | `users-api-tg`    | .NET Backend   |


```

---
