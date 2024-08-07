using System;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Timers;
using System.Threading;
using System.Xml;

namespace OPCUAProject

{
    static class Program
    {
        /// <summary>
        /// Uygulamanýn giriþ noktasýdýr.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); 

        }
    }
    public partial class Form1 : Form
    {
        private Session? session; 
        private long start;
        private long end;
        private System.Timers.Timer timer;
        SqlConnection bag = new SqlConnection("Data Source=LAPTOP-UP1SG9TI\\SQLEXPRESS;Integrated Security=True;");
        int prevUniqueID = 0;

        public Form1()
        {
            InitializeComponent();
            ConnectToOpcUaServer();
            timer = new System.Timers.Timer(500); // 500ms
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private async void ConnectToOpcUaServer()
        {
            try
            {
                ApplicationInstance application = new ApplicationInstance
                {
                    ApplicationName = "OPC UA Client",
                    ApplicationType = ApplicationType.Client
                };

                string configPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml");
                Opc.Ua.ApplicationConfiguration config = await application.LoadApplicationConfiguration(configPath, false);

                config.CertificateValidator.CertificateValidation += (sender, eventArgs) =>
                {
                    eventArgs.Accept = true;
                };

                EndpointDescription endpointDescription = CoreClientUtils.SelectEndpoint("opc.tcp://192.168.251.1:4840", false, 15000);
                ConfiguredEndpoint endpoint = new ConfiguredEndpoint(null, endpointDescription, EndpointConfiguration.Create(config));

                session = await Session.Create(config, endpoint, false, "OPC UA Client", 60000, null, null);

                ReadInitialValues();
                AddMonitoredItem("deger1", 4);
                AddMonitoredItem("deger2", 4);
                AddMonitoredItem("deger3", 4);
                AddMonitoredItem("degerf1", 4);
                AddMonitoredItem("degerf2", 4);
                AddMonitoredItem("deger4", 4);
                AddMonitoredItem("degerf3", 4);
                AddMonitoredItem("b2", 4);
                AddMonitoredItem("b1", 4);
                AddMonitoredItem("Uretim_Raporu_Ilk", 4);

                button4.MouseDown += new MouseEventHandler(button4_MouseDown);
                button4.MouseUp += new MouseEventHandler(button4_MouseUp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to OPC UA server: " + ex.Message);
            }
        }

        private void ReadInitialValues()
        {
            if (session == null)
            {
                MessageBox.Show("Session is not initialized.");
                return;
            }

            try 
            {
                NodeId deger3NodeId = new NodeId("deger3", 4);
                NodeId degerf2NodeId = new NodeId("degerf2", 4);


                DataValue deger3Value = session.ReadValue(deger3NodeId);
                DataValue degerf2Value = session.ReadValue(degerf2NodeId);

                if (deger3Value.StatusCode != StatusCodes.Good)
                {
                    MessageBox.Show("Error reading deger3: " + deger3Value.StatusCode);
                }

                if (degerf2Value.StatusCode != StatusCodes.Good)
                {
                    MessageBox.Show("Error reading degerf2: " + degerf2Value.StatusCode);
                }


                // Deðerleri okuyun ve arayüze yazýn
                textBox4.Text = deger3Value.Value.ToString();
                textBox5.Text = degerf2Value.Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading initial values: " + ex.Message);
            }
        }

        private void ExploreNodes(NodeId nodeId, int indent = 0)
        {
            try
            {
                ReferenceDescriptionCollection references;
                Byte[] continuationPoint;

                session.Browse(
                    null,
                    null,
                    nodeId,
                    0u,
                    BrowseDirection.Forward,
                    ReferenceTypeIds.HierarchicalReferences,
                    true,
                    (uint)NodeClass.Object | (uint)NodeClass.Variable,
                    out continuationPoint,
                    out references);

                foreach (var rd in references)
                {
                    NodeId childNodeId = ExpandedNodeId.ToNodeId(rd.NodeId, session.NamespaceUris);

                    // NamespaceUri'yi alýn
                    string namespaceUri = session.NamespaceUris.GetString(childNodeId.NamespaceIndex);
                    Console.WriteLine($"{new string(' ', indent * 2)}{rd.DisplayName.Text} (NamespaceUri: {namespaceUri}): {childNodeId}");

                    // Eðer node ismi "Array_deger10_11" ise elemanlarýný keþfet
                    if (rd.DisplayName.Text == "Array_deger10_11" || rd.DisplayName.Text == "NumOfVars")
                    {
                        // Array_deger10_11'i bulduðunuzda, onu okuyun ve elemanlarýný listeleyin
                        DataValue arrayValue = session.ReadValue(childNodeId);
                        if (arrayValue.Value is ExtensionObject extensionObject)
                        {
                            if (extensionObject.Body is byte[] byteArray)
                            {
                                // Byte array'i çözümleyerek array elemanlarýný alýn
                                using (var stream = new MemoryStream(byteArray))
                                using (var reader = new BinaryReader(stream))
                                {
                                    // Burada array elemanlarýný okuyabilirsiniz, array'in boyutunu ve tipini bilmeniz gerekiyor
                                    int arrayLength = reader.ReadInt32(); // Örnek olarak, ilk 4 byte array uzunluðunu tutar
                                    for (int i = 0; i < arrayLength; i++)
                                    {
                                        int elementValue = reader.ReadInt32(); // Array elemanlarýnýn tipi int ise
                                        Console.WriteLine($"Array_deger10_11[{i}]: {elementValue}");
                                    }
                                }
                            }
                        }
                    }

                    // Alt düðümleri keþfet
                    if (rd.NodeClass == NodeClass.Object || rd.NodeClass == NodeClass.Variable)
                    {
                        ExploreNodes(childNodeId, indent + 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error browsing node: " + ex.Message);
            }
        }


        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                SaveDataToDatabase();
            });
        }

        private void SaveDataToDatabase()
        {
            if (session == null || !session.Connected)
            {
                MessageBox.Show("Session is not initialized or not connected.");
                return;
            }
            try
            {
                // Baðlantýyý açýn
                bag.Open();
                int uniqueID = 1, uretimSuresiS = 1, urunUzunluguCm = 1, urunGenisligiCm = 1;
                NodeId uretimRaporuIlk = new NodeId("Uretim_Raporu_Ilk", 4);
                DataValue Rapor = session.ReadValue(uretimRaporuIlk);
                if (Rapor.Value is ExtensionObject extensionObject)
                {
                    if (extensionObject.Body is byte[] byteArray)
                    {
                        using (var stream = new MemoryStream(byteArray))
                        using (var reader = new BinaryReader(stream))
                        {
                            if (byteArray.Length >= 2)
                            {
                                stream.Position = 0;
                                uniqueID = reader.ReadInt16();
                            }
                            if (byteArray.Length >= 4)
                            {
                                stream.Position = 2;
                                uretimSuresiS = reader.ReadInt16();
                            }
                            if (byteArray.Length >= 6)
                            {
                                stream.Position = 4;
                                urunUzunluguCm = reader.ReadInt16();
                            }
                            if (byteArray.Length >= 8)
                            {
                                stream.Position = 6;
                                urunGenisligiCm = reader.ReadInt16();
                            }
                        }
                    }
                }

                if (uniqueID == 0 || uniqueID == prevUniqueID)
                {
                    return;
                }

                // SQL komutunu oluþturun
                string sql = "INSERT INTO uretimRaporu1.dbo.uretimRaporu1 (UniqueID, UretimSuresi_s, UrunUzunlugu_cm, UrunGenisligi_cm) VALUES (@UniqueID, @UretimSuresi_s, @UrunUzunlugu_cm, @UrunGenisligi_cm)";
                using (SqlCommand komut = new SqlCommand(sql, bag))
                {
                    komut.Parameters.AddWithValue("@UretimSuresi_s", uretimSuresiS);
                    komut.Parameters.AddWithValue("@UrunUzunlugu_cm", urunUzunluguCm);
                    komut.Parameters.AddWithValue("@UrunGenisligi_cm", urunGenisligiCm);
                    komut.Parameters.AddWithValue("@UniqueID", uniqueID);
                    komut.ExecuteNonQuery();
                }

                // OPC UA sunucusuna yeni verileri yazma
                NodeId Uretim_Raporu_Unique_Kaydedilen = new NodeId("Uretim_Raporu_Unique_Kaydedilen", 4);
                DataValue Uretim_degeri = new DataValue(new Variant((short)uniqueID));

                WriteValue writeValue = new WriteValue
                {
                    NodeId = Uretim_Raporu_Unique_Kaydedilen,
                    AttributeId = Attributes.Value,
                    Value = Uretim_degeri
                };

                WriteValueCollection valuesToWrite = new WriteValueCollection { writeValue };
                StatusCodeCollection results;
                DiagnosticInfoCollection diagnosticInfos;

                // OPC UA oturumu ile yazma iþlemi
                ResponseHeader responseHeader = session.Write(
                    null,
                    valuesToWrite,
                    out results,
                    out diagnosticInfos);

                if (results[0] == StatusCodes.Good)
                {
                    prevUniqueID = uniqueID;
                }
                else
                {
                    MessageBox.Show("Error writing Uretim Raporu: " + results[0]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanýna yazma sýrasýnda bir hata oluþtu: " + ex.Message);
            }
            finally
            {
                bag.Close();
            }
        }

        private void AddMonitoredItem(string nodeName, ushort namespaceIndex) 
        {
            if (session == null)
            {
                MessageBox.Show("Session is not initialized.");
                return;
            }

            try
            {
                Subscription subscription = new Subscription(session.DefaultSubscription)
                {
                    PublishingInterval = 100
                };

                MonitoredItem monitoredItem = new MonitoredItem(subscription.DefaultItem)
                {
                    StartNodeId = new NodeId(nodeName, namespaceIndex),
                    AttributeId = Attributes.Value,
                    DisplayName = nodeName,
                    SamplingInterval = 100
                };

                monitoredItem.Notification += OnMonitoredItemNotification;

                subscription.AddItem(monitoredItem);
                session.AddSubscription(subscription);
                subscription.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding monitored item: " + ex.Message);
            }
        }

        private void OnMonitoredItemNotification(MonitoredItem item, MonitoredItemNotificationEventArgs e) 
        {
            foreach (var value in item.DequeueValues())
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    switch (item.DisplayName)
                    {
                        case "deger1":
                            textBox3.Text = value.WrappedValue.ToString();
                            break;
                        case "deger2":
                            textBox1.Text = value.WrappedValue.ToString();
                            break;
                        case "degerf1":
                            textBox2.Text = value.WrappedValue.ToString();
                            break;
                        case "deger3":
                            textBox4.Text = value.WrappedValue.ToString();
                            break;
                        case "degerf2":
                            textBox5.Text = value.WrappedValue.ToString();
                            break;
                        case "deger4":
                            textBox6.Text = value.WrappedValue.ToString();
                            break;
                        case "degerf3":
                            textBox7.Text = value.WrappedValue.ToString();
                            break;
                        case "b2":
                            textBox8.Text = value.WrappedValue.ToString();
                            break;
                        case "b1":
                            // b1 deðiþkeninin deðerini textBox9 üzerinde güncelle
                            textBox9.Text = value.WrappedValue.ToString();
                            break;
                        case "Uretim_Raporu_Ilk":
                            if (value.Value is ExtensionObject extensionObject)
                            {
                                if (extensionObject.Body is byte[] byteArray)
                                {
                                    // Byte array'i çözümleyerek struct alanlarýný alýn
                                    using (var stream = new MemoryStream(byteArray))
                                    using (var reader = new BinaryReader(stream))
                                    {
                                        // Ýlk alan: UretimSuresi_S (2 byte - short) 
                                        if (byteArray.Length >= 4)
                                        {
                                            stream.Position = 2;
                                            short uretimSuresiS = reader.ReadInt16();
                                            Console.WriteLine($"UretimSuresi_S: {uretimSuresiS}");
                                            textBox10.Text = uretimSuresiS.ToString();
                                        }

                                        // Ýkinci alan: UrunUzunlugu_cm (2 byte - short)
                                        if (byteArray.Length >= 6)
                                        {
                                            stream.Position = 4;
                                            short urunUzunluguCm = reader.ReadInt16();
                                            Console.WriteLine($"UrunUzunlugu_cm: {urunUzunluguCm}");
                                            textBox11.Text = urunUzunluguCm.ToString();
                                        }

                                        // Üçüncü alan: UrunGenisligi_cm (2 byte - short)
                                        if (byteArray.Length >= 8)
                                        {
                                            stream.Position = 6;
                                            short urunGenisligiCm = reader.ReadInt16();
                                            Console.WriteLine($"UrunGenisligi_cm: {urunGenisligiCm}");
                                            textBox12.Text = urunGenisligiCm.ToString();
                                        }
                                    }
                                }
                            }
                            break;

                    }
                });
            }
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            if (session == null || !session.Connected)
            {
                MessageBox.Show("Session is not initialized or not connected.");
                return;
            }

            try
            {
                // TextBox6'dan deðeri al
                string textBoxValue = textBox6.Text;

                // Integer deðeri dönüþtür
                if (!short.TryParse(textBoxValue, out short deger4))
                {
                    MessageBox.Show("Invalid input. Please enter a valid integer.");
                    return;
                }

                // Sysmac Studio'daki deger4 deðiþkenine yaz
                NodeId deger4NodeId = new NodeId("ns=4;s=deger4"); // NodeId'nin doðru olduðundan emin olun
                DataValue deger4Value = new DataValue(new Variant(deger4));

                WriteValue writeValue = new WriteValue
                {
                    NodeId = deger4NodeId,
                    AttributeId = Attributes.Value,
                    Value = deger4Value
                };

                WriteValueCollection valuesToWrite = new WriteValueCollection { writeValue };
                StatusCodeCollection results;
                DiagnosticInfoCollection diagnosticInfos;

                // OPC UA oturumu ile yazma iþlemi
                ResponseHeader responseHeader = session.Write(
                    null,
                    valuesToWrite,
                    out results,
                    out diagnosticInfos);

                // Yazma iþleminin sonucunu kontrol et
                if (results[0] == StatusCodes.Good)
                {
                    MessageBox.Show("Deger4 written successfully.");
                }
                else
                {
                    MessageBox.Show("Error writing deger4: " + results[0]);
                }
            }
            catch (ServiceResultException ex)
            {
                MessageBox.Show("ServiceResultException: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing deger4: " + ex.Message);
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            if (session == null)
            {
                MessageBox.Show("Session is not initialized.");
                return;
            }

            try
            {
                NodeId degerf3NodeId = new NodeId("ns=4;s=degerf3");

                // TextBox7'deki deðeri oku ve uygun türe dönüþtür
                if (float.TryParse(textBox7.Text, out float degerf3))
                {
                    DataValue degerf3Value = new DataValue(new Variant(degerf3));

                    WriteValue writeValue = new WriteValue
                    {
                        NodeId = degerf3NodeId,
                        AttributeId = Attributes.Value,
                        Value = degerf3Value
                    };

                    WriteValueCollection valuesToWrite = new WriteValueCollection { writeValue };
                    StatusCodeCollection results;
                    DiagnosticInfoCollection diagnosticInfos;

                    session.Write(null, valuesToWrite, out results, out diagnosticInfos);

                    if (results[0] == StatusCodes.Good)
                    {
                        MessageBox.Show("Degerf3 written successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Error writing degerf3: " + results[0]);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter a valid float value.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing degerf3: " + ex.Message);
            }
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            if (session == null)
            {
                MessageBox.Show("Session is not initialized.");
                return;
            }

            try
            {
                NodeId b2NodeId = new NodeId("b2", 4);
                bool b2Value = bool.Parse(textBox8.Text);

                WriteValue writeValue = new WriteValue
                {
                    NodeId = b2NodeId,
                    AttributeId = Attributes.Value,
                    Value = new DataValue(b2Value)
                };

                WriteValueCollection valuesToWrite = new WriteValueCollection { writeValue };
                StatusCodeCollection results;
                DiagnosticInfoCollection diagnosticInfos;

                session.Write(null, valuesToWrite, out results, out diagnosticInfos);

                if (results[0] == StatusCodes.Good)
                {

                }
                else
                {
                    MessageBox.Show("Error writing b2: " + results[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing b2: " + ex.Message);
            }
        }
        // button4 MouseDown olayý
        private async void button4_MouseDown(object sender, MouseEventArgs e)
        {
            if (session == null)
            {
                MessageBox.Show("Session is not initialized.");
                return;
            }

            try
            {
                start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                NodeId b2NodeId = new NodeId("b2", 4);


                WriteValue writeValue = new WriteValue
                {
                    NodeId = b2NodeId,
                    AttributeId = Attributes.Value,
                    Value = new DataValue(true)
                };

                WriteValueCollection valuesToWrite = new WriteValueCollection { writeValue };
                StatusCodeCollection results;
                DiagnosticInfoCollection diagnosticInfos;

                session.Write(null, valuesToWrite, out results, out diagnosticInfos);

                if (results[0] == StatusCodes.Good)
                {

                }
                else
                {
                    MessageBox.Show("Error writing b2: " + results[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing b2: " + ex.Message);
            }

        }

        // button4 MouseUp olayý
        private async void button4_MouseUp(object sender, MouseEventArgs e)
        {
            if (session == null)
            {
                MessageBox.Show("Session is not initialized.");
                return;
            }
            end = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long total = end - start;
            if (total > 150)
            {
                try
                {
                    NodeId b2NodeId = new NodeId("b2", 4);


                    WriteValue writeValue = new WriteValue
                    {
                        NodeId = b2NodeId,
                        AttributeId = Attributes.Value,
                        Value = new DataValue(false)
                    };

                    WriteValueCollection valuesToWrite = new WriteValueCollection { writeValue };
                    StatusCodeCollection results;
                    DiagnosticInfoCollection diagnosticInfos;

                    session.Write(null, valuesToWrite, out results, out diagnosticInfos);

                    if (results[0] == StatusCodes.Good)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Error writing b2: " + results[0]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error writing b2: " + ex.Message);
                }
            }
        }
        private async void button4_click(object sender, EventArgs e)
        {
            if (session == null)
            {
                MessageBox.Show("Session is not initialized.");
                return;
            }
            end = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long total = end - start;
            if (total <= 150)
            {
                try
                {
                    NodeId b2NodeId = new NodeId("b2", 4);


                    WriteValue writeValue = new WriteValue
                    {
                        NodeId = b2NodeId,
                        AttributeId = Attributes.Value,
                        Value = new DataValue(true)
                    };

                    WriteValueCollection valuesToWrite = new WriteValueCollection { writeValue };
                    StatusCodeCollection results;
                    DiagnosticInfoCollection diagnosticInfos;

                    session.Write(null, valuesToWrite, out results, out diagnosticInfos);

                    if (results[0] == StatusCodes.Good)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Error writing b2: " + results[0]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error writing b2: " + ex.Message);
                }
            }
            await Task.Delay(5000);

            try
            {
                NodeId b2NodeId = new NodeId("b2", 4);


                WriteValue writeValue = new WriteValue
                {
                    NodeId = b2NodeId,
                    AttributeId = Attributes.Value,
                    Value = new DataValue(false)
                };

                WriteValueCollection valuesToWrite = new WriteValueCollection { writeValue };
                StatusCodeCollection results;
                DiagnosticInfoCollection diagnosticInfos;

                session.Write(null, valuesToWrite, out results, out diagnosticInfos);

                if (results[0] != StatusCodes.Good)
                {
                    MessageBox.Show("Error writing b2: " + results[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing b2: " + ex.Message);
            }
        }
    }
}