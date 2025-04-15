
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


namespace WindowsFormsAppHash
{
    public partial class Form1 : Form
    {
        //private object loginsDataSet;
        string connectionString = "Data Source=sqlexpress;Initial Catalog=gds_cj3022048;User ID=aluno;Password=aluno;Encrypt=True;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Conectado");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados:");

                }

            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            AdicionarUsuario(txtUsuario.Text, txtSenha.Text, txtConfirmar.Text, txtEmail.Text);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            {
                //variaveis locais para tratar o usuario e a senha
                string usuario = txtUsuario.Text;
                string senha = Crypto.sha256encrypt(txtSenha.Text);

                //percorre cada tabela do banco de dados

            }
        }

        public void AdicionarUsuario(string usuario, string senha, string confirSenha, string email)
        {
            string senhaHash = Crypto.sha256encrypt(senha);
            if (senha != confirSenha)
            {
                throw new ArgumentException("As senhas não coincidem.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Usuarios (Usuario, Senha, Email) VALUES (@Usuario, @Senha, @Email)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Usuario", usuario);
                    command.Parameters.AddWithValue("@Senha", senhaHash);
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("dados inseridos!");
                }
            }
        }

    }
}