using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SATO.MLComponent;

namespace printer
{
    public class SaToPrint
    {
        MLComponent MLComponent;
        public SaToPrint()
        {
            MLComponent = new MLComponent();

        }
        public string PadBoth(string source, int length)
        {
            int spaces = length - source.Length;
            int padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft).PadRight(length);

        }


        public bool PrintLabel(List<string> xPrintData, string xPrinterDriver, int NumberVincode)
        {
            try
            {
                if (NumberVincode > 2)
                {
                    int Result = 0;
                    String filePath = Environment.CurrentDirectory + "\\Layout.mllayx";

                    filePath = "C:\\Users\\Public\\Documents\\SATO\\MLV5\\Layout1.mllayx";

                    MLComponent.LayoutFile = filePath;



                    string[] fieldNames = {
                          "PartNumber", "PartName", "Customer", "CustomerCode", "QuantityBox",
                          "PoNo", "Inspector", "ProductionDate", "QRcodeID", "AccountNumber","Product","QRcode"
};

                    for (int i = 0; i < fieldNames.Length; i++)
                    {
                        if (i < xPrintData.Count)
                        {
                            MLComponent.SetPrnDataField(fieldNames[i], PadBoth(xPrintData[i],25));
                        }
                    }


                    // So luong in
                    MLComponent.SetPrnDataField("Print quantity", "1");
                    MLComponent.Setting = "DRV:" + xPrinterDriver;


                    Result = MLComponent.OpenPort(1);

                    if (Result != 0)
                    {
                        return false;
                    }


                    Result = MLComponent.Output();


                    if (Result != 0)
                    {
                        return false;
                    }


                    //Cho phep cut
                    //MLComponent.Cut();
                    Result = MLComponent.ClosePort();
                    if (Result != 0)
                    {
                        return false;
                    }
                    
                    // bắt đầu in bảng Vincode
                    Result = 0;

                    filePath = "C:\\Users\\Public\\Documents\\SATO\\MLV5\\Layout.mllayx";

                    MLComponent.LayoutFile = filePath;


                    for (int i = 0; i < NumberVincode; i++)
                    {
                        string fieldName = $"Vincode{i + 1}";
                        if (i + 12 < xPrintData.Count) // Kiểm tra xem phần tử tồn tại
                        {
                            Result = MLComponent.SetPrnDataField(fieldName, PadBoth(xPrintData[i+12],25));
                        }
                        else
                        {
                            // Nếu không có đủ dữ liệu cho vincode, bỏ qua
                            break;
                        }
                    }
                    // So luong in
                    MLComponent.SetPrnDataField("Print quantity", "1");
                    MLComponent.Setting = "DRV:" + xPrinterDriver;


                    Result = MLComponent.OpenPort(1);

                    if (Result != 0)
                    {
                        return false;
                    }


                    Result = MLComponent.Output();


                    if (Result != 0)
                    {
                        return false;
                    }


                    //Cho phep cut
                    //MLComponent.Cut();
                    Result = MLComponent.ClosePort();
                    if (Result != 0)
                    {
                        return false;
                    }
                }


                // In trường hợp ít hơn 2 vincodes
                else 
                {
                    int Result = 0;
                    String filePath = Environment.CurrentDirectory + "\\Layout.mllayx";

                    filePath = "C:\\Users\\Public\\Documents\\SATO\\MLV5\\Layout2.mllayx";

                    MLComponent.LayoutFile = filePath;



                    string[] fieldNames = {
                          "PartNumber", "PartName", "Customer", "CustomerCode", "QuantityBox",
                          "PoNo", "Inspector", "ProductionDate", "QRcodeID", "AccountNumber", 
                          "Vincode1", "Vincode2","Product","QRcode"
};

                    //for (int i = 0; i < fieldNames.Length; i++)
                    //{
                    //    if (i < xPrintData.Count)
                    //    {
                    //        MLComponent.SetPrnDataField(fieldNames[i], xPrintData[i]);
                    //    }
                    //}
                    for (int i = 0; i < fieldNames.Length; i++)
                    {
                        if (i < xPrintData.Count)
                        {
                            MLComponent.SetPrnDataField(fieldNames[i], PadBoth(xPrintData[i], 25));
                        }

                    }


                    // So luong in
                    MLComponent.SetPrnDataField("Print quantity", "1");
                    MLComponent.Setting = "DRV:" + xPrinterDriver;


                    Result = MLComponent.OpenPort(1);

                    if (Result != 0)
                    {
                        return false;
                    }


                    Result = MLComponent.Output();


                    if (Result != 0)
                    {
                        return false;
                    }


                    //Cho phep cut
                    //MLComponent.Cut();
                    Result = MLComponent.ClosePort();
                    if (Result != 0)
                    {
                        return false;
                    }
                }

            }
            catch (MLComponentException EX)
            {
                System.Windows.Forms.MessageBox.Show(EX.Message);
                return false;
            }
            return true;
        }

    }
}
