namespace TstSvC
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SVC_Compactar_Installer = new System.ServiceProcess.ServiceProcessInstaller();
            this.SVC_Compactar = new System.ServiceProcess.ServiceInstaller();
            // 
            // SVC_Compactar_Installer
            // 
            this.SVC_Compactar_Installer.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SVC_Compactar_Installer.Password = null;
            this.SVC_Compactar_Installer.Username = null;
            // 
            // SVC_Compactar
            // 
            this.SVC_Compactar.DisplayName = "SVC_Compactar";
            this.SVC_Compactar.ServiceName = "SVC_Compactar";
            this.SVC_Compactar.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.SVCompactacaoAr_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SVC_Compactar_Installer,
            this.SVC_Compactar});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SVC_Compactar_Installer;
        private System.ServiceProcess.ServiceInstaller SVC_Compactar;
    }
}