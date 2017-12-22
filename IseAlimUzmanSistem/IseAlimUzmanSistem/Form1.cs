using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IseAlimUzmanSistem
{
    public partial class uzmanForm : Form
    {
        SqlConnection baglan = new SqlConnection("Data Source=ATILGAN;Initial Catalog=IseAlimUzmanSistem;Integrated Security=True");
        public uzmanForm()
        {
            InitializeComponent();
        }

        private void tecrubeCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bilgilerList1.Items.Add("Tecrübe: "+tecrubeCombobox.Text);
        }

        private void yascomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bilgilerList1.Items.Add("Yaş aralığı: "+yascomboBox.Text);
        }

        private void dilCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bilgilerList1.Items.Add("Y. dil: " + dilCombobox.Text);
        }

        private void iletisimCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bilgilerList1.Items.Add("iletişimi: " + iletisimCombobox.Text);
        }

        private void uzmanForm_Load(object sender, EventArgs e)
        {
            bilgilerList1.Items.Add("Aday İzlenimi");
        }

        int AAP = 0, ADP = 0; //Aday Alım Puanı ve Aday Değerlendirme Puanı

        private void btnİlerle_Click(object sender, EventArgs e)
        {
            
            double sonuc=0;
            if (tecrubeCombobox.Text == "Az")
            {
                AAP += 5; ADP += 5;
            }
            if (tecrubeCombobox.Text == "Orta")
            {
                AAP += 10; ADP += 10;
            }
            if (tecrubeCombobox.Text == "Çok")
            {
                AAP += 15; ADP += 10;
            }
            if (yascomboBox.Text == "25-30")
            {
                ADP += 10; AAP += 10;
            }
            if (yascomboBox.Text == "30-35")
            {
                ADP += 5; AAP += 5;
            }
            if (yascomboBox.Text == "35-40")
            {
                ADP += 0; AAP += 5;
            }
            if (dilCombobox.Text == "Orta Seviye")
            {
                ADP += 5; AAP += 10;
            }
            if (dilCombobox.Text == "İyi Seviye")
            {
                ADP += 10; AAP += 15;
            }
            if (iletisimCombobox.Text == "Orta Seviye")
            {
                ADP += 5; AAP += 5;
            }
            if (iletisimCombobox.Text == "İyi Seviye")
            {
                ADP += 5; AAP += 10;
            }
            sonuc = ADP * 0.30 + AAP * 0.70;
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                    SqlCommand komut = new SqlCommand("select sonuc from gercekler where Asama='1'", baglan);
                    SqlDataReader oku = komut.ExecuteReader();
                    oku.Read();
                    string deger=oku["sonuc"].ToString();
                    oku.Close();
                    double sayi = Convert.ToDouble(deger);
                    if (sonuc < sayi)
                    {
                        MessageBox.Show("Diğer aşamalarda adayı değerlendirmek yerine değerlendirmeye bakınız.");
                        SqlCommand komut2 = new SqlCommand("select hareket from  hareketler where id=1", baglan);
                        SqlDataReader okuma = komut2.ExecuteReader();
                        okuma.Read();
                        string yazdir = okuma["hareket"].ToString();
                        degerlendirmeList.Items.Add(yazdir);
                        okuma.Close();
                    }else
                    {
                        MessageBox.Show("Aday birinci aşama için yeterli gibi görünüyor.İkinci Aşamaya geçiniz.");
                        bilgilerList2.Items.Add(adTextBox.Text+" "+soyadTextBox.Text);
                        bilgilerList2.Items.Add("Adayın AAP puanı:"+ AAP);
                        bilgilerList2.Items.Add("Adayın ADP puanı:"+ ADP);
                        

                    }
                    label25.Text = "Yeni değerlendirme için uygulamayı kapatıp açınız.";
                    baglan.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("hata oluştu" + ex.Message);
            }
        

        }

        private void ilgiAlaniCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bilgilerList2.Items.Add("İlgi Alanı:"+ ilgiAlaniCombobox.Text);
        }

        private void sertifikaCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bilgilerList2.Items.Add("Sertifika:" + sertifikaCombobox.Text);
        }

        private void diplomaCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bilgilerList2.Items.Add("Diploma:" +diplomaCombobox.Text);
        }

        private void refCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bilgilerList2.Items.Add("Referans:" + refCombobox.Text);
        }

        private void ucretCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bilgilerList3.Items.Add("Ucret:" +ucretCombo.Text);
        }

        private void gitCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bilgilerList3.Items.Add("GitHub etkinliği:" +gitCombo.Text);
        }

        private void rankCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bilgilerList3.Items.Add("H. Çözüm Sayısı:" +rankCombo.Text);
        }

        private void btnUcuncuAsama_Click(object sender, EventArgs e)
        {
            double sonuc = 0;
            if (ucretCombo.Text == "3000-4000")
            {
                AAP += 10; ADP += 10;
            }
            if (ucretCombo.Text == "4000-5000")
            {
                AAP += 5; ADP += 5;
            }
            if (ucretCombo.Text == "5000-6000")
            {
                AAP += 5;
            }
            if(gitCombo.Text == "Orta Seviye")
            {
                AAP += 5; ADP += 5;
            }
            if (gitCombo.Text == "İyi Seviye")
            {
                AAP += 10; ADP += 5;
            }
            if (rankCombo.Text == "1-20")
            {
                AAP += 10; ADP += 5;
            }
            if (rankCombo.Text == "20 ve üstü")
            {
                AAP += 15; ADP += 10;
            }
            sonuc = ADP * 0.30 + AAP * 0.70;
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                    // Çıkartım için bir prosedür yazılmıştır
                    /*create proc prosedur
                    @gelen float
                    as begin
                    declare @max int
                    select @max=max(id) from gercekler 
                    where sonuc<@gelen
                    select h.hareket from hareketler h
                    inner join durumlar d on d.hareketId=h.id
                    inner join gercekler g on g.id=d.gercekId
                    where g.id=@max
                    end */
                    SqlCommand komut = new SqlCommand("exec prosedur @sonuc", baglan);
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@sonuc",sonuc);
                    //komut.Parameters.Clear();
                    SqlDataReader oku = komut.ExecuteReader();
                    oku.Read();
                    String yazilacak = oku["hareket"].ToString();
                    degerlendirmeList.Items.Add(yazilacak);
                    oku.Close();
                    SqlCommand komut2 = new SqlCommand("select id from hareketler where hareket='"+yazilacak+"'", baglan);
                    SqlDataReader oku2 = komut2.ExecuteReader();
                    oku2.Read();
                    int id = Convert.ToInt16(oku2["id"].ToString());
                    if(id>2) {
                        veriGrup.Visible = true;
                        label25.Text = "Aday bilgilerini veritabanına kaydediniz.";
           
                    }
                    if (id < 2)
                    {

                        label25.Text = "Yeni değerlendirme için uygulamayı kapatıp açınız.";
                        /*AAP = 0;
                        ADP = 0;
                        sonuc = 0;*/

                    }
                    oku2.Close();
                    MessageBox.Show("Sonucu öğrenmek için Degerlendirme kısmına bakınız.");
                    baglan.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu" + ex.Message);

            }

        }

        private void kaydetButon_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                    string kaydet2 = "insert into kayıt(ad,soyad,tel,mail,adres) values(@ad,@soyad,@tel,@mail,@adres)";
                    SqlCommand komut = new SqlCommand(kaydet2, baglan);
                    komut.Parameters.AddWithValue("@ad", adText.Text);
                    komut.Parameters.AddWithValue("@soyad", soyadText.Text);
                    komut.Parameters.AddWithValue("@tel", telText.Text);
                    komut.Parameters.AddWithValue("@mail", mailText.Text);
                    komut.Parameters.AddWithValue("@adres", adresText.Text);
                    komut.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show(adText.Text+" "+soyadText.Text+" bilgileri veri tabanımıza kaydedildi.");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata oluştu" + ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize eminmisiniz?", "Uyarı!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void degerlendirmeButon_Click(object sender, EventArgs e)
        {
            uzmanForm frm = new uzmanForm();
            this.Hide();
            frm.Show();
        }

        private void btnİkinciAsama_Click(object sender, EventArgs e)
        {
            
                double sonuc = 0;
                if(ilgiAlaniCombobox.Text=="Web Uygulamalar")
                {
                    AAP += 5; ADP += 5;
                }
                if(ilgiAlaniCombobox.Text=="Oyun Programlama")
                {
                    AAP += 5;
                }
                if (ilgiAlaniCombobox.Text == "Siber Güvenlik")
                {
                    AAP += 15; ADP += 20;
                }
                if (ilgiAlaniCombobox.Text == "Mobil Uygulamalar")
                {
                    AAP += 10; ADP += 5;
                }
                if (sertifikaCombobox.Text == "5-10")
                {
                    AAP += 5; ADP += 10;
                }
                if(sertifikaCombobox.Text == "10 ve üstü")
                {
                    AAP += 10; ADP += 15;
                }
                if (refCombobox.Text == "Var")
                {
                    AAP += 10;
                }
                if (diplomaCombobox.Text == "2.50-3.00")
                {
                    AAP += 5; ADP += 5;
                }
                if(diplomaCombobox.Text=="3.00 ve üstü")
                {
                    AAP += 10; ADP += 10;
                }
                sonuc = ADP * 0.30 + AAP * 0.70;
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                    SqlCommand komut = new SqlCommand("select sonuc from gercekler where Asama='2'", baglan);
                    SqlDataReader oku = komut.ExecuteReader();
                    oku.Read();
                    string deger = oku["sonuc"].ToString();
                    oku.Close();
                    double sayi = Convert.ToDouble(deger);
                    if (sonuc < sayi)
                    {
                        MessageBox.Show("Diğer aşamalarda adayı değerlendirmek yerine değerlendirmeye bakınız.");
                        SqlCommand komut2 = new SqlCommand("select hareket from  hareketler where id=2", baglan);
                        SqlDataReader okuma = komut2.ExecuteReader();
                        okuma.Read();
                        string yazdir = okuma["hareket"].ToString();
                        degerlendirmeList.Items.Add(yazdir);
                        okuma.Close();
                    }
                    else
                    {
                        MessageBox.Show("Aday ikinci aşama için de yeterli gibi görünüyor.Son aşamaya geçiniz.");
                        bilgilerList2.Items.Add("Adayın güncel AAP puanı:" + AAP);
                        bilgilerList2.Items.Add("Adayın güncel ADP puanı:" + ADP);
                        bilgilerList3.Items.Add(adTextBox.Text +" "+soyadTextBox.Text);
                        bilgilerList3.Items.Add("Adayın AAP puanı:"+AAP);
                        bilgilerList3.Items.Add("Adayın ADP puanı:" +ADP);

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata oluştu"+ex.Message);
            }
            label25.Text = "Yeni değerlendirme için uygulamayı kapatıp açınız.";
            baglan.Close();

            
        }
    }
    }

