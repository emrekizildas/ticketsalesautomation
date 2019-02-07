using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilgeTurizmUI
{
    public partial class GidisDonus : Form
    {
        public AnaForm anaform;
        public GidisDonus(AnaForm gelenForm)
        {
            InitializeComponent();
            anaform = gelenForm;
        }

        private void GidisDonus_Load(object sender, EventArgs e)
        {
            label1.Text = anaform.cmbNereden.Text + " - " + anaform.cmbNereye.Text + " Seferi";
            label8.Text = anaform.cmbNereye.Text + " - " + anaform.cmbNereden.Text + " Seferi";
            rdbGidisSeferStandart.Checked = true;
            rdbDonusSeferStandart.Checked = true;


            //Kampanya var mı kontrolü
            decimal standartGidis = Metotlar.db.SeferBilgiTablo.FirstOrDefault(x => x.Nereden == Bilgiler.neredenSehir && x.Nereye == Bilgiler.nereyeSehir && x.OtobusTipi == "Standart").Ucret;
            decimal suitGidis = Metotlar.db.SeferBilgiTablo.FirstOrDefault(x => x.Nereden == Bilgiler.neredenSehir && x.Nereye == Bilgiler.nereyeSehir && x.OtobusTipi == "Suit").Ucret;

            decimal standartDonus = Metotlar.db.SeferBilgiTablo.FirstOrDefault(x => x.Nereden == Bilgiler.nereyeSehir && x.Nereye == Bilgiler.neredenSehir && x.OtobusTipi == "Standart").Ucret;

            decimal suitDonus = Metotlar.db.SeferBilgiTablo.FirstOrDefault(x => x.Nereden == Bilgiler.nereyeSehir && x.Nereye == Bilgiler.neredenSehir && x.OtobusTipi == "Suit").Ucret;

            lblStandartGidis.Text = string.Format("{0:c}", standartGidis);
            lblSuitGidis.Text = string.Format("{0:c}", suitGidis);

            lblStandartDonus.Text = string.Format("{0:c}", standartDonus);
            lblSuitDonus.Text = string.Format("{0:c}", suitDonus);

            if(standartGidis >= suitGidis) { lblKampanya.Show();  }
            if(standartDonus >= suitDonus) { lblKampanyaDonus.Show();  }
        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            Hide();
            anaform.Show();
        }

        private void btnSeferSec_Click(object sender, EventArgs e)
        {
            Bilgiler.gidisSeferID = Metotlar.SeferIDBul(Bilgiler.neredenSehir, Bilgiler.nereyeSehir, (rdbGidisSeferStandart.Checked ? "Standart" : "Suit"));
            Bilgiler.donusSeferID = Metotlar.SeferIDBul(Bilgiler.nereyeSehir, Bilgiler.neredenSehir, (rdbDonusSeferStandart.Checked ? "Standart" : "Suit"));
            if (!Metotlar.YerVarMi(Bilgiler.gidisSeferID, Bilgiler.gidisTarihi))
            {
                MessageBox.Show("Gidiş seferimizde boş koltuk bulunmamaktadır.");
                return;
            }
            else if (!Metotlar.YerVarMi(Bilgiler.donusSeferID, Bilgiler.donusTarihi))
            {
                MessageBox.Show("Donüş seferimizde boş koltuk bulunmamaktadır.");
                return;
            }

            Bilgiler.gidisOtobusTipi = rdbGidisSeferStandart.Checked ? OtobusTipi.Standart : OtobusTipi.Suit;
            Bilgiler.donusOtobusTipi = rdbDonusSeferStandart.Checked ? OtobusTipi.Standart : OtobusTipi.Suit;
            Bilgiler.gidisSaati = rdbGidisSeferStandart.Checked ? "11:00" : "13:00";
            Bilgiler.donusSaati = rdbDonusSeferStandart.Checked ? "11:00" : "13:00";

            //Eğer dönüş seferi standart ise TRUE değeri gönderilir.
            //Eğer dönüş seferi suit ise FALSE değeri gönderilir.
            if (rdbGidisSeferStandart.Checked)
            {
                StandartOtobus birinciOtobus = new StandartOtobus(this, rdbDonusSeferStandart.Checked);
                birinciOtobus.Show();

                Hide();
            }
            else
            {
                SuitOtobus ikinciOtobus = new SuitOtobus(this, rdbDonusSeferStandart.Checked);
                ikinciOtobus.Show();
                Hide();
            }

        }

        private void GidisDonus_FormClosed(object sender, FormClosedEventArgs e)
        {
            anaform.Show();
        }
    }
}
