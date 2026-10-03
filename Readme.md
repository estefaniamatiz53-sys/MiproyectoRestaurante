# MiproyectoRestaurant - Sistema de Control Financiero

## Descripción
Aplicación de escritorio desarrollada en C# (WinForms) para gestionar las ventas de un pequeño restaurante (empanadas, arepas y gaseosas), permitiendo evaluar inversión en insumos, ingresos brutos y ganancias netas en tiempo real.

## Patrones de Diseño Utilizados
- **Factory Method (Creacional):** Centraliza y desencadena la instanciación de productos base (Empanada, Arepa, Gaseosa, Tinto) sin acoplar los formularios.
- **Facade (Estructural):** Expone un punto de acceso único (`RestauranteFacade`) para que la interfaz gráfica y los reportes consulten los cálculos financieros sin tocar la lógica interna.
- **Decorator (Estructural):** Permite añadir ingredientes extra a los productos base actualizando dinámicamente el precio de venta y el costo de inversión.
- **Composite (Estructural):** Unifica la gestión de productos individuales y combos dentro del historial de ventas.

## Estructura del Proyecto
- `Formularios/`: Interfaz gráfica con botones para registrar ventas y consultar el reporte financiero.
- `Negocio/`: Clases principales que contienen la implementación de los patrones de diseño y lógica financiera.
- `Program.cs`: Punto de entrada que permite ejecutar el formulario principal o la demostración por consola.

## Requisitos e Instrucciones de Ejecución
1. **Entorno de desarrollo:** Visual Studio 2022 con la carga de trabajo de desarrollo de escritorio de .NET.
2. **Ejecución básica (Interfaz gráfica):**
   - Abrir la solución `MiproyectoRestaurant.sln` en Visual Studio.
   - Presionar `F5` o el botón **Iniciar** (`MiproyectoRestaurant`).
3. **Ejecución por Consola:**
   - Para ejecutar la demostración en modo consola, pasar el argumento `--consola` al ejecutar el binario compilado.
