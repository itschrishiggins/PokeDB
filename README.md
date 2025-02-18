# Pokémon Database & API Setup Guide

This repository contains two **Visual Studio projects**:
1. **MVC Application** – Connects to the `PokemonDB` database.
2. **Custom API** – Connects to the `APIPokemonInfoDB` database.

Both projects require **SQL Server** to function properly. Follow the steps below to set up your environment.

---

## 📌 Prerequisites

Before you begin, ensure you have the following installed:

✅ **[SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)** (or a full SQL Server installation)  
✅ **[SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup)** (for database management)  
✅ **[Visual Studio (Community or higher)](https://visualstudio.microsoft.com/)**  
✅ **.NET Framework** (version based on the project requirements)

---

## 🛠 Step 1: Restore Databases

1. Download the database backup (`.bak`) files from this repository:
   - **PokemonDB.bak** (for MVC project)
   - **APIPokemonInfoDB.bak** (for API project)

2. Open **SQL Server Management Studio (SSMS)** and connect to your local SQL Server instance (e.g., `localhost\SQLEXPRESS`).

3. Right-click **Databases** → **Restore Database…**.

4. Select **Device**, then click **Add…** and choose the appropriate `.bak` file.

5. Click **OK** to restore the database.

---

## 🔧 Step 2: Configure Database Connections

After restoring the databases, update the **connection strings** in both projects.

### **MVC Project (`Web.config`)**
1. Open the `Web.config` file in the **MVC project**.
2. Find the `<connectionStrings>` section.
3. Replace the existing connection string with your own:
   ```xml
   <connectionStrings>
       <add name="PokemonDBEntitiesConnectionString" 
            connectionString="metadata=res://*/Models.ConnectionToPokemonDBEDM.csdl|res://*/Models.ConnectionToPokemonDBEDM.ssdl|res://*/Models.ConnectionToPokemonDBEDM.msl;
            provider=System.Data.SqlClient;
            provider connection string=&quot;data source=YOUR_SERVER_NAME;initial catalog=PokemonDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" 
            providerName="System.Data.EntityClient" />
   </connectionStrings>
   ```
4. Replace `YOUR_SERVER_NAME` with your **SQL Server instance name** (e.g., `localhost\SQLEXPRESS`).
5. If using **SQL Authentication** instead of Windows Authentication, replace:
   ```xml
   integrated security=True;
   ```
   With:
   ```xml
   user id=YOUR_USERNAME;password=YOUR_PASSWORD;
   ```

---

### **API Project (`Web.config`)**
1. Open the `Web.config` file in the **API project**.
2. Find the `<connectionStrings>` section.
3. Replace the existing connection string:
   ```xml
   <connectionStrings>
       <add name="PokemonInfoDB_ConnectionString" 
            connectionString="metadata=res://*/Models.PokemonInfoDB_InfoModel.csdl|res://*/Models.PokemonInfoDB_InfoModel.ssdl|res://*/Models.PokemonInfoDB_InfoModel.msl;
            provider=System.Data.SqlClient;
            provider connection string=&quot;data source=YOUR_SERVER_NAME;initial catalog=APIPokemonInfoDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" 
            providerName="System.Data.EntityClient" />
   </connectionStrings>
   ```
4. Replace `YOUR_SERVER_NAME` with your **SQL Server instance name**.
5. If using **SQL Authentication**, update `user id` and `password`.

---

## 🚀 Step 3: Run the Projects (API First!)

🚨 **Important:** You must start the **API** project before running the **MVC** project. The MVC application relies on the API for data retrieval. If the API is not running, the MVC app will not function correctly.

### **Steps to Run the Projects**
1. Open the **Visual Studio solution** (`.sln` file).
2. In **Solution Explorer**, set both **MVC** and **API** projects as **startup projects**:
   - Right-click the solution → Select **Set Startup Projects…**
   - Choose **Multiple startup projects** and set **API** to start **before** MVC.
3. Press **F5** or click **Start** to launch the applications.

---

## 🔧 Additional Configuration Required

### **MVC Controller Edit**
To ensure the MVC project correctly interacts with the API, update the following line in `PokemonTblsController.cs` (line 113):

```csharp
client.BaseAddress = new Uri("LOCALADDRESS/api/"); // local address followed by api/ e.g: ("http://localhost:49189/api/");
```
Replace `LOCALADDRESS` with your actual local API address.

### **Launching the MVC Application**
To launch the MVC application, navigate to:

```
Views > Home > Home.cshtml
```
Then, **open it in a browser (debugging mode)** to ensure everything is working correctly.

---

## 📝 Troubleshooting

### 1️⃣ **Database Connection Issues**
- Ensure **SQL Server** is running.
- Check that the database is properly **restored**.
- Verify that your **SQL Server instance name** is correct.

### 2️⃣ **API or MVC Project Not Running**
- Make sure **Visual Studio** is running with **Administrator privileges**.
- **Ensure the API project is running before starting the MVC project**.
- Check **IIS Express** settings.
- Restart your machine if necessary.

---

## 💡 Additional Notes

- You can **customize** the API and MVC projects to add more Pokémon features.
- If you want to deploy these projects, consider setting up **Azure SQL Database** or **Docker for SQL Server**.

---

## 🤝 Contributing

Feel free to contribute by **submitting issues or pull requests**!

1. Fork the repository.
2. Create a new branch: `git checkout -b feature-newFeature`.
3. Commit your changes: `git commit -m "Added new feature"`.
4. Push to the branch: `git push origin feature-newFeature`.
5. Submit a pull request.

---

## 📜 License

This project is licensed under the **MIT License**.

---
🚀 Enjoy catching them all!

