using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace InventoryApp
{
    public partial class MainWindow : Window
    {
        private List<Barang> daftarBarang = new List<Barang>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Simpan_Click(object sender, RoutedEventArgs e)
        {
            Barang b = new Barang()
            {
                KodeBarang = txtKode.Text,
                NamaBarang = txtNama.Text,
                Kategori = txtKategori.Text,
                Jumlah = int.Parse(txtJumlah.Text),
                Harga = decimal.Parse(txtHarga.Text)
            };
            daftarBarang.Add(b);
            MessageBox.Show("Barang disimpan.");
            KosongkanForm();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var barang = daftarBarang.FirstOrDefault(b => b.KodeBarang == txtKode.Text);
            if (barang != null)
            {
                barang.NamaBarang = txtNama.Text;
                barang.Kategori = txtKategori.Text;
                barang.Jumlah = int.Parse(txtJumlah.Text);
                barang.Harga = decimal.Parse(txtHarga.Text);
                MessageBox.Show("Barang diperbarui.");
            }
            else
            {
                MessageBox.Show("Barang tidak ditemukan.");
            }
        }

        private void BarangMasuk_Click(object sender, RoutedEventArgs e)
        {
            var barang = daftarBarang.FirstOrDefault(b => b.KodeBarang == txtKode.Text);
            if (barang != null)
            {
                int masuk = int.Parse(txtJumlah.Text);
                barang.Jumlah += masuk;
                MessageBox.Show("Stok barang bertambah.");
            }
            else
            {
                MessageBox.Show("Barang tidak ditemukan.");
            }
        }

        private void BarangKeluar_Click(object sender, RoutedEventArgs e)
        {
            var barang = daftarBarang.FirstOrDefault(b => b.KodeBarang == txtKode.Text);
            if (barang != null)
            {
                int keluar = int.Parse(txtJumlah.Text);
                if (barang.Jumlah >= keluar)
                {
                    barang.Jumlah -= keluar;
                    MessageBox.Show("Barang berhasil keluar.");
                }
                else
                {
                    MessageBox.Show("Stok tidak mencukupi.");
                }
            }
            else
            {
                MessageBox.Show("Barang tidak ditemukan.");
            }
        }

        private void Cari_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtKode.Text.ToLower();
            var hasil = daftarBarang.FirstOrDefault(b =>
                b.KodeBarang.ToLower().Contains(keyword) ||
                b.NamaBarang.ToLower().Contains(keyword));
            if (hasil != null)
            {
                MessageBox.Show($"Ditemukan:\nKode: {hasil.KodeBarang}\nNama: {hasil.NamaBarang}\nJumlah: {hasil.Jumlah}\nHarga: {hasil.Harga:C}");
            }
            else
            {
                MessageBox.Show("Barang tidak ditemukan.");
            }
        }

        private void Laporan_Click(object sender, RoutedEventArgs e)
        {
            string laporan = "Laporan Stok Akhir:\n";
            foreach (var b in daftarBarang)
            {
                decimal total = b.Harga * b.Jumlah;
                laporan += $"Kode: {b.KodeBarang}, Nama: {b.NamaBarang}, Stok: {b.Jumlah}, Harga: {b.Harga:C}, Total: {total:C}\n";
            }
            MessageBox.Show(laporan);
        }

        private void KosongkanForm()
        {
            txtKode.Clear();
            txtNama.Clear();
            txtKategori.Clear();
            txtJumlah.Clear();
            txtHarga.Clear();
        }
    }
}
