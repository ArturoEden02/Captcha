using System;
using System.Windows.Forms;
using System.IO;
using Nu4it;
using nu4itExcel;
using nu4itFox;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Collections.ObjectModel;
using System.Threading;
using System.Drawing;
using System.Net;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Tesseract;
using AForge.Imaging.Filters;
using iTextSharp.text.pdf;

namespace Captcha.Clases
{
    class Navegacion
    {
        TextBox BarraEstado;
        usaR objNu4 = new usaR();
        nuExcel objNuExcel = new nuExcel();
        nufox objNuFox = new nufox();
        string rutalog = "";
        IWebDriver driver;
        string[] placa = new string[1];
        public bool FuncionPrincipalRe(TextBox Estado, string rutalog, string NumPlca)
        {
            BarraEstado = Estado;
            this.rutalog = rutalog;
            BarraEstado.Text = "Iniciando Proceso de Consulta en vehicular";
            objNu4.ReportarLog(rutalog, "Iniciando Proceso de Consulta en RENAPO");
            bool Exito = true;
            try
            {
                IniciarPortal("https://portalcfdi.facturaelectronica.sat.gob.mx/", NumPlca);
            }
            catch (Exception e)
            {
                Exito = false;
                objNu4.ReportarLog(rutalog, "Error\n" + e.ToString());
            }
            finally
            {
                driver.Close();
                driver.Quit();
            }
            return (Exito);
        }


        public void IniciarPortal(string url, string placas)
        {
            placa[0] = placas;
            BarraEstado.Text = "Iniciando navegador";
            objNu4.ReportarLog(rutalog, "Iniciando navegador");
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--window-size=600,800");
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            driver = new ChromeDriver(driverService, options);
            driver.Navigate().GoToUrl(url);
            Wait("Nombre", "jcaptcha");
            Thread.Sleep(2000);
            for (int a = 0; a < placa.Length; a++)
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile("file_name_string.Png", ScreenshotImageFormat.Png);
                Bitmap bitmap;
                bitmap = new Bitmap(Directory.GetCurrentDirectory() + @"\file_name_string.Png");
                Rectangle cloneRect = new Rectangle(14, 382, 206, 93);
                PixelFormat format = bitmap.PixelFormat;
                Bitmap clone = bitmap.Clone(cloneRect, format);
                bitmap.Dispose();
                clone.Save(Directory.GetCurrentDirectory() + @"\file_name_string2.Png", System.Drawing.Imaging.ImageFormat.Png);
                Image imagi = procesarImagen(clone, 220);
                string valorcaptcha = reconhecerCaptcha(imagi);
                imagi.Dispose();
                valorcaptcha = valorcaptcha.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace(" ", "");
                if (valorcaptcha != "" && valorcaptcha.Length >= 6)
                {
                    driver.FindElement(By.Name("jcaptcha")).Click();
                    driver.FindElement(By.Name("jcaptcha")).SendKeys(valorcaptcha);
                    Thread.Sleep(1000);

                    ReadOnlyCollection<IWebElement> tags = driver.FindElements(By.TagName("a"));
                    foreach (IWebElement item in tags)
                    {
                        string tex = item.Text;
                        if (tex == "BUSCAR")
                        {
                            item.Click();
                            break;
                        }
                    }
                    var hecho = driver.FindElements(By.Name("placa"));
                    if (hecho.Count == 0)
                    {
                        MessageBox.Show("Esta hecho :)");
                    }
                    else
                    {
                        a--;
                        driver.Navigate().Refresh();
                    }
                }
                else
                {
                    a--;
                    driver.Navigate().Refresh();
                }
            }
        }


        public bool Wait(string tipo, string IDElement)
        {
            bool Seguir = false;
            if (tipo == "Id")
            {
                ReadOnlyCollection<IWebElement> ChecarElemento = driver.FindElements(By.Id(IDElement));
                int w = 1;
                while (ChecarElemento.Count == 0 && w <= 10)
                {
                    objNu4.ReportarLog(rutalog, "Buscando elemento, Intento " + w++);
                    ChecarElemento = driver.FindElements(By.Id(IDElement));
                    System.Threading.Thread.Sleep(500);
                }
                if (ChecarElemento.Count != 0) { Seguir = true; }
            }
            if (tipo == "LinkText")
            {
                int w = 0;
                ReadOnlyCollection<IWebElement> ChecarElemento = driver.FindElements(By.LinkText(IDElement));
                while (ChecarElemento.Count == 0 && w <= 10)
                {
                    objNu4.ReportarLog(rutalog, "Buscando elemento, Intento " + w++);
                    ChecarElemento = driver.FindElements(By.LinkText(IDElement));
                    System.Threading.Thread.Sleep(500);
                }
                if (ChecarElemento.Count != 0) { Seguir = true; }
            }
            if (tipo == "Nombre")
            {
                int w = 0;
                ReadOnlyCollection<IWebElement> ChecarElemento = driver.FindElements(By.Name(IDElement));
                while (ChecarElemento.Count == 0 && w <= 10)
                {
                    objNu4.ReportarLog(rutalog, "Buscando elemento, Intento " + w++);
                    ChecarElemento = driver.FindElements(By.Name(IDElement));
                    System.Threading.Thread.Sleep(500);
                }
                if (ChecarElemento.Count != 0) { Seguir = true; }
            }
            return (Seguir);
        }

        public Bitmap procesarImagen(Bitmap source, int umb)
        {
            Bitmap target = new Bitmap(source.Width, source.Height, source.PixelFormat);
            // Recorrer pixel de la imagen
            for (int i = 0; i < source.Width; i++)
            {
                for (int e = 0; e < source.Height; e++)
                {
                    // Color del pixel
                    Color col = source.GetPixel(i, e);
                    // Escala de grises
                    byte gray = (byte)(col.R * 0.3f + col.G * 0.59f + col.B * 0.11f);
                    // Blanco o negro
                    byte value = 0;
                    if (gray > umb)
                    {
                        value = 255;
                    }
                    // Asginar nuevo color
                    Color newColor = System.Drawing.Color.FromArgb(value, value, value);
                    target.SetPixel(i, e, newColor);

                }
            }
            target.Save(Directory.GetCurrentDirectory() + @"\file_name_string3.Png", System.Drawing.Imaging.ImageFormat.Png);
            return (target);
        }

        private byte[] GetImageBytes(Bitmap image, ImageLockMode lockMode, out BitmapData bmpData)
        {
            bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                    lockMode, image.PixelFormat);

            byte[] imageBytes = new byte[bmpData.Stride * image.Height];

            Marshal.Copy(bmpData.Scan0, imageBytes, 0, imageBytes.Length);

            return imageBytes;
        }

        private string reconhecerCaptcha(Image img)
        {
            Bitmap imagem = new Bitmap(img);
            imagem = imagem.Clone(new Rectangle(0, 0, img.Width, img.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Erosion erosion = new Erosion();
            Dilatation dilatation = new Dilatation();
            Invert inverter = new Invert();
            ColorFiltering cor = new ColorFiltering();
            cor.Blue = new AForge.IntRange(200, 255);
            cor.Red = new AForge.IntRange(200, 255);
            cor.Green = new AForge.IntRange(200, 255);
            Opening open = new Opening();
            BlobsFiltering bc = new BlobsFiltering();
            Closing close = new Closing();
            GaussianSharpen gs = new GaussianSharpen();
            ContrastCorrection cc = new ContrastCorrection();
            bc.MinHeight = 10;
            FiltersSequence seq = new FiltersSequence(gs, inverter, open, inverter, bc, inverter, open, cc, cor, bc, inverter);
            imagem = seq.Apply(imagem);
            //imagem.Save(Directory.GetCurrentDirectory() + "\\Captcha2.png", System.Drawing.Imaging.ImageFormat.Png);
            string reconhecido = OCR(imagem);
            //string reconhecido = ocr.Principal(Directory.GetCurrentDirectory() + "\\Captcha2.png");
            return reconhecido;
        }

        private string OCR(Bitmap b)
        {
            string res = "";
            using (var engine = new TesseractEngine(@"tessdata", "spa", EngineMode.Default))
            {
                engine.SetVariable("tessedit_char_whitelist", "1234567890abcdefghijklmnopqrstuvwxyz");
                engine.SetVariable("tessedit_unrej_any_wd", true);

                using (var page = engine.Process(b, PageSegMode.SingleBlock))
                    res = page.GetText();
            }
            return res;
        }

    }
}
