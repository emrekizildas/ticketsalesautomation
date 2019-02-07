﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BilgeTurizm.DATA;
using BİlgeTurizm.DAL;
using System.Drawing;
using System.Text.RegularExpressions;

namespace BilgeTurizmUI
{
    public static class Metotlar
    {

        public static Context db = new Context();

        /// <summary>
        /// Koltuklarınızı istediğiniz otobüs türüne yükleyebilirsiniz.
        /// </summary>
        /// <param name="tarih">Sefer tarihi</param>
        /// <param name="seferID">Sefer ID</param>
        /// <param name="grp">Groupbox ismi giriniz</param>
        public static void KoltukYukle(DateTime tarih, int seferID, GroupBox grp)
        {
            //TODO, otobüs tipi eklenecek
            List<Bilet> biletler = db.BiletTablo.Where(x => x.KalkisTarihi == tarih && x.SeferBilgileriID == seferID).ToList();

            foreach (Bilet item in biletler)
            {
                foreach (Control pb in grp.Controls)
                {
                    if (pb is PictureBox)
                    {
                        if (pb.Name == ("pictureBox" + item.KoltukNo))
                        {
                            if (item.Musteri.Cinsiyet)
                            {
                                pb.Tag = 1;
                                ((PictureBox)pb).Image = Properties.Resources.erkekkafasi;
                            }
                            else
                            {
                                pb.Tag = 0;
                                ((PictureBox)pb).Image = Properties.Resources.kadinkafasi;
                            }
                            pb.Enabled = false;
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Verilen Sefer ID ile Kalkis Yeri şehrinin ID sini getirir.
        /// </summary>
        /// <param name="seferID"></param>
        /// <returns></returns>
        public static int KalkisYeri(int seferID)
        {
            SeferBilgileri sefer = db.SeferBilgiTablo.FirstOrDefault(x => x.ID == seferID);
            return sefer.Nereden;
        }

        /// <summary>
        /// Verilen Sefer ID ile Donus Yeri şehrinin ID sini getirir.
        /// </summary>
        /// <param name="seferID"></param>
        /// <returns></returns>
        public static int DonusYeri(int seferID)
        {
            SeferBilgileri sefer = db.SeferBilgiTablo.FirstOrDefault(x => x.ID == seferID);
            return sefer.Nereye;
        }

        /// <summary>
        /// Girilen parametrelere ait Sefer ID değerini int gönderir.
        /// </summary>
        /// <param name="nereden">Kalkış şehir ID</param>
        /// <param name="nereye">Varış şehir ID</param>
        /// <param name="otobusTipi">"Standart" veya "Suit otobüs tipi</param>
        /// <returns></returns>
        public static int SeferIDBul(int nereden, int nereye, string otobusTipi)
        {
            SeferBilgileri sefer = db.SeferBilgiTablo.FirstOrDefault(x => x.Nereden == nereden && x.Nereye == nereye && x.OtobusTipi == otobusTipi);
            return sefer.ID;
        }

        public static bool BosAlanVarMi(FlowLayoutPanel cnt)
        {
            foreach (Control item in cnt.Controls)
            {
                if (item is TextBox && item.Text.Trim() == "")
                    return true;
                else if (item is MaskedTextBox && item.Text.Trim() == "")
                    return true;
                else if (item is ComboBox && ((ComboBox)item).SelectedIndex == -1)
                    return true;
                else if (item is TextBox && item.Name.Contains("Email"))
                {
                    Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                    if (!reg.IsMatch(item.Text)) return true;

                }
                else if (item is TextBox && item.Name.Contains("TCK"))
                {
                    if (item.Text.Length != 11) return true;
                }
            }
            return false;
        }

        public static bool BosAlanVarMi(GroupBox cnt)
        {
            foreach (Control item in cnt.Controls)
            {
                if (item is TextBox && item.Text.Trim() == "")
                    return true;
                else if (item is MaskedTextBox && item.Text.Trim() == "")
                    return true;
                else if (item is ComboBox && ((ComboBox)item).SelectedIndex == -1)
                    return true;
                else if (item is TextBox && item.Name == "txtCvvKodu")
                {
                    if (item.Text.Length != 3) return true;
                }
                else if (item is TextBox && item.Name == "txtKartNumarasi")
                {
                    if (item.Text.Length != 16) return true;
                }
            }
            return false;
        }

        public static void GidenKoltuklariSec(Form form, PictureBox koltuk, Dictionary<int, string> gidistekiKoltuklar)
        {
            int secilenKoltuk = Convert.ToInt32(koltuk.Name.Substring(10, (koltuk.Name.Length - 10)));
            if (koltuk.BackColor == Color.Lime)
            {
                //Bu koltuk daha önce seçilmiş.
                koltuk.BackColor = SystemColors.Control;
                gidistekiKoltuklar.Remove(secilenKoltuk);
            }
            else
            {
                //Bu koltuk daha önce seçilmemiş.
                koltuk.BackColor = Color.Lime;
                if (form.GetType() == typeof(SuitOtobus) && secilenKoltuk <= 8)
                {
                    gidistekiKoltuklar.Add(secilenKoltuk, "farketmez");
                    return;
                }

                if (secilenKoltuk % 2 == 0)
                {
                    //Çift sayı ise koltuk numarasının bir eksiğine bakarız.
                    string yandakiKoltukName = "pictureBox" + (secilenKoltuk - 1);
                    PictureBox pb = (PictureBox)(form.Controls.Find(yandakiKoltukName, true)[0]);
                    if (pb.Tag != null && pb.Tag.ToString() == "1")
                    {
                        gidistekiKoltuklar.Add(secilenKoltuk, "erkek");
                    }
                    else if (pb.Tag != null && pb.Tag.ToString() == "0")
                    {
                        gidistekiKoltuklar.Add(secilenKoltuk, "kadın");
                    }
                    else
                    {
                        gidistekiKoltuklar.Add(secilenKoltuk, "farketmez");
                    }
                }
                else
                {
                    //Tek sayı ise koltuk numarasının bir fazlasına bakarız.
                    string yandakiKoltukName = "pictureBox" + (secilenKoltuk + 1);
                    PictureBox pb = (PictureBox)(form.Controls.Find(yandakiKoltukName, true)[0]);
                    if (pb.Tag != null && pb.Tag.ToString() == "1")
                    {
                        gidistekiKoltuklar.Add(secilenKoltuk, "erkek");
                    }
                    else if (pb.Tag != null && pb.Tag.ToString() == "0")
                    {
                        gidistekiKoltuklar.Add(secilenKoltuk, "kadın");
                    }
                    else
                    {
                        gidistekiKoltuklar.Add(secilenKoltuk, "farketmez");
                    }
                }
            }
        }

        public static string SehirBul(int sehirID)
        {
            return db.Sehirler.FirstOrDefault(x => x.SehirID == sehirID).Sehir;
        }

        public static string YemekAdi(int yemekID)
        {
            return db.Yemekler.FirstOrDefault(x => x.YemekID == yemekID).MenuAdi;
        }

        public static decimal FiyatBul(int seferID)
        {
            return db.SeferBilgiTablo.FirstOrDefault(x => x.ID == seferID).Ucret;
        }

        public static string PNRKoduUret()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool YerVarMi(int seferID, DateTime tarih)
        {
            int koltukSayisi = db.BiletTablo.Where(x => x.SeferBilgileriID == seferID && x.KalkisTarihi == tarih).ToList().Count;
            string otobusTipi = db.SeferBilgiTablo.FirstOrDefault(x => x.ID == seferID).OtobusTipi;
            if (otobusTipi == "Standart" && koltukSayisi >= 42)
            {
                return false;
            }
            else if (otobusTipi == "Suit" && koltukSayisi >= 30)
            {
                return false;
            }
            else
                return true;
        }
    }
}