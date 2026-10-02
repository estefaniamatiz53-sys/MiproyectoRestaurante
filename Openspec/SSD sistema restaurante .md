# Especificación de Diseño de Software (SDD) - Sistema Restaurante

## 1. Problema
Un restaurante pequeño que vende empanadas, arepas, gaseosas y tinto carece de un mecanismo eficiente para registrar sus transacciones diarias. Requiere llevar el control exacto de las ventas brutas, los costos de inversión en insumos y la ganancia neta. Para evitar acoplar la interfaz gráfica (formulario) con la lógica financiera y permitir la creación flexible de combos y adiciones, se necesita una arquitectura orientada a patrones de diseño.

## 2. Requisitos
- **RF1 (Registro de Ventas):** El sistema debe permitir registrar la venta de productos base (empanada, arepa, gaseosa, tinto).
- **RF2 (Inversión vs. Venta):** Cada producto debe manejar de forma independiente su costo de inversión en insumos y su precio de venta al público.
- **RF3 (Adiciones):** El sistema debe permitir agregar ingredientes extra (como queso) modificando dinámicamente el precio y costo.
- **RF4 (Combos):** El sistema debe permitir agrupar varios productos en un único pedido o combo.
- **RF5 (Reporte Financiero):** El sistema debe calcular y presentar las ventas totales, la inversión total y la ganancia neta acumulada.

## 3. Patrón Seleccionado
- **Patrón Creacional:** 
  - *Factory Method:* Encargado de centralizar y parametrizar la instanciación de los productos base sin acoplar el formulario a las clases concretas.
- **Patrones Estructurales:**
  - *Facade:* Proporciona una interfaz unificada para que los botones del formulario consulten reportes sin interactuar directamente con los subsistemas.
  - *Decorator:* Permite añadir adiciones o extras a los productos base de forma dinámica.
  - *Composite:* Permite tratar productos individuales y combos de manera uniforme al calcular costos y precios.

## 4. Diseño Propuesto
El diseño contempla una estructura donde la interfaz (formulario) interactúa de manera exclusiva con la clase `RestauranteFacade`.
- `ProductoFactory` crea las instancias concretas de los productos (`Empanada`, `Arepa`, etc.).
- `DecoradorProducto` envuelve objetos de tipo `Producto` para extender su comportamiento (costo y precio adicional).
- `PedidoComposite` implementa la misma interfaz de consulta que un `Producto`, pero almacena una lista interna de elementos para calcular los totales de un grupo de productos de forma recursiva.
- `RestauranteFacade` coordina el registro de las transacciones y expone el método `obtenerReporteDiario()`.

## 5. Criterios de Aceptación
- **CA1:** Al ejecutar el reporte diario desde la fachada, la ganancia neta debe coincidir exactamente con la resta: `Ventas Totales - Inversión Total`.
- **CA2:** Al solicitar un producto mediante `ProductoFactory`, el objeto devuelto debe contener los valores correctos de costo y precio de venta predeterminados.
- **CA3:** La adición de un ingrediente extra mediante `Decorator` debe incrementar simultáneamente el costo de inversión y el precio final del producto base.
- **CA4:** Un objeto de tipo `PedidoComposite` debe retornar la suma de precios y costos de todos sus elementos contenidos de forma transparente.