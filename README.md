# Proyecto Cuentas - Microservicios .NET 8

Sistema de gestión de clientes, cuentas y movimientos bancarios, construido con **.NET 8**, arquitectura de **microservicios**, **Clean Architecture**, **CQRS**. Persistencia sobre **PostgreSQL**.

## Arquitectura

El sistema está compuesto por 2 microservicios independientes, cada uno con su propia base de datos:

| Microservicio | Responsabilidad | Puerto |
|---|---|---|
| **Client.API** | Gestión de clientes | 5050 |
| **Account.API** | Gestión de cuentas, movimientos y reportes | 5051 |

## Levantar el proyecto

Desde la raíz del repositorio/src, ejecutar:

```bash
docker compose up
```

o en segundo plano:

```bash
docker compose up -d --build
```
## Migraciones de base de datos

Las migraciones de Entity Framework Core se aplican **de forma automática al iniciar cada microservicio**, siempre que la variable de entorno `ASPNETCORE_ENVIRONMENT` esté configurada como `Development`

## Endpoints

### Clientes.API — `https://localhost:5050`

| Método | Endpoint | Descripción |
|---|---|---|
| POST | `/clientes` | Crea un nuevo cliente |
| GET | `/clientes?PageIndex={n}&PageSize={n}` | Lista clientes de forma paginada |
| GET | `/clientes/{id}` | Obtiene un cliente por su Id |
| PUT | `/clientes/{id}` | Actualiza los datos de un cliente |
| DELETE | `/clientes/{id}` | Elimina un cliente |
| POST | `/clientes/login` | Autentica un cliente (identificación + password) |

### Cuentas.API — `https://localhost:5051`

| Método | Endpoint | Descripción |
|---|---|---|
| POST | `/cuentas` | Crea una nueva cuenta para un cliente |
| GET | `/cuentas?PageIndex={n}&PageSize={n}` | Lista cuentas de forma paginada |
| GET | `/cuentas/{id}` | Obtiene una cuenta por su Id |
| PUT | `/cuentas/{id}` | Actualiza tipo/estado de una cuenta |
| GET | `/clientes/{clienteId}/cuentas` | Lista las cuentas asociadas a un cliente |
| POST | `/movimientos` | Registra un movimiento (depósito/retiro) sobre una cuenta |
| PUT | `/movimientos/{id}/cuentas/{cuentaId}` | Actualiza un movimiento existente |
| GET | `/movimientos/cuentas/{cuentaId}` | Lista los movimientos de una cuenta |
| GET | `/movimientos/cuentas/{cuentaId}/reporte?fechaDesde={fecha}&fechaHasta={fecha}` | Reporte de movimientos de una cuenta por rango de fechas |
| GET | `/reportes/{clienteId}?fechaDesde={fecha}&fechaHasta={fecha}` | Reporte de estado de cuentas de un cliente por rango de fechas |
