using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace InventoryApp
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Barang> daftarBarang = new ObservableCollection<Barang>();

        public MainWindow()
        {
            InitializeComponent();
            dataGridBarang.ItemsSource = daftarBarang;
        }

        private void Simpan_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKode.Text)) return;

            var existing = daftarBarang.FirstOrDefault(b => b.KodeBarang == txtKode.Text);
            if (existing != null)
            {
                MessageBox.Show("Kode barang sudah ada.");
                return;
            }

            if (int.TryParse(txtJumlah.Text, out int jumlah) && decimal.TryParse(txtHarga.Text, out decimal harga))
            {
                daftarBarang.Add(new Barang
                {
                    KodeBarang = txtKode.Text,
                    NamaBarang = txtNama.Text,
                    Kategori = txtKategori.Text,
                    Jumlah = jumlah,
                    Harga = harga
                });
                KosongkanForm();
            }
            else
            {
                MessageBox.Show("Jumlah dan Harga harus berupa angka.");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var barang = daftarBarang.FirstOrDefault(b => b.KodeBarang == txtKode.Text);
            if (barang != null)
            {
                barang.NamaBarang = txtNama.Text;
                barang.Kategori = txtKategori.Text;

                if (int.TryParse(txtJumlah.Text, out int jumlah)) barang.Jumlah = jumlah;
                if (decimal.TryParse(txtHarga.Text, out decimal harga)) barang.Harga = harga;

                dataGridBarang.Items.Refresh();
                KosongkanForm();
            }
            else
            {
                MessageBox.Show("Barang tidak ditemukan.");
            }
        }

        private void Hapus_Click(object sender, RoutedEventArgs e)
        {
            var barang = daftarBarang.FirstOrDefault(b => b.KodeBarang == txtKode.Text);
            if (barang != null)
            {
                daftarBarang.Remove(barang);
                KosongkanForm();
            }
            else
            {
                MessageBox.Show("Barang tidak ditemukan.");
            }
        }

        private void BarangMasuk_Click(object sender, RoutedEventArgs e)
        {
            var barang = daftarBarang.FirstOrDefault(b => b.KodeBarang == txtKode.Text);
            if (barang != null && int.TryParse(txtJumlah.Text, out int jumlahMasuk))
            {
                barang.Jumlah += jumlahMasuk;
                dataGridBarang.Items.Refresh();
                KosongkanForm();
            }
            else
            {
                MessageBox.Show("Barang tidak ditemukan atau jumlah tidak valid.");
            }
        }

        private void BarangKeluar_Click(object sender, RoutedEventArgs e)
        {
            var barang = daftarBarang.FirstOrDefault(b => b.KodeBarang == txtKode.Text);
            if (barang != null && int.TryParse(txtJumlah.Text, out int jumlahKeluar))
            {
                if (barang.Jumlah >= jumlahKeluar)
                {
                    barang.Jumlah -= jumlahKeluar;
                    dataGridBarang.Items.Refresh();
                    KosongkanForm();
                }
                else
                {
                    MessageBox.Show("Stok tidak cukup.");
                }
            }
            else
            {
                MessageBox.Show("Barang tidak ditemukan atau jumlah tidak valid.");
            }
        }

        private void Cari_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtCari.Text.ToLower();
            var hasil = daftarBarang.Where(b =>
                b.KodeBarang.ToLower().Contains(keyword) ||
                b.NamaBarang.ToLower().Contains(keyword) ||
                b.Kategori.ToLower().Contains(keyword)).ToList();

            dataGridBarang.ItemsSource = hasil;
        }

        private void Laporan_Click(object sender, RoutedEventArgs e)
        {
            string laporan = "Laporan Stok Akhir:\n\n";
            decimal totalNilai = 0;

            foreach (var b in daftarBarang)
            {
                decimal nilai = b.Jumlah * b.Harga;
                totalNilai += nilai;
                laporan += $"{b.KodeBarang} - {b.NamaBarang} - {b.Jumlah} x {b.Harga:C} = {nilai:C}\n";
            }

            laporan += $"\nTotal Nilai Stok: {totalNilai:C}";
            MessageBox.Show(laporan);
        }

        private void KosongkanForm()
        {
            txtKode.Text = "";
            txtNama.Text = "";
            txtKategori.Text = "";
            txtJumlah.Text = "";
            txtHarga.Text = "";
            txtKode.Focus();
        }
    }

    
