﻿using BilgeTurizm.DATA;
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
    public partial class YolcuBilgileri : Form
    {
        List<Yolcu> gidenYolcular;
        List<Yolcu> donusYolcular;
        Random random;

        public YolcuBilgileri()
        {
            InitializeComponent();
        }

        private void YolcuBilgileri_Load(object sender, EventArgs e)
        {
            random = new Random();
            Bilgiler.PNRKodu = Metotlar.PNRKoduUret();

            GidenYolcuBilgileriFormunuOlustur();

            if (Bilgiler.seyahatTipi == SeyehatTipi.GidisDonus)
            {
                DonenYolcuBilgileriFormunuOlustur();
            }
            else
            {
                grpDonusLabels.Hide();
                lblDonus.Hide();
                donusBilgiPaneli.Hide();
                btnDevamEt.Location = new Point(btnDevamEt.Location.X, gidisBilgiPaneli.Bottom + 15);
                chkSigorta.Location = new Point(chkSigorta.Location.X, gidisBilgiPaneli.Bottom + 15);
                lblSigorta.Location = new Point(chkSigorta.Location.X, gidisBilgiPaneli.Bottom + 15);
                this.Height = gidisBilgiPaneli.Height + 200;
            }

        }

        private void DonenYolcuBilgileriFormunuOlustur()
        {
            foreach (KeyValuePair<int, string> koltuk in Bilgiler.donusSecilenKoltuklar)
            {
                //Koltuk Numarası Label
                Label koltukNo = new Label();
                koltukNo.AutoSize = false;
                koltukNo.Width = 40;
                koltukNo.Height = 21;
                koltukNo.TextAlign = ContentAlignment.BottomCenter;
                koltukNo.Text = koltuk.Key.ToString();
                koltukNo.Name = "";

                //TC Kimlik No
                TextBox TCKimlikNo = new TextBox();
                TCKimlikNo.KeyPress += TCKimlikNo_KeyPress;
                TCKimlikNo.MaxLength = 11;
                TCKimlikNo.Name = "donusTCKimlikNo-" + koltuk.Key;

                //Ad
                TextBox ad = new TextBox();
                ad.KeyPress += Ad_KeyPress;
                ad.Name = "donusAd-" + koltuk.Key;

                //Soyad
                TextBox soyad = new TextBox();
                soyad.KeyPress += Ad_KeyPress;
                soyad.Name = "donusSoyad-" + koltuk.Key;

                //E-Mail Adresi
                TextBox emailAdresi = new TextBox();
                emailAdresi.Name = "donusEmail-" + koltuk.Key;

                //Telefon No
                TextBox telefonNo = new TextBox();
                telefonNo.KeyPress += TCKimlikNo_KeyPress;
                telefonNo.Width = 86;
                telefonNo.Name = "donusTelefonNo-" + koltuk.Key;


                //Cinsiyet
                ComboBox cinsiyet = new ComboBox();
                cinsiyet.DropDownStyle = ComboBoxStyle.DropDownList;
                switch (koltuk.Value)
                {
                    case "erkek":
                        cinsiyet.Items.Add("Erkek");
                        break;
                    case "kadın":
                        cinsiyet.Items.Add("Kadın");
                        break;
                    default:
                        cinsiyet.Items.Add("Erkek");
                        cinsiyet.Items.Add("Kadın");
                        break;
                }
                cinsiyet.Name = "donusCinsiyet-" + koltuk.Key;

                ComboBox yemek = new ComboBox();
                yemek.DropDownStyle = ComboBoxStyle.DropDownList;
                yemek.Name = "donusYemek-" + koltuk.Key;
                yemek.DataSource = Metotlar.db.Yemekler.ToList();
                yemek.DisplayMember = "MenuAdi";
                yemek.ValueMember = "YemekID";


                //Çocuk mu?
                CheckBox cocuk = new CheckBox();
                cocuk.Width = 74;
                cocuk.TextAlign = ContentAlignment.MiddleCenter;
                cocuk.Name = "donusCocuk-" + koltuk.Key;


                donusBilgiPaneli.Controls.AddRange(new Control[] { koltukNo, TCKimlikNo, ad, soyad, emailAdresi, telefonNo, cinsiyet, yemek, cocuk });
            }
        }

        private void GidenYolcuBilgileriFormunuOlustur()
        {
            foreach (KeyValuePair<int, string> koltuk in Bilgiler.gidisSecilenKoltuklar)
            {
                //Koltuk Numarası Label
                Label koltukNo = new Label();
                koltukNo.AutoSize = false;
                koltukNo.Width = 40;
                koltukNo.Height = 21;
                koltukNo.TextAlign = ContentAlignment.BottomCenter;
                koltukNo.Text = koltuk.Key.ToString();

                //TC Kimlik No
                TextBox TCKimlikNo = new TextBox();
                TCKimlikNo.KeyPress += TCKimlikNo_KeyPress;
                TCKimlikNo.MaxLength = 11;
                TCKimlikNo.Name = "gidisTCKimlikNo-" + koltuk.Key;

                //Ad
                TextBox ad = new TextBox();
                ad.KeyPress += Ad_KeyPress;
                ad.Name = "gidisAd-" + koltuk.Key;

                //Soyad
                TextBox soyad = new TextBox();
                soyad.KeyPress += Ad_KeyPress;
                soyad.Name = "gidisSoyad-" + koltuk.Key;

                //E-Mail Adresi
                TextBox emailAdresi = new TextBox();
                emailAdresi.Name = "gidisEmail-" + koltuk.Key;

                //Telefon No
                TextBox telefonNo = new TextBox();
                telefonNo.KeyPress += TCKimlikNo_KeyPress;
                telefonNo.Width = 86;
                telefonNo.Name = "gidisTelefonNo-" + koltuk.Key;


                //Cinsiyet
                ComboBox cinsiyet = new ComboBox();
                cinsiyet.DropDownStyle = ComboBoxStyle.DropDownList;
                switch (koltuk.Value)
                {
                    case "erkek":
                        cinsiyet.Items.Add("Erkek");
                        break;
                    case "kadın":
                        cinsiyet.Items.Add("Kadın");
                        break;
                    default:
                        cinsiyet.Items.Add("Erkek");
                        cinsiyet.Items.Add("Kadın");
                        break;
                }
                cinsiyet.Name = "gidisCinsiyet-" + koltuk.Key;


                //Yemek Tipi
                ComboBox yemek = new ComboBox();
                yemek.DropDownStyle = ComboBoxStyle.DropDownList;
                yemek.Name = "gidisYemek-" + koltuk.Key;
                yemek.DataSource = Metotlar.db.Yemekler.ToList();
                yemek.DisplayMember = "MenuAdi";
                yemek.ValueMember = "YemekID";

                //Çocuk mu?
                CheckBox cocuk = new CheckBox();
                cocuk.Width = 74;
                cocuk.TextAlign = ContentAlignment.MiddleCenter;
                cocuk.Name = "gidisCocuk-" + koltuk.Key;

                gidisBilgiPaneli.Controls.AddRange(new Control[] { koltukNo, TCKimlikNo, ad, soyad, emailAdresi, telefonNo, cinsiyet, yemek, cocuk });
            }
        }

        private void TCKimlikNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Ad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                        && !char.IsSeparator(e.KeyChar);
        }

        private void btnDevamEt_Click(object sender, EventArgs e)
        {
            if ((!Metotlar.BosAlanVarMi(gidisBilgiPaneli)))
            {
                Bilgiler.SigortaVarMi = chkSigorta.Checked;

                gidenYolcular = new List<Yolcu>();
                GidenYolculariKaydet();
                Bilgiler.gidisMusteriler = gidenYolcular;


                if (Bilgiler.rezerveMi == false)
                {
                    if (Bilgiler.seyahatTipi == SeyehatTipi.GidisDonus)
                    {
                        if ((!Metotlar.BosAlanVarMi(donusBilgiPaneli)))
                        {
                            donusYolcular = new List<Yolcu>();
                            DonusYolculariniKaydet();
                            Bilgiler.donusMusteriler = donusYolcular;
                        }
                        else
                        {
                            MessageBox.Show("Lütfen Boş Alan Bırakmayınız ve Geçerli Değerler Giriniz...");
                            return;
                        }
                    }

                    OdemeEkrani oe = new OdemeEkrani();
                    oe.Show();
                    Hide();
                }
                else
                {

                    if (Bilgiler.seyahatTipi == SeyehatTipi.GidisDonus)
                    {
                        if ((!Metotlar.BosAlanVarMi(donusBilgiPaneli)))
                        {
                            donusYolcular = new List<Yolcu>();
                            DonusYolculariniKaydet();
                            Bilgiler.donusMusteriler = donusYolcular;
                        }
                        else
                        {
                            MessageBox.Show("Lütfen Boş Alan Bırakmayınız ve Geçerli Değerler Giriniz...");
                            return;
                        }
                    }

                    //Rezerve ise burası çalışacak
                    GidisKaydet();
                    if (Bilgiler.seyahatTipi == SeyehatTipi.GidisDonus)
                    {
                        if (!Metotlar.BosAlanVarMi(donusBilgiPaneli))
                        {
                            DonusKaydet();
                        }
                        else
                        {
                            MessageBox.Show("Lütfen Boş Alan Bırakmayınız ve Geçerli Değerler Giriniz...");
                            return;
                        }
                    }


                    OzetEkrani oe = new OzetEkrani();
                    oe.Show();
                    Hide();
                }

                

            }
            else
            {
                MessageBox.Show("Lütfen Boş Alan Bırakmayınız ve Geçerli Değerler Giriniz...");
                return;
            }

        }    

        private void DonusYolculariniKaydet()
        {
            foreach (KeyValuePair<int, string> koltuk in Bilgiler.donusSecilenKoltuklar)
            {
                Yolcu yolcu = new Yolcu();
                string adName = "donusAd-" + koltuk.Key;
                TextBox ad = (TextBox)donusBilgiPaneli.Controls.Find(adName, true)[0];
                yolcu.Ad = ad.Text;

                string soyadName = "donusSoyad-" + koltuk.Key;
                TextBox soyad = (TextBox)donusBilgiPaneli.Controls.Find(adName, true)[0];
                yolcu.SoyAd = soyad.Text;

                string emailName = "donusEmail-" + koltuk.Key;
                TextBox email = (TextBox)donusBilgiPaneli.Controls.Find(emailName, true)[0];
                yolcu.EMail = email.Text;

                string telefonName = "donusTelefonNo-" + koltuk.Key;
                TextBox telefon = (TextBox)donusBilgiPaneli.Controls.Find(telefonName, true)[0];
                yolcu.Telefon = telefon.Text;


                string cinsiyetName = "donusCinsiyet-" + koltuk.Key;
                ComboBox cinsiyet = (ComboBox)donusBilgiPaneli.Controls.Find(cinsiyetName, true)[0];
                switch (cinsiyet.Text)
                {
                    case "Erkek":
                        yolcu.Cinsiyet = true;
                        break;
                    case "Kadın":
                        yolcu.Cinsiyet = false;
                        break;
                    default:
                        break;
                }

                string tcName = "donusTCKimlikNo-" + koltuk.Key;
                TextBox tc = (TextBox)donusBilgiPaneli.Controls.Find(tcName, true)[0];
                yolcu.TcNo = tc.Text;

                string yemekName = "donusYemek-" + koltuk.Key;
                ComboBox yemek = (ComboBox)donusBilgiPaneli.Controls.Find(yemekName, true)[0];
                yolcu.yemekID = (int)yemek.SelectedValue;

                string cocukName = "donusCocuk-" + koltuk.Key;
                CheckBox cocuk = (CheckBox)donusBilgiPaneli.Controls.Find(cocukName, true)[0];
                yolcu.YetiskinMi = !cocuk.Checked;

                yolcu.koltukNo = koltuk.Key;
                donusYolcular.Add(yolcu);
            }
        }

        private void GidenYolculariKaydet()
        {
            foreach (KeyValuePair<int, string> koltuk in Bilgiler.gidisSecilenKoltuklar)
            {
                Yolcu yolcu = new Yolcu();
                string adName = "gidisAd-" + koltuk.Key;
                TextBox ad = (TextBox)gidisBilgiPaneli.Controls.Find(adName, true)[0];
                yolcu.Ad = ad.Text;

                string soyadName = "gidisSoyad-" + koltuk.Key;
                TextBox soyad = (TextBox)gidisBilgiPaneli.Controls.Find(soyadName, true)[0];
                yolcu.SoyAd = soyad.Text;

                string emailName = "gidisEmail-" + koltuk.Key;
                TextBox email = (TextBox)gidisBilgiPaneli.Controls.Find(emailName, true)[0];
                yolcu.EMail = email.Text;

                string telefonName = "gidisTelefonNo-" + koltuk.Key;
                TextBox telefon = (TextBox)gidisBilgiPaneli.Controls.Find(telefonName, true)[0];
                yolcu.Telefon = telefon.Text;


                string cinsiyetName = "gidisCinsiyet-" + koltuk.Key;
                ComboBox cinsiyet = (ComboBox)gidisBilgiPaneli.Controls.Find(cinsiyetName, true)[0];
                switch (cinsiyet.Text)
                {
                    case "Erkek":
                        yolcu.Cinsiyet = true;
                        break;
                    case "Kadın":
                        yolcu.Cinsiyet = false;
                        break;
                    default:
                        break;
                }

                string tcName = "gidisTCKimlikNo-" + koltuk.Key;
                TextBox tc = (TextBox)gidisBilgiPaneli.Controls.Find(tcName, true)[0];
                yolcu.TcNo = tc.Text;

                string yemekName = "gidisYemek-" + koltuk.Key;
                ComboBox yemek = (ComboBox)gidisBilgiPaneli.Controls.Find(yemekName, true)[0];
                yolcu.yemekID = (int)yemek.SelectedValue;

                string cocukName = "gidisCocuk-" + koltuk.Key;
                CheckBox cocuk = (CheckBox)gidisBilgiPaneli.Controls.Find(cocukName, true)[0];
                yolcu.YetiskinMi = !cocuk.Checked;


                yolcu.koltukNo = koltuk.Key;
                gidenYolcular.Add(yolcu);
            }
        }

        private void DonusKaydet()
        {

            foreach (Yolcu item in Bilgiler.donusMusteriler)
            {


                Musteriler musteri = new Musteriler();
                musteri.Ad = item.Ad;
                musteri.SoyAd = item.SoyAd;
                musteri.TcNo = item.TcNo;
                musteri.EMail = item.EMail;
                musteri.Telefon = item.Telefon;
                musteri.Cinsiyet = item.Cinsiyet;
                Metotlar.db.MusterilerTablo.Add(musteri);
                Metotlar.db.SaveChanges();

                Bilet bilet = new Bilet();
                bilet.YemekID = item.yemekID;
                bilet.RezerveMi = Bilgiler.rezerveMi;
                bilet.KoltukNo = (short)item.koltukNo;
                bilet.KalkisTarihi = Bilgiler.donusTarihi;
                bilet.VarisTarihi = Bilgiler.donusTarihi;
                bilet.SeferBilgileriID = Bilgiler.donusSeferID;
                bilet.MusteriID = musteri.MusteriID;
                bilet.YetiskinMi = item.YetiskinMi;
                bilet.SigortaliMi = Bilgiler.SigortaVarMi;
                bilet.ToplamFiyat = Bilgiler.ToplamFiyat;
                bilet.PnrKodu = Bilgiler.PNRKodu;
                Metotlar.db.BiletTablo.Add(bilet);
                Metotlar.db.SaveChanges();


            }
        }

        private void GidisKaydet()
        {

            foreach (Yolcu item in Bilgiler.gidisMusteriler)
            {
                if (Bilgiler.SigortaVarMi)
                {
                    Bilgiler.ToplamFiyat += 20;
                }

                if (item.YetiskinMi)
                {
                    Bilgiler.ToplamFiyat += Metotlar.FiyatBul(Bilgiler.gidisSeferID);
                }
                else
                {
                    Bilgiler.ToplamFiyat += Metotlar.FiyatBul(Bilgiler.gidisSeferID) * 0.8m;
                }

                if (Bilgiler.gidisOtobusTipi == OtobusTipi.Suit && item.koltukNo <= 8)
                {
                    Bilgiler.ToplamFiyat += 20;
                }
            }

            if(Bilgiler.seyahatTipi == SeyehatTipi.GidisDonus)
            {
                if(Bilgiler.donusMusteriler.Count != 0)
                {
                    foreach (Yolcu item in Bilgiler.donusMusteriler)
                    {
                        if (Bilgiler.SigortaVarMi)
                        {
                            Bilgiler.ToplamFiyat += 20;
                        }

                        if (item.YetiskinMi)
                        {
                            Bilgiler.ToplamFiyat += Metotlar.FiyatBul(Bilgiler.donusSeferID);
                        }
                        else
                        {
                            Bilgiler.ToplamFiyat += Metotlar.FiyatBul(Bilgiler.donusSeferID) * 0.8m;
                        }

                        if (Bilgiler.donusOtobusTipi == OtobusTipi.Suit && item.koltukNo <= 8)
                        {
                            Bilgiler.ToplamFiyat += 20;
                        }
                    }
                }
            }
            


            foreach (Yolcu item in Bilgiler.gidisMusteriler)
            {

                Musteriler musteri = new Musteriler();
                musteri.Ad = item.Ad;
                musteri.SoyAd = item.SoyAd;
                musteri.EMail = item.EMail;
                musteri.TcNo = item.TcNo;
                musteri.Telefon = item.Telefon;
                musteri.Cinsiyet = item.Cinsiyet;
                Metotlar.db.MusterilerTablo.Add(musteri);
                Metotlar.db.SaveChanges();


                Bilet bilet = new Bilet();
                bilet.YemekID = item.yemekID;
                bilet.RezerveMi = Bilgiler.rezerveMi;
                bilet.KoltukNo = (short)item.koltukNo;
                bilet.KalkisTarihi = Bilgiler.gidisTarihi;
                bilet.VarisTarihi = Bilgiler.gidisTarihi;
                bilet.SeferBilgileriID = Bilgiler.gidisSeferID;
                bilet.MusteriID = musteri.MusteriID;
                bilet.YetiskinMi = item.YetiskinMi;
                bilet.SigortaliMi = Bilgiler.SigortaVarMi;
                bilet.ToplamFiyat = Bilgiler.ToplamFiyat;
                bilet.PnrKodu = Bilgiler.PNRKodu;
                Metotlar.db.BiletTablo.Add(bilet);
                Metotlar.db.SaveChanges();

            }
        }

        private void YolcuBilgileri_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void chkSigorta_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSigorta.Checked)
                lblSigorta.Visible = true;
            else
                lblSigorta.Visible = false;
        }
    }
}
