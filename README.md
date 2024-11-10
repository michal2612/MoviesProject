![Build & Test](https://github.com/michal2612/MoviesProject/actions/workflows/build.yml/badge.svg)

# MoviesProject

## Run the app

### With Docker

**Build image** - you must be in `Dockerfile` location
```
docker build -t moviesproject:latest .
```

**Run container**

```
docker run -d -p 5050:8080 --name moviesapp moviesproject
```

**Swagger will available at:**

```
http://localhost:5050/swagger/index.html
```

---

### Locally

Requirements:
- `dotnet` https://dotnet.microsoft.com/en-us/download 
- `dotnet ef` https://learn.microsoft.com/en-us/ef/core/cli/dotnet#installing-the-tools

**Run:**

```
setup.bat
```

**Swagger will available at:**

```
http://localhost:5050/swagger/index.html
```

---
