#region Librerias

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Anexar Librerias que son requeridas
using System.IO; // Manejo de archvios y directorios
using System.Diagnostics; // Manejo de procesos activos
using Ionic.Zip; // Manejo de zip para comprimir y descomprimir archivos

//Librerias Realizadas en NUit4Automation
using Nu4it;
using nu4itExcel;
using nu4itFox;

//Librerias para la automatizaciòn de Excel
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

//Libreria para obtener la MacAddress de la computadora donde se pretende ejecutar el programa
using System.Net.NetworkInformation;

//NO SE REQUIERE Librerias para la automatizaciòn de Internet Explorer. Sólo Agregar la referencia 
#endregion

namespace Captcha
{
    public partial class Form1 : Form
    {

        #region ConstantesGlobales

        //CONSTANTES GLOBALES**********
        const int MAX = 10000;
        const int SI = 1;
        const int NO = 0;

        //Datos Constantes para el uso del FTP
        const string USUARIO = "versiones@bestcollect.org";
        const string PASSWORD = "t@N@Ed@MPcA%";
        const string SERVIDOR = "ftp://bestcollect.com.mx";
        const string CARPETA_FTP = "private/updates";
        const string CARPETA_FTP_EXC = "private/Errores";
        Clases.Navegacion nav = new Clases.Navegacion();
        //*****FIN DE LAS CONSTANTES GLOBALES******

        #endregion

        #region VariablesGlobales

        //*****VARIABLES GLOBALES******

        //Variables requeridas para crear la bitácora de ejecución y el nombre de archivos que se suben y descargan por FTP
        string RUTA_ARCHIVO_LOG; //Ruta y nombre del archvio de la bitácora
        string NOMFILEEXC = "Bit"; //Nombre de Archivo con el que se sube la bitacora por FTP en caso de ser necesario
        string NOMFILEZIP; // AQUÍ SE DEBE DAR NOMBRE AL:    (ESTA VARIABLE)
                           //Archivo que se descarga para actualización
                           //El nombre se encuentra en el archvio ini del respectivo proyecto 
                           //(EL ARCHIVO INI DEBE ENCONTRARSE EN LA MISMA CARPETA QUE EL EJECUTABLE DE ESTE PROYECTO)
                           //Debe ser un archvio ZIP por ejemplo ParaReIn.zip  
        string NOMFILELIC = "Captcha.txt"; //AQUÍ SE DEBE DAR NOMBRE AL:    (ESTA VARIABLE) Archivo de licencias
        string VersionProyecto; //Nombre del proyecto

        #region ObjetosGlobales

        //Creación de objetos para invocar métodos ya desarrollados 
        usaR objNu4 = new usaR(); //Objeto para usar funciones generales 
        nuExcel objNuExcel = new nuExcel(); //Objeto para usar funciones sobre Excel
        nufox objNuFox = new nufox(); //Objeto para usar Funciones de FOXPRO en C# 

        SHDocVw.InternetExplorer MiIE; //Variable para automatizar la instanciación del objeto de Internet Explorer

        //Variables para la automatización de Excel
        Excel.Application MiExcel;//Instancia de Excel
        Excel.Workbook ArchivoTrabajoExcel;
        Excel.Worksheet HojaExcel;
        Excel.PivotTable TablaDinamica;
        Excel.Range Rango;

        #endregion

        //Arreglos globales
        string[] CONTENIDOINI = new string[0];

        //*****FIN DE LAS VARIBLES GLOBALES*************************************************************************

        #endregion

        #region EventosControlesFormulario

        //*FUNCIONES Y PROCEDIMIENTO SOBRE LOS EVENTOS EN EL FORMULARIO*********************************************

        //Función que regresa la MacAddress de la computadora donde se ejecuta el programa
        public string ObtenMacAddress()
        {
            string id = "";
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            id = sMacAddress;
            return (id);
        }

        //Función que baja el archivo FTP donde se almacenan las MacAdress y verifica si la computadora Actual tiene licencia para ejecutar el ROBOT
        public int ChecarLicencia()
        {
            int licencia = SI, numUsu, numPer;
            string contenidoArchivo, macaddress, descarga, rutaArchivoLic, rutaNomArchLic, usuarios, permisos, usuActuales;
            descarga = SERVIDOR + "/" + CARPETA_FTP + "/" + NOMFILELIC;
            rutaArchivoLic = Directory.GetCurrentDirectory();
            rutaNomArchLic = rutaArchivoLic + @"\" + NOMFILELIC;
            objNu4.DescargaFTP(USUARIO, PASSWORD, descarga, rutaNomArchLic);
            if (File.Exists(rutaNomArchLic))
            {
                macaddress = ObtenMacAddress();
                StreamReader sr = new StreamReader(rutaNomArchLic);
                contenidoArchivo = sr.ReadToEnd();
                sr.Close();
                if (contenidoArchivo.IndexOf(macaddress) < 0)
                {
                    usuarios = contenidoArchivo.Substring(0, 4);
                    permisos = contenidoArchivo.Substring(4, 4);
                    usuActuales = usuarios;
                    numUsu = Convert.ToInt16(usuarios);
                    numPer = Convert.ToInt16(permisos);
                    if (numUsu < numPer)
                    {
                        numUsu++;
                        usuarios = Convert.ToString(numUsu);
                        usuarios = "000" + usuarios;
                        if (usuarios.Length > 4)
                        {
                            usuarios = Convert.ToString(numUsu);
                            usuarios = "00" + usuarios;
                            if (usuarios.Length > 4)
                            {
                                usuarios = Convert.ToString(numUsu);
                                usuarios = "0" + usuarios;
                                if (usuarios.Length > 4) { usuarios = Convert.ToString(numUsu); }
                            }
                        }

                        contenidoArchivo = contenidoArchivo.Replace(usuActuales, usuarios);
                        contenidoArchivo = contenidoArchivo + "\n" + macaddress;

                        StreamWriter fw = new StreamWriter(rutaNomArchLic);
                        fw.WriteLine(contenidoArchivo);
                        fw.Close();
                        objNu4.SubeFTP(USUARIO, PASSWORD, rutaNomArchLic, descarga);
                    }
                    else { licencia = NO; }
                }
                File.Delete(rutaNomArchLic);
            }
            else { licencia = NO; }
            return (licencia);
        }

        //Configuración inicial del Formulario e invocación a la función para checar si hay licencia de ejecución
        public Form1()
        {
            InitializeComponent();
            VersionProyecto = Process.GetCurrentProcess().ProcessName;
            lblVersion.Text = VersionProyecto;
            dataGridView1.Visible = false;
            cmdActualizar.Visible = false;
            cmdApagar.Visible = false;
            cmdTrabajoRobot.Visible = false;
            pictureBox1.Image = Properties.Resources.LOGO_NU4;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(50, 50);
            int permiso;
            permiso = SI;
            if (permiso == SI)
            {
                cmdTrabajoRobot.Visible = true;
                cmdActualizar.Visible = true;
                cmdApagar.Visible = true;
                txtProgreso.Text = "Puedo empezar a trabajar";
            }
            else
            {
                MessageBox.Show("No tiene licencia o está vencida\nSolicitar licencia de uso del ROBOT a:\nNü4It Automation \n Teléfono: 50206474  \n Ext. Sistemas: 2262");
                txtProgreso.Text = "Lo siento. No puedo trabajar";
            }
        }

        //Procedimiento que se ejecuta cuando se da clic al botón Actualizar
        //Baja la versión más reciente del proyecto por FTP
        private void cmdActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string descarga, ruta, rutaActualizaciones, rutaVersiones;
                descarga = SERVIDOR + "/" + CARPETA_FTP + "/" + NOMFILEZIP;
                ruta = Directory.GetCurrentDirectory();
                rutaActualizaciones = ruta + @"\Actualizaciones";
                if (!(Directory.Exists(rutaActualizaciones))) { Directory.CreateDirectory(rutaActualizaciones); }
                else
                {
                    string[] fileszip = Directory.GetFiles(rutaActualizaciones, "*.zip");
                    foreach (string s in fileszip) { System.IO.File.Delete(s); }
                }
                rutaActualizaciones = rutaActualizaciones + @"\" + NOMFILEZIP;
                objNu4.DescargaFTP(USUARIO, PASSWORD, descarga, rutaActualizaciones);
                rutaVersiones = ruta;
                if (!(Directory.Exists(rutaVersiones))) { Directory.CreateDirectory(rutaVersiones); }
                ZipFile objzip = new ZipFile();
                objzip = ZipFile.Read(rutaActualizaciones);
                objzip.ExtractAll(rutaVersiones);
                objzip.Dispose();
                Application.Exit();
            }
            catch (Exception ex)
            {
                objNu4.ArchivoExcepciones(ex.ToString());
                MessageBox.Show(new Form() { TopMost = true }, "Ocurrio algo inesperado al querer actualizar. Favor de avisar al proveedor");
            }
        }

        //Procedimiento que se ejecuta cuando se da clic al botón Apagar
        //Cierra la aplicación y por lo tanto el formulario
        private void cmdApagar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Procedimiento que se ejecuta cuando se da clic a la opción Informes del proveedor del menú informes
        //Muestra información de la empresa
        private void informesDelProveedorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(new Form() { TopMost = true }, "Nü4It Automation \n Teléfono: 50206474  \n Ext. Sistemas: 2262");
        }

        //Procedimiento que se ejecuta cuando se da clic a la opción Informes del Robot del menú informes
        //Muestra nombre y versión del robot
        private void informesDelRobotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(new Form() { TopMost = true }, "Se está ejecutando el Robot: " + VersionProyecto);
        }

        //Procedimiento que se ejecuta cuando se da clic a la opción Cambiar Rutas de Archivos
        //Abre el formulario que permite modificar las rutas dónde se encuentran los archivos con los que el ROBOT trabaja
        private void cambiarRutasDeArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarRutasArchivos cra = new CambiarRutasArchivos();
            cra.Show();
        }

        //Procedimiento que se ejecuta cuando se da clic a la opción Cambiar Datos para uso de portal
        //Abre el formulario que permite modificar los datos para interactuar con el portal de Internet
        private void cambiarDatosParaUsoDePortalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarDatosPortal cdp = new CambiarDatosPortal();
            cdp.Show();
        }

        //Procedimiento que se ejecuta cuando se da clic al botón Realizar Trabajo
        //Checa la licencia
        //Se crea la bitácora; se toma la hora de inicio Y SE INVOCA A LA FUNCIÓN PRINCIPAL para comenzar la tareas propias del ROBOT
        //En caso de que haya ocurrido una excepción o un error la bitácora se manda por FTP para que sea rebisada por el programador
        //En caso de que todo haya salido bien se registra la hora en que terminó para reportar el tiempo transcurrido
        //En cualquiera de los casos anteriores la bitacora es elimida de la computadora del usuario...
        //Pero la línea de codigo que hace la eliminación está copmetada hay que descomentarla una vez que se va a crear 
        //el ejecutable definitivo que se entregará al cliente
        private void cmdTrabajoRobot_Click(object sender, EventArgs e)
        {
            try
            {
                int exito, TiempoEjecucion;
                string HoraIni, HoraFin, BitacoraLOG, RutaSubir;
                txtProgreso.Text = "Proceso iniciado";
                NOMFILEEXC = "Bit" + VersionProyecto;
                NOMFILEEXC = NOMFILEEXC.Replace(".vshost", "");
                BitacoraLOG = objNu4.GeneraNombreArchivo(NOMFILEEXC + "_", "log");
                NOMFILEEXC = objNu4.GeneraNombreArchivo(NOMFILEEXC + "_", "log");
                RutaSubir = SERVIDOR + "/" + CARPETA_FTP_EXC + "/" + NOMFILEEXC;
                RUTA_ARCHIVO_LOG = Directory.GetCurrentDirectory();
                RUTA_ARCHIVO_LOG = RUTA_ARCHIVO_LOG + @"\" + BitacoraLOG;
                objNu4.CreaArchivoLog(RUTA_ARCHIVO_LOG);
                DateTime Inicio = DateTime.Now;
                objNu4.ReportarLog(RUTA_ARCHIVO_LOG, VersionProyecto);
                exito = FUNCION_PRINCIPAL();//INVOCACIÓN A LA FUNCIÓN PRINCIPAL **************************************************
                if (exito == SI)
                {
                    DateTime fin = DateTime.Now;
                    HoraIni = Convert.ToString(Inicio);
                    HoraFin = Convert.ToString(fin);
                    TiempoEjecucion = (fin - Inicio).Minutes;
                    objNu4.ReportarLog(RUTA_ARCHIVO_LOG, "PROCESO FINALIZADO CON EXITO. \nHora de inicio: " + HoraIni + ".\nHora al terminar: " + HoraFin + ". \nMe tarde: " + Convert.ToString(TiempoEjecucion) + " minutos.");
                    //File.Delete(RUTA_ARCHIVO_LOG);
                    MessageBox.Show(new Form() { TopMost = true }, "PROCESO FINALIZADO CON EXITO. \nHora de inicio: " + HoraIni + ".\nHora al terminar: " + HoraFin + ". \nMe tarde: " + Convert.ToString(TiempoEjecucion) + " minutos.");
                }
                else
                {
                    objNu4.ReportarLog(RUTA_ARCHIVO_LOG, "NO SE PUDO CONCLUIR EL PROCESO");
                    //objNu4.SubeFTP(USUARIO, PASSWORD, RUTA_ARCHIVO_LOG, RutaSubir);
                    //File.Delete(RUTA_ARCHIVO_LOG);
                    MessageBox.Show(new Form() { TopMost = true }, "NO SE PUDO CONCLUIR EL PROCESO");
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                string RutaSubir;
                RutaSubir = SERVIDOR + "/" + CARPETA_FTP_EXC + "/" + NOMFILEEXC;
                objNu4.ReportarLog(RUTA_ARCHIVO_LOG, ex.ToString());
                //objNu4.SubeFTP(USUARIO, PASSWORD, RUTA_ARCHIVO_LOG, RutaSubir);
                //File.Delete(RUTA_ARCHIVO_LOG);
                MessageBox.Show(new Form() { TopMost = true }, "Ocurrio algo inesperado; favor de avisar al proveedor");
                Application.Exit();
            }
        }

        #endregion

        //*******************************************************************************************************************

        #region ProcedimientosComunesTodosRobots

        //Procedimiento que Lee el archivo InfoGeneral.ini y su contenido lo pasa al arreglo CONTENIDOINI[]
        public int LeerArchivoIni()
        {
            int continuar = SI;
            char[] delimiterChars = { '\r', '\n' };
            string lineaTexto, RutaDelINI = Directory.GetCurrentDirectory();
            RutaDelINI = RutaDelINI + @"\InfoGeneral.ini";
            if (File.Exists(RutaDelINI))
            {
                StreamReader sr = new StreamReader(RutaDelINI);
                lineaTexto = sr.ReadToEnd();
                CONTENIDOINI = lineaTexto.Split(delimiterChars);
                sr.Close();
            }
            else
            {
                continuar = NO;
                MessageBox.Show(new Form() { TopMost = true }, "NO SE ENCONTRO:\n" + RutaDelINI);
            }
            return (continuar);
        }

        #endregion

        /********************************************************************************************************************
        *********************************************************************************************************************
        FUNCIÓN PRINCIPAL---FUNCIÓN PRINCIPAL---FUNCIÓN PRINCIPAL---FUNCIÓN PRINCIPAL---FUNCIÓN PRINCIPAL---FUNCIÓN PRINCIPAL
        *********************************************************************************************************************
        ********************************************************************************************************************/

        public int FUNCION_PRINCIPAL()
        {
            int exito = SI;
            exito = LeerArchivoIni();
            if (exito == SI)
            {
                if (txtplaca.Text != "")
                {
                    if (txtplaca.Text.Length == 6 || txtplaca.Text.Length == 7)
                    {
                        bool hecho = nav.FuncionPrincipalRe(txtProgreso, RUTA_ARCHIVO_LOG, txtplaca.Text.ToUpper());
                        if (hecho == true) { exito = SI; } else { exito = NO; }
                    }
                    else
                    {
                        MessageBox.Show("El numero de placa tiene que ser de 6 o 7 digitos");
                        exito = NO;
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar un numero de placa");
                    exito = NO;
                }
            }
            else { MessageBox.Show(new Form() { TopMost = true }, "NO SE PUDO LEER EL ARCHIVO INI"); }
            return (exito);
        }

    }
}
