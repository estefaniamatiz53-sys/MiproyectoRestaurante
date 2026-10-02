using System.Globalization;
using MiproyectoRestaurant.Formularios;
using MiproyectoRestaurant.Negocio;

namespace MiproyectoRestaurant;

/// <summary>
/// Punto de entrada de la aplicacion.
///
/// El mismo ejecutable ofrece dos modos:
///   - Sin argumentos  -> abre el formulario WinForms (modo interactivo).
///   - --consola       -> ejecuta la demostracion completa por consola, sin
///                        abrir ninguna ventana. Util para pruebas automaticas.
///
/// Ambos modos consumen EXCLUSIVAMENTE la fachada <see cref="RestauranteFacade"/>.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        AplicarCultura();

        if (args.Any(a => a.Equals("--consola", StringComparison.OrdinalIgnoreCase)))
        {
            DemostracionConsola();
            return;
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new FormularioPrincipal());
    }

    /// <summary>
    /// Fuerza la cultura colombiana para que los importes se muestren como
    /// $2.500 (punto como separador de miles) y no como $2,500.
    /// </summary>
    private static void AplicarCultura()
    {
        try
        {
            var colombia = new CultureInfo("es-CO");
            CultureInfo.DefaultThreadCurrentCulture = colombia;
            CultureInfo.DefaultThreadCurrentUICulture = colombia;
        }
        catch (CultureNotFoundException)
        {
            // Sin soporte de es-CO: Dinero.Formatear tiene su propio respaldo.
        }
    }

    /// <summary>
    /// Demostracion del flujo completo por consola, equivalente a pulsar los
    /// cuatro botones del formulario en este orden.
    /// </summary>
    private static void DemostracionConsola()
    {
        var facade = new RestauranteFacade();

        Encabezado("SISTEMA DE CONTROL DE VENTAS - RESTAURANTE");
        Console.WriteLine("Demostracion por consola de la fachada RestauranteFacade.\n");

        // --- Boton 1: venta simple (Factory Method) -----------------------
        Console.WriteLine(">> Boton 1: Vender Empanada");
        facade.RegistrarVenta(TipoProducto.Empanada);

        // --- Boton 2: venta con ingrediente extra (Decorator) -------------
        Console.WriteLine(">> Boton 2: Vender Arepa con Queso Extra");
        facade.RegistrarVenta(TipoProducto.Arepa, TipoAdicion.QuesoExtra);

        // --- Boton 3: combo (Composite) -----------------------------------
        Console.WriteLine(">> Boton 3: Vender Combo (Empanada + Gaseosa)");
        facade.RegistrarCombo("Empanada", "Gaseosa");

        // --- Venta extra para que el desglose tenga varios conceptos ------
        facade.RegistrarVenta(TipoProducto.Tinto);
        facade.RegistrarVenta(TipoProducto.Arepa, TipoAdicion.QuesoExtra, TipoAdicion.AjiEspecial);
        facade.RegistrarCombo("Tinto", "Empanada+HuevoExtra");

        // --- Boton 4: reporte (Facade) ------------------------------------
        Console.WriteLine();
        Console.WriteLine(">> Boton 4: Ver Reporte Financiero");
        Console.WriteLine();
        Console.WriteLine(facade.ObtenerReporteDiario().GenerarResumen());

        // --- Comprobaciones de los patrones -------------------------------
        Console.WriteLine();
        Console.WriteLine(new string('=', 60));
        Console.WriteLine("VERIFICACION DE LOS PATRONES");
        Console.WriteLine(new string('=', 60));
        VerificarPatrones(facade);

        Console.WriteLine();
        Console.WriteLine("Demostracion finalizada.");
    }

    private static void VerificarPatrones(RestauranteFacade facade)
    {
        // Factory Method: la fachada crea el producto, el llamador nunca.
        var empanada = ProductoFactory.CrearProducto(TipoProducto.Empanada);
        MostrarComprobacion(
            "Factory Method crea la Empanada con precio e inversion del menu",
            empanada.PrecioVenta == 2500m && empanada.CostoInversion == 1000m,
            $"precio {Dinero.Formatear(empanada.PrecioVenta)} / inv {Dinero.Formatear(empanada.CostoInversion)}");

        // Decorator: el precio sube y la inversion tambien.
        var arepa = ProductoFactory.CrearProducto(TipoProducto.Arepa);
        var conQueso = new DecoradorProducto(arepa, ProductoFactory.CrearAdicion(TipoAdicion.QuesoExtra));
        MostrarComprobacion(
            "Decorator suma la adicion a precio (3500 -> 4300) y a inversion (1500 -> 1850)",
            conQueso.PrecioVenta == 4300m && conQueso.CostoInversion == 1850m,
            $"{conQueso.Nombre}: {Dinero.Formatear(conQueso.PrecioVenta)} / inv {Dinero.Formatear(conQueso.CostoInversion)}");

        // Decorator encadenado: dos capas sobre el mismo producto.
        var doble = new DecoradorProducto(conQueso, ProductoFactory.CrearAdicion(TipoAdicion.AjiEspecial));
        MostrarComprobacion(
            "Los decorators se encadenan (Arepa + Queso + Aji = 4600)",
            doble.PrecioVenta == 4600m,
            $"{doble.Nombre}: {Dinero.Formatear(doble.PrecioVenta)}");

        // Composite: se comporta como un producto mas y suma lo mismo.
        var combo = new PedidoComposite(new IProducto[] { empanada, ProductoFactory.CrearProducto(TipoProducto.Gaseosa) });
        MostrarComprobacion(
            "Composite suma los elementos (2500 + 3000 = 5500) con la misma interfaz",
            combo.PrecioVenta == 5500m && combo.CostoInversion == 2800m,
            $"{combo.Nombre}: {Dinero.Formatear(combo.PrecioVenta)} / inv {Dinero.Formatear(combo.CostoInversion)}");

        // Composite anidado: un combo dentro de otro combo.
        var comboAnidado = new PedidoComposite(new IProducto[] { combo, ProductoFactory.CrearProducto(TipoProducto.Tinto) });
        MostrarComprobacion(
            "Composite anidado (Combo dentro de combo = 5500 + 1200 = 6700)",
            comboAnidado.PrecioVenta == 6700m,
            $"{comboAnidado.Nombre}: {Dinero.Formatear(comboAnidado.PrecioVenta)}");

        // Regla de oro: todos son IProducto intercambiables.
        MostrarComprobacion(
            "Producto, decorador y combo comparten el mismo contrato IProducto",
            empanada is IProducto && conQueso is IProducto && combo is IProducto,
            "IProducto: ProductoBase, DecoradorProducto, PedidoComposite");

        // Cierre de caja.
        var antes = facade.ObtenerReporteDiario();
        facade.ReiniciarDia();
        var despues = facade.ObtenerReporteDiario();
        MostrarComprobacion(
            "ReiniciarDia limpia los acumulados para la nueva jornada",
            antes.CantidadVentas == 6 && despues.CantidadVentas == 0 && despues.Ganancia == 0m,
            $"{antes.CantidadVentas} ventas -> {despues.CantidadVentas} ventas");
    }

    private static void MostrarComprobacion(string descripcion, bool cumple, string detalle)
    {
        var marca = cumple ? "[OK]  " : "[FALLA]";
        Console.WriteLine($"{marca} {descripcion}");
        Console.WriteLine($"       -> {detalle}");
    }

    private static void Encabezado(string titulo)
    {
        Console.WriteLine(new string('=', 60));
        Console.WriteLine(titulo);
        Console.WriteLine(new string('=', 60));
    }
}
