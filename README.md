# 🐶🐱 Sistema Veterinaria San Miguel

Proyecto de consola en **C# con Entity Framework Core y MySQL** para gestionar los clientes, mascotas, veterinarios y atenciones médicas de una veterinaria.

El sistema fue desarrollado aplicando **POO (Programación Orientada a Objetos)** y migraciones de **Entity Framework** para manejar la base de datos.

---

##  Tecnologías
- C# .NET 7 (Consola)
- Entity Framework Core
- MySQL (con Pomelo.EntityFrameworkCore.MySql)
- LINQ para consultas avanzadas

---

## Base de datos con EF Core y Migraciones

La base de datos se creó usando **Entity Framework Core** con migraciones automáticas.  
De esta manera, las entidades del proyecto (`Cliente`, `Mascota`, `Veterinario`, `AtencionMedica`) se reflejan en tablas de MySQL.

### Pasos realizados:
1. Configuración del `AppDbContext` con la cadena de conexión a MySQL:
   ```csharp
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
       if (!optionsBuilder.IsConfigured)
       {
           optionsBuilder.UseMySql(
               "server=localhost;database=database_sanmiguel;user=root;password=1234",
               new MySqlServerVersion(new Version(8, 0, 36))
           );
       }
   
   ```
2. Creación de la primera migración:
```
    dotnet ef migrations add InitialCreate
```
3. Aplicación de la migración para generar las tablas:
```
    dotnet ef database update

```
4. Con cada nueva entidad agregada (ej. AtencionMedica), se crea una nueva migración:
```
    dotnet ef migrations add CreacionTablaAtenciones
    dotnet ef database update

```
**Tablas generadas en MySQL**
- Clientes
- Mascotas
- Veterinarios
- AtencionesMedicas
- __EFMigrationsHistory (control interno de EF Core)

De esta forma, el esquema de la base de datos se mantiene sincronizado con el modelo de clases del proyecto.
## Funcionalidades

### Gestión de Clientes
- Registrar cliente
- Listar clientes
- Editar cliente
- Eliminar cliente

### Gestión de Mascotas
- Registrar mascota
- Listar mascotas
- Editar mascota
- Eliminar mascota

### Gestión de Veterinarios
- Registrar veterinario
- Listar veterinarios
- Editar veterinario
- Eliminar veterinario

### Gestión de Atenciones Médicas
- Registrar atención médica (fecha, diagnóstico, tratamiento, precio, mascota y veterinario)
- Listar atenciones médicas
- Eliminar atención médica

### Historial Médico
- Mostrar todas las atenciones médicas realizadas a una mascota determinada  
  (incluyendo datos del cliente y del veterinario asociado).

---

## Consultas Avanzadas (LINQ + EF Core)

1. Consultar todas las mascotas de un cliente.
2. Consultar el veterinario con más atenciones realizadas.
3. Consultar la especie de mascota más atendida en la clínica.
4. Consultar el cliente con más mascotas registradas.

---

## Ejemplo de sobrecarga de métodos
En la clase `MascotaService` se implementó la sobrecarga del método `ListarMascotas`:

```csharp
// Lista todas las mascotas
public void ListarMascotas()
{
    var mascotas = _context.Mascotas.ToList();
    ...
}

// Lista las mascotas filtrando por cliente
public void ListarMascotas(int clienteId)
{
    var mascotas = _context.Mascotas
        .Where(m => m.ClienteId == clienteId)
        .ToList();
    ...
}
```
---

## Justificación de cómo se aplicó POO

El sistema aplica **Programación Orientada a Objetos (POO)** en varios aspectos:

### Clases principales
- **Cliente** → Representa al dueño de mascotas, con atributos como `Nombre`, `Apellido`, `Dirección`.
- **Mascota** → Representa a cada animal, con atributos como `Nombre`, `Especie`, `Raza` y relación con su `Cliente`.
- **Veterinario** → Representa al profesional encargado de atender a las mascotas.
- **AtencionMedica** → Registra cada atención (fecha, diagnóstico, tratamiento, precio) y se relaciona con una `Mascota` y un `Veterinario`.

### Relaciones entre clases
- **Un Cliente tiene muchas Mascotas** (1 → N).
- **Una Mascota pertenece a un Cliente** (N → 1).
- **Una Mascota tiene varias Atenciones Médicas** (1 → N).
- **Una Atención Médica es realizada por un Veterinario** (N → 1).

Estas relaciones se reflejan en la base de datos gracias a **Entity Framework Core**, respetando las claves primarias y foráneas.

### Uso de Herencia
Se aplicó herencia en la definición de **entidades y servicios**, reutilizando código común.  
Por ejemplo, las clases de servicio (`ClienteService`, `MascotaService`, `VeterinarioService`, `AtencionMedicaService`) siguen un patrón similar, reutilizando la lógica de acceso al contexto y los métodos de CRUD.

### Sobrecarga de métodos
Se implementó **sobrecarga** en `MascotaService` con el método `ListarMascotas`:
```csharp
// Lista todas las mascotas
public void ListarMascotas()
{
    var mascotas = _context.Mascotas.ToList();
    ...
}

// Lista las mascotas de un cliente específico
public void ListarMascotas(int clienteId)
{
    var mascotas = _context.Mascotas
        .Where(m => m.ClienteId == clienteId)
        .ToList();
    ...
}
```
---

## Diagramas UML

### Diagrama de Clases
Representa las entidades principales del sistema (Cliente, Mascota, Veterinario, Atención Médica) y sus relaciones.  

![Diagrama de Clases](./img/diagrama_clases.jpeg)

---

### Diagrama de Casos de Uso
Muestra cómo interactúa el usuario con el sistema (registrar cliente, registrar mascota, registrar atención, consultas avanzadas, etc.).

![Diagrama de Casos de Uso](./img/diagrama_caso_uso.jpeg)

---

