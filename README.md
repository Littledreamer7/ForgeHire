# 🚀 ForgeHire – AI-Powered SaaS Job Portal (Backend)

ForgeHire is a **multi-role SaaS Job Portal backend** built with ASP.NET Core, designed as a lightweight **Applicant Tracking System (ATS)** with support for **AI-driven workflows, voice-based interactions, and multilingual readiness**.

---

## 🔥 What’s Implemented

### 🔐 Authentication

* OTP-based login system
* Automatic user creation
* JWT-based authentication with role support

---

### 👤 User & Candidate System

* User → Candidate profile mapping
* Mobile number normalization (+91 format)
* Secure identity handling

---

### 💼 Job Management

* Create, update, delete job postings
* Company-based job ownership
* Role-based access (Admin / HR)
* Public job listing for candidates

---

### 📄 Job Application System

* Apply to jobs
* Prevent duplicate applications
* Application tracking per candidate
* Withdraw application support

---

### 🧑‍💼 Applicant Management (HR)

* View applicants per job
* Update application status:

  * Applied
  * Shortlisted
  * Rejected
  * Hired

---

### 🧠 Intelligent Features (Foundation)

* AI-ready architecture for job and candidate matching
* Voice interaction support (job discovery & actions)
* Multilingual-ready design for broader accessibility

---

## 🧱 Architecture

* Clean modular structure (Jobs, Applications, Auth, Users)
* Separation of concerns (Controllers, Services, Repositories)
* DTO-based API design
* Scalable SaaS backend architecture

---

## 🔄 Core Flow

1. User logs in via OTP → JWT issued
2. Candidate profile is resolved from user
3. HR creates job postings
4. Candidate views and applies to jobs
5. Application is stored with status tracking
6. HR reviews applicants and updates status
7. Candidate tracks application progress

---

## 🧠 Tech Stack

* ASP.NET Core Web API
* Entity Framework Core
* MySQL
* JWT Authentication

---

## 🔐 Roles

* Admin
* HR
* Candidate

---

## 📡 Key APIs

### Jobs

* `GET /api/jobs` → HR/Admin jobs
* `GET /api/jobs/public` → Candidate job listing
* `POST /api/jobs` → Create job
* `PUT /api/jobs/{id}` → Update job

### Applications

* `POST /api/applications` → Apply to job
* `GET /api/applications/my` → Candidate applications
* `PUT /api/applications/{id}/withdraw` → Withdraw
* `PUT /api/applications/{id}/status` → Update status
* `GET /api/applications/job/{jobId}` → View applicants

---

## 🎯 Project Status

✅ Core ATS backend completed
✅ Ready for frontend integration

---

## 📌 Author

ForgeHire – AI-enabled SaaS hiring platform backend
