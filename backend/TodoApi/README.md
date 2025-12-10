# ToDo App - Angular 21 + .NET 9

## Resumen
Aplicación To-Do con autenticación JWT, CRUD de tareas, dashboard y pruebas unitarias. Backend en .NET 9 (InMemory DB). Frontend en Angular 17 con Angular Material y uso mínimo de NgRx para manejo de tareas.

## Requisitos
- .NET 9 SDK
- Node.js 18+
- Angular CLI
- npm

## Ejecución

### Backend
1. `cd backend/TodoApi`
2. `dotnet run`
3. Swagger: `https://localhost:7059/swagger`

### Frontend
1. `cd frontend`
2. `npm install`
3. `ng serve --open`

Credenciales demo:
- email: `test@demo.com`
- password: `Password123`

## Decisiones técnicas
- **InMemory DB** para facilitar ejecución y evaluación. En producción cambiar a SQL Server con EF Core.
- **JWT** para autenticación stateless.
- **NgRx** usado únicamente para el feature `tasks` (balance entre escalabilidad y tiempo de entrega).
- **Angular Material** para acelerar UI/UX.

## Pruebas
- Backend: `dotnet test` (xUnit)
- Frontend: `ng test` (Karma + Jasmine)

## Mejoras posibles
- Hash de contraseñas (BCrypt).
- Refresh tokens.
- Paginación y filtros avanzados.
- End-to-end tests con Cypress.
- Agregar editar y eliminar, funcionalidades pendientes en el frond

# Manejo de certificados
- Se corrio set NODE_TLS_REJECT_UNAUTHORIZED=0 para que en local no presente fallas de certificados al momento de consumir desde el frond 
