<h1 align="center">
  <img src="https://upload.wikimedia.org/wikipedia/commons/7/71/Street_Fighter_old_logo.png" width="60" alt="Street Fighter Logo"/>
  <br/>
  Street Fighter API
</h1>

<p align="center">
  REST API construida con <strong>.NET 8</strong> para gestionar información del universo de Street Fighter — personajes, versiones, movimientos e imágenes.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white"/>
  <img src="https://img.shields.io/badge/Entity_Framework_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white"/>
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white"/>
  <img src="https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white"/>
  <img src="https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black"/>
</p>

<hr/>

## Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8)
- [SQL Server](https://www.microsoft.com/es-mx/sql-server/sql-server-downloads) o SQL Server LocalDB (incluido con Visual Studio)
- Visual Studio 2022 o superior

---

## Instalación

### 1. Clonar el repositorio

```bash
git clone https://github.com/tu-usuario/sf-api.git
cd sf-api
```

### 2. Restaurar paquetes NuGet

En Visual Studio, al abrir la solución los paquetes se restauran automáticamente. Si no, desde la **Package Manager Console**:

```powershell
dotnet restore
```

### 3. Configurar el `appsettings.json`

Crea o edita el archivo `appsettings.json` en la raíz del proyecto con la siguiente estructura:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=StreetFighterDb;Integrated Security=true;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "tu_clave_secreta_de_minimo_32_caracteres_aqui",
    "Issuer": "sf-api",
    "Audience": "sf-api-users"
  }
}
```

> ⚠️ **Importante:** Cambia el valor de `Jwt.Key` por una clave propia segura de al menos 32 caracteres. Nunca subas tu `appsettings.json` real al repositorio.

### 4. Correr las migraciones

Desde la **Package Manager Console** en Visual Studio (**Tools → NuGet Package Manager → Package Manager Console**):

```powershell
Update-Database
```

Esto creará la base de datos y todas las tablas automáticamente.

### 5. Ejecutar el proyecto

Presiona `F5` en Visual Studio o desde consola:

```bash
dotnet run
```

La API estará disponible en `https://localhost:{puerto}`. Puedes explorar los endpoints desde Swagger en:

```
https://localhost:{puerto}/swagger
```

---

## Autenticación

La API usa **JWT Bearer tokens**. Para acceder a los endpoints protegidos:

1. Obtén un token desde el endpoint de autenticación.
2. En Swagger, haz clic en el botón **Authorize** e ingresa: `Bearer {tu_token}`

---

## Estructura de la base de datos

| Tabla | Descripción |
|---|---|
| `Fighters` | Personajes base con datos canónicos |
| `FighterVersions` | Versiones del personaje por juego |
| `FighterMoves` | Movimientos de cada versión |
| `Games` | Juegos de la franquicia |
| `Images` | Imágenes asociadas a cualquier entidad |
