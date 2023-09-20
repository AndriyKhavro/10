## Start sql engines

```cmd
docker compose up -d
```

## Generate data

```cmd
dotnet run --project SqlDataGenerator/SqlDataGenerator.csproj -- mysql
dotnet run --project SqlDataGenerator/SqlDataGenerator.csproj -- postgres
```

## Results

### Postgres

| Transaction Isolation Level | Lost Update   | Dirty Read  | Non-Repeatable Read | Phantom Read     |
|-----------------------------|----------------|------------|---------------------|------------------|
| READ UNCOMMITTED            | YES            | NO         | YES                 | YES              |
| READ COMMITTED              | YES            | NO         | YES                 | YES              |
| REPEATABLE READ             | NO (Error)     | NO         | NO                  | NO               |
| SERIALIZABLE                | NO (Error)     | NO         | NO                  | NO               |

### MySQL

| Transaction Isolation Level | Lost Update   | Dirty Read  | Non-Repeatable Read | Phantom Read      |
|-----------------------------|---------------|-------------|---------------------|-------------------|
| READ UNCOMMITTED            | YES           | YES         | YES                 | YES               |
| READ COMMITTED              | YES           | NO          | YES                 | YES               |
| REPEATABLE READ             | YES           | NO          | NO                  | YES               |
| SERIALIZABLE                | NO (Lock)     | NO          | NO (Lock)           | NO (Lock)         |
