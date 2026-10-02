using MiproyectoRestaurant.Negocio;

namespace MiproyectoRestaurant.Formularios;

/// <summary>
/// Interfaz grafica del sistema de ventas.
///
/// REGLA DE DISENO CENTRAL: los botones NO conocen la logica interna. Nunca
/// escriben "new Empanada()" ni "new DecoradorProducto(...)" ni
/// "new PedidoComposite(...)". Toda la complejidad (Factory Method, Decorator y
/// Composite) queda encapsulada dentro de <see cref="RestauranteFacade"/>, que
/// es el unico objeto al que esta capa habla.
/// </summary>
public partial class FormularioPrincipal : Form
{
    /// <summary>Punto unico de acceso a la logica del restaurante.</summary>
    private readonly RestauranteFacade _facade = new();

    public FormularioPrincipal()
    {
        InitializeComponent();

        RefrescarResumen();
        RefrescarMovimientos();
    }

    // ---------------------------------------------------------------------
    // Botones: cada uno invoca un metodo distinto de la fachada.
    // ---------------------------------------------------------------------

    /// <summary>Boton 1: venta simple de un producto base (Factory Method).</summary>
    private void BtnEmpanada_Click(object? sender, EventArgs e)
    {
        Ejecutar("venta", () => _facade.RegistrarVenta(TipoProducto.Empanada));
    }

    /// <summary>
    /// Boton 2: venta de un producto con ingrediente extra.
    /// La adicion se aplica como una capa de Decorator dentro de la fachada.
    /// </summary>
    private void BtnArepaConQueso_Click(object? sender, EventArgs e)
    {
        Ejecutar("venta", () => _facade.RegistrarVenta(TipoProducto.Arepa, TipoAdicion.QuesoExtra));
    }

    /// <summary>
    /// Boton 3: venta de un combo. La fachada arma un PedidoComposite por
    /// dentro; este boton solo pasa los nombres de los componentes.
    /// </summary>
    private void BtnCombo_Click(object? sender, EventArgs e)
    {
        Ejecutar("combo", () => _facade.RegistrarCombo("Empanada", "Gaseosa"));
    }

    /// <summary>
    /// Boton 4: reporte financiero. Tambien pasa unicamente por la fachada.
    /// </summary>
    private void BtnReporte_Click(object? sender, EventArgs e)
    {
        var reporte = _facade.ObtenerReporteDiario();

        if (reporte.CantidadVentas == 0)
        {
            MessageBox.Show(
                "Todavia no hay ventas registradas en el dia.\n\n" +
                "Usa los botones 1, 2 o 3 para registrar una venta primero.",
                "Reporte financiero",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        MessageBox.Show(
            reporte.GenerarResumen(),
            "Reporte financiero del dia",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    /// <summary>Utilidad para cerrar la caja del dia y reiniciar los acumulados.</summary>
    private void BtnReiniciar_Click(object? sender, EventArgs e)
    {
        var reporte = _facade.ObtenerReporteDiario();

        if (reporte.CantidadVentas == 0)
        {
            _facade.ReiniciarDia();
            RefrescarResumen();
            RefrescarMovimientos();
            return;
        }

        var confirmacion = MessageBox.Show(
            $"El dia tiene {reporte.CantidadVentas} venta(s) por {Dinero.Formatear(reporte.Ventas)}.\n\n" +
            "Deseas cerrar la caja e iniciar un nuevo dia?",
            "Reiniciar dia",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirmacion != DialogResult.Yes)
        {
            return;
        }

        _facade.ReiniciarDia();
        RefrescarResumen();
        RefrescarMovimientos();
    }

    // ---------------------------------------------------------------------
    // Refresco de la vista
    // ---------------------------------------------------------------------

    /// <summary>Actualiza los tres indicadores leyendo el reporte de la fachada.</summary>
    private void RefrescarResumen()
    {
        var reporte = _facade.ObtenerReporteDiario();

        lblVentasValor.Text = Dinero.Formatear(reporte.Ventas);
        lblInversionValor.Text = Dinero.Formatear(reporte.Inversion);
        lblGananciaValor.Text = Dinero.Formatear(reporte.Ganancia);
    }

    /// <summary>Pinta el historial de movimientos del dia.</summary>
    private void RefrescarMovimientos()
    {
        lstMovimientos.BeginUpdate();
        try
        {
            lstMovimientos.Items.Clear();
            foreach (var linea in _facade.ObtenerBitacora())
            {
                lstMovimientos.Items.Add(linea);
            }
        }
        finally
        {
            lstMovimientos.EndUpdate();
        }

        if (lstMovimientos.Items.Count > 0)
        {
            lstMovimientos.TopIndex = lstMovimientos.Items.Count - 1;
        }
    }

    /// <summary>
    /// Ejecuta una accion de la fachada y refresca la vista. Centraliza el
    /// manejo de errores para que ningun boton quede sin proteccion.
    /// </summary>
    private void Ejecutar(string concepto, Func<Venta> accion)
    {
        try
        {
            var venta = accion();
            RefrescarResumen();
            RefrescarMovimientos();
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {concepto} registrado: {venta.Producto.Nombre}");
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"No se pudo registrar la operacion.\n\n{ex.Message}",
                "Error de operacion",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
