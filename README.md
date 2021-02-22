# Visma.Test.Back

Backend for employee management application.

## Usage

- The migration process will create a database with tables and data. Then, it is needed to change the **DefaultConnection** property. It is not necessary to provide an existent database name. However, the user id and password must already exists.
- [optional] Change the **localHost** just if your frontend is not being served in the port 4200.
```json
{
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=.;Database=Database44;User Id=sa; Password=123;"
  },
  "JwtIssuerOptions": {
    "key": "C34A0D0214F14106AC04BA4410F23D12098F40DD973943ABF0D3756A2EEDAEC",
    "issuer": "http://localhost:4200/",
    "audience": "http://localhost:4200/",
    "expirationTime": 200
  },
  "AllowedOrigin": {
    "localHost": "http://localhost:4200"
  },
  "ExceptionSettings": {
    "ShowCustomMessage": false,
    "CustomMessage": "An unexpected error ocurred. Please, try again."
  }
}
```

Open Package Manager Console and run the command below. This command will finish the migration process and create the database with the tables and data.

```bash
  Update-Database
```
