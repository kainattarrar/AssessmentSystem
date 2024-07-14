Setting Up and Running the Project:

1. Install Visual Studio or Visual Studio Code.

2. Install Docker and configure Docker Containers to run Redis and RabbitMQ.

3. Update the RabbitMQ and Redis connection settings in appsettings.json.

4. Install PostgreSQL and create a database.

5. Update the connection string in DataContext.cs with your PostgreSQL database connection string.

6. Open Package Manager Console (Visual Studio) and run the following commands to apply migrations and update the database:
   > Add-migration InitialCreate
   > Update-database

Ensure all services start correctly, including RabbitMQ, Redis, and PostgreSQL.
Use tools like Postman or Insomnia to test API endpoints and verify JWT token generation and question solving functionalities.

---------------------------------------------------------------------------------------------------------------------------------

Projenin Kurulumu ve Çalıştırılması:

Visual Studio veya Visual Studio Code'u yükleyin.

Docker'ı yükleyin ve Redis ve RabbitMQ'nun çalışması için Docker konteynerlerini yapılandırın.

appsettings.json dosyasındaki RabbitMQ ve Redis bağlantı ayarlarını güncelleyin.

PostgreSQL'i yükleyin ve bir veritabanı oluşturun.

DataContext.cs içinde PostgreSQL veritabanı bağlantı dizesini güncelleyin.

Package Manager Console'u (Visual Studio) açın ve aşağıdaki komutları çalıştırarak migrasyonları uygulayın ve veritabanını güncelleyin:
    > Add-migration InitialCreate
    > Update-database

RabbitMQ, Redis ve PostgreSQL gibi tüm servislerin doğru şekilde başladığından emin olun.
API endpointlerini test etmek ve JWT token üretimi ile soru çözme işlevlerini doğrulamak için Postman veya Insomnia gibi araçları kullanın.
