using PdfSharp;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using SautinSoft.Document;
using Microsoft.Office.Interop.Word;
using System.Text;
using Spire.Doc;
using PdfSharp.Drawing;
using System.Diagnostics;
using System.Reflection;

namespace Sistema_de_Certificados.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Obsolete]
        public ActionResult aber()
        {
            for (int i = 0; i < 2; i++)
            {
                //var projectDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                ////var html = System.IO.File.ReadAllText(projectDir + "/Plantilla_Certificados/Prueba.html");
                //var html = "<div class='wcdiv wcpage' style='width:842pt; height:595.5pt;'> <div class='wcdiv'><img class='wcimg' style='left: 808.25pt; top: 22.25pt; width: 15.9pt; height: 412.3pt;' src='https://myfiles.space/user_files/115582_3570d36d90ad7a84/1662743737_certificado-instituto/1662743737_certificado-instituto-1.png'></div> <div class='wcdiv'><img class='wcimg' style='left: 17.7pt; top: 22.4pt; width: 806.45pt; height: 16.2pt;' src='https://myfiles.space/user_files/115582_3570d36d90ad7a84/1662743737_certificado-instituto/1662743737_certificado-instituto-2.png'></div> <div class='wcdiv'><img class='wcimg' style='left: 808.25pt; top: 433.92pt; width: 15.9pt; height: 137pt;' src='https://myfiles.space/user_files/115582_3570d36d90ad7a84/1662743737_certificado-instituto/1662743737_certificado-instituto-3.png'></div> <div class='wcdiv'><img class='wcimg' style='left: 16.95pt; top: 553.66pt; width: 806.45pt; height: 16.2pt;' src='https://myfiles.space/user_files/115582_3570d36d90ad7a84/1662743737_certificado-instituto/1662743737_certificado-instituto-4.png'></div> <div class='wcdiv'><img class='wcimg' style='left: 17.7pt; top: 22.25pt; width: 15.9pt; height: 412.3pt;' src='https://myfiles.space/user_files/115582_3570d36d90ad7a84/1662743737_certificado-instituto/1662743737_certificado-instituto-5.png'></div> <div class='wcdiv'><img class='wcimg' style='left: 17.7pt; top: 433.92pt; width: 15.9pt; height: 137pt;' src='https://myfiles.space/user_files/115582_3570d36d90ad7a84/1662743737_certificado-instituto/1662743737_certificado-instituto-6.png'></div> <div class='wcdiv' style='left:182.45pt; top:147.02pt;'> <div class='wcdiv' style='left:-0.38pt; top:-0.38pt; width:628px; height:34px;'><object type='image/svg+xml' data='https://myfiles.space/user_files/115582_3570d36d90ad7a84/1662743737_certificado-instituto/1662743737_certificado-instituto-7.svg'></object></div> asdasdasdasdasdasd </div> <div class='wcdiv'><img class='wcimg' style='left: 384.5pt; top: 393.9pt; width: 86.65pt; height: 52.35pt;' src='https://myfiles.space/user_files/115582_3570d36d90ad7a84/1662743737_certificado-instituto/1662743737_certificado-instituto-8.png'></div> </div>";
                ////string plantilla = string.Format(html, "aber");
                //var css = "@font-face { font-family:'Arial Black'; font-style:normal; font-weight:normal; src:local('â˜º'), url('font1.woff') format('woff'); } @font-face { font-family:'Arial'; font-style:normal; font-weight:bold; src:local('â˜º'), url('font2.woff') format('woff'); } @font-face { font-family:'Verdana'; font-style:normal; font-weight:bold; src:local('â˜º'), url('font3.woff') format('woff'); } .wcdiv { position:absolute; } .wcspan { position:absolute; white-space:pre; color:#000000; font-size:12pt; } .wcimg { position:absolute; } .wcsvg { position:absolute; } .wcpage { position:relative; margin:10pt auto 10pt auto; overflow:hidden; } @media print { body { margin:0pt; padding:0pt; } .wcpage { page-break-after:always; margin:0pt; padding:0pt; } } .wctext001 { font-family:'Arial Black'; font-style:normal; font-weight:normal; } .wctext002 { font-family:'Verdana'; font-style:normal; font-weight:bold; } .wctext003 { font-family:'Arial'; font-style:normal; font-weight:bold; }";
                //var cssData = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.ParseStyleSheet(css, true);
                //PdfDocument pdf = PdfGenerator.GeneratePdf(html, PageSize.A4, 0, cssData);
                //pdf.Save("D:\\git- sls\\Sistema de Certificados\\document" + i + ".pdf");
                //            var testHtml = @"<head>
                //    <style>
                //        .test {
                //            background-color: linen;
                //            color: maroon;
                //        }
                //    </style>
                //</head>
                //<body class=""test"">
                //    <p>
                //        <h1>Hello World</h1>
                //        This is html rendered text with css and image.
                //    </p>
                //    <p>
                //        <img src=""http://fc62.deviantart.com/fs11/i/2006/236/d/e/The_Planet_Express_Ship_by_gasclown.jpg"" height=""100"" width=""100"">
                //    </p>
                //</body>";
                var testHtml = @"<head>
	<style>
		.contenedor {
			position: relative;
			display: inline-block;
			text-align: center;
		}

		.texto-encima {
			position: absolute;
			top: 10px;
			left: 10px;
		}

		.centrado {
			position: absolute;
			top: 50%;
			left: 50%;
			transform: translate(-50%, -50%);
		}
	</style>
</head>
<body>
	<div class=""contenedor"">
		<img src=""https://cdn.computerhoy.com/sites/navi.axelspringer.es/public/media/image/2021/09/mejor-aplicacion-fondos-pantalla-animados-llegaria-android-dentro-poco-2482649.jpg"" />
		<div class=""texto-encima"">Texto</div>
		<div class=""centrado"">Centrado</div>
	</div>
</body>";

                //PdfDocument pdf = PdfGenerator.GeneratePdf(testHtml, PageSize.A1, 0, null, null, null);
                //pdf.Save("D:\\git- sls\\Sistema de Certificados\\document" + i + ".pdf");

                //PdfDocument document = PdfGenerator.GeneratePdf(testHtml, PageSize.A1, 0, null, null, null);

                //// Create a new page        
                //PdfPage page = document.Pages[0];
                //page.Orientation = PageOrientation.Portrait;

                //XGraphics gfx = XGraphics.FromPdfPage(page, XPageDirection.Downwards);

                //// Draw background
                //gfx.DrawImage(XImage.FromFile("pdf_overlay.png"), 0, 0);
            }
            var projectDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            var html = System.IO.File.ReadAllText(projectDir + "/Plantilla_Certificados/Prueba.html");
            string file = projectDir + "/Plantilla_Certificados/Prueba.html";
            string imageloc = projectDir + "/Plantilla_Certificados/Constancia_general.jpg";
            for (int i = 0; i < 10; i++)
            {
                v(file, imageloc, i);
            }
            
            return RedirectToAction("Index");
        }

        void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, width, height);
        }

        private void GeneratePDF(string filename, string imageLoc, int contador)
        {
            PdfDocument document = new PdfDocument();
            //PdfDocument document = PdfGenerator.GeneratePdf("<h1>a</h1>", PageSize.A1, 0, null, null, null);

            // Create an empty page or load existing
            PdfPage page = document.AddPage();
            // Get an XGraphics object for drawing
            XImage xImage = XImage.FromFile(imageLoc);
            page.Width = xImage.PixelWidth;
            page.Height = xImage.PixelHeight;
            //XGraphics gfx = XGraphics.FromPdfPage(page);
            XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawImage(gfx, imageLoc, 0, 0, xImage.PixelWidth, xImage.PixelHeight);
            //
            XBrush redBrush = new XSolidBrush(XColor.FromArgb(96, 185, 153));
            XBrush negro = new XSolidBrush(XColor.FromArgb(71, 71, 71));
            //gfx.DrawString("black", XFontStyle.BoldItalic, redBrush, new XRect(x, y, width, height), XStringFormats.Center);

            //
            const XFontStyle BoldItalicUnderline = XFontStyle.Bold | XFontStyle.Italic | XFontStyle.Underline;
            // Create a font
            //XFont font = new XFont("Arial", 40, XFontStyle.Bold | XFontStyle.Italic | XFontStyle.Underline);
            XFont font = new XFont("Arial", 55, XFontStyle.Bold | XFontStyle.Underline);
            XFont font2 = new XFont("Arial Black", 30);
            // Draw the text
            

            // Draw the text
            gfx.DrawString("aberr", font2, negro,
                           new XRect(0, 410, page.Width, page.Height),
                           XStringFormats.TopCenter);

            gfx.DrawString("HOLAAAAAAAAAA", font, redBrush,
                           new XRect(0, 490, page.Width, page.Height),
                           XStringFormats.TopCenter);

            gfx.DrawString("Realizado", font2, negro,
                           new XRect(0, 610, page.Width, page.Height),
                           XStringFormats.TopCenter);

            gfx.DrawString("Con una", font2, negro,
                           new XRect(0, 680, page.Width, page.Height),
                           XStringFormats.TopCenter);


            // Save and start View
            document.Save(filename);
            //Process.Start(filename);
            document.Save("D:\\git- sls\\Sistema de Certificados\\document"+ contador    + ".pdf");
        }

        public ActionResult v(string filename, string imageLoc, int contador)
        {
            //Generar Documento PDF
            PdfDocument document = new PdfDocument();
            ///PdfDocument document = PdfGenerator.GeneratePdf("<h1>a</h1>", PageSize.A1, 0, null, null, null);

            //Crear una Página
            PdfPage page = document.AddPage();

            //Obtener las dimensiones de la imagen
            XImage xImage = XImage.FromFile(imageLoc);
            page.Width = xImage.PixelWidth;
            page.Height = xImage.PixelHeight;

            //Imagen de fondo
            ///XGraphics gfx = XGraphics.FromPdfPage(page);
            XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawImage(gfx, imageLoc, 0, 0, xImage.PixelWidth, xImage.PixelHeight);

            //Colores de Letra
            XBrush colorTxt = new XSolidBrush(XColor.FromArgb(134, 127, 71));
            ///gfx.DrawString("black", XFontStyle.BoldItalic, redBrush, new XRect(x, y, width, height), XStringFormats.Center);

            ///Tipos de letra
            ///const XFontStyle BoldItalicUnderline = XFontStyle.Bold | XFontStyle.Italic | XFontStyle.Underline;
            ///

            //Crear fuente
            ///XFont font = new XFont("Arial", 40, XFontStyle.Bold | XFontStyle.Italic | XFontStyle.Underline);
            ///XFont font3 = new XFont("Arial", 55, XFontStyle.Bold | XFontStyle.Underline);
            XFont fontTxt = new XFont("Arial", 40, XFontStyle.Bold);

            string txt = "DIAZ MUÑOZ SEBASTIAN DAVID";

            //Dibujar texto
            gfx.DrawString(txt, fontTxt, colorTxt,
                           new XRect(0, 535, page.Width, page.Height),
                           XStringFormats.TopCenter);

            //Guardar PDF
            document.Save("D:\\git- sls\\Expoalimentaria_Certificados\\Sistema de Certificados\\document" + contador + ".pdf");
            document.Dispose();

            return RedirectToAction("Index");
		}

        public static Byte[] PdfSharpConvert(String html)
        {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }

        static void SaveToHtmlFile()
        {
            //
            var projectDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            var html = System.IO.File.ReadAllText(projectDir + "/Plantilla_Certificados/Prueba.html");
            //
            string inputFile = @"..\..\Prueba.html";

            DocumentCore dc = DocumentCore.Load(inputFile);

            string fileHtmlFixed = @"Fixed-as-file.html";
            string fileHtmlFlowing = @"Flowing-as-file.html";

            // Save to HTML file: HtmlFixed.
            dc.Save(fileHtmlFixed, new HtmlFixedSaveOptions()
            {
                Version = HtmlVersion.Html5,
                CssExportMode = CssExportMode.Inline
            });

            // Save to HTML file: HtmlFlowing.
            dc.Save(fileHtmlFlowing, new HtmlFlowingSaveOptions()
            {
                Version = HtmlVersion.Html5,
                CssExportMode = CssExportMode.Inline,
                ListExportMode = HtmlListExportMode.ByHtmlTags
            });

            // Open the results for demonstration purposes.
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(fileHtmlFixed) { UseShellExecute = true });
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(fileHtmlFlowing) { UseShellExecute = true });

        }
    }
}