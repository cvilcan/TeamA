using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;

namespace ServiceHelpers
{
    public class ConvertText2Pdf
    {
        public PdfDocument Convert(string path)
        {
            PdfDocument pdf = new PdfDocument();
                         
           
            try
            {
                string line = null;
                System.IO.TextReader readFile = new StreamReader(path);
                int yPoint = 0;

                
                pdf.Info.Title = "TXT to PDF";
                PdfPage pdfPage = pdf.AddPage();
                XGraphics graph = XGraphics.FromPdfPage(pdfPage);
                XFont font = new XFont("Verdana", 20, XFontStyle.Regular);

                while (true)
                {
                    line = readFile.ReadLine();
                    if (line == null)
                    {
                        break; // TODO: might not be correct. Was : Exit While
                    }
                    else
                    {
                        graph.DrawString(line, font, XBrushes.Black, new XRect(40, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        yPoint = yPoint + 40;
                    }
                }

                string pdfFilename = Path.GetFileName(Path.GetDirectoryName(path));
                pdf.Save(pdfFilename);
                readFile.Close();
                readFile = null;
                Process.Start(pdfFilename);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();

            }
            return pdf;
            
        }
    }
}
