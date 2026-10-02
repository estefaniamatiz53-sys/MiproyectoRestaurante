// ============================================================================
//  NEGOCIO - Sistema de Control de Ventas para Restaurante
//  Archivo unico con TODA la logica de negocio (sin dependencias de UI).
//
//  Patrones de diseno implementados aqui:
//    1. FACTORY METHOD  -> ProductoFactory
//    2. DECORATOR       -> DecoradorProducto
//    3. COMPOSITE       -> PedidoComposite
//    4. FACADE          -> RestauranteFacade
//
//  Decision de arquitectura clave: IProducto es el contrato comun. Gracias a
//  el, un producto base, un producto con ingredientes extra y un combo se
//  pueden usar de forma INTERCAMBIABLE. Eso es lo que hace posible que el
//  Decorator y el Composite se comporten de manera transparente.
// ============================================================================

using System.Globalization;

namespace MiproyectoRestaurant.Negocio;

#region Enumeraciones

/// <summary>Productos base del menu.</summary>
public enum TipoProducto
{
    Empanada,
    Arepa,
    Gaseosa,
    Tinto
}

/// <summary>Ingredientes extra que se anaden como adiciones a un producto.</summary>
public enum TipoAdicion
{
    QuesoExtra,
    AjiEspecial,
    HuevoExtra
}

#endregion

#region Contrato comun (habilita Decorator y Composite)

/// <summary>
/// Contrato unico de todo lo que se puede vender. Lo implementan tanto los
/// productos base como los decorados y los combos.
/// </summary>
public interface IProducto
{
    string Nombre { get; }
    decimal PrecioVenta { get; }
    decimal CostoInversion { get; }
    decimal Ganancia => PrecioVenta - CostoInversion;
}

/// <summary>Base comun de los productos simples del menu.</summary>
public abstract class ProductoBase : IProducto
{
    public abstract string Nombre { get; }
    public abstract decimal PrecioVenta { get; }
    public abstract decimal CostoInversion { get; }

    public override string ToString() => Nombre;
}

#endregion

#region PATRON CREACIONAL: FACTORY METHOD

/// <summary>Empanada - Precio $2.500 | Inversion $1.000.</summary>
public sealed class Empanada : ProductoBase
{
    public override string Nombre => "Empanada";
    public override decimal PrecioVenta => 2500m;
    public override decimal CostoInversion => 1000m;
}

/// <summary>Arepa - Precio $3.500 | Inversion $1.500.</summary>
public sealed class Arepa : ProductoBase
{
    public override string Nombre => "Arepa";
    public override decimal PrecioVenta => 3500m;
    public override decimal CostoInversion => 1500m;
}

/// <summary>Gaseosa - Precio $3.000 | Inversion $1.800.</summary>
public sealed class Gaseosa : ProductoBase
{
    public override string Nombre => "Gaseosa";
    public override decimal PrecioVenta => 3000m;
    public override decimal CostoInversion => 1800m;
}

/// <summary>Tinto - Precio $1.200 | Inversion $400.</summary>
public sealed class Tinto : ProductoBase
{
    public override string Nombre => "Tinto";
    public override decimal PrecioVenta => 1200m;
    public override decimal CostoInversion => 400m;
}

/// <summary>
/// PATRON CREACIONAL: FACTORY METHOD.
///
/// Centraliza la creacion de los productos base y de las adiciones. La
/// interfaz grafica NUNCA usa "new" sobre un producto: siempre pasa por aqui,
/// de modo que cambiar un precio o agregar un producto al menu se hace en un
/// unico punto del sistema.
/// </summary>
public static class ProductoFactory
{
    /// <summary>Crea un producto base a partir de su tipo.</summary>
    public static ProductoBase CrearProducto(TipoProducto tipo) => tipo switch
    {
        TipoProducto.Empanada => new Empanada(),
        TipoProducto.Arepa => new Arepa(),
        TipoProducto.Gaseosa => new Gaseosa(),
        TipoProducto.Tinto => new Tinto(),
        _ => throw new ArgumentOutOfRangeException(
                nameof(tipo), tipo, "Tipo de producto no soportado por la cocina.")
    };

    /// <summary>Crea un producto base a partir de su nombre ("Arepa", "Tinto"...).</summary>
    public static ProductoBase CrearProducto(string nombre)
    {
        if (!TryParseTipoProducto(nombre, out var tipo))
        {
            throw new ArgumentException(
                $"El producto '{nombre}' no existe en el menu.", nameof(nombre));
        }

        return CrearProducto(tipo);
    }

    /// <summary>Crea un ingrediente extra con su precio y su costo de insumo.</summary>
    public static AdicionProducto CrearAdicion(TipoAdicion tipo) => tipo switch
    {
        TipoAdicion.QuesoExtra => new AdicionProducto("Queso Extra", 800m, 350m),
        TipoAdicion.AjiEspecial => new AdicionProducto("Aji Especial", 300m, 100m),
        TipoAdicion.HuevoExtra => new AdicionProducto("Huevo Extra", 600m, 250m),
        _ => throw new ArgumentOutOfRangeException(
                nameof(tipo), tipo, "Adicion no soportada por la cocina.")
    };

    /// <summary>
    /// Atajo del Decorator: crea el producto base y lo envuelve con una capa
    /// por cada adicion solicitada, sin que el llamador use "new Decorador...".
    /// </summary>
    public static IProducto CrearProductoConAdiciones(
        TipoProducto tipo, params TipoAdicion[] adiciones)
    {
        IProducto producto = CrearProducto(tipo);

        foreach (var adicion in adiciones)
        {
            producto = new DecoradorProducto(producto, CrearAdicion(adicion));
        }

        return producto;
    }

    public static bool TryParseTipoProducto(string? nombre, out TipoProducto tipo)
    {
        tipo = default;
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return false;
        }

        var limpio = nombre.Trim();

        // Acepta el nombre en espanol o el nombre del enumerado (Empanada / Empanada).
        if (Enum.TryParse(limpio, ignoreCase: true, out tipo))
        {
            return true;
        }

        switch (limpio.ToLowerInvariant())
        {
            case "empanada": case "empanadas":
                tipo = TipoProducto.Empanada; return true;
            case "arepa": case "arepas":
                tipo = TipoProducto.Arepa; return true;
            case "gaseosa": case "gaseosas":
                tipo = TipoProducto.Gaseosa; return true;
            case "tinto": case "tintos": case "café": case "cafe":
                tipo = TipoProducto.Tinto; return true;
            default:
                return false;
        }
    }
}

#endregion

#region PATRON ESTRUCTURAL: DECORATOR

/// <summary>
/// Ingrediente extra por si solo. No se vende, pero si se puede medir: por eso
/// cumple IProducto y el Decorator puede sumar sus cifras a las del producto.
/// </summary>
public sealed record AdicionProducto(string Nombre, decimal PrecioVenta, decimal CostoInversion)
    : IProducto;

/// <summary>
/// PATRON ESTRUCTURAL: DECORATOR.
///
/// Envuelve un IProducto y suma dinamicamente el precio de venta y el costo de
/// inversion de una adicion, sin modificar la clase original ni obligar a
/// crear una subclase por cada combinacion posible (ArepaConQueso, ArepaConAji,
/// ArepaConQuesoYAji...). Los decoradores se pueden encadenar entre si.
/// </summary>
public sealed class DecoradorProducto : IProducto
{
    private readonly IProducto _envuelto;
    private readonly IProducto _adicion;

    public DecoradorProducto(IProducto envuelto, IProducto adicion)
    {
        _envuelto = envuelto ?? throw new ArgumentNullException(nameof(envuelto));
        _adicion = adicion ?? throw new ArgumentNullException(nameof(adicion));
    }

    /// <summary>Producto original, antes de las adiciones.</summary>
    public IProducto Envuelto => _envuelto;

    public string Nombre => $"{_envuelto.Nombre} + {_adicion.Nombre}";

    public decimal PrecioVenta => _envuelto.PrecioVenta + _adicion.PrecioVenta;

    public decimal CostoInversion => _envuelto.CostoInversion + _adicion.CostoInversion;

    public override string ToString() => Nombre;
}

#endregion

#region PATRON ESTRUCTURAL: COMPOSITE

/// <summary>
/// PATRON ESTRUCTURAL: COMPOSITE.
///
/// Agrupa varios IProducto (simples, decorados o a su vez composites) en un
/// unico objeto que EXPONE LOS MISMOS METODOS que un producto individual.
/// Suma precio e inversion de forma transparente, de modo que la fachada no
/// necesita saber si esta vendiendo una empanada o un combo de tres cosas.
/// </summary>
public sealed class PedidoComposite : IProducto
{
    private readonly List<IProducto> _elementos = new();

    public PedidoComposite()
    {
    }

    public PedidoComposite(IEnumerable<IProducto> elementos)
    {
        if (elementos is null)
        {
            throw new ArgumentNullException(nameof(elementos));
        }

        _elementos.AddRange(elementos);
        Validar();
    }

    public IReadOnlyList<IProducto> Elementos => _elementos;

    public void Agregar(IProducto elemento)
    {
        if (elemento is null)
        {
            throw new ArgumentNullException(nameof(elemento));
        }

        _elementos.Add(elemento);
    }

    public string Nombre => $"Combo ({string.Join(" + ", _elementos.Select(e => e.Nombre))})";

    public decimal PrecioVenta => _elementos.Sum(e => e.PrecioVenta);

    public decimal CostoInversion => _elementos.Sum(e => e.CostoInversion);

    private void Validar()
    {
        if (_elementos.Count == 0)
        {
            throw new InvalidOperationException("Un combo debe tener al menos un producto.");
        }
    }

    public override string ToString() => Nombre;
}

#endregion

#region Modelo del reporte

/// <summary>Una venta ya registrada. Es la unidad que acumula el dia.</summary>
public sealed record Venta(int Numero, DateTime Fecha, IProducto Producto)
{
    public decimal PrecioVenta => Producto.PrecioVenta;
    public decimal CostoInversion => Producto.CostoInversion;
    public decimal Ganancia => Producto.PrecioVenta - Producto.CostoInversion;
}

/// <summary>Una fila del desglose: como se comporte cada concepto en el dia.</summary>
public sealed record LineaReporte(string Concepto, int Veces, decimal Ventas, decimal Inversion)
{
    public decimal Ganancia => Ventas - Inversion;
}

/// <summary>
/// Resultado de <see cref="RestauranteFacade.ObtenerReporteDiario"/>.
/// Contiene {ventas, inversion, ganancia} mas el detalle por concepto.
/// </summary>
public sealed record ReporteDiario(
    DateTime Fecha,
    decimal Ventas,
    decimal Inversion,
    decimal Ganancia,
    int CantidadVentas,
    IReadOnlyList<LineaReporte> Desglose)
{
    public string GenerarResumen()
    {
        var lineas = new List<string>
        {
            $"FECHA: {Fecha:dd/MM/yyyy}",
            new string('-', 46),
            $"Ventas totales ......... {Dinero.Formatear(Ventas)}",
            $"Inversion total ........ {Dinero.Formatear(Inversion)}",
            $"Ganancia neta .......... {Dinero.Formatear(Ganancia)}",
            new string('-', 46),
            $"Ventas registradas ..... {CantidadVentas}"
        };

        if (Desglose.Count > 0)
        {
            lineas.Add(string.Empty);
            lineas.Add("DESGLOSE POR CONCEPTO");
            lineas.Add(new string('-', 46));
            foreach (var linea in Desglose)
            {
                lineas.Add($"  {linea.Concepto}");
                lineas.Add(
                    $"    x{linea.Veces}  ventas {Dinero.Formatear(linea.Ventas),12}  " +
                    $"inv {Dinero.Formatear(linea.Inversion),12}  " +
                    $"gan {Dinero.Formatear(linea.Ganancia),12}");
            }
        }

        return string.Join(Environment.NewLine, lineas);
    }
}

/// <summary>Formato de moneda colombiano ($2.500), con respaldo si falla ICU.</summary>
public static class Dinero
{
    private static readonly CultureInfo? Colombia = CrearCultura();

    private static CultureInfo? CrearCultura()
    {
        try
        {
            return new CultureInfo("es-CO");
        }
        catch (CultureNotFoundException)
        {
            // Modo invariante: se separa a mano para no romper la salida.
            return null;
        }
    }

    public static string Formatear(decimal valor)
    {
        if (Colombia is not null)
        {
            return "$" + valor.ToString("N0", Colombia);
        }

        return "$" + string.Join(".", Groupos(valor.ToString("F0")));
    }

    private static string[] Groupos(string digitos)
    {
        var grupos = new List<string>();
        for (var i = digitos.Length; i > 0; i -= 3)
        {
            grupos.Insert(0, digitos.Substring(Math.Max(0, i - 3), Math.Min(3, i)));
        }

        return grupos.ToArray();
    }
}

#endregion

#region PATRON ESTRUCTURAL: FACADE

/// <summary>
/// PATRON ESTRUCTURAL: FACADE.
///
/// Unico punto de contacto entre la interfaz grafica (los botones del
/// formulario) y la logica interna. El formulario no conoce el Factory, ni
/// los Decorators, ni el Composite: solo pide "registra esta venta" y consulta
/// el reporte del dia.
/// </summary>
public sealed class RestauranteFacade
{
    private readonly List<Venta> _ventasDelDia = new();
    private readonly List<string> _bitacora = new();
    private int _consecutivo;

    /// <summary>
    /// REGISTRAR VENTA simple. Equivale a <c>registrarVenta(tipoProducto)</c>.
    /// </summary>
    public Venta RegistrarVenta(TipoProducto tipo)
        => RegistrarVenta(tipo, Array.Empty<TipoAdicion>());

    /// <summary>
    /// REGISTRAR VENTA con adiciones. Cada adicion se aplica como una capa
    /// de Decorator sobre el producto base.
    /// </summary>
    public Venta RegistrarVenta(TipoProducto tipo, params TipoAdicion[] adiciones)
    {
        var producto = ProductoFactory.CrearProductoConAdiciones(tipo, adiciones);
        return Registrar(producto);
    }

    /// <summary>
    /// REGISTRAR COMBO a partir de nombres, por ejemplo
    /// <c>RegistrarCombo("Empanada", "Gaseosa")</c>. Cada componente admite
    /// adiciones con el separador "+": <c>"Arepa+QuesoExtra"</c>.
    /// </summary>
    public Venta RegistrarCombo(params string[] componentes)
    {
        if (componentes is null || componentes.Length == 0)
        {
            throw new ArgumentException(
                "El combo debe tener al menos un componente.", nameof(componentes));
        }

        return RegistrarCombo(componentes.Select(ConstruirComponente));
    }

    /// <summary>
    /// REGISTRAR COMBO a partir de objetos IProducto ya armados.
    /// </summary>
    public Venta RegistrarCombo(IEnumerable<IProducto> componentes)
    {
        var combo = new PedidoComposite(componentes);
        return Registrar(combo);
    }

    /// <summary>
    /// REPORTE DIARIO. Devuelve el objeto con {ventas, inversion, ganancia}.
    /// </summary>
    public ReporteDiario ObtenerReporteDiario()
    {
        var ventas = _ventasDelDia.Sum(v => v.PrecioVenta);
        var inversion = _ventasDelDia.Sum(v => v.CostoInversion);

        var desglose = _ventasDelDia
            .GroupBy(v => v.Producto.Nombre)
            .Select(g => new LineaReporte(
                g.Key,
                g.Count(),
                g.Sum(v => v.PrecioVenta),
                g.Sum(v => v.CostoInversion)))
            .OrderByDescending(l => l.Ventas)
            .ToList();

        return new ReporteDiario(
            Fecha: DateTime.Now,
            Ventas: ventas,
            Inversion: inversion,
            Ganancia: ventas - inversion,
            CantidadVentas: _ventasDelDia.Count,
            Desglose: desglose);
    }

    /// <summary>Historial de ventas del dia, en orden de registro.</summary>
    public IReadOnlyList<Venta> ObtenerVentasDelDia() => _ventasDelDia.AsReadOnly();

    /// <summary>Bitacora de eventos, para mostrarla en el formulario.</summary>
    public IReadOnlyList<string> ObtenerBitacora() => _bitacora.AsReadOnly();

    /// <summary>Cierra la caja del dia: vacia el acumulado para la siguiente jornada.</summary>
    public void ReiniciarDia()
    {
        _ventasDelDia.Clear();
        _bitacora.Clear();
        _consecutivo = 0;
    }

    private Venta Registrar(IProducto producto)
    {
        var venta = new Venta(++_consecutivo, DateTime.Now, producto);
        _ventasDelDia.Add(venta);

        _bitacora.Add(
            $"#{venta.Numero:D3}  {producto.Nombre,-34} " +
            $"precio {Dinero.Formatear(venta.PrecioVenta),10}  " +
            $"inv {Dinero.Formatear(venta.CostoInversion),10}  " +
            $"gan {Dinero.Formatear(venta.Ganancia),10}");

        return venta;
    }

    /// <summary>Convierte "Arepa+QuesoExtra" en un IProducto ya decorado.</summary>
    private static IProducto ConstruirComponente(string especificacion)
    {
        var partes = especificacion.Split('+', StringSplitOptions.RemoveEmptyEntries
                                                | StringSplitOptions.TrimEntries);

        if (partes.Length == 0 || !ProductoFactory.TryParseTipoProducto(partes[0], out var tipo))
        {
            throw new ArgumentException(
                $"'{especificacion}' no corresponde a un producto del menu.", nameof(especificacion));
        }

        var adiciones = new List<TipoAdicion>();
        for (var i = 1; i < partes.Length; i++)
        {
            if (!Enum.TryParse(partes[i], ignoreCase: true, out TipoAdicion adicion))
            {
                throw new ArgumentException(
                    $"'{partes[i]}' no es una adicion valida.", nameof(especificacion));
            }

            adiciones.Add(adicion);
        }

        return ProductoFactory.CrearProductoConAdiciones(tipo, adiciones.ToArray());
    }
}

#endregion
