# AI Chatbot Backend (C# .NET)

This is the backend API for a simple AI chatbot application.  
The backend is built with **C# (.NET Web API)** and is deployed on **Render**.

The frontend (React) sends user messages to this backend, which then calls the AI model (Groq or any other provider) and returns the response.

---

## 🚀 Features
- .NET Web API backend  
- Single chat endpoint
- Forwards user messages to AI model & returns AI-generated responses  
- Deployed on Render  
- Dockerfile included

---

## ⚠️ Important Warning
This project uses Groq Cloud's free tier, which has strict rate limits.

Please avoid sending too many messages in a short span of time, or you may hit the free-tier limit and application will not work!

---

## 🛠️ Tech Stack

### Frontend

* **React** (Fetch API for backend calls)
* In-line CSS
* JavaScript

➡️ **Frontend Repo:** `https://github.com/ShravanSk123/ai-chatbot-frontend`

### Backend

* **C#** (.NET Core Web API)

### AI Model

* **Groq Cloud API**

---

## 🌐 Deployment
This backend is deployed on Render.

To deploy your own:

- Create a Web Service on Render

- Connect your GitHub repo

- Add required environment variables

- Deploy


