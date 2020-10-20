using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/******************biblio (ports)************************/
using System.IO.Ports;
/********************biblio(map)**************************************/
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GoogleDirections;
/********************les biblio (api)**********************************/
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;


namespace MyTracker
{
    public partial class Form1 : Form
    {
        /**********variable (resultat arduino)******************************/
        string dataIN;
     

        public Form1()
        {
            InitializeComponent();
            cutsomizedesign();
            textBox3.Visible = false;

        }


        private void cutsomizedesign()
        {
            panel3.Visible = false;
        }
         
        /************button :coordinates(afficher panel 3 ou bien fermer)**************************************************/
        private void button1_Click(object sender, EventArgs e)
        {
            if (panel3.Visible==false)
            {
                panel3.Visible = true;
            }
            else
            {
                panel3.Visible = false;
            }
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            /***********************donner l'acces pour envoyer  les donnees  au api*********************************/
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            /************************Connecter le serial port ******************************************/
            try
            {
                serialPort1.Close();
                serialPort1.PortName = "COM2";
                serialPort1.BaudRate = 9600;
                serialPort1.DataBits = 8;
                serialPort1.Parity = Parity.None;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Handshake = Handshake.None;
                serialPort1.Encoding = System.Text.Encoding.Default;
                serialPort1.Open();
            }
            catch (Exception)
            {
                

            }

        }
        

       


        /***************button close*************************************/
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        /********************button zoom on****************************************/
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            gMapControl1.Zoom = gMapControl1.Zoom + 2;
        }
          /************************design button*********************************************/
        private void button1_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.FromArgb(15,8,28);
        }
        /******************design ()*******************************/
        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox2, "Exit");
            toolTip1.BackColor = Color.FromArgb(15,8,28);
            toolTip1.ForeColor = Color.White;
            toolTip1.OwnerDraw = true;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox3, "Serach");
            toolTip1.BackColor = Color.FromArgb(15, 8, 28);
            toolTip1.ForeColor = Color.White;
            toolTip1.OwnerDraw = true;
        }

        private void pictureBox8_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox8, "Zoom in");
            toolTip1.BackColor = Color.FromArgb(15, 8, 28);
            toolTip1.ForeColor = Color.White;
            toolTip1.OwnerDraw = true;
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox6, "Zoom out");
            toolTip1.BackColor = Color.FromArgb(15, 8, 28);
            toolTip1.ForeColor = Color.White;
            toolTip1.OwnerDraw = true;
        }

        /************************design button*************************************/
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            button1.BackColor = Color.FromArgb(15, 8, 28);

        }
        /***********************fonction afficher textbox******************************/
        private void showdata(object sender, EventArgs e)
        {
            textBox3.Text = dataIN;
        }

        /************************button search ********************************************/
        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {  /*****supprimer list des markert****/
                if (gMapControl1.Overlays.Count>0)
                    {
                        gMapControl1.Overlays.RemoveAt(0);
                        gMapControl1.Refresh();
                    }
              /***************** remplir les donnees du port dans un variable (dataIn)********/
            dataIN = serialPort1.ReadExisting();

            /***************** fontction afficher les donnees ********/
            this.Invoke(new EventHandler(showdata));

                /***********divisier data reçu dans les deux textbox******/
            if (textBox3.Text != null)
            {
                string some = textBox3.Text + ',' + ',';
                string[] final = some.Split(',');
                textBox1.Text = final[0];
                textBox2.Text = final[1];
                if (textBox1.Text != null && textBox2.Text != null)
                {
                    /****remplir map par les cooordonnees***/
                    try
                    {
                        string lt = textBox1.Text;
                        string lg = textBox2.Text;
                        gMapControl1.DragButton = MouseButtons.Left;
                        gMapControl1.MapProvider = GMapProviders.GoogleMap;
                        double lat = Convert.ToDouble(lt);
                        double lon = Convert.ToDouble(lg);
                        gMapControl1.Position = new PointLatLng(lat, lon);
                        gMapControl1.MinZoom = 1;
                        gMapControl1.MaxZoom = 100;
                        gMapControl1.Zoom = 10;
                        GMapOverlay markersOverlay = new GMapOverlay("markers");
                        GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(lat, lon), GMarkerGoogleType.red);
                        markersOverlay.Markers.Add(marker);
                        gMapControl1.Overlays.Add(markersOverlay);

                    }
                    catch (Exception)
                    {

                    }

                }


            }
            }
            catch (Exception)
            {
                
                
            }
        }
        /*********************design phone*******************************/
        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox7, "Move to Mobile App");
            toolTip1.BackColor = Color.FromArgb(15, 8, 28);
            toolTip1.ForeColor = Color.White;
            toolTip1.OwnerDraw = true;
        }
       

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
              /*********************afficher l'adresse sur label *********************************************/
                RootObject rootObject = getAddress(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));
                label2.Text = "Full Adress : " + rootObject.display_name;

                /********************code pour appeler la fonction create *********/
                Adresse ad = new Adresse();
                ad.adress = rootObject.display_name;
                ad.latitude =Convert.ToDouble(textBox1.Text);
                ad.longitude =Convert.ToDouble(textBox2.Text);
                RestHelper.create(ad);

            }
        }
        public static RootObject getAddress(double lat, double lon)
        {
            /*******************************Afficher l'adress ***********************************/
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            webClient.Headers.Add("Referer", "http://www.microsoft.com");
            var jsonData = webClient.DownloadData("http://nominatim.openstreetmap.org/reverse?format=json&lat=" + lat + "&lon=" + lon);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));
            RootObject rootObject = (RootObject)ser.ReadObject(new MemoryStream(jsonData));
            return rootObject;
        }

       

        /***********************zooom off************************************************/
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            gMapControl1.Zoom = gMapControl1.Zoom - 2;
        }

       
    }
}
