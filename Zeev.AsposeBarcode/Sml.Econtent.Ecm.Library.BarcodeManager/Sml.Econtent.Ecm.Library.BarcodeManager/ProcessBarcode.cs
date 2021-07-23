using DTI.ImageMan.Barcode;
using DTI.ImageMan.Winforms;
using Sml.Econtent.Ecm.Library.BarcodeManager.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sml.Econtent.Ecm.Library.BarcodeManager
{
    public class ProcessBarcode
    {

        #region Properties

        private XDocument BarcodeSettings;
        private List<BarcodeFormat> possibleFormats;
        private string Frames;
        private RulesCollection Rules;

        public static List<string> BarcodeFormats
        {
            get
            {
                List<string> result = new List<string>();

                foreach (BarcodeFormat bf in Enum.GetValues(typeof(BarcodeFormat)))
                {
                    result.Add(bf.ToString());
                }

                return result;
            }
        }

        #endregion

        #region Contructor

        public ProcessBarcode(string xmlBarcodeTypes)
        {
            this.BarcodeSettings = XDocument.Parse(xmlBarcodeTypes);

            possibleFormats = new List<BarcodeFormat>();
            XElement barcodes = BarcodeSettings.Descendants("barcodes").FirstOrDefault();

            //Barcode types to be recognized
            foreach (XElement barcordeType in barcodes.Descendants("barcode"))
            {
                string barcodeName = barcordeType.Attribute("name").Value.ToLower();

                switch (barcodeName)
                {
                    case "unknown":
                        possibleFormats.Add(BarcodeFormat.Unknown);
                        break;
                    case "qrcode":
                        possibleFormats.Add(BarcodeFormat.QRCode);
                        break;
                    case "datamatrix":
                        possibleFormats.Add(BarcodeFormat.DataMatrix);
                        break;
                    case "pdf417":
                        possibleFormats.Add(BarcodeFormat.PDF417);
                        break;
                    case "upca":
                        possibleFormats.Add(BarcodeFormat.UPCA);
                        break;
                    case "upce":
                        possibleFormats.Add(BarcodeFormat.UPCE);
                        break;
                    case "upcsupplemental2digit":
                        possibleFormats.Add(BarcodeFormat.UPCSupplemental2Digit);
                        break;
                    case "upcsupplemental5digit":
                        possibleFormats.Add(BarcodeFormat.UPCSupplemental5Digit);
                        break;
                    case "ean13":
                        possibleFormats.Add(BarcodeFormat.EAN13);
                        break;
                    case "ean8":
                        possibleFormats.Add(BarcodeFormat.EAN8);
                        break;
                    case "interleaved2of5":
                        possibleFormats.Add(BarcodeFormat.Interleaved2of5);
                        break;
                    case "standard2of5":
                        possibleFormats.Add(BarcodeFormat.Standard2of5);
                        break;
                    case "industrial2of5":
                        possibleFormats.Add(BarcodeFormat.Industrial2of5);
                        break;
                    case "code39":
                        possibleFormats.Add(BarcodeFormat.Code39);
                        break;
                    case "code39extended":
                        possibleFormats.Add(BarcodeFormat.Code39Extended);
                        break;
                    case "codabar":
                        possibleFormats.Add(BarcodeFormat.Codabar);
                        break;
                    case "postnet":
                        possibleFormats.Add(BarcodeFormat.PostNet);
                        break;
                    case "bookland":
                        possibleFormats.Add(BarcodeFormat.Bookland);
                        break;
                    case "isbn":
                        possibleFormats.Add(BarcodeFormat.ISBN);
                        break;
                    case "jan13":
                        possibleFormats.Add(BarcodeFormat.JAN13);
                        break;
                    case "msimod10":
                        possibleFormats.Add(BarcodeFormat.MSIMod10);
                        break;
                    case "msi2mod10":
                        possibleFormats.Add(BarcodeFormat.MSI2Mod10);
                        break;
                    case "msimod11":
                        possibleFormats.Add(BarcodeFormat.MSIMod11);
                        break;
                    case "msimod11mod10":
                        possibleFormats.Add(BarcodeFormat.MSIMod11Mod10);
                        break;
                    case "modifiedplessey":
                        possibleFormats.Add(BarcodeFormat.ModifiedPlessey);
                        break;
                    case "code11":
                        possibleFormats.Add(BarcodeFormat.Code11);
                        break;
                    case "usd8":
                        possibleFormats.Add(BarcodeFormat.USD8);
                        break;
                    case "ucc12":
                        possibleFormats.Add(BarcodeFormat.UCC12);
                        break;
                    case "ucc13":
                        possibleFormats.Add(BarcodeFormat.UCC13);
                        break;
                    case "logmars":
                        possibleFormats.Add(BarcodeFormat.LOGMARS);
                        break;
                    case "code128":
                        possibleFormats.Add(BarcodeFormat.Code128);
                        break;
                    case "code128a":
                        possibleFormats.Add(BarcodeFormat.Code128B);
                        break;
                    case "code128b":
                        possibleFormats.Add(BarcodeFormat.DataMatrix);
                        break;
                    case "code128c":
                        possibleFormats.Add(BarcodeFormat.Code128C);
                        break;
                    //case "itf14":
                    //    possibleFormats.Add(BarcodeFormat.ITF14);
                    //    break;
                    case "code93":
                        possibleFormats.Add(BarcodeFormat.Code93);
                        break;
                    case "telepen":
                        possibleFormats.Add(BarcodeFormat.Telepen);
                        break;
                    case "fim":
                        possibleFormats.Add(BarcodeFormat.FIM);
                        break;
                    case "upceanextension":
                        possibleFormats.Add(BarcodeFormat.UPCEANExtension);
                        break;
                    case "aztec":
                        possibleFormats.Add(BarcodeFormat.Aztec);
                        break;
                    case "rss14":
                        possibleFormats.Add(BarcodeFormat.RSS14);
                        break;
                    case "rssexpanded":
                        possibleFormats.Add(BarcodeFormat.RSSExpanded);
                        break;
                    case "maxicode":
                        possibleFormats.Add(BarcodeFormat.MaxiCode);
                        break;
                    default:
                        //throw new Exception("Was informed a barcode type not recognized");
                        break;
                }
            }

            //Frames to be used
            XElement xFrames = BarcodeSettings.Descendants("frames").FirstOrDefault();
            Frames = xFrames.Attribute("choosenFrames").Value;

            //Rules to be applied
            XElement rules = BarcodeSettings.Descendants("rules").FirstOrDefault();
            foreach (XElement rule in rules.Descendants("rule"))
            {
                FieldCollection fields = null;

                XElement xFields = rule.Descendants("fields").FirstOrDefault();

                if (xFields != null)
                {
                    fields = new FieldCollection();

                    if (Rules == null)
                        Rules = new RulesCollection();

                    foreach (XElement xField in xFields.Descendants("field"))
                    {
                        fields.Add(xField.Attribute("name").Value, xField.Attribute("value").Value, xField.Attribute("description").Value);
                    }

                    Rules.Add(int.Parse(rule.Attribute("order").Value), rule.Attribute("searchType").Value, rule.Attribute("argument").Value, fields);
                }
            }
        }

        #endregion

        #region Public Methods

        public string FindBarcode(string filename)
        {
            string result = string.Empty;
            XElement barcodes;
            Bitmap toDecoder = null;
            try
            {
                toDecoder = new Bitmap(filename);
                Size aQuarter = new Size(toDecoder.Width / 2, toDecoder.Height / 2);
                Size halfH = new Size(toDecoder.Width, toDecoder.Height / 2);
                Size halfW = new Size(toDecoder.Width / 2, toDecoder.Height);

                if (Frames.Equals("Frame1"))
                {
                    Rectangle frame = new Rectangle(new Point(toDecoder.Width / 2, 0), aQuarter);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame2"))
                {
                    Rectangle frame = new Rectangle(new Point(0, 0), aQuarter);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame3"))
                {
                    Rectangle frame = new Rectangle(new Point(0, toDecoder.Height / 2), aQuarter);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame4"))
                {
                    Rectangle frame = new Rectangle(new Point(toDecoder.Width / 2, toDecoder.Height / 2), aQuarter);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame1,Frame2"))
                {
                    Rectangle frame = new Rectangle(new Point(0, 0), halfH);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame3,Frame4"))
                {
                    Rectangle frame = new Rectangle(new Point(0, toDecoder.Height / 2), halfH);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame2,Frame3"))
                {
                    Rectangle frame = new Rectangle(new Point(0, 0), halfW);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame1,Frame4"))
                {
                    Rectangle frame = new Rectangle(new Point(toDecoder.Width / 2, 0), halfW);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }

                BarcodeDecoder barCodeDecoder = new BarcodeDecoder();

                Result[] decodedBarcodes = barCodeDecoder.DecodeMultiple(toDecoder, possibleFormats);


                if (decodedBarcodes != null)
                {
                    barcodes = new XElement("barcodes");
                    //barcodes.Add(new XAttribute("Compilação", "201510261650"));

                    List<Result> rulesApplieds = new List<Result>();

                    if (Rules != null)
                    {
                        foreach (RulesEntity rule in Rules)
                        {
                            if (rule.SearchType.Equals("StartWith", StringComparison.CurrentCultureIgnoreCase))
                            {
                                rulesApplieds.AddRange(decodedBarcodes.Where(x => x.Text.StartsWith(rule.Argument)).Select(x => x).ToArray());
                                decodedBarcodes = decodedBarcodes.Where(x => !x.Text.StartsWith(rule.Argument)).Select(x => x).ToArray();
                            }
                            else if (rule.SearchType.Equals("Contains", StringComparison.CurrentCultureIgnoreCase))
                            {
                                rulesApplieds.AddRange(decodedBarcodes.Where(x => x.Text.Contains(rule.Argument)).Select(x => x).ToArray());
                                decodedBarcodes = decodedBarcodes.Where(x => !x.Text.Contains(rule.Argument)).Select(x => x).ToArray();
                            }
                            else if (rule.SearchType.Equals("EndWith", StringComparison.CurrentCultureIgnoreCase))
                            {
                                rulesApplieds.AddRange(decodedBarcodes.Where(x => x.Text.EndsWith(rule.Argument)).Select(x => x).ToArray());
                                decodedBarcodes = decodedBarcodes.Where(x => !x.Text.EndsWith(rule.Argument)).Select(x => x).ToArray();
                            }
                        }

                        foreach (Result item in rulesApplieds)
                        {

                            XElement barcode = new XElement("barcode");
                            barcode.Add(new XAttribute("format", item.BarcodeFormat.ToString()));
                            barcode.Add(new XAttribute("x1", item.ResultPoints[0].X));
                            barcode.Add(new XAttribute("y1", item.ResultPoints[0].Y));
                            barcode.Add(new XAttribute("x2", item.ResultPoints[1].X));
                            barcode.Add(new XAttribute("y2", item.ResultPoints[1].Y));
                            barcode.Add(new XAttribute("canBreak", "true"));
                            barcode.Add(new XCData(item.Text));

                            barcodes.Add(barcode);
                        }
                    }

                    foreach (Result item in decodedBarcodes)
                    {

                        XElement barcode = new XElement("barcode");
                        barcode.Add(new XAttribute("format", item.BarcodeFormat.ToString()));
                        barcode.Add(new XAttribute("x1", item.ResultPoints[0].X));
                        barcode.Add(new XAttribute("y1", item.ResultPoints[0].Y));
                        barcode.Add(new XAttribute("x2", item.ResultPoints[1].X));
                        barcode.Add(new XAttribute("y2", item.ResultPoints[1].Y));
                        barcode.Add(new XAttribute("canBreak", Rules != null && Rules.Count > 0 ? "false" : "true"));
                        barcode.Add(new XCData(item.Text));

                        barcodes.Add(barcode);
                    }
                }
                else
                {
                    barcodes = new XElement("barcodes", "Não foi possível localizar nenhum código de barras");
                }

                result = barcodes.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (toDecoder != null)
                    toDecoder.Dispose();
            }

            return result;
        }

        public FieldCollection GetFieldsByRule(string barcode)
        {
            FieldCollection fieldCollection = new FieldCollection();
            List<Result> rulesApplieds = new List<Result>();

            if (Rules != null)
            {
                foreach (RulesEntity rule in Rules)
                {
                    if (rule.SearchType.Equals("StartWith", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (barcode.StartsWith(rule.Argument))
                            fieldCollection = rule.Fields;
                    }
                    else if (rule.SearchType.Equals("Contains", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (barcode.Contains(rule.Argument))
                            fieldCollection = rule.Fields;
                    }
                    else if (rule.SearchType.Equals("EndWith", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (barcode.EndsWith(rule.Argument))
                            fieldCollection = rule.Fields;
                    }
                }
            }

            return fieldCollection;
        }

        #endregion

        #region Auxiliar Methods

        #endregion
    }
}
