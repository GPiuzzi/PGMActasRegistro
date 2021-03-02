using Actas.AccesoDatos;
using Actas.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Actas.Controllers
{
    public class RegistroController : Controller
    {
        public HtmlString GuardarFormulario(datosFormularios datosFormulario)
        {
            var resultado = FormAD.GestionarActa(datosFormulario);
            var mensaje = resultado ? "Guardado Exitoso. Se descargará una copia del acta" : "Error al intentar realizar el guardado. Revise los datos";
            var response = new { success = resultado, Message = mensaje };


            System.Web.Script.Serialization.JavaScriptSerializer jSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return new HtmlString(jSerializer.Serialize(response));
        }

       

        public ActionResult ReportePDF(int nroActa)
        {
     
            FileStream fs = new FileStream("D://Reporte/ReporteActaNro"+nroActa+".pdf", FileMode.Create);
            //    MemoryStream ms = new MemoryStream();

            Document documento = new Document(PageSize.A4, 20, 20, 70, 20);
            PdfWriter pw = PdfWriter.GetInstance(documento, fs);
           // pw.PageEvent = new HeaderFooter();

            documento.Open();
            PdfContentByte cb = pw.DirectContent;
            //declaraciones 
            Font municipalidad = new Font(Font.FontFamily.COURIER, 10, Font.BOLD, BaseColor.BLACK);
            Font texto = new Font(Font.FontFamily.COURIER, 9, Font.NORMAL, BaseColor.BLACK);
            Font fecha = new Font(Font.FontFamily.COURIER, 8, Font.NORMAL, BaseColor.BLACK);
            Image imagen = Image.GetInstance("C:/Users/GINA/Desktop/programlogo.jpg");

            imagen.ScalePercent(3f);

            var datosActa = ActaAD.datosActa(nroActa);
            var datosInfractor = ActaAD.datosInfractor(nroActa);
            PdfPCell space = new PdfPCell(new Phrase("\n \n \n")) { Colspan = 3, BorderWidthBottom = 0, BorderWidthRight = 0, BorderWidthTop = 0 };
            PdfPCell space2 = new PdfPCell(new Phrase("\n \n \n")) { BorderWidthTop = 0 };

            Chunk linebreak = new Chunk("\n");
            //datos


            var tabla = new PdfPTable(4) { WidthPercentage=100f};
        

            var c1 = new PdfPCell(imagen) { BorderWidthRight = 0, Padding = 5f };

            var c2 = new PdfPCell(new Phrase("Municipalidad de Río Quinto", municipalidad)) {Colspan = 2,  HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthLeft = 0, BorderWidthRight = 0 };
            var c3 = new PdfPCell(new Phrase("Emisión: " + datosActa[7], fecha)) {  HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidthLeft = 0 };
            tabla.AddCell(c1);
            tabla.AddCell(c2);
            tabla.AddCell(c3);

            var c4 = new PdfPCell(new Phrase("Destinatario: " + datosInfractor[0] + " " + datosInfractor[1], texto)) { Colspan = 4, Rowspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthBottom = 0, BorderWidthTop = 0 };
            var c5 = new PdfPCell(new Phrase("Domicilio:" + datosInfractor[2] + " " + datosInfractor[3] + ", " + datosInfractor[4], texto)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthTop = 0, BorderWidthBottom = 0 };
            tabla.AddCell(c4);
            tabla.AddCell(c5);
            /*c1.BorderColor = BaseColor.WHITE;
            c2.BorderColor= BaseColor.WHITE;*/
            var c6 = new PdfPCell(new Phrase("Identificación: Dominio " + datosActa[5] + " , Marca: " + datosActa[6] + " , Modelo: " + datosActa[3] + " , Tipo: " + datosActa[2] + " , Color: " + datosActa[4], texto)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthBottom = 0 };
            tabla.AddCell(c6);
       string dateString = datosActa[9];
            DateTime parsedDateTime;
            string formattedDate = "";
            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out parsedDateTime))
            {
                formattedDate = parsedDateTime.ToString("dd/MM/yyyy");
            }
            else
            {
                Console.WriteLine("Parsing failed");
            }
            var c7 = new PdfPCell(new Phrase("Nro Acta: "+nroActa, texto)) { HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthBottom = 0, BorderWidthRight = 0 };
            var c8 = new PdfPCell(new Phrase("Fecha: " + formattedDate, texto)) { HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthBottom = 0, BorderWidthLeft = 0, BorderWidthRight = 0 };
            var c9 = new PdfPCell(new Phrase("Lugar: " + datosActa[8], texto)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthBottom = 0, BorderWidthLeft = 0, };
            var c10 = new PdfPCell(new Phrase("Inspector: " + datosActa[1] + ", " + datosActa[0], texto)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthTop = 0, BorderWidthRight = 0 };
            var c11 = new PdfPCell(new Phrase("Jurisdicción: Municipalidad de Rio Quinto", texto)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthLeft = 0, BorderWidthTop = 0, };
            tabla.AddCell(c7);
            tabla.AddCell(c8);
            tabla.AddCell(c9);
            tabla.AddCell(c10);
            tabla.AddCell(c11);


     

            var c12 = new PdfPCell(new Phrase("INFRACCIÓN A LA DISPOSICIÓN LEGAL - ORDENANZA NRO 7559:", texto)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthTop = 0, BorderWidthRight = 0 };
            var c13 = new PdfPCell(new Phrase("IMPORTE", texto)) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidthTop = 0 };
            tabla.AddCell(c12);
            tabla.AddCell(c13);
            double total = 0;

            List<CodInf> lista = ActaAD.datosCodInf(nroActa);

            foreach (var element in lista)
            {
                var cell1 = new PdfPCell(new Phrase(element.codigo + " - " + element.concepto, texto)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthTop = 0, BorderWidthBottom = 0, BorderWidthRight = 0 };

                var monto = (element.monto).ToString();



                var cell3 = new PdfPCell(new Phrase("$"+monto, texto)) { HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidthTop = 0, BorderWidthBottom = 0 };

                tabla.AddCell(cell1);

                tabla.AddCell(cell3);








                total = total + double.Parse(element.monto.ToString());
            }
            tabla.AddCell(space);
            tabla.AddCell(space2);

            var c14 = new PdfPCell(new Phrase("CONCEPTO", texto)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthRight = 0 };

            var c15 = new PdfPCell(new Phrase("TOTAL", texto)) { HorizontalAlignment = Element.ALIGN_CENTER, BorderWidthTop = 0 };
            var c14b = new PdfPCell(new Phrase("multa", texto)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthBottom = 0, BorderWidthRight = 0, BorderWidthTop = 0 };
            var c15b = new PdfPCell(new Phrase("$" + (total).ToString(), texto)) { HorizontalAlignment = Element.ALIGN_RIGHT, BorderWidthBottom = 0, BorderWidthTop = 0 };

            var c16 = new PdfPCell(new Phrase("TOTAL A PAGAR", texto)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthRight = 0, BorderWidthBottom = 0 };
            // var c17 = new PdfPCell(new Phrase("totalinnumbers", texto)) { HorizontalAlignment = Element.ALIGN_CENTER };
            var c18 = new PdfPCell(new Phrase("FECHA", texto)) { Colspan = 3, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidthRight = 0 };
            var c19 = new PdfPCell(new Phrase(datosActa[9], texto)) { HorizontalAlignment = Element.ALIGN_RIGHT };
            tabla.AddCell(c14);
            tabla.AddCell(c15);
            tabla.AddCell(c14b);
            tabla.AddCell(c15b);
            tabla.AddCell(space);
            tabla.AddCell(space2);
            tabla.AddCell(c16);
            tabla.AddCell(c15b);
            tabla.AddCell(c18);
            tabla.AddCell(c19);
            tabla.SpacingBefore = 30;
            tabla.SpacingAfter = 10;
            tabla.KeepRowsTogether(0);
            documento.Add(tabla);




            DottedLineSeparator separator = new DottedLineSeparator();
            documento.Add(separator);


            var tabla2 = new PdfPTable(4) { WidthPercentage = 100f };
            tabla2.AddCell(c1);
            tabla2.AddCell(c2);
            tabla2.AddCell(c3);
            var c20 = new PdfPCell(new Phrase("NRO. ACTA: " + datosActa[12] + " - VENCIMIENTO: " +formattedDate, texto)) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4, BorderWidthBottom = 0, BorderWidthTop = 0 };

            tabla2.AddCell(c20);


            // CODE 128

            iTextSharp.text.pdf.PdfContentByte cb2 = new iTextSharp.text.pdf.PdfContentByte(pw);


            //numero de acta desde bd
           

            Barcode128 code128 = new Barcode128();
              code128.CodeType = Barcode.CODE128;

            string BARdatos =  nroActa.ToString() + " "+datosActa[5] + " " + datosActa[9] + " " + total.ToString();

   

code128.ChecksumText = true;
            code128.GenerateChecksum = true;
            code128.Code = BARdatos;
            code128.AltText = nroActa.ToString();
            var c21 = new PdfPCell(code128.CreateImageWithBarcode(cb2, null, null)) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4, BorderWidthBottom = 0, BorderWidthTop = 0, VerticalAlignment = Element.ALIGN_MIDDLE };

            tabla2.AddCell(c21);
            var c22 = new PdfPCell(new Phrase("talón para la municipalidad", fecha)) { HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 4, BorderWidthTop = 0 };
            tabla2.AddCell(c22);

            tabla2.SpacingBefore = 10;
            tabla2.SpacingAfter = 10;
            tabla2.KeepRowsTogether(0);
            documento.Add(tabla2);




            documento.Add(separator);


            var tabla3 = new PdfPTable(4) { WidthPercentage = 100f };
            tabla3.AddCell(c1);
            tabla3.AddCell(c2);
            tabla3.AddCell(c3);

            tabla3.AddCell(c20);
            var c23 = new PdfPCell(code128.CreateImageWithBarcode(cb, null, null)) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2, BorderWidthTop = 0, BorderWidthRight = 0, PaddingLeft = 5f, VerticalAlignment = Element.ALIGN_MIDDLE };

            tabla3.AddCell(c23);






            string QRdatos = "Nro de Acta: " + nroActa.ToString() + "\nFecha de Vencimiento: " + formattedDate + "\nMonto a Pagar : $" + total.ToString() +
                "\nNro de Dominio: " + datosActa[5] +
                "\nInfractor: " + datosInfractor[0] + " " + datosInfractor[1] + " ";

            BarcodeQRCode codQR = new BarcodeQRCode(QRdatos, 100, 100, null);


            var c24 = new PdfPCell(codQR.GetImage()) { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2, BorderWidthTop = 0, BorderWidthLeft = 0, PaddingBottom = 5f, VerticalAlignment = Element.ALIGN_MIDDLE };
            var c25 = new PdfPCell(new Phrase("\n", texto)) { Colspan = 4, BorderWidth = 0 };


            tabla3.AddCell(c24);
            tabla3.AddCell(c25);

            tabla3.SpacingBefore = 10;
            tabla3.SpacingAfter = 10;
            tabla3.KeepRowsTogether(0);
            documento.Add(tabla3);



            documento.Add(separator);
            documento.Add(linebreak);

            documento.Add(linebreak);




            documento.Close();  //Se cierra el documento

            /*      byte[] bytesStream = ms.ToArray();   //Esto es para que el PDF se abra en una pestaña, y de ahí que se pueda descargar. Lo crea en memoria

                  ms = new MemoryStream();
                  ms.Write(bytesStream, 0, bytesStream.Length);
                  ms.Position = 0;*/

            // return new FileStreamResult(fs, "application/pdf");
     
            return null;
        }

       /* class HeaderFooter : PdfPageEventHelper //nueva clase para poner el footer en cada página
        {
            public override void OnEndPage(PdfWriter pw, Document document)
            { //Header que no voy a hacer por ahora

           

                PdfPTable tbHeader = new PdfPTable(5);
                Font municipalidad = new Font(Font.FontFamily.COURIER, 12, Font.BOLD, BaseColor.BLACK);
                Font texto = new Font(Font.FontFamily.COURIER, 10, Font.NORMAL, BaseColor.BLACK);
                Image imagen = Image.GetInstance("C:/Users/GINA/Desktop/programlogo.jpg");
               imagen.ScalePercent(3f);


                tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbHeader.DefaultCell.Border = 0;


                var c1 = new PdfPCell(imagen) { BorderWidth = 0 };

                var c2 = new PdfPCell(new Phrase("MUNICIPALIDAD DE RIO V - Actas de Infracciones de Tránsito ", municipalidad)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0 };
            

                tbHeader.AddCell(c1);
      
                tbHeader.AddCell(c2);

  

                tbHeader.SpacingAfter = 20;

                tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, pw.PageSize.GetTop(document.TopMargin) +40, pw.DirectContent);

            }*/
        
    }
}
