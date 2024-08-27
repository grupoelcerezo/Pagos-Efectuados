namespace PagosEfectuados
{
    partial class Index
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Proceso = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.Proceso)).BeginInit();
            // 
            // Proceso
            // 
            this.Proceso.Enabled = true;
            this.Proceso.Interval = 3000D;
            this.Proceso.Elapsed += new System.Timers.ElapsedEventHandler(this.Proceso_Elapsed);
            // 
            // Index
            // 
            this.ServiceName = "Index";
            ((System.ComponentModel.ISupportInitialize)(this.Proceso)).EndInit();

        }

        #endregion

        private System.Timers.Timer Proceso;
    }
}
