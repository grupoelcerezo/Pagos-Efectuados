namespace PagosEfectuados
{
    partial class ProjectInstaller
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
            this.InstallerPagosEfectuados = new System.ServiceProcess.ServiceProcessInstaller();
            this.ServicioPagosEfectuados = new System.ServiceProcess.ServiceInstaller();
            // 
            // InstallerPagosEfectuados
            // 
            this.InstallerPagosEfectuados.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.InstallerPagosEfectuados.Password = null;
            this.InstallerPagosEfectuados.Username = null;
            // 
            // ServicioPagosEfectuados
            // 
            this.ServicioPagosEfectuados.ServiceName = "Index";
            this.ServicioPagosEfectuados.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.InstallerPagosEfectuados,
            this.ServicioPagosEfectuados});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller InstallerPagosEfectuados;
        private System.ServiceProcess.ServiceInstaller ServicioPagosEfectuados;
    }
}