using System.Collections.Generic;
using System.Linq;
using System.Windows;
using InventoryApp.Models;

namespace InventoryApp
{
    public partial class MainWindow : Window
    {
        private static List<Barang> daftarBarang = new List<Barang>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Simpan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Barang b = new Barang
                {
                    KodeBarang = txtKode.Text,
                    NamaBarang = txtNama.Text,
                    Kategori = txtKategori.Text,
                    Jumlah = int.Parse(txtJumlah.Text)
                };

                daftarBarang.Add(b);
                MessageBox.Show("Barang berhasil disimpan.");
                ClearForm();
            }
            catch
            {
                MessageBox.Show("Input tidak valid.");
            }
        }

        private void TambahStok_Click(object sender, RoutedEventArgs e)
        {
            var barang = daftarBarang.FirstOrDefault(b => b.KodeBarang == txtMasukKode.Text);
            if (barang != null)
            {
                if (int.TryParse(txtJumlahMasuk.Text, out int tambah))
                {
                    barang.Jumlah += tambah;
                    MessageBox.Show("Stok berhasil ditambahkan.");
                }
                else
                {
                    MessageBox.Show("Jumlah Masuk tidak valid.");
                }
            }
            else
            {
                MessageBox.Show("Barang tidak ditemukan.");
            }
        }

        private void KurangiStok_Click(object sender, RoutedEventArgs e)
        {
            var barang = daftarBarang.FirstOrDefault(b => b.KodeBarang == txtKeluarKode.Text);
            if (barang != null)
            {
                if (int.TryParse(txtJumlahKeluar.Text, out int keluar))
                {
                    if (barang.Jumlah >= keluar)
                    {
                        barang.Jumlah -= keluar;
                        MessageBox.Show("Stok berhasil dikurangi.");
                    }
                    else
                    {
                        MessageBox.Show("Stok tidak cukup.");
                    }
                }
                else
                {
                    MessageBox.Show("Jumlah Keluar tidak valid.");
                }
            }
            else
            {
                MessageBox.Show("Barang tidak ditemukan.");
            }
        }

        private void Cari_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtCari.Text.ToLower();
            var hasil = daftarBarang.Where(b => b.KodeBarang.ToLower().Contains(keyword) ||
                                                b.NamaBarang.ToLower().Contains(keyword)).ToList();

            if (hasil.Count > 0)
            {
                string msg = string.Join("\n", hasil.Select(b => $"{b.KodeBarang} - {b.NamaBarang} - {b.Jumlah}"));
                MessageBox.Show(msg, "Hasil Pencarian");
            }
            else
            {
                MessageBox.Show("Barang tidak ditemukan.");
            }
        }

        private void Laporan_Click(object sender, RoutedEventArgs e)
        {
            string laporan = "Laporan Stok Akhir:\n\n";
            foreach (var b in daftarBarang)
            {
                laporan += $"{b.KodeBarang} - {b.NamaBarang} - Stok: {b.Jumlah}\n";
            }

            MessageBox.Show(laporan, "Laporan");
        }

        private void ClearForm()
        {
            txtKode.Clear();
            txtNama.Clear();
            txtKategori.Clear();
            txtJumlah.Clear();
        }
    }
}
