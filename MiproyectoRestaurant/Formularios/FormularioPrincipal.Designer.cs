#nullable enable

using System.Drawing;
using System.Windows.Forms;

namespace MiproyectoRestaurant.Formularios;

/// <summary>
/// Parte autogenerada por el diseñador de Visual Studio: declaracion de
/// controles y disposicion. No editar la logica aqui.
/// </summary>
partial class FormularioPrincipal
{
    /// <summary>Contenedor de componentes requeridos para el diseno.</summary>
    private System.ComponentModel.IContainer? components = null;

    private System.Windows.Forms.Panel panelResumen = null!;
    private System.Windows.Forms.Label lblTitulo = null!;
    private System.Windows.Forms.Label lblVentasCaption = null!;
    private System.Windows.Forms.Label lblVentasValor = null!;
    private System.Windows.Forms.Label lblInversionCaption = null!;
    private System.Windows.Forms.Label lblInversionValor = null!;
    private System.Windows.Forms.Label lblGananciaCaption = null!;
    private System.Windows.Forms.Label lblGananciaValor = null!;
    private System.Windows.Forms.Button btnEmpanada = null!;
    private System.Windows.Forms.Button btnArepaConQueso = null!;
    private System.Windows.Forms.Button btnCombo = null!;
    private System.Windows.Forms.Button btnReporte = null!;
    private System.Windows.Forms.Button btnReiniciar = null!;
    private System.Windows.Forms.Label lblMovimientos = null!;
    private System.Windows.Forms.ListBox lstMovimientos = null!;

    /// <summary>Limpia los recursos que se estan usando.</summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.panelResumen = new System.Windows.Forms.Panel();
        this.lblGananciaValor = new System.Windows.Forms.Label();
        this.lblInversionValor = new System.Windows.Forms.Label();
        this.lblVentasValor = new System.Windows.Forms.Label();
        this.lblGananciaCaption = new System.Windows.Forms.Label();
        this.lblInversionCaption = new System.Windows.Forms.Label();
        this.lblVentasCaption = new System.Windows.Forms.Label();
        this.lblMovimientos = new System.Windows.Forms.Label();
        this.lstMovimientos = new System.Windows.Forms.ListBox();
        this.btnReiniciar = new System.Windows.Forms.Button();
        this.btnReporte = new System.Windows.Forms.Button();
        this.btnCombo = new System.Windows.Forms.Button();
        this.btnArepaConQueso = new System.Windows.Forms.Button();
        this.btnEmpanada = new System.Windows.Forms.Button();
        this.lblTitulo = new System.Windows.Forms.Label();
        this.panelResumen.SuspendLayout();
        this.SuspendLayout();

        // panelResumen
        this.panelResumen.BackColor = System.Drawing.Color.FromArgb(246, 247, 249);
        this.panelResumen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.panelResumen.Location = new System.Drawing.Point(20, 58);
        this.panelResumen.Name = "panelResumen";
        this.panelResumen.Size = new System.Drawing.Size(740, 96);
        this.panelResumen.TabIndex = 0;

        // lblVentasCaption
        this.lblVentasCaption.AutoSize = true;
        this.lblVentasCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblVentasCaption.ForeColor = System.Drawing.Color.FromArgb(90, 96, 106);
        this.lblVentasCaption.Location = new System.Drawing.Point(20, 16);
        this.lblVentasCaption.Name = "lblVentasCaption";
        this.lblVentasCaption.Size = new System.Drawing.Size(110, 19);
        this.lblVentasCaption.Text = "VENTAS TOTALES";
        this.lblVentasCaption.TabIndex = 0;

        // lblVentasValor
        this.lblVentasValor.AutoSize = true;
        this.lblVentasValor.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblVentasValor.ForeColor = System.Drawing.Color.FromArgb(22, 128, 74);
        this.lblVentasValor.Location = new System.Drawing.Point(20, 44);
        this.lblVentasValor.Name = "lblVentasValor";
        this.lblVentasValor.Size = new System.Drawing.Size(80, 34);
        this.lblVentasValor.Text = "$0";
        this.lblVentasValor.TabIndex = 1;

        // lblInversionCaption
        this.lblInversionCaption.AutoSize = true;
        this.lblInversionCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblInversionCaption.ForeColor = System.Drawing.Color.FromArgb(90, 96, 106);
        this.lblInversionCaption.Location = new System.Drawing.Point(275, 16);
        this.lblInversionCaption.Name = "lblInversionCaption";
        this.lblInversionCaption.Size = new System.Drawing.Size(125, 19);
        this.lblInversionCaption.Text = "INVERSIÓN TOTAL";
        this.lblInversionCaption.TabIndex = 2;

        // lblInversionValor
        this.lblInversionValor.AutoSize = true;
        this.lblInversionValor.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblInversionValor.ForeColor = System.Drawing.Color.FromArgb(192, 96, 16);
        this.lblInversionValor.Location = new System.Drawing.Point(275, 44);
        this.lblInversionValor.Name = "lblInversionValor";
        this.lblInversionValor.Size = new System.Drawing.Size(80, 34);
        this.lblInversionValor.Text = "$0";
        this.lblInversionValor.TabIndex = 3;

        // lblGananciaCaption
        this.lblGananciaCaption.AutoSize = true;
        this.lblGananciaCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblGananciaCaption.ForeColor = System.Drawing.Color.FromArgb(90, 96, 106);
        this.lblGananciaCaption.Location = new System.Drawing.Point(530, 16);
        this.lblGananciaCaption.Name = "lblGananciaCaption";
        this.lblGananciaCaption.Size = new System.Drawing.Size(130, 19);
        this.lblGananciaCaption.Text = "GANANCIA NETA";
        this.lblGananciaCaption.TabIndex = 4;

        // lblGananciaValor
        this.lblGananciaValor.AutoSize = true;
        this.lblGananciaValor.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblGananciaValor.ForeColor = System.Drawing.Color.FromArgb(31, 78, 168);
        this.lblGananciaValor.Location = new System.Drawing.Point(530, 44);
        this.lblGananciaValor.Name = "lblGananciaValor";
        this.lblGananciaValor.Size = new System.Drawing.Size(80, 34);
        this.lblGananciaValor.Text = "$0";
        this.lblGananciaValor.TabIndex = 5;

        this.panelResumen.Controls.Add(this.lblVentasCaption);
        this.panelResumen.Controls.Add(this.lblVentasValor);
        this.panelResumen.Controls.Add(this.lblInversionCaption);
        this.panelResumen.Controls.Add(this.lblInversionValor);
        this.panelResumen.Controls.Add(this.lblGananciaCaption);
        this.panelResumen.Controls.Add(this.lblGananciaValor);

        // lblTitulo
        this.lblTitulo.AutoSize = true;
        this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(28, 32, 38);
        this.lblTitulo.Location = new System.Drawing.Point(18, 18);
        this.lblTitulo.Name = "lblTitulo";
        this.lblTitulo.Size = new System.Drawing.Size(410, 29);
        this.lblTitulo.Text = "Sistema de Control de Ventas - Restaurante";
        this.lblTitulo.TabIndex = 1;

        // btnEmpanada
        this.btnEmpanada.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnEmpanada.Location = new System.Drawing.Point(20, 170);
        this.btnEmpanada.Name = "btnEmpanada";
        this.btnEmpanada.Size = new System.Drawing.Size(360, 46);
        this.btnEmpanada.TabIndex = 2;
        this.btnEmpanada.Text = "1. Vender Empanada";
        this.btnEmpanada.UseVisualStyleBackColor = true;
        this.btnEmpanada.Click += new System.EventHandler(this.BtnEmpanada_Click);

        // btnArepaConQueso
        this.btnArepaConQueso.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnArepaConQueso.Location = new System.Drawing.Point(400, 170);
        this.btnArepaConQueso.Name = "btnArepaConQueso";
        this.btnArepaConQueso.Size = new System.Drawing.Size(360, 46);
        this.btnArepaConQueso.TabIndex = 3;
        this.btnArepaConQueso.Text = "2. Vender Arepa con Queso Extra";
        this.btnArepaConQueso.UseVisualStyleBackColor = true;
        this.btnArepaConQueso.Click += new System.EventHandler(this.BtnArepaConQueso_Click);

        // btnCombo
        this.btnCombo.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnCombo.Location = new System.Drawing.Point(20, 224);
        this.btnCombo.Name = "btnCombo";
        this.btnCombo.Size = new System.Drawing.Size(360, 46);
        this.btnCombo.TabIndex = 4;
        this.btnCombo.Text = "3. Vender Combo (Empanada + Gaseosa)";
        this.btnCombo.UseVisualStyleBackColor = true;
        this.btnCombo.Click += new System.EventHandler(this.BtnCombo_Click);

        // btnReporte
        this.btnReporte.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnReporte.ForeColor = System.Drawing.Color.White;
        this.btnReporte.Location = new System.Drawing.Point(400, 224);
        this.btnReporte.Name = "btnReporte";
        this.btnReporte.Size = new System.Drawing.Size(360, 46);
        this.btnReporte.TabIndex = 5;
        this.btnReporte.Text = "4. Ver Reporte Financiero";
        this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnReporte.FlatAppearance.BorderSize = 0;
        this.btnReporte.BackColor = System.Drawing.Color.FromArgb(31, 78, 168);
        // Debe ser false para que FlatStyle/BackColor realmente se apliquen.
        this.btnReporte.UseVisualStyleBackColor = false;
        this.btnReporte.Click += new System.EventHandler(this.BtnReporte_Click);

        // btnReiniciar
        this.btnReiniciar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnReiniciar.ForeColor = System.Drawing.Color.FromArgb(110, 116, 126);
        this.btnReiniciar.Location = new System.Drawing.Point(20, 280);
        this.btnReiniciar.Name = "btnReiniciar";
        this.btnReiniciar.Size = new System.Drawing.Size(740, 32);
        this.btnReiniciar.TabIndex = 6;
        this.btnReiniciar.Text = "Reiniciar dia (cerrar caja y empezar de nuevo)";
        this.btnReiniciar.UseVisualStyleBackColor = true;
        this.btnReiniciar.Click += new System.EventHandler(this.BtnReiniciar_Click);

        // lblMovimientos
        this.lblMovimientos.AutoSize = true;
        this.lblMovimientos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblMovimientos.ForeColor = System.Drawing.Color.FromArgb(90, 96, 106);
        this.lblMovimientos.Location = new System.Drawing.Point(20, 328);
        this.lblMovimientos.Name = "lblMovimientos";
        this.lblMovimientos.Size = new System.Drawing.Size(160, 19);
        this.lblMovimientos.Text = "MOVIMIENTOS DEL DIA";
        this.lblMovimientos.TabIndex = 7;

        // lstMovimientos
        this.lstMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
        this.lstMovimientos.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lstMovimientos.FormattingEnabled = true;
        this.lstMovimientos.IntegralHeight = false;
        this.lstMovimientos.ItemHeight = 20;
        this.lstMovimientos.Location = new System.Drawing.Point(20, 354);
        this.lstMovimientos.Name = "lstMovimientos";
        this.lstMovimientos.Size = new System.Drawing.Size(740, 256);
        this.lstMovimientos.TabIndex = 8;

        // FormularioPrincipal
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(780, 640);
        this.Controls.Add(this.lblTitulo);
        this.Controls.Add(this.panelResumen);
        this.Controls.Add(this.btnEmpanada);
        this.Controls.Add(this.btnArepaConQueso);
        this.Controls.Add(this.btnCombo);
        this.Controls.Add(this.btnReporte);
        this.Controls.Add(this.btnReiniciar);
        this.Controls.Add(this.lblMovimientos);
        this.Controls.Add(this.lstMovimientos);
        this.MinimumSize = new System.Drawing.Size(700, 560);
        this.Name = "FormularioPrincipal";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Sistema de Control de Ventas - Restaurante";

        this.panelResumen.ResumeLayout(false);
        this.panelResumen.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
